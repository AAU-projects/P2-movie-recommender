using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using NUnit.Framework;

namespace RecommenderSystem
{
    class NUnitTesting
    {
        [TestCase("testuser", "testpassword", ExpectedResult = true)]
        [TestCase("thisusernameshouldneverbemade", "thispasswordshouldneverbegiven4545$£@{{€$", ExpectedResult = false)]
        [TestCase("", "", ExpectedResult = false)]
        public bool Login_ReceiveUsernameAndPasssword_returnsState(string username, string password)
        {
            return MySqlCommands.FindUser(username, password);
        }

        [TestCase("hey", ExpectedResult = true)]
        [TestCase("asdasdasdasdasdsadas", ExpectedResult = false)]
        [TestCase("", ExpectedResult = true)]
        public bool UsernameExists_ChecksIfUsernameExists_ReturnsStateIfUsernameExists(string username)
        {
            return MySqlCommands.UserExist(username);
        }

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
