using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RecommenderSystem
{
    class ColdStart : Menu
    {
        public List<int> _usedNumbers = new List<int>();
        public ColdStart(string title) : base(title)
        {
        }

        public override void Select()
        {
            Console.Clear();
            FindUnratedMovies(10); 
        }

        public virtual void FindUnratedMovies(int numberOfMovies)
        {
            List<int> rateMoviesNumbers = new List<int>();
            List<MovieMenuItem> MoviesColdStart = new List<MovieMenuItem>();
            int totalNumberOfMovies = MySqlCommands.NumberOfRowsInTable("imdbdata");

            rateMoviesNumbers.Clear();
            rateMoviesNumbers.AddRange(GenerateRandomNumber(totalNumberOfMovies, numberOfMovies, _usedNumbers));
            MoviesColdStart = MySqlCommands.FindMovieFromID(rateMoviesNumbers);
            foreach (var movieMenuItem in MoviesColdStart)
            {
                AddMenuItem(movieMenuItem);
            }
            AddMenuItem(new ColdStartNextPage($"--- Page {_usedNumbers.Count / 10 + 1} ---", _usedNumbers));

            Console.Clear();
            base.Select();

        }

        private List<int> GenerateRandomNumber(int totalNumberOfMovies, int numberOfGeneratedNumbers, List<int> usedNumbers)
        {
            List<int> rateMoviesNumbers = new List<int>();
            Random random = new Random();
            int number = 0;

            for (int i = 0; i < numberOfGeneratedNumbers; i++)
            {
                do
                {
                    number = random.Next(1, totalNumberOfMovies);
                } while (usedNumbers.Contains(number));
                usedNumbers.Add(number);
                rateMoviesNumbers.Add(number);
            }
            return rateMoviesNumbers; 
        }
    }
}
