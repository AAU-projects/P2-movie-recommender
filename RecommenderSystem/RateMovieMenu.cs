using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RecommenderSystem
{
    class RateMovieMenu : Menu
    {
        public RateMovieMenu(string title) : base(title)
        { }

        public RateMovieMenu(string title, params MenuItemBase[] items) : base(title)
        {
            if (items != null)
            {
                foreach (MenuItemBase menuItem in items)
                {
                    AddMenuItem(menuItem);
                }
            }
        }

        private int _thumbsOnePossition;

        protected override void DrawMenu()
        {
            _thumbsOnePossition = Console.CursorTop;
            foreach (var thumbs in _menuItems)
            {
                thumbs.PrintTitleColored();
            }

            HighlightText(_menuItems[0].Title, _thumbsOnePossition);
        }

        protected override void HighlightText(string text, int pos)
        {
            Console.SetCursorPosition(0, pos);
            Console.BackgroundColor = ConsoleColor.White;
            Console.ForegroundColor = ConsoleColor.Black;
            Console.Write(text);
        }

        protected override void RemoveHighlight(string text, int pos)
        {
            Console.SetCursorPosition(0, pos);
            Console.ResetColor();
            Console.Write(text);
        }

        protected override void MoveUp()
        {
            if (Console.CursorTop - 1 < _thumbsOnePossition) return;
            RemoveHighlight(_menuItems[Console.CursorTop - _thumbsOnePossition - 1].Title, Console.CursorTop);
            HighlightText(_menuItems[Console.CursorTop - _thumbsOnePossition].Title, Console.CursorTop - 1);
        }
    }
}
