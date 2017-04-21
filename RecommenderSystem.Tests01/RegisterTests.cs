using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using NUnit.Framework;

namespace RecommenderSystem.Tests
{
    class RegisterTests
    {
        [TestCase("lo", ExpectedResult = false)]
        [TestCase("", ExpectedResult = false)]
        [TestCase("!!fuck", ExpectedResult = false)]
        [TestCase("lo23232#", ExpectedResult = false)]
        [TestCase("hehell433", ExpectedResult = true)]
        [TestCase("lggdsado", ExpectedResult = true)]
        [TestCase("asd", ExpectedResult = true)]
        [TestCase("sad4", ExpectedResult = true)]
        public bool ValidInputForUserRegister_GivesAstring_ReturnsIfInputIsValid(string input)
        {
            Register registerTest = new Register();
            return registerTest.IsInputValid(input);
        }
    }
}
