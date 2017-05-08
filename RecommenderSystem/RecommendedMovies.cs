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
        private IEnumerable<KeyValuePair<MovieMenuItem, double>> recommendender = new KeyValuePair<MovieMenuItem, double>[0];
        public RecommendedMovies(string title) : base(title)
        {
        }

        public RecommendedMovies(string title, params MenuItemBase[] items) : base(title, items)
        {
        }

        public override void Select()
        {
            _menuItems.Clear();
            Recommender.GetRecommendedMovies();
            foreach (var movie in Recommender.movieRatingsWeight.Take(10))
            {
                AddMenuItem(movie.Key);
            }
            base.Select();
        }
    }
}
