using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RecommenderSystem
{
    class Menu : MenuItemBase
    {
        protected bool Running = false;
        protected List<MenuItemBase> MenuItems = new List<MenuItemBase>();

        private const ConsoleColor Black = ConsoleColor.Black;
        private const ConsoleColor White = ConsoleColor.White;

        public Menu(string title) : this(title, null)
        { }

        public Menu(string title, params MenuItemBase[] items) : base(title)
        {
            if (items != null)
            {
                foreach (MenuItemBase menuItem in items)
                {
                    AddMenuItem(menuItem);
                }
            }
        }

        public void AddMenuItem(params MenuItemBase[] items)
        {
            if (items != null)
            {
                foreach (MenuItemBase menuItem in items)
                {
                    MenuItems.Add(menuItem);
                }
            }
        }

        public virtual void Start()
        {
            Running = true;
            Console.CursorVisible = false;
            Console.ForegroundColor = Black;

            DrawMenu();
            do
            {
                HandleInput();
            } while (Running);
        }

        protected virtual void DrawMenu()
        {
            Console.Clear();
            Console.Title = this.Title;
            CenterText($"- {this.Title} -");

            foreach (MenuItemBase item in MenuItems)
            {
                CenterText(item.Title);
            }

            HighlightText(MenuItems[0].Title, 1);
        }

        public void HandleInput()
        {
            ConsoleKeyInfo cki = Console.ReadKey();
            switch (cki.Key)
            {
                case ConsoleKey.Backspace:
                case ConsoleKey.Escape:
                    Running = false;
                    break;
                case ConsoleKey.UpArrow:
                    MoveUp();
                    break;
                case ConsoleKey.DownArrow:
                    MoveDown();
                    break;
                case ConsoleKey.Enter:
                    SelectMenuItem();
                    break;
                default:
                    Console.SetCursorPosition(Console.CursorLeft - 1, Console.CursorTop);
                    break;
            }
        }

        protected virtual void MoveUp()
        {
            if (Console.CursorTop - 2 < 0) return;

            RemoveHighlight(MenuItems[Console.CursorTop - 1].Title, Console.CursorTop);
            HighlightText(MenuItems[Console.CursorTop - 2].Title, Console.CursorTop - 1);
        }

        private void MoveDown()
        {
            if (Console.CursorTop >= MenuItems.Count) return;

            RemoveHighlight(MenuItems[Console.CursorTop - 1].Title, Console.CursorTop);
            HighlightText(MenuItems[Console.CursorTop].Title, Console.CursorTop + 1);
        }

        private void SelectMenuItem()
        {
            Console.ForegroundColor = White;
            GetSelectedMenuItem().Select();
            Console.Title = Title;

            DrawMenu();
        }

        private MenuItemBase GetSelectedMenuItem()
        {
            return MenuItems[Console.CursorTop - 1];
        }

        private static void CenterText(string text)
        {
            Console.ResetColor();
            Console.Write(new string(' ', (Console.WindowWidth - text.Length) / 2));
            Console.WriteLine(text);
            Console.ForegroundColor = ConsoleColor.Black;
        }

        private static void Highlightcolor(bool state = true)
        {
            if (state)
            {
                Console.BackgroundColor = ConsoleColor.White;
                Console.ForegroundColor = ConsoleColor.Black;
            }
            else
            {
                Console.BackgroundColor = ConsoleColor.Black;
                Console.ForegroundColor = ConsoleColor.Black;
            }
        }

        protected virtual void HighlightText(string text, int pos)
        {
            int startingPoint = (Console.WindowWidth - text.Length) / 2;

            Console.SetCursorPosition(startingPoint, pos);
            Highlightcolor();
            Console.Write(text);
            Highlightcolor(false);
        }

        protected virtual void RemoveHighlight(string text, int pos)
        {
            int startingPoint = (Console.WindowWidth - text.Length) / 2;

            Console.SetCursorPosition(startingPoint, pos);
            Console.ResetColor();
            Console.Write(text);
            Console.ForegroundColor = ConsoleColor.Black;
        }

        public override void Select()
        {
            this.Start();
        }

        protected void SortMenuItems()
        {
            MenuItems = MenuItems.OrderBy(o => o.Title).ToList();
        }
    }
}
