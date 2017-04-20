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

        public User(string userName)
        {
            Username = userName;
            NumberOfMoviesRated = MySqlCommands.NumberOfRowsInTable($"{Username}_movies");
        }

        public static void UpdateUser()
        {
            NumberOfMoviesRated = MySqlCommands.NumberOfRowsInTable($"{Username}_movies");
        }
    }
}
