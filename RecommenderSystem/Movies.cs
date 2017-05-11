using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RecommenderSystem
{
    static class Movies
    {
        private static List<MovieMenuItem> _allMovies;

        public static void LoadAllMovies()
        {
            _allMovies = MySqlCommands.GetMovies();
        }

        public static List<MovieMenuItem> AllMovies
        {
            get => _allMovies;
        }

        public static MovieMenuItem GetMovieByID(int id)
        {
            return AllMovies.Find(m => m.MovieId == id);
        }

        public static List<MovieMenuItem> GetMoviesByID(List<int> ids)
        {
            return AllMovies.FindAll(m => ids.Contains(m.MovieId));
        }
    }
}
