using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RecommenderSystem
{
    class ColdStart : Menu
    {
        public ColdStart(string title) : base(title)
        {
        }

        public override void Select()
        {
            Console.Clear();
            Console.WriteLine($"Choose more than 10 Movies \nPress enter to continue");
            Console.ReadLine();
            Console.Clear();

            FindUnratedMovies(10); 
            
        }

        private void FindUnratedMovies(int numberOfMovies)
        {
            List<int> usedNumbers = new List<int>();
            int totalNumberOfMovies = MySqlCommands.NumberOfRowsInTable("imdbdata");
           
            do
            {
                usedNumbers = GenerateRandomNumber(totalNumberOfMovies, numberOfMovies, usedNumbers);
                
            } while (true);

        }

        private List<int> GenerateRandomNumber(int totalNumberOfMovies, int numberOfGeneratedNumbers, List<int> usedNumbers)
        {
            Random random = new Random();
            int number = 0;

            for (int i = 0; i < numberOfGeneratedNumbers; i++)
            {
                do
                {
                    number = random.Next(1, totalNumberOfMovies);
                } while (usedNumbers.Contains(number));

                usedNumbers.Add(number);
            }
            return usedNumbers; 
        }
    }
}
