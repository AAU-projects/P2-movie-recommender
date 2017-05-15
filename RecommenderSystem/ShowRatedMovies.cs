using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RecommenderSystem
{
    class ShowRatedMovies : Menu
    {
        private int _lastNumberOfRatedMovies;

        public ShowRatedMovies(string title) : base(title)
        {
        }

        public override void Select()
        {
            if (_lastNumberOfRatedMovies != User.NumberOfMoviesRated)
            {
                MenuItems.Clear();
                List<int> movieIDs = MySqlCommands.GetUserRatedMovies();
                List<MovieMenuItem> moviesRated = MySqlCommands.FindMovieFromId(movieIDs);

                foreach (var movie in moviesRated)
                {
                    AddMenuItem(movie);
                }
            }
            
            _lastNumberOfRatedMovies = User.NumberOfMoviesRated;
            Console.Clear();

            base.Select();
        }
    }
}
