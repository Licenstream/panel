using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Threading.Tasks;
using System.Xml.Linq;
using Domain;
using Domain.Interfaces;
using MySqlConnector;

namespace Infrastructure
{
    public class DatabaseHandler
    {
        public class DatabaseConnection
        {
            private static string connectionString = "server=localhost;database=Panel;user=root;password=;";

            public static MySqlConnection GetConnection()
            {
                return new MySqlConnection(connectionString);
            }

            public static bool Licence()
            {
                MySqlConnection connection = null;

                try
                {
                    connection = GetConnection();
                    connection.Open();
                    return true;
                }
                catch (Exception)
                {
                    return false;
                }
                finally
                {
                    if (connection != null)
                    {
                        connection.Close();
                    }
                }
            }
        }
    }
}
