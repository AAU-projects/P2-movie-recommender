using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RecommenderSystem
{
    static class MySqlCommands2
    {
        private static string myConnectionString = "server=90.185.187.114;uid=program;pwd=123;database=recommender_system;";
        private static MySqlConnection conn = new MySqlConnection { ConnectionString = myConnectionString };

        public static bool RateMovie(int movieId, string enumvalue)
        {
            if (IsMovieRated(movieId))
            {
                MySqlCommand cmd = new MySqlCommand($"UPDATE {User.Username}_movies SET rating = '{enumvalue}' WHERE movieID = {movieId}", conn);

                return SendNonQuery(cmd);
            }
            else
            {
                MySqlCommand cmd = new MySqlCommand($"INSERT INTO {User.Username}_movies (movieID, rating) " +
                                                    $"Values ('{movieId}', '{enumvalue}')", conn);

                return SendNonQuery(cmd);
            }
        }

        public static bool IsMovieRated(int movieId)
        {
            MySqlCommand cmd = new MySqlCommand($"SELECT count(*) FROM {User.Username}_movies WHERE movieID= @movieID", conn);
            cmd.Parameters.AddWithValue("@movieID", movieId);

            MySqlDataReader myReader = SendQuery(cmd);

            if (Convert.ToInt32(myReader[0]) == 1) return true;

            return false;
        }

        public static List<MovieMenuItem> FindMovieFromID(List<int> id)
        {
            List<MovieMenuItem> MovieList = new List<MovieMenuItem>();
            List<string> actors = new List<string>();

            MySqlCommand cmd = new MySqlCommand();
            MySqlDataReader myReader;

            for (int i = 0; i < id.Count; i++)
            {
                cmd.CommandText = $"SELECT * FROM imdbdata WHERE ID='{id[i]}';";

                myReader = SendQuery(cmd);

                actors = new List<string>();

                try
                {
                    for (int j = 0; j < 10; j++)
                    {
                        actors.Add(myReader[$"Cast{j + 1}"].ToString());
                    }

                    MovieList.Add(new MovieMenuItem(Convert.ToInt32(myReader["ID"]), myReader["Movie"].ToString(), myReader["Year"].ToString(),
                        Convert.ToDouble(myReader["Rating"]), Convert.ToInt32(myReader["Runtime"]),
                        myReader["PlotOutline"].ToString(), myReader["Director"].ToString(), actors));

                    myReader.Close();
                }
                catch (Exception ex)
                {
                    Console.WriteLine(ex.Message);
                }
            }

            return MovieList;
        }

        public static List<MovieMenuItem> GetMovies()
        {
            List<MovieMenuItem> allMovies = new List<MovieMenuItem>();
            List<string> actors = new List<string>();

            MySqlCommand cmd = new MySqlCommand();
            cmd.CommandText = "SELECT * FROM imdbdata";
            cmd.Connection = conn;

            MySqlDataReader myReader = SendQuery(cmd);

            actors = new List<string>();

            try
            {
                while (myReader.Read())
                {
                    for (int i = 0; i < 10; i++)
                    {
                        actors.Add(myReader[$"Cast{i + 1}"].ToString());
                    }

                    allMovies.Add(new MovieMenuItem(Convert.ToInt32(myReader["ID"]), myReader["Movie"].ToString(), myReader["Year"].ToString(),
                        Convert.ToDouble(myReader["Rating"]), Convert.ToInt32(myReader["Runtime"]),
                        myReader["PlotOutline"].ToString(), myReader["Director"].ToString(), actors));
                }
                
                return allMovies;
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                return new List<MovieMenuItem>();
            }
            
        }

        public static bool FindUser(string userName, string password)
        {
            MySqlCommand cmd = new MySqlCommand();
            cmd.CommandText = "SELECT  count(*) FROM users WHERE Username = @username AND Password = @password";
            cmd.Parameters.AddWithValue("@username", userName);
            cmd.Parameters.AddWithValue("@password", password);
            cmd.Connection = conn;

            MySqlDataReader myReader = SendQuery(cmd);

            if (Convert.ToInt32(myReader[0]) == 1) return true;

            return false;
        }

        public static bool UserExist(string userName)
        {
            if (!userName.Any(char.IsLetterOrDigit) && userName.Length <= 3) return true;

            MySqlCommand cmd = new MySqlCommand();
            cmd.CommandText = "SELECT  count(*) FROM users WHERE Username = @username";
            cmd.Parameters.AddWithValue("@username", userName);
            cmd.Connection = conn;

            MySqlDataReader myReader = SendQuery(cmd);

            if (Convert.ToInt32(myReader[0]) == 1) return true;

            return false;
        }

        public static bool CreateNewUser(string firstName, string lastName, string userName, string password)
        {
            if (!userName.Any(char.IsLetterOrDigit) && userName.Length <= 3) return false;

            MySqlCommand cmd = new MySqlCommand("INSERT INTO users (Firstname, Lastname, Username, Password) " +
                                                    "Values ('" + firstName + "', '" + lastName + "', '" + userName +
                                                    "', '" + password + "')", conn);

            return SendNonQuery(cmd);         
        }

        public static bool CreateUserTable(string userName)
        {
            MySqlCommand cmd = new MySqlCommand(
                $"CREATE TABLE `{userName}_movies` ( `id`  int NOT NULL AUTO_INCREMENT , `movieID`  int NULL , `rating`  varchar(255) NULL , PRIMARY KEY (`id`));", conn);

            return SendNonQuery(cmd);
        }

        public static int NumberOfRowsInTable(string table)
        {
            try
            {
                string query = $"SELECT COUNT(*) FROM {table}";
                using (MySqlCommand cmd = new MySqlCommand(query, conn))
                {
                    conn.Open();
                    return Convert.ToInt32(cmd.ExecuteScalar());
                }
            }
            catch (MySqlException e)
            {
                Console.WriteLine(e.Message);
                return 0;
            }
            finally
            {
                conn.Close();
            }

        }

        private static bool SendNonQuery(MySqlCommand cmd)
        {
            try
            {
                conn.Open();

                cmd.ExecuteNonQuery();

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

        private static MySqlDataReader SendQuery(MySqlCommand cmd)
        {
            try
            {
                conn.Open();

                MySqlDataReader myReader = cmd.ExecuteReader();
                while (myReader.Read()) { }

                return myReader;

            }
            catch (MySqlException ex)
            {
                Console.WriteLine(ex.Message);
                return null;
            }
            finally
            {
                conn.Close();
            }
        }
    }
}
