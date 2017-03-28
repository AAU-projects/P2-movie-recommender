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

            bool success = MySqlCommands.CreateNewUser(firstName, lastName, userName, password);

            if (success)
                Console.WriteLine("Success!!!!!!!!");
            else
                Console.WriteLine("Failed!!!");

            Console.ReadLine();
        }
    }
}
