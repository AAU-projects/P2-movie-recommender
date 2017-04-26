using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RecommenderSystem
{
    class TopActors : MenuItemBase
    {
        public TopActors(string title) : base(title)
        {
            //Recommender.Update("actors");
        }

        public override void Select()
        {
            Recommender.Update("actors");
            Console.Clear();
            foreach (var actor in User.Preferences["actors"])
            {
                Console.WriteLine($"{actor.Key} | {actor.Value[(int)UserRating.weight]}");
            }
            Console.ReadLine();
        }
    }
}
