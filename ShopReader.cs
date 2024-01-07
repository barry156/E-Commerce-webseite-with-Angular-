using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using System.Data.SqlClient;
using System.Data;
using System.Collections.Generic;

//namespace ConsoleApp1
//{
class ShopReader : IShopReader{
    // Verbindungszeichenfolge zur lokalen SQL Server-Datenbank
    private string connectionString = "Data Source=DESKTOP-ODAGOGJ\\SQLEXPRESS;Initial Catalog=ShopDB;User ID=shopUser;Password=123456789;";
    private SqlConnection connection;

    public ShopReader() {
        connection = new SqlConnection(connectionString);
    }

    public List<Customer> ReadAllCustomer() {
        List<Customer> customerList = new List<Customer>();

        try {
            connection.Open();

            SqlCommand command      = new SqlCommand("SELECT * FROM kunden", connection);
            SqlDataReader reader    = command.ExecuteReader();

            while (reader.Read())
            {
                Customer customer = new Customer
                {
                    ID              = Convert.ToInt32(reader["id"]),
                    Name            = reader["name"].ToString(),
                    Password        = reader["password"].ToString(),
                    Email           = reader["email"].ToString(),
                    Bestellungen    = reader["bestellungen"].ToString()
                };
                customerList.Add(customer);
            }
            connection.Close();
        }
        catch (Exception e) {
            Console.WriteLine("Fehler beim Verbindungsaufbau: " + e.Message);
            if (connection != null && connection.State == ConnectionState.Open) {
                connection.Close();
            }
        }
        return customerList;
        //return new List<Customer>();
    }
}