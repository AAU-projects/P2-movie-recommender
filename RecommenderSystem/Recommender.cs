using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RecommenderSystem
{
    static class Recommender
    {
        private static List<int> movieIDs = new List<int>();
        private static List<MovieMenuItem> moviesRated = new List<MovieMenuItem>();

        private static int _thumbsUpRating = 1;
        private static int _thumbsDownRating = 0;

        public static void Update()
        {
            movieIDs = MySqlCommands.GetUserRatedMovies();
            moviesRated = MySqlCommands.FindMovieFromID(movieIDs);

            FindGenres();
            CalculateGenreWeight();
            SortByWeight();
        }

        private static void FindGenres()
        {
            foreach (var movie in moviesRated)
            {
                int genreRating = movie.UserRating == "thumbsup" ? _thumbsUpRating : _thumbsDownRating;
                string[] genres = movie.Genre.Replace(" ", "").Split(',');

                foreach (var genre in genres)
                {
                    if (User.preferences["genre"].ContainsKey(genre))
                    {
                        User.preferences["genre"][genre][(int)UserRating.thumbs_ups] += genreRating;     //Store the thumbs up
                        User.preferences["genre"][genre][(int)UserRating.total_rated] += 1;               //Stores the number of movies rated
                    }  
                    else
                        User.preferences["genre"].Add(genre, new double[] {genreRating, 1, 0});
                }
            }
        }
        private static void CalculateGenreWeight()
        {
            double thumbsUpGenre;
            double numberOfMoviesRatedGenre;

            foreach (var genre in User.preferences["genre"])
            {
                thumbsUpGenre = genre.Value[(int)UserRating.thumbs_ups];
                numberOfMoviesRatedGenre = genre.Value[(int)UserRating.total_rated];

                 genre.Value[(int)UserRating.weight] = thumbsUpGenre / User.NumberOfMoviesRated / numberOfMoviesRatedGenre * thumbsUpGenre;
            }
        }

        private static void SortByWeight()
        {
            List<KeyValuePair<string, double[]>> bob = User.preferences["genre"].OrderByDescending(x => x.Value[(int)UserRating.weight]).ToList();
            User.preferences["genre"].Clear();      //Deletes content of dict genre

            foreach (var item in bob)               //Adds our new values
            {
                User.preferences["genre"].Add(item.Key, item.Value);
            }
        }

    }
}
