using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RecommenderSystem
{
    static class MySqlCommands
    {
        private static string myConnectionString = "server=90.185.187.114;uid=program;pwd=123;database=recommender_system;";
        private static MySqlConnection conn = new MySqlConnection { ConnectionString = myConnectionString };

        public static string FindRatingFromMovieID(int id)
        {
            MySqlCommand cmd = new MySqlCommand();
            DataTable results;
            string result = "notRated";

            cmd.CommandText = $"SELECT * FROM {User.Username}_movies WHERE movieID = @id;";
            cmd.Parameters.AddWithValue("@id", id);
            cmd.Connection = conn;
            results = SendQuery(cmd);

            try
            {
                foreach (DataRow row in results.Rows)
                {
                    result = row[2].ToString();
                }

                return result;

            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                return result;
            }
        }

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

            DataTable results = SendQuery(cmd);

            if (Convert.ToInt32(results.Rows[0][0]) == 1) return true;

            return false;
        }

        public static List<MovieMenuItem> FindMovieFromID(List<int> id)
        {
            List<MovieMenuItem> MovieList = new List<MovieMenuItem>();
            List<string> actors = new List<string>();

            MySqlCommand cmd = new MySqlCommand();
            DataTable results;

            for (int i = 0; i < id.Count; i++)
            {
                cmd.CommandText = $"SELECT * FROM imdbdata WHERE ID='{id[i]}';";
                cmd.Connection = conn;

                results = SendQuery(cmd);
                
                try
                {
                    foreach (DataRow row in results.Rows)
                    {
                        actors = new List<string>();
                        for (int j = 8; j < 18; j++)
                        {
                            actors.Add(row[j].ToString());
                        }

                        MovieList.Add(new MovieMenuItem(Convert.ToInt32(row[0]), row[1].ToString(), row[4].ToString(),
                            Convert.ToDouble(row[2]), Convert.ToInt32(row[5]),
                            row[6].ToString(), row[7].ToString(), actors));
                    }
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

            DataTable results = SendQuery(cmd);
            
            try
            {
                foreach (DataRow row in results.Rows)
                {
                    actors = new List<string>();
                    for (int i = 8; i < 18; i++)
                    {
                        actors.Add(row[i].ToString());
                    }

                    allMovies.Add(new MovieMenuItem(Convert.ToInt32(row[0]), row[1].ToString(), row[4].ToString(),
                        Convert.ToDouble(row[2]), Convert.ToInt32(row[5]),
                        row[6].ToString(), row[7].ToString(), actors));
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

            DataTable results = SendQuery(cmd);

            if (Convert.ToInt32(results.Rows[0][0]) == 1) return true;

            return false;
        }

        public static bool UserExist(string userName)
        {
            if (!userName.Any(char.IsLetterOrDigit) && userName.Length <= 3) return true;

            MySqlCommand cmd = new MySqlCommand();
            cmd.CommandText = "SELECT  count(*) FROM users WHERE Username = @username";
            cmd.Parameters.AddWithValue("@username", userName);
            cmd.Connection = conn;

            DataTable results = SendQuery(cmd);

            if (Convert.ToInt32(results.Rows[0][0]) == 1) return true;

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
        public static List<int> GetUserRatedMovies()
        {
            List<int> MovieID = new List<int>();

            MySqlCommand cmd = new MySqlCommand();
            DataTable results;

            cmd.CommandText = $"SELECT * FROM {User.Username}_movies;";
            cmd.Connection = conn;

            results = SendQuery(cmd);

            try
            {
                foreach (DataRow row in results.Rows)
                {
                    MovieID.Add(Convert.ToInt32(row[1]));
                }

            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }

            return MovieID;
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

        private static DataTable SendQuery(MySqlCommand cmd)
        {
            DataTable results = new DataTable();
            try
            {
                conn.Open();

                using (MySqlDataReader reader = cmd.ExecuteReader())
                {
                    try
                    {
                        results.Load(reader);
                    }
                    catch (ConstraintException)
                    {
                    }
                    catch (Exception ex)
                    {
                        Console.WriteLine(ex.Message);
                        Console.ReadLine();
                    }
                }

                return results;
            }
            catch (MySqlException ex)
            {
                Console.WriteLine(ex.Message);
                return results;
            }
            finally
            {
                conn.Close();
            }
        }
    }
}
