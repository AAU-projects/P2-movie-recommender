using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RecommenderSystem
{
    class Login : MenuItemBase
    {
        public Login() : base("Login")
        {
        }

        public override void Select()
        {
            Console.Clear();

            Console.Write("Username: ");
            string[] username = Console.ReadLine().Split(' ');

            Console.Write("Password: ");
            string password = Console.ReadLine();

            bool success = MySqlCommands.FindUser(username[0], password);
            bool debug = checkForDebug(username);
            bool coldstart = checkforColdStart(username);

            if (success)
            {
                PrintStringColored("\nYou are now logged in", ConsoleColor.Green);
                Console.WriteLine("Press any key to continue");
                Console.ReadKey();

                new User(username[0], debug);

                if (User.NumberOfMoviesRated < 10 && !coldstart)
                {
                    ColdStart coldStartMenu = new ColdStart($"Cold Start - you have rated {User.NumberOfMoviesRated} out of 10 movies");
                    coldStartMenu.Select();
                }
                else
                {
                    Menu loggedInMenu = new Startmenu($"Welcome {User.Username}!");
                    loggedInMenu.Start();
                }
            }
            else
            {
                PrintStringColored("\nWrong password or username", ConsoleColor.Red);
                Console.ReadKey();
            }
        }

        private bool checkforColdStart(string[] username)
        {
            foreach (var parameter in username)
            {
                if (parameter == "-c")
                {
                    return true;
                }
            }
            return false;
        }

        private bool checkForDebug(string[] username)
        {
            foreach (var parameter in username)
            {
                if (parameter == "-d")
                {
                    return true;
                }
            }
            return false;
        }
    }
}
