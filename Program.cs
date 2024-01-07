using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using System.Data.SqlClient;
using System.Collections.Generic;

    class Program
    {
        static void Main(string[] args)
        {
            IShopReader shopReaderObj = new ShopReader();

            List<Customer> customerList = shopReaderObj.ReadAllCustomer();

            foreach (var customer in customerList)
            {
                Console.WriteLine($"ID: {customer.ID}, Name: {customer.Name}, Email: {customer.Email}, Bestellungen: {customer.Bestellungen}");
            }

            /*
            // Verbindungszeichenfolge zur lokalen SQL Server-Datenbank
            string connectionString = "Data Source=DESKTOP-ODAGOGJ\\SQLEXPRESS;Initial Catalog=ShopDB;User ID=shopUser;Password=123456789;";

            // SQL-Abfrage (optional)
            string query = "SELECT * FROM kunden";

            // Verbindung erstellen
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                try
                {
                    // Verbindung öffnen
                    connection.Open();
                    
                    // Jetzt können Sie SQL-Abfragen ausführen, z.B. mit SqlCommand
                    using (SqlCommand command = new SqlCommand(query, connection))
                    {
                        Console.WriteLine(command);
                        // Beispiel: Ausführen einer Abfrage und Lesen der Ergebnisse
                        SqlDataReader reader = command.ExecuteReader();
                        Console.WriteLine(reader);
                        while (reader.Read())
                        {
                            // Hier können Sie auf die Daten zugreifen, z.B. reader["Spaltenname"]
                            Console.WriteLine(reader["name"]);
                        }
                    }
                    
                    Console.WriteLine("Connection open");
                }
                catch (Exception ex)
                {
                    Console.WriteLine("Fehler beim Verbindungsaufbau: " + ex.Message);
                }
            }
            */
        }
    }
