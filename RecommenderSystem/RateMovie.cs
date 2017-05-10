using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RecommenderSystem
{
    class RateMovie : MenuItemBase
    {
        private readonly int _movieId;
        private readonly string _enumvalue;

        public RateMovie(string title, ConsoleColor color, string enumvalue, int movieId) : base(title, color)
        {
            _movieId = movieId;
            _enumvalue = enumvalue;
        }
        
        public override void Select()
        {
            Console.Clear();

            PrintStringColored("Movie Rated!", ConsoleColor.Magenta);
            Console.WriteLine("\nPress any key to continue...");

            MySqlCommands.RateMovie(_movieId, _enumvalue);
            User.UpdateUser();

            Console.ReadKey();
        } 
    }
}
