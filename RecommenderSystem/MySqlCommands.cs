using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace RecommenderSystem
{
    static class MySqlCommands
    {
        private static string _myConnectionString = "server=90.185.187.114;uid=program;pwd=123;database=recommender_system;";
        private static readonly MySqlConnection _connection = new MySqlConnection { ConnectionString = _myConnectionString };


        public static List<MovieMenuItem> FindMovieByGenre(List<string> genre)
        {
            List<MovieMenuItem> genreMovies = new List<MovieMenuItem>();
            MySqlCommand cmd = new MySqlCommand();

            cmd.CommandText = $"SELECT * FROM imdbdata WHERE Genre LIKE '%{genre[0]}%' OR Genre LIKE '%{genre[1]}%' OR Genre LIKE '%{genre[2]}%'";
            cmd.Connection = _connection;

            DataTable results = SendQuery(cmd);

            try
            {
                foreach (DataRow row in results.Rows)
                {
                    var actors = new List<string>();

                    for (int j = 8; j < 18; j++)            //selects from column 8 to column 17 in the MySQL DB
                    {
                        if (row[j] != null)
                        {
                            actors.Add(row[j].ToString());
                        }
                    }

                    genreMovies.Add(new MovieMenuItem(Convert.ToInt32(row[0]), row[1].ToString(), row[4].ToString(),
                    Convert.ToDouble(row[2]), Convert.ToInt32(row[5]), row[3].ToString(), row[6].ToString(), row[7].ToString(), actors));
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }

            return genreMovies;
        } 

        public static string FindRatingFromMovieId(int id)
        {
            MySqlCommand cmd = new MySqlCommand();
            string result = "notRated";

            cmd.CommandText = $"SELECT * FROM {User.Username}_movies WHERE movieID = @id;";
            cmd.Parameters.AddWithValue("@id", id);
            cmd.Connection = _connection;

            DataTable results = SendQuery(cmd);

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
            if (IsMovieIdValid(movieId) && IsEnumvalueValid(enumvalue))
            {
                if (IsMovieRated(movieId))
                {
                    MySqlCommand cmd = new MySqlCommand($"UPDATE {User.Username}_movies SET rating = '{enumvalue}' WHERE movieID = {movieId}", _connection);

                    return SendNonQuery(cmd);
                }
                else
                {
                    MySqlCommand cmd = new MySqlCommand($"INSERT INTO {User.Username}_movies (movieID, rating) " +
                                                        $"Values ('{movieId}', '{enumvalue}')", _connection);

                    return SendNonQuery(cmd);
                }                
            }

            return false;
        }

        private static bool IsMovieIdValid(int movieId)
        {
            int numberOfMovies = NumberOfRowsInTable("imdbdata");

            return movieId > 0 && movieId <= numberOfMovies;
        }

        private static bool IsEnumvalueValid(string enumvalue)
        {
            if (enumvalue == "thumbsup" || enumvalue == "thumbsdown") return true;

            return false;
        }

        public static bool IsMovieRated(int movieId)
        {
            MySqlCommand cmd = new MySqlCommand($"SELECT count(*) FROM {User.Username}_movies WHERE movieID= @movieID", _connection);
            cmd.Parameters.AddWithValue("@movieID", movieId);

            DataTable results = SendQuery(cmd);

            if (Convert.ToInt32(results.Rows[0][0]) == 1) return true;

            return false;
        }

        public static List<MovieMenuItem> FindMovieFromId(List<int> id)
        {
            List<MovieMenuItem> movieList = new List<MovieMenuItem>();

            MySqlCommand cmd = new MySqlCommand();

            for (int i = 0; i < id.Count; i++)
            {
                cmd.CommandText = $"SELECT * FROM imdbdata WHERE ID='{id[i]}';";
                cmd.Connection = _connection;

                DataTable results = SendQuery(cmd);
                
                try
                {
                    foreach (DataRow row in results.Rows)
                    {
                        var actors = new List<string>();

                        for (int j = 8; j < 18; j++)
                        {
                            if (row[j] != null)
                            {
                                actors.Add(row[j].ToString());
                            }
                        }

                        movieList.Add(new MovieMenuItem(Convert.ToInt32(row[0]), row[1].ToString(), row[4].ToString(),
                        Convert.ToDouble(row[2]), Convert.ToInt32(row[5]), row[3].ToString(), row[6].ToString(), row[7].ToString(), actors));
                    }
                }
                catch (Exception ex)
                {
                    Console.WriteLine(ex.Message);
                }
            }

            return movieList;
        }

        public static List<MovieMenuItem> GetMovies()
        {
            List<MovieMenuItem> allMovies = new List<MovieMenuItem>();

            MySqlCommand cmd = new MySqlCommand();
            cmd.CommandText = "SELECT * FROM imdbdata";
            cmd.Connection = _connection;

            DataTable results = SendQuery(cmd);
            
            try
            {
                foreach (DataRow row in results.Rows)
                {
                    var actors = new List<string>();

                    for (int i = 8; i < 18; i++)
                    {
                        if (row[i] != null)
                        {
                            actors.Add(row[i].ToString());
                        }
                    }

                    allMovies.Add(new MovieMenuItem(Convert.ToInt32(row[0]), row[1].ToString(), row[4].ToString(),
                        Convert.ToDouble(row[2]), Convert.ToInt32(row[5]), row[3].ToString(),row[6].ToString(),row[7].ToString(), actors));
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
            cmd.Connection = _connection;

            DataTable results = SendQuery(cmd);

            if (Convert.ToInt32(results.Rows[0][0]) == 1) return true;

            return false;
        }

        public static bool UserExist(string userName)
        {
            if (!Regex.IsMatch(userName, @"^[a-zA-Z0-9]+$") || userName.Length < 3) return false;

            MySqlCommand cmd = new MySqlCommand();
            cmd.CommandText = "SELECT  count(*) FROM users WHERE Username = @username";
            cmd.Parameters.AddWithValue("@username", userName);
            cmd.Connection = _connection;

            DataTable results = SendQuery(cmd);

            if (Convert.ToInt32(results.Rows[0][0]) == 1) return true;

            return false;
        }

        public static bool CreateNewUser(string firstName, string lastName, string userName, string password)
        {
            if (!Regex.IsMatch(userName, @"^[a-zA-Z0-9]+$") || userName.Length < 3) return false;

            MySqlCommand cmd = new MySqlCommand("INSERT INTO users (Firstname, Lastname, Username, Password) " +
                                                    "Values ('" + firstName + "', '" + lastName + "', '" + userName +
                                                    "', '" + password + "')", _connection);

            return SendNonQuery(cmd);         
        }

        public static bool CreateUserTable(string userName)
        {
            MySqlCommand cmd = new MySqlCommand(
                $"CREATE TABLE `{userName}_movies` ( `id`  int NOT NULL AUTO_INCREMENT , `movieID`  int NULL , `rating`  varchar(255) NULL , PRIMARY KEY (`id`));", _connection);

            return SendNonQuery(cmd);
        }

        public static int NumberOfRowsInTable(string table) //Note: Mangler Unittest
        {
            try
            {
                string query = $"SELECT COUNT(*) FROM {table}";
                using (MySqlCommand cmd = new MySqlCommand(query, _connection))
                {
                    _connection.Open();

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
                _connection.Close();
            }

        }

        public static List<int> GetUserRatedMovies()
        {
            List<int> movieId = new List<int>();

            MySqlCommand cmd = new MySqlCommand();

            cmd.CommandText = $"SELECT * FROM {User.Username}_movies;";
            cmd.Connection = _connection;

            DataTable results = SendQuery(cmd);

            try
            {
                foreach (DataRow row in results.Rows)
                {
                    movieId.Add(Convert.ToInt32(row[1]));
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }

            return movieId;
        }

        private static bool SendNonQuery(MySqlCommand cmd)
        {
            try
            {
                _connection.Open();

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
                _connection.Close();
            }
        }

        private static DataTable SendQuery(MySqlCommand cmd)
        {
            DataTable results = new DataTable();
            try
            {
                _connection.Open();

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
                        Console.ReadKey();
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
                _connection.Close();
            }
        }
    }
}
