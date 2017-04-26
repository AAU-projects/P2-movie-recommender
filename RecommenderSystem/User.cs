using System;
using System.Collections.Generic;
using System.Data.Entity.Migrations.Model;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RecommenderSystem
{
    class User
    {
        public static string Username;
        public static int NumberOfMoviesRated;

        public static Dictionary<String, Dictionary<String, double[]>> preferences = new Dictionary<string, Dictionary<string, double[]>>();

        public User(string userName)
        {
            preferences.Clear();
            Username = userName;
            NumberOfMoviesRated = MySqlCommands.NumberOfRowsInTable($"{Username}_movies");
            preferences.Add("genre", new Dictionary<string, double[]>());
        }

        public static void UpdateUser()
        {
            NumberOfMoviesRated = MySqlCommands.NumberOfRowsInTable($"{Username}_movies");
        }
    }
}
