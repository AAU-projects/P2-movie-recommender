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

        public static MovieMenuItem GetMovieByID(int id)
        {
            return _allMovies.Find(m => m.MovieId == id);
        }

        public static List<MovieMenuItem> GetMoviesByID(List<int> ids)
        {
            return _allMovies.FindAll(m => ids.Contains(m.MovieId));
        }
    }
}
