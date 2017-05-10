using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RecommenderSystem
{
    class TopDirector : MenuItemBase
    {
        public TopDirector(string title) : base(title)
        { }

        public override void Select()
        {
            Recommender.Update("directors");
            Console.Clear();

            foreach (var director in User.Preferences["directors"])
            {
                Console.WriteLine($"{director.Key} | {director.Value[(int)UserRating.Weight]}");
            }

            Console.ReadKey();
        }
    }
}
