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

        public static Dictionary<String, Dictionary<String, double[]>> Preferences = new Dictionary<string, Dictionary<string, double[]>>();

        public User(string userName)
        {
            Preferences.Clear();
            Username = userName;
            NumberOfMoviesRated = MySqlCommands.NumberOfRowsInTable($"{Username}_movies");
            Preferences.Add("genre", new Dictionary<string, double[]>());
            Preferences.Add("actors", new Dictionary<string, double[]>());
            Preferences.Add("directors", new Dictionary<string, double[]>());
        }

        public static void UpdateUser()
        {
            NumberOfMoviesRated = MySqlCommands.NumberOfRowsInTable($"{Username}_movies");
        }
    }
}
