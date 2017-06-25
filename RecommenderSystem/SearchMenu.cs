using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RecommenderSystem
{
    class SearchMenu : Menu
    {
        private string title;

        public SearchMenu(string title) : base(title)
        {
            this.title = title;

        }

        public override void Select()
        {
            Console.Clear();
            MenuItems.Clear();

            Console.WriteLine("Enter a keyword for a movie (title, genre, year, actor, director etc.)");
            string search = Console.ReadLine();

            List<MovieMenuItem> moviesSearch = MySqlCommands.SearchForKeyWord(search);

            if (moviesSearch.Count != 0)
            {
                Title = $"Keyword search: {search}";
                foreach (var movieMenu in moviesSearch)
                {
                    AddMenuItem(movieMenu);
                }

                Console.Clear();

                base.Select();
            }
            else
            {
                Console.WriteLine("No results found");
                Console.ReadKey();
                Running = false;
            }

            Title = $"{this.title}";
        }
    }
}