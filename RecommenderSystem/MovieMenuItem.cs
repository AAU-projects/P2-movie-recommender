using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RecommenderSystem
{
    class MovieMenuItem : MenuItemBase
    {
        public readonly int MovieId;
        private readonly string _releaseDate;
        private readonly double _rating;
        private readonly int _duration;
        public readonly string Genre;
        private readonly string _resume;
        public readonly string Director;
        public readonly List<string> Actors;
        public string UserRating;

        public MovieMenuItem(int movieId, string title, string releaseDate, double rating, int duration, string genre, string resume, string director, List<string> actors) : base(title)
        {
            this.MovieId = movieId;
            this._releaseDate = releaseDate;
            this._rating = rating;
            this._duration = duration;
            this.Genre = genre;
            this._resume = resume;
            this.Director = director;
            this.Actors = actors;
            this.UserRating = MySqlCommands.FindRatingFromMovieId(MovieId);
        }
        
        public override void Select()
        {
            UserRating = MySqlCommands.FindRatingFromMovieId(MovieId);
            Console.Clear();

            if (User.DebugState)
            {
                printDebugMovieDetails(UserRating);
            }
            else
            {
                printMovieDetails(UserRating);
            }

            RateMovieMenu rateMenu = new RateMovieMenu("Rate this movie", MovieId);
            User.UpdateUser();

            rateMenu.Start();
        }

        private void printMovieDetails(string UserRating)
        {
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
                Console.WriteLine($"\nothers have rated this movie {_rating } on IMDB");
            }
            else
            {
                Console.WriteLine(); // new line for style
            }
        }

        private void printDebugMovieDetails(string UserRating)
        {
            Console.WriteLine($"{Title}   {_releaseDate}");
            string[] genres = Genre.Replace(" ", "").Split(',');
            foreach (var genre in genres)
            {
                try
                {
                    Console.Write($"{genre}({User.Preferences["genre"][genre][2]:N4}), ");
                }
                catch (Exception e)
                {
                    Console.Write($"{genre}(0), ");
                }
                
            }      
            Console.WriteLine($"\n");
            Console.WriteLine($"Duration: {_duration} min");
            Console.WriteLine($"{_resume}");

            try
            {
                Console.WriteLine($"Director: {Director}({User.Preferences["directors"][Director][2]:N4})");
            }
            catch (Exception e)
            {
                Console.WriteLine($"Director: {Director}(0)");
            }

            Console.WriteLine("\nLeading actors");

            foreach (var actor in Actors)
            {
                try
                {
                    Console.WriteLine($"{actor}({User.Preferences["actors"][actor][2]:N4})");
                }
                catch (Exception e)
                {
                    Console.WriteLine($"{actor}(0)");
                }
            }

            if (UserRating != "notRated")
            {
                PrintStringColoredInLine($"\nYou have rated this movie: ", ConsoleColor.Magenta);
                if (UserRating == "thumbsup")
                    PrintStringColored("thumbs up", ConsoleColor.Green);
                else if (UserRating == "thumbsdown")
                    PrintStringColored("thumbs down", ConsoleColor.Red);
                Console.WriteLine($"\nothers have rated this movie {_rating } on IMDB");
            }
            else
            {
                Console.WriteLine(); // new line for style
            }
        }

    }
}
