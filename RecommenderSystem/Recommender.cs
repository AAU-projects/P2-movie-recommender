using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RecommenderSystem
{
    static class Recommender
    {
        private static List<int> _movieIDs = new List<int>();
        private static List<MovieMenuItem> _moviesRated = new List<MovieMenuItem>();

        private const int ThumbsUpRating = 1;
        private const int ThumbsDownRating = 0;

        public static void Update(params string[] items)
        {
            _movieIDs = MySqlCommands.GetUserRatedMovies();
            _moviesRated = MySqlCommands.FindMovieFromID(_movieIDs);

            FindType(items);
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
                            User.Preferences[type][item][(int)UserRating.thumbs_ups] += movieRating;     //Store the thumbs up
                            User.Preferences[type][item][(int)UserRating.total_rated] += 1;               //Stores the number of movies rated
                        }
                        else
                            User.Preferences[type].Add(item, new double[] { movieRating, 1, 0 });
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
                double thumbsUps = item.Value[(int)UserRating.thumbs_ups];
                double numberOfMoviesRated = item.Value[(int)UserRating.total_rated];

                item.Value[(int)UserRating.weight] = thumbsUps / User.NumberOfMoviesRated / numberOfMoviesRated * thumbsUps;
            }
        }

        private static void SortByWeight(string type)
        {
            List<KeyValuePair<string, double[]>> sortedList = User.Preferences[type].OrderByDescending(x => x.Value[(int)UserRating.weight]).ToList();
            User.Preferences[type].Clear();      //Deletes content of dict genre

            foreach (var item in sortedList)        //Adds our new values
            {
                User.Preferences[type].Add(item.Key, item.Value);
            }
        }

    }
}
