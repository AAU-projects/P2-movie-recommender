using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RecommenderSystem
{
    class Actor : Person
    {
        public Actor(string firstName, string lastName) : base(firstName, lastName)
        {
        }

        public List<Movie> MoviesAttended = new List<Movie>();
    }
}
