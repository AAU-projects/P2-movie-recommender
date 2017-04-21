using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RecommenderSystem
{
    class MovieMenuItem : MenuItemBase
    {
        public MovieMenuItem(int movieID, string title, string releaseDate, double rating, int duration, string resume, string director, List<string> actors) : base(title)
        {
            this._movieID = movieID;
            this._releaseDate = releaseDate;
            this._rating = rating;
            this._duration = duration;
            this._resume = resume;
            this._director = director;
            this._actors = actors;
        }

        private int _movieID;
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
            Console.WriteLine($"");
            Console.WriteLine($"Duration: {_duration} min");
            Console.WriteLine($"{_resume}");
            Console.WriteLine($"Director: {_director}.");
            Console.WriteLine("\nLeading actors");
            foreach (var actor in _actors)
            {
                Console.WriteLine(actor);
            }

            string ratingForMovie = MySqlCommands.FindRatingFromMovieID(_movieID);

            if (ratingForMovie != "notRated")
            {
                PrintStringColored($"\nYou have rated this movie: {ratingForMovie}\n", ConsoleColor.Magenta);
            }
            else
            {
                Console.WriteLine(); // new line for style
            }
            

            RateMovieMenu rateMenu = new RateMovieMenu("Rate this movie", _movieID);
            User.UpdateUser();
            rateMenu.Start();
        }
    }
}
