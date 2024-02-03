using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using System.Data.SqlClient;

class DatabaseTableCreator : IDatabaseTableCreator
{
    private static string CONNECTION_STRING = "Data Source=DESKTOP-ODAGOGJ\\SQLEXPRESS;Initial Catalog=ShopDB;User ID=shopUser;Password=123456789;";

    public int CreateTables()
    {
        try
        {
            using (SqlConnection connection = new SqlConnection(CONNECTION_STRING))
            {
                connection.Open();

                // Tabelle 'Article' erstellen
                if (!TableExists(connection, "Article"))
                {
                    CreateArticleTable(connection);
                }

                // Tabelle 'Order' erstellen
                if (!TableExists(connection, "Orders"))
                {
                    CreateOrderTable(connection);
                }

                // Tabelle 'Customer' erstellen
                if (!TableExists(connection, "Customer"))
                {
                    CreateCustomerTable(connection);
                }

                // Tabelle 'OrderArticle' erstellen
                if (!TableExists(connection, "OrderArticle"))
                {
                    CreateOrderArticleTable(connection);
                }

                connection.Close();
                Console.WriteLine("Tabellen erfolgreich erstellt.");
            }
            return 1;
        }
        catch (Exception e)
        {
            Console.WriteLine("Fehler beim erstellen der Tabellen: " + e.Message);
            return 0;
        }
    }

    private void CreateArticleTable(SqlConnection connection)
    {
        string createTableQuery = @"
        CREATE TABLE Article (
            id INT PRIMARY KEY IDENTITY(1,1) NOT NULL,
            name NVARCHAR(255),
            price DECIMAL(10,2)
        )";

        ExecuteNonQuery(connection, createTableQuery);
    }

    private void CreateOrderTable(SqlConnection connection)
    {
        string createTableQuery = @"
        CREATE TABLE Orders (
            id INT PRIMARY KEY IDENTITY(1,1) NOT NULL,
            payd BIT,
            totalPrice DECIMAL(10,2)
        )";

        ExecuteNonQuery(connection, createTableQuery);
    }

    private void CreateCustomerTable(SqlConnection connection)
    {
        string createTableQuery = @"
        CREATE TABLE Customer (
            id INT PRIMARY KEY IDENTITY(1,1) NOT NULL,
            name NVARCHAR(255),
            password NVARCHAR(255),
            email NVARCHAR(255)
        )";

        ExecuteNonQuery(connection, createTableQuery);
    }

    private void CreateOrderArticleTable(SqlConnection connection)
    {
        string createTableQuery = @"
        CREATE TABLE OrderArticle (
            orderid INT REFERENCES Orders(id) NOT NULL,
            articleid INT REFERENCES Article(id) NOT NULL
        )";

        ExecuteNonQuery(connection, createTableQuery);
    }

    private void ExecuteNonQuery(SqlConnection connection, string query)
    {
        using (SqlCommand command = new SqlCommand(query, connection))
        {
            command.ExecuteNonQuery();
        }
    }
    private bool TableExists(SqlConnection connection, string tableName)
    {
        string query = $"SELECT COUNT(*) FROM INFORMATION_SCHEMA.TABLES WHERE TABLE_NAME = '{tableName}'";
        using (SqlCommand command = new SqlCommand(query, connection))
        {
            int tableCount = Convert.ToInt32(command.ExecuteScalar());
            return tableCount > 0;
        }
    }
}
