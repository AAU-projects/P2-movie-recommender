using System;
using System.Collections.Generic;
using System.Data.Entity.Migrations.Model;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RecommenderSystem
{
    static class Recommender
    {
        private static List<int> _movieIDs = new List<int>();
        private static List<MovieMenuItem> _moviesRated = new List<MovieMenuItem>();
        public static IEnumerable<KeyValuePair<MovieMenuItem, double>> MovieRatingsWeight = new Dictionary<MovieMenuItem, double>();

        private const int ThumbsUpRating = 1;
        private const int ThumbsDownRating = 0;

        public static void Update(params string[] items)
        {
            _movieIDs = MySqlCommands.GetUserRatedMovies();
            _moviesRated = Movies.GetMoviesByID(_movieIDs);

            FindType(items);
        }

        public static void GetRecommendedMovies()
        {
            Dictionary<MovieMenuItem, double> localmovieRatingsWeight = new Dictionary<MovieMenuItem, double>();
            MovieRatingsWeight = new Dictionary<MovieMenuItem, double>();
            
            Console.Clear();
            Console.WriteLine("Caculating...");
            Update("genre", "directors", "actors");

            foreach (var movie in Movies.AllMovies)
            {
                if (!MySqlCommands.IsMovieRated(movie.MovieId))
                {
                    double movieWeight = 0;

                    if (User.Preferences["directors"].ContainsKey(movie.Director))                                  // weight for directors
                    {
                        movieWeight += User.Preferences["directors"][movie.Director][(int)UserRating.Weight];
                    }
         
                    string[] genres = movie.Genre.Replace(" ", "").Split(',');                                       // weight for genres
                    string topGenre = User.Preferences["genre"].Keys.FirstOrDefault(g => genres.Contains(g));
                    int numberOfGenres = genres.Length;

                    foreach (var genre in genres)
                    {
                        try
                        {
                            if (topGenre == genre)
                            {
                                movieWeight += User.Preferences["genre"][genre][(int)UserRating.Weight];
                            }
                            else
                            {
                                movieWeight += User.Preferences["genre"][genre][(int)UserRating.Weight]/numberOfGenres;
                            }
                        }
                        catch (KeyNotFoundException)
                        {
                            movieWeight += 0;
                        }
                    }

                    foreach (var actor in movie.Actors)
                    {
                        if (User.Preferences["actors"].ContainsKey(actor))
                        {
                            movieWeight += User.Preferences["actors"][actor][(int)UserRating.Weight];
                        }
                    }
                    movie.Title += $" | {movieWeight:0.00}";
                    localmovieRatingsWeight.Add(movie, movieWeight);
                }
            }

            MovieRatingsWeight = localmovieRatingsWeight.OrderByDescending(m => m.Value);
            Console.Clear();
        }


        private static void FindType(params string[] types)
        {
            foreach (string type in types)
            {
                User.Preferences[type].Clear();

                foreach (var movie in _moviesRated)
                {
                    int movieRating = movie.UserRating == "thumbsup" ? ThumbsUpRating : ThumbsDownRating;
                    List<string> itemList = new List<string>();

                    if (type == "genre")
                    {
                        itemList = movie.Genre.Replace(" ", "").Split(',').ToList();
                    }
                    else if (type == "actors")
                    {
                        itemList = movie.Actors;
                    }
                    else if (type == "directors")
                    {
                        itemList.Add(movie.Director);
                    }

                    foreach (var item in itemList)
                    {
                        if (User.Preferences[type].ContainsKey(item))
                        {
                            User.Preferences[type][item][(int) UserRating.ThumbsUps] += movieRating;        //Store the thumbs up
                            User.Preferences[type][item][(int) UserRating.TotalRated] += 1;                 //Stores the number of movies rated
                        }
                        else
                        {
                            User.Preferences[type].Add(item, new double[] { movieRating, 1, 0 });
                        }      
                    }
                }

                CalculateWeight(type);
                SortByWeight(type);
            }
        }

        private static void CalculateWeight(string type)
        {
            foreach (var item in User.Preferences[type])
            {
                double thumbsUps = item.Value[(int)UserRating.ThumbsUps];
                double numberOfMoviesRated = item.Value[(int)UserRating.TotalRated];

                item.Value[(int)UserRating.Weight] = thumbsUps / User.NumberOfMoviesRated / numberOfMoviesRated * thumbsUps;
            }
        }

        private static void SortByWeight(string type)
        {
            List<KeyValuePair<string, double[]>> sortedList = User.Preferences[type].OrderByDescending(x => x.Value[(int)UserRating.Weight]).ToList();
            User.Preferences[type].Clear();      //Deletes content of dict genre

            foreach (var item in sortedList)        //Adds our new values
            {
                User.Preferences[type].Add(item.Key, item.Value);
            }
        }

    }
}
