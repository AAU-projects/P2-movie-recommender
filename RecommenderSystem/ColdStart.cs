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
        public bool UnitTest = false;

        public ColdStart(string title) : base(title)
        {
            UsedNumbers = new List<int>();
        }

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
            else if (User.NumberOfMoviesRated < 10)
            {
                FindUnratedMovies(10);
            }

            if (!UnitTest)
<<<<<<< HEAD
	        {
=======
            {
>>>>>>> refs/remotes/origin/master
                this.Start();
            }
        }

        private void UpdateTitle()
        {
            Title = $"Cold Start - you have rated {User.NumberOfMoviesRated + 1} out of 10 movies";
        }

        private void FindUnratedMovies(int numberOfMovies)
        {
            if (_firststart)
            {
                List<int> rateMoviesNumbers = new List<int>();
                int totalNumberOfMovies = MySqlCommands.NumberOfRowsInTable("imdbdata");

                rateMoviesNumbers.Clear();
                rateMoviesNumbers.AddRange(GenerateRandomNumber(totalNumberOfMovies, numberOfMovies, UsedNumbers));
                List<MovieMenuItem> MoviesColdStart = MySqlCommands.FindMovieFromID(rateMoviesNumbers);
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
                UpdateTitle();
            } while (_running && User.NumberOfMoviesRated < 10);

            if (User.NumberOfMoviesRated >= 10 && UsedNumbers.Count % 10 == 0) // makes sure that only one startpage is created, a bool could maybe be created with a adress pointer to each class instead?
            {
                Menu loggedInMenu = new Startmenu($"Welcome {User.Username}!");
                loggedInMenu.Start();
                UsedNumbers.Add(0);
            }
        }
    }
}