using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RecommenderSystem
{
    class TopGenre : MenuItemBase
    {
        public TopGenre(string title) : base(title)
        { }

        public override void Select()
        {
            Recommender.Update("genre");
            Console.Clear();

            foreach (var genre in User.Preferences["genre"])
            {
                Console.WriteLine($"{genre.Key} | {genre.Value[(int)UserRating.Weight]}");
            }

            Console.ReadKey();
        }
    }
}
