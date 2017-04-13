using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RecommenderSystem
{
    class RateMovie : MenuItemBase
    {
        public RateMovie(string title, ConsoleColor color) : base(title, color)
        {}
        
        public override void Select()
        {
            Console.Write("Movie Rated! \t");
        } 
    }
}
