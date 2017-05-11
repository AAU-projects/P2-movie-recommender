using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace RecommenderSystem
{
    class Register : MenuItemBase
    {
        public Register() : base("Create New User")
        {
        }

        public override void Select()
        {
            bool success = false;
            string firstname, lastname, username, password;

            do
            {
                Console.Clear();
                Console.Write("Firstname: ");
                firstname = Console.ReadLine();
                firstname = CultureInfo.CurrentCulture.TextInfo.ToTitleCase(firstname.ToLower());
            } while (!IsInputValid(firstname));

            do
            {
                Console.Clear();
                Console.Write("Lastname: ");
                lastname = Console.ReadLine();
                lastname = CultureInfo.CurrentCulture.TextInfo.ToTitleCase(lastname.ToLower());
            } while (!IsInputValid(lastname));

            do
            {
                do
                {
                    Console.Clear();
                    Console.Write("Username: ");
                    username = Console.ReadLine();
                } while (!IsInputValid(username));

                do
                {
                    Console.Clear();
                    Console.Write("Password: ");
                    password = Console.ReadLine();
                } while (!IsInputValid(password));

                if (MySqlCommands.UserExist(username))
                {
                    PrintStringColored("\nUsername is already taken!", ConsoleColor.Red);
                }
                else
                {
                    success = MySqlCommands.CreateNewUser(firstname, lastname, username, password);
                    if (success)
                    {
                        PrintStringColored("\nUser was successfully created", ConsoleColor.Green);
                        MySqlCommands.CreateUserTable(username);
                    }
                    else
                    {
                        PrintStringColored("\nFailed to create user", ConsoleColor.Red);
                    }
                }
                Console.ReadKey();
            } while (!success);
        }

        public bool IsInputValid(string input)
        {
            if (input.Length >= 3 && Regex.IsMatch(input, @"^[a-zA-Z0-9]+$"))
            {
                return true;
            }

            PrintStringColored("Input should be atleast 3 characters", ConsoleColor.Red);
            Console.WriteLine("\nPress any key to try again");

            return false;
        }
    }
}
