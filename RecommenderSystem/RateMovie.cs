using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RecommenderSystem
{
    class RateMovie : MenuItemBase
    {
        public RateMovie(string title, ConsoleColor color) : base(title)
        {
            this._titleColor = color;
        }

        private ConsoleColor _titleColor;


        public override void Select()
        {

        } 
    }
}
