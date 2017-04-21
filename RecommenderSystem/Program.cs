using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MySql.Data.MySqlClient;

namespace RecommenderSystem
{
    class Program
    {
        static void Main(string[] args)
        {
            Menu menu = new Menu("Recommender System : Start", new Login(), new Register());
            menu.Start();
        }

        

    }
}
