using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RecommenderSystem
{
    class MenuItem : MenuItemBase
    {
        public MenuItem(string title) : this(title, null)
        { }

        public MenuItem(string title, string content) : base(title)
        {
            this.Content = content;
        }

        public string Content { get; }

        public override void Select()
        {
            Console.Title = Title;
            Console.Clear();
            Console.WriteLine($"{this.Title}\n\n{this.Content}\n\nPress Enter to return");
            Console.ReadKey();
        }
    }
}
