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
                new RecommendedMovies("Your recommendations"),
                new MovieMenu("View all movies"),
                new MovieMenu("Rate movies", false),
                new ShowRatedMovies("Your rated movies"));

            if (User.DebugState)
            {
                AddMenuItem(
                new TopGenre("Top Genres"),
                new TopActors("Top Actors"),
                new TopDirector("Top Directors"));

                Recommender.Update("genre", "directors", "actors");
            }
        }
    }
}
