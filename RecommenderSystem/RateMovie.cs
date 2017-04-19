using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RecommenderSystem
{
    class RateMovie : MenuItemBase
    {
        private int _movieID;
        private string _enumvalue;

        public RateMovie(string title, ConsoleColor color, string enumvalue, int movieID) : base(title, color)
        {
            _movieID = movieID;
            _enumvalue = enumvalue;
        }
        
        public override void Select()
        {
            Console.Write("Movie Rated! \t");
            MySqlCommands.RateMovie(_movieID, _enumvalue);
        } 
    }
}
