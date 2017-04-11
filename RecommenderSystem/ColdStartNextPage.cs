using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RecommenderSystem
{
    class ColdStartNextPage : ColdStart
    {

        public ColdStartNextPage(string title, List<int> usedNumbers) : base(title)
        {
            _usedNumbers = usedNumbers;
        }

        public override void FindUnratedMovies(int numberOfMovies)
        {
            base.FindUnratedMovies(numberOfMovies);
        }
    }
}
