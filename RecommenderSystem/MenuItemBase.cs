﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RecommenderSystem
{
    abstract class MenuItemBase
    {
        public MenuItemBase(string title)
        {
            this.Title = title;
        }

        protected MenuItemBase(string title, ConsoleColor color)
        {
            this.Title = title;
            this._titleColor = color;
        }

        public virtual string Title { get; set; }
        public abstract void Select();
        protected ConsoleColor _titleColor;

        public void PrintStringColored(string input, ConsoleColor color)
        {
            Console.ForegroundColor = color;
            Console.WriteLine(input);
            Console.ForegroundColor = ConsoleColor.White;
        }

        public void PrintStringColoredInLine(string input, ConsoleColor color)
        {
            Console.ForegroundColor = color;
            Console.Write(input);
            Console.ForegroundColor = ConsoleColor.White;
        }

        public void PrintTitleColored()
        {
            PrintStringColored(Title, _titleColor);
        }
    }
}
