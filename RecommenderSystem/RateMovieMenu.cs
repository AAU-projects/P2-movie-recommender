﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RecommenderSystem
{
    class RateMovieMenu : MenuItemBase
    {
        public RateMovieMenu(string title) : base(title)
        { }

        private List<RateMovie> _options = new List<RateMovie>(){new RateMovie("Thumps up", ConsoleColor.Green), new RateMovie("Thumps down", ConsoleColor.Red)};
        private int _optionOnePossition;
        private int _currentOption;
        private bool _running = false;

        public override void Select()
        {
            this.Start();
        }

        public void Start()
        {
            _running = true;
            DrawMenu();
            do
            {
                HandleInput();
            } while (_running);
        }

        private void HandleInput()
        {
            ConsoleKeyInfo cki = Console.ReadKey();
            switch (cki.Key)
            {
                case ConsoleKey.Backspace:
                case ConsoleKey.Escape:
                    _running = false;
                    break;
                case ConsoleKey.UpArrow:
                    Console.SetCursorPosition(Console.CursorLeft - 1, Console.CursorTop);
                    MoveUp();
                    break;
                case ConsoleKey.DownArrow:
                    Console.SetCursorPosition(Console.CursorLeft - 1, Console.CursorTop);
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

        private void SetCurrentOption(int position)
        {
            Console.SetCursorPosition(0, Console.CursorTop);
            Console.Write("   ");
            _currentOption = position - _optionOnePossition;
            Console.SetCursorPosition(0,position);
            Console.Write("->");
        }

        private void SelectMenuItem()
        {
            _options.ElementAt(_currentOption).Select();
        }

        private void MoveDown()
        {
            if (Console.CursorTop + 1 > _optionOnePossition + (_options.Count - 1)) return;
            SetCurrentOption(Console.CursorTop + 1);
        }

        private void MoveUp()
        {
            if (Console.CursorTop -1 < _optionOnePossition) return;
            SetCurrentOption(Console.CursorTop - 1);
        }

        private void DrawMenu()
        {
            _optionOnePossition = Console.CursorTop;
            foreach (RateMovie option in _options)
            {
                Console.Write("   ");
                option.PrintTitleColored();
            }
            Console.WriteLine("\nUse Up and Down arrow keys to choose an rating and enter to confirm.\nPress Esc to go back");
            SetCurrentOption(_optionOnePossition);
        }
    }
}
