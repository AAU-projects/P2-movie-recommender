using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RecommenderSystem
{
    class ShowRatedMovies : Menu
    {
        private int lastNumberOfRatedMovies = 0;
        public ShowRatedMovies(string title) : base(title)
        {
        }

        public override void Select()
        {
            
            if (lastNumberOfRatedMovies != User.NumberOfMoviesRated)
            {
                _menuItems.Clear();
                List<int> movieIDs = MySqlCommands.GetUserRatedMovies();
                List<MovieMenuItem> moviesRated = MySqlCommands.FindMovieFromID(movieIDs);
                foreach (var movie in moviesRated)
                {
                    AddMenuItem(movie);
                }
            }
            

            lastNumberOfRatedMovies = User.NumberOfMoviesRated;
            Console.Clear();
            base.Select();
        }
    }
}
