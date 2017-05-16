using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RecommenderSystem
{
    class MovieMenu : Menu
    {
        private bool _firstRun = true;
        private readonly bool _showRated;

        public MovieMenu(string title) : this(title, true)
        { }

        public MovieMenu(string title, bool showRated) : base(title)
        {
            this._showRated = showRated;
        }

        public override void Select()
        {
            if (_firstRun && _showRated)
            {
                List<MovieMenuItem> allMovies = MySqlCommands.GetMovies();
                foreach (var movie in allMovies)
                {
                    AddMenuItem(movie);
                }
                _firstRun = false;
            }
            updateMovies();
            Console.Clear();
            base.Select();
        }

        private void updateMovies()
        {
            if (!_showRated)
            {
                MenuItems.Clear();
                List<MovieMenuItem> allMovies = MySqlCommands.GetMovies();
                foreach (var movie in allMovies)
                {
                    if (!MySqlCommands.IsMovieRated(movie.MovieId))
                    {
                        AddMenuItem(movie);
                    }
                }
            }
        }
    }
}
