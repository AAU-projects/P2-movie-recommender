using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;

namespace RecommenderSystem
{
    class ColdStart : Menu
    {
        public List<int> UsedNumbers = new List<int>();
        public bool _firststart = true;

        public ColdStart(string title, List<int> usedNumbers) : base(title)
        {
            UsedNumbers = usedNumbers;
        }

        public override void Select()
        {
            Console.Clear();

            if (User.NumberOfMoviesRated >= 10)
            {
                _running = false;
            }
            else
            {
                FindUnratedMovies(10);
                this.Start();
            }
        }

        public void FindUnratedMovies(int numberOfMovies)
        {
            if (_firststart)
            {
                List<int> rateMoviesNumbers = new List<int>();
                List<MovieMenuItem> MoviesColdStart = new List<MovieMenuItem>();
                int totalNumberOfMovies = MySqlCommands.NumberOfRowsInTable("imdbdata");

                rateMoviesNumbers.Clear();
                rateMoviesNumbers.AddRange(GenerateRandomNumber(totalNumberOfMovies, numberOfMovies, UsedNumbers));
                MoviesColdStart = MySqlCommands.FindMovieFromID(rateMoviesNumbers);
                foreach (var movieMenuItem in MoviesColdStart)
                {
                    AddMenuItem(movieMenuItem);
                }
                ColdStart nextPage = new ColdStart($"--- Page {UsedNumbers.Count / 10 + 1} ---", UsedNumbers);
                AddMenuItem(nextPage);
                _firststart = false;
            }
            Console.Clear();
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

        public override void Start()
        {
            _running = true;
            Console.CursorVisible = false;
            Console.ForegroundColor = ConsoleColor.Black;
            DrawMenu();
            do
            {
                HandleInput();
            } while (_running && User.NumberOfMoviesRated < 10);

            Menu loggedInMenu = new Startmenu($"Welcome {User.Username}!");
            loggedInMenu.Start();
        }
    }
}
