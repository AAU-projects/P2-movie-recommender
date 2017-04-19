using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Runtime.InteropServices;
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
            string firstName, lastName, userName, password;

            Console.Clear();
            do
            {
                Console.Clear();
                Console.Write("Firstname: ");
                firstName = Console.ReadLine();
                firstName = CultureInfo.CurrentCulture.TextInfo.ToTitleCase(firstName.ToLower());
            } while (!IsInputValid(firstName));

            do
            {
                Console.Clear();
                Console.Write("Lastname: ");
                lastName = Console.ReadLine();
                lastName = CultureInfo.CurrentCulture.TextInfo.ToTitleCase(lastName.ToLower());
            } while (!IsInputValid(lastName));

            do
            {
                do
                {
                    Console.Clear();
                    Console.Write("Username: ");
                    userName = Console.ReadLine();
                } while (!IsInputValid(userName));

                do
                {
                    Console.Clear();

                    Console.Write("Password: ");
                    password = Console.ReadLine();
                } while (!IsInputValid(password));


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
                        MySqlCommands.CreateUserTable(userName);
                    }
                    else
                    {
                        PrintStringColored("Failed to create user", ConsoleColor.Red);
                    }
                }
                Console.ReadLine();
            } while (!success);
        }

        public bool IsInputValid(string input)
        {
            if (input.Length >= 3 && input.Any(char.IsLetterOrDigit))
            {
                return true;
            }
            PrintStringColored("Input should be atleast 3 characters", ConsoleColor.Red);
            Console.WriteLine("\nPress any key to try again");
            Console.ReadKey();
            return false;
        }
    }
}
