using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RecommenderSystem
{
    class Program
    {
        static void Main(string[] args)
        {
            MySql.Data.MySqlClient.MySqlConnection conn;
            string myConnectionString = "server=127.0.0.1;uid=program;pwd=123;database=recommender_system;";

            try
            {
                conn = new MySql.Data.MySqlClient.MySqlConnection {ConnectionString = myConnectionString};
                conn.Open();
            }
            catch (MySql.Data.MySqlClient.MySqlException ex)
            {
                Console.WriteLine(ex.Message);
            }

            Menu menu = new Menu("Recommender System : Login", new Login());
            Console.Beep();

            menu.Start();
        }
    }
}
