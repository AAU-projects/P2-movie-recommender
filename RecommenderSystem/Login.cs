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
                Console.WriteLine("You are now logged in");
            else
                Console.WriteLine("Wrong password or username");

            Console.ReadLine();
        }
    }
}
