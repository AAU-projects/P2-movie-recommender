using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RecommenderSystem
{
    class Movie
    {
        public string Title { get; private set;}
        public DateTime ReleaseDate;
        public int Duration;
        public string Resume;
        public List<Director> Directors;
        public List<Actor> Actors;
    }
}
