using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using NUnit.Framework;

namespace RecommenderSystem.Tests
{
    class RecommenderTest
    {
        // (a^2)/(b*c) == a/b/c*a
        // example for expected result
        // action weight = thumbsUps(1) / numberOfMoviesRated(3) / numberOfMoviesItemRated(1) * thumbsUps(1);
        // action weight = 1 / 3 / 1 * 1 = 0,33;
        [TestCase("genre", "Action", ExpectedResult = 0.33)]
        [TestCase("genre", "Drama", ExpectedResult = 0.67)] // 2/3/2*2 = 0,67 
        [TestCase("actors", "Harrison Ford", ExpectedResult = 0.33)]       // 1/3/1/1 = 0,33
        public double Update_TestUserPreferencesAction(string type, string item)
        {
            new User("testuserPreferences");
            MySqlCommands.RateMovie(12, "thumbsup"); // star wars movie
            MySqlCommands.RateMovie(13, "thumbsup"); // forrest gump
            MySqlCommands.RateMovie(10, "thumbsup"); // fight club
            Recommender.Update(type);

            return Math.Round(User.Preferences[type][item][(int)UserRating.Weight], 2);
        }

        [Test]
        public void GetRecommendedMovies_testuser_StarWarsRecommended()
        {
            new User("testuser");
            MySqlCommands.RateMovie(12, "thumbsup"); // star wars movie
            MySqlCommands.RateMovie(212, "thumbsup"); // star wars movie
            MySqlCommands.RateMovie(73, "thumbsup"); // star wars movie
            MySqlCommands.RateMovie(10, "thumbsup"); // fight club
            MySqlCommands.RateMovie(13, "thumbsup"); // forrest gump
            MySqlCommands.RateMovie(22, "thumbsup"); // se7en

            Recommender.Update("genre", "directors", "actors");
            Recommender.AlgorithmMovieWeight(MySqlCommands.FindMovieFromId(new List<int> {20}).ToList());
            // given user data with these 6 movies for the movieid 20, Star Wars(1997)
            // genre: action(0,5), adventure(0,5), fantasy(0,5), sci-fi(0,5)
            // director: george lucas(0)
            // actors: Mark(0,5), Harrison(0,5), Carrie(0,5), Peter C(0), Alec(0,1667), Anthony(0,3333), Kenny(0,1667), Peter M(0,3333), David(0,1667), Phil(0)

            // total director = 0
            // total actors = 0,5 + 0,5 + 0,5 + 0 + 0,1667 + 0,3333 + 0,1667 + 0,3333 + 0,1667 + 0 = 2,6667
            // total genre = 0,5 + 0,5/4 + 0,5/4 + 0,5/4 = 0,875

            // total weight = 0 + 2,6667 + 0,875 = 3,5417

            KeyValuePair<MovieMenuItem, double> movie = Recommender.MovieRatingsWeight.First();

            Assert.AreEqual(Math.Round(3.5417, 2), (Math.Round(movie.Value, 2))); // rounds down due to data is more preceise
        }
    }
}
