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
            string userName = Console.ReadLine();
            Console.Write("Password: ");
            string password = Console.ReadLine();
            Console.WriteLine();

            bool success = MySqlCommands.FindUser(userName, password);

            if (success)
            {
                PrintStringColored("You are now logged in", ConsoleColor.Green);
                Console.WriteLine("Press any key to continue");
                Console.ReadKey();
                Menu LoggedInMenu = new Menu($"Welcome {userName}!", new MovieMenu("View all movies"), new MovieMenu("Rate movies"), new ColdStart("Cold start", new List<int>()));
                LoggedInMenu.Start();
            }
            else
                PrintStringColored("Wrong password or username", ConsoleColor.Red);

            Console.ReadLine();
        }
    }
}
