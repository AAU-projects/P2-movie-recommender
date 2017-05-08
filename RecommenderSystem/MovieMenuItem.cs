using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RecommenderSystem
{
    class MovieMenuItem : MenuItemBase
    {
        public MovieMenuItem(int movieID, string title, string releaseDate, double rating, int duration, string genre, string resume, string director, List<string> actors) : base(title)
        {
            this._movieID = movieID;
            this._releaseDate = releaseDate;
            this._rating = rating;
            this._duration = duration;
            this.Genre = genre;
            this._resume = resume;
            this.Director = director;
            this.Actors = actors;
            this.UserRating = MySqlCommands.FindRatingFromMovieID(_movieID);
        }

        public readonly int _movieID;
        private readonly string _releaseDate;
        private readonly double _rating;
        private readonly int _duration;
        public readonly string Genre;
        private readonly string _resume;
        public readonly string Director;
        public readonly List<string> Actors;
        public string UserRating;
        
        public override void Select()
        {
            UserRating = MySqlCommands.FindRatingFromMovieID(_movieID);

            Console.Clear();
            Console.WriteLine($"{Title}   {_releaseDate}");
            Console.WriteLine($"{Genre}");
            Console.WriteLine($"");
            Console.WriteLine($"Duration: {_duration} min");
            Console.WriteLine($"{_resume}");
            Console.WriteLine($"Director: {Director}.");
            Console.WriteLine("\nLeading actors");
            foreach (var actor in Actors)
            {
                Console.WriteLine(actor);
            }
                        
            if (UserRating != "notRated")
            {
                PrintStringColoredInLine($"\nYou have rated this movie: ", ConsoleColor.Magenta);
                if (UserRating == "thumbsup")
                    PrintStringColored("thumbs up", ConsoleColor.Green);
                else if (UserRating == "thumbsdown")
                    PrintStringColored("thumbs down", ConsoleColor.Red);
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
