using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MySql.Data.MySqlClient;

namespace RecommenderSystem
{
    static class MySqlCommands
    {
        static string myConnectionString = "server=90.185.187.114;uid=program;pwd=123;database=recommender_system;";
        static MySqlConnection conn = new MySqlConnection { ConnectionString = myConnectionString };

        public static bool CreateNewUser(string firstName, string lastName, string userName, string password)
        {
            if (!userName.Any(char.IsLetterOrDigit) && userName.Length <= 3)
            {
                return false;
            }

            try
            {
                conn.Open();

                MySqlCommand cmd = new MySqlCommand("INSERT INTO users (Firstname, Lastname, Username, Password) " +
                                                    "Values ('" + firstName + "', '" + lastName + "', '" + userName +
                                                    "', '" + password + "')", conn);

                MySqlDataReader myReader = cmd.ExecuteReader();
                while (myReader.Read())
                {
                }

                return true;
            }
            catch (MySqlException ex)
            {
                Console.WriteLine(ex.Message);
                return false;
            }
            finally
            {
                conn.Close();
            }
        }

        public static bool UserExist(string userName)
        {
            if (!userName.Any(char.IsLetterOrDigit) && userName.Length <= 3)
            {
                return true;
            }
            try
            {
                conn.Open();

                MySqlCommand cmd = new MySqlCommand();
                cmd.CommandText = "SELECT  count(*) FROM users WHERE Username = @username";
                cmd.Parameters.AddWithValue("@username", userName);
                cmd.Connection = conn;

                MySqlDataReader myReader = cmd.ExecuteReader();
                while (myReader.Read())
                {
                }

                if (Convert.ToInt32(myReader[0]) == 1)
                {
                    return true;
                }
                return false;
            }
            catch (MySqlException ex)
            {
                Console.WriteLine(ex.Message);
                return false;
            }
            finally
            {
                conn.Close();
            }
        }

        public static bool FindUser(string userName, string password)
        {
            try
            {
                conn.Open();

                MySqlCommand cmd = new MySqlCommand();
                cmd.CommandText = "SELECT  count(*) FROM users WHERE Username = @username AND Password = @password";
                cmd.Parameters.AddWithValue("@username", userName);
                cmd.Parameters.AddWithValue("@password", password);
                cmd.Connection = conn;

                MySqlDataReader myReader = cmd.ExecuteReader();
                while (myReader.Read())
                {
                }

                if (Convert.ToInt32(myReader[0]) == 1)
                {
                    return true;
                }
                return false;
            }
            catch (MySqlException ex)
            {
                Console.WriteLine(ex.Message);
                return false;
            }
            finally
            {
                conn.Close();
            }
        }

        public static List<MovieMenuItem> GetMovies()
        {
            try
            {
                List<MovieMenuItem> allMovies = new List<MovieMenuItem>();
                conn.Open();
                MySqlCommand cmd = new MySqlCommand();
                cmd.CommandText = "SELECT * FROM imdbdata";
                cmd.Connection = conn;

                MySqlDataReader myReader = cmd.ExecuteReader();
                List<string> actors = new List<string>();
                while (myReader.Read())
                {
                    actors = new List<string>();
                    for (int i = 0; i < 10; i++)
                    {
                        actors.Add(myReader[$"Cast{i + 1}"].ToString());   
                    }
                    allMovies.Add(new MovieMenuItem(myReader["Movie"].ToString(), myReader["Year"].ToString(),
                        Convert.ToDouble(myReader["Rating"]), Convert.ToInt32(myReader["Runtime"]), myReader["PlotOutline"].ToString(), myReader["Director"].ToString(), actors));
                }

                return allMovies;
            }
            catch (MySqlException ex)
            {
                Console.WriteLine(ex.Message);
                
                return new List<MovieMenuItem>();
            }
            finally
            {
                conn.Close();
            }
        }
    }
}