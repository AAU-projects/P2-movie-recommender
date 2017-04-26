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
        {
            Recommender.Update();
        }

        public override void Select()
        {
            Console.Clear();
            foreach (var genre in User.preferences["genre"])
            {
                Console.WriteLine($"{genre.Key} | {genre.Value[(int)UserRating.weight]}");
            }
            Console.ReadLine();
        }
    }
}
