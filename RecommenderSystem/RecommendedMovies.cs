using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RecommenderSystem
{
    class RecommendedMovies : Menu
    {
        public RecommendedMovies(string title) : base(title)
        {
        }

        public RecommendedMovies(string title, params MenuItemBase[] items) : base(title, items)
        {
        }

        public override void Select()
        {
            Console.Clear();
            MenuItems.Clear();
            Recommender.GetRecommendedMovies();
            Console.Clear();
            foreach (var movie in Recommender.MovieRatingsWeight.Take(10))
            {
                AddMenuItem(movie.Key);
            }

            base.Select();
        }
    }
}
