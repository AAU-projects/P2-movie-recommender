using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RecommenderSystem
{
    class Startmenu : Menu
    {
        public Startmenu(string title) : base(title)
        {
            AddMenuItem(
                new RecommendedMovies("Your recommdantions"),
                new MovieMenu("View all movies"),
                new MovieMenu("Rate movies", false),
                new ShowRatedMovies("Your rated movies"),
                new TopGenre("Top Genres"),
                new TopActors("Top Actors"),
                new TopDirector("Top Directors"));
        }
    }
}
