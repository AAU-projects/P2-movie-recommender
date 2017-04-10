using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RecommenderSystem
{
    class MovieMenu : Menu
    {
        public MovieMenu(string title) : base(title)
        {
        }

        private bool _firstRun = true;

        public override void Select()
        {
            if (_firstRun)
            {
                List<MovieMenuItem> allMovies = MySqlCommands.GetMovies();
                foreach (var movie in allMovies)
                {
                    AddMenuItem(movie);
                }
                _firstRun = false;
            }
            Console.Clear();
            base.Select();
        }
    }
}
