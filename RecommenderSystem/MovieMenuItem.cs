using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RecommenderSystem
{
    class MovieMenuItem : MenuItemBase
    {
        public MovieMenuItem(string title, string releaseDate, double rating, int duration, string resume, string director, List<string> actors) : base(title)
        {
            this._releaseDate = releaseDate;
            this._rating = rating;
            this._duration = duration;
            this._resume = resume;
            this._director = director;
            this._actors = actors;
        }
        private string _releaseDate;
        private double _rating;
        private int _duration;
        private string _resume;
        private string _director;
        private List<string> _actors;
        
        public override void Select()
        {
            Console.Clear();
            Console.WriteLine($"{Title}   {_releaseDate}");
            Console.WriteLine($"Duration: {_duration}");
            Console.WriteLine($"{_resume}");
            Console.WriteLine($"Director: {_director}.");
            Console.WriteLine("\nLeading actors");
            foreach (var actor in _actors)
            {
                Console.WriteLine(actor);
            }
            Console.ReadLine();
        }
    }
}
