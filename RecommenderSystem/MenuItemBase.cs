﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RecommenderSystem
{
    abstract class MenuItemBase
    {
        protected MenuItemBase(string title)
        {
            this.Title = title;
        }
        public virtual string Title { get; }
        public abstract void Select();

        public void PrintStringColored(string source, ConsoleColor color)
        {
            Console.ForegroundColor = color;
            Console.WriteLine($"{source}");
            Console.ForegroundColor = ConsoleColor.White;
        }
    }
}
