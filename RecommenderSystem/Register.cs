using System;
using System.Collections.Generic;
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

            do
            {
                Console.Clear();
                do
                {
                    Console.Clear();
                    Console.Write("Firstname: ");
                    firstName = Console.ReadLine();
                } while (!IsInputValid(firstName));

                do
                {
                    Console.Clear();
                    Console.Write("Lastname: ");
                    lastName = Console.ReadLine();
                } while (!IsInputValid(lastName));

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
                    Console.WriteLine("Username is already taken!");
                }
                else
                {
                    success = MySqlCommands.CreateNewUser(firstName, lastName, userName, password);
                    if (success)
                    {
                        Console.WriteLine("User was successfully created");
                    }
                    else
                    {
                        Console.WriteLine("Failed to create user");
                    }
                }
                Console.ReadLine();
            } while (!success);
        }

        public bool IsInputValid(string input)
        {
            if (input.Length < 3)
            {
                return false;
            }
            if (input.Any(char.IsLetterOrDigit) && input.Length >= 3)
            {
                return true;
            }
            Console.WriteLine("Input should be atleast 3 characters");
            Console.WriteLine("Press any key to try again");
            Console.ReadKey();
            return false;
        }
    }
}
