using NUnit.Framework;
using RecommenderSystem;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RecommenderSystem.Tests
{
    [TestFixture()]
    public class ColdStartTests
    {
        [TestCase(9, ExpectedResult = false)]
        [TestCase(0, ExpectedResult = false)]
        [TestCase(22, ExpectedResult = true)]
        [TestCase(10, ExpectedResult = true)]
        public bool Select_IfNumberOfMoviesRatedForTheUserIsAboveOrEqualToTen_StopMenu(int numberOfMoviesRated)
        {
            ColdStart coldstart = new ColdStart("");
            User.NumberOfMoviesRated = numberOfMoviesRated;
            coldstart.UnitTest = true;
            coldstart.Select();
            return coldstart.Firststart;
        }

        [TestCase(1, ExpectedResult = 10)]
        [TestCase(2, ExpectedResult = 20)]
        [TestCase(3, ExpectedResult = 30)]
        [TestCase(4, ExpectedResult = 40)]
        public int Select_GeneratedMoviesPerNextPage_NumberOfMovies(int page)
        {
            List<int> list = new List<int>();
            ColdStart nextPage = new ColdStart("", list);

            for (int i = 0; i < page; i++)
            {
                nextPage = new ColdStart("", list);
                nextPage.UnitTest = true;
                nextPage.Select();
            }

            return nextPage.UsedNumbers.Count;
        }
    }
}