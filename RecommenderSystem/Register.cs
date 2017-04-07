using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RecommenderSystem
{
    class Register : MenuItemBase
    {
        // Gør så firstname og lastname altid er med stort forbogstav
        public Register() : base("Create New User")
        {
        }

        public override void Select()
        {
            bool success = false;
            do
            {
                Console.Clear();
                Console.Write("Firstname: ");
                string firstName = Console.ReadLine();
                Console.Write("Lastname: ");
                string lastName = Console.ReadLine();
                Console.Write("Username: ");
                string userName = Console.ReadLine();
                Console.Write("Password: ");
                string password = Console.ReadLine();
                Console.WriteLine();

                if (MySqlCommands.UserExist(userName))
                {
                    PrintStringColored("Username is already taken!", ConsoleColor.Red);
                }
                else
                {
                    success = MySqlCommands.CreateNewUser(firstName, lastName, userName, password);
                    if (success)
                    {
                        PrintStringColored("User was successfully created", ConsoleColor.Green);
                    }
                    else
                    {
                        PrintStringColored("Failed to create user", ConsoleColor.Red);
                    }
                }

            } while (!success);

            Console.ReadLine();
        }
    }
}
