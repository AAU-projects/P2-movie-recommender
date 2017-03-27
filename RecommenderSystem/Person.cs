using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RecommenderSystem
{
    abstract class Person
    {
        public Person(string firstName, string lastName )
        {
            _firstName = firstName;
            _lastName = lastName;
        }

        private string _firstName;
        private string _lastName;

        public virtual string Name => _firstName + " " + _lastName;
    }
}
