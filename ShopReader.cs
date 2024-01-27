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

    //SPEZIAL ABFRAGEN
    public List<Article> GetArticlesByOrderID(int orderID)
    {
        List<Article> articleList = new List<Article>();

        try
        {
            connection.Open();

            SqlCommand command = new SqlCommand(
                "SELECT A.* " +
                "FROM Artikel A " +
                "JOIN BestellungsArtikel BA ON A.ID = BA.ArtikelID " +
                "WHERE BA.BestellungsID = @OrderID", connection);
            command.Parameters.AddWithValue("@OrderID", orderID);
            SqlDataReader reader = command.ExecuteReader();

            while (reader.Read())
            {
                Article article = new Article
                {
                    ID = Convert.ToInt32(reader["ID"]),
                    Name = reader["Name"].ToString(),
                    Preis = Convert.ToDecimal(reader["Preis"])
                };

                articleList.Add(article);
            }
            connection.Close();
        }
        catch (Exception e)
        {
            Console.WriteLine("Fehler beim Verbindungsaufbau: " + e.Message);
            if (connection != null && connection.State == ConnectionState.Open)
            {
                connection.Close();
            }
        }
        return articleList;
    }

    //EINZEL ABFRAGEN
    public Article ReadArticleByID(int articleID)
    {
        Article article = null;

        try
        {
            connection.Open();

            SqlCommand command      = new SqlCommand("SELECT * FROM Artikel WHERE ID = @ArticleID", connection);
            command.Parameters.AddWithValue("@ArticleID", articleID);
            SqlDataReader reader    = command.ExecuteReader();

            if (reader.Read())
            {
                article = new Article
                {
                    ID = Convert.ToInt32(reader["ID"]),
                    Name = reader["Name"].ToString(),
                    Preis = Convert.ToDecimal(reader["Preis"])
                };
            }

            connection.Close();
        }
        catch (Exception e)
        {
            Console.WriteLine("Fehler beim Verbindungsaufbau: " + e.Message);
            if (connection != null && connection.State == ConnectionState.Open)
            {
                connection.Close();
            }
        }
        return article;
    }

    // LISTEN ABFRAGEN
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

    public List<Article> ReadAllArticles()
    {
        List<Article> articleList = new List<Article>();

        try
        {
            connection.Open();

            SqlCommand command      = new SqlCommand("SELECT * FROM Artikel", connection);
            SqlDataReader reader    = command.ExecuteReader();

            while (reader.Read())
            {
                Article article = new Article
                {
                    ID      = Convert.ToInt32(reader["ID"]),
                    Name    = reader["Name"].ToString(),
                    Preis   = Convert.ToDecimal(reader["Preis"])
                };
                articleList.Add(article);
            }
            connection.Close();
        }
        catch (Exception e)
        {
            Console.WriteLine("Fehler beim Verbindungsaufbau: " + e.Message);
            if (connection != null && connection.State == ConnectionState.Open)
            {
                connection.Close();
            }
        }
        return articleList;
    }

    public List<Order> ReadAllOrders()
    {
        List<Order> orderList = new List<Order>();

        try
        {
            connection.Open();
            
            SqlCommand command      = new SqlCommand("SELECT * FROM Bestellungen", connection);
            SqlDataReader reader    = command.ExecuteReader();

            while (reader.Read())
            {
                int orderId = Convert.ToInt32(reader["ID"]);
                List<Article> articleList = new List<Article>(); 

                Order order = new Order
                {
                    ID = orderId,
                    ArticleList = articleList,
                    Payd = Convert.ToBoolean(reader["Bezahlt"]),
                    TotalPrice = Convert.ToDecimal(reader["Gesamtpreis"])
                };
                orderList.Add(order);
            }
            connection.Close();

            foreach (var order in orderList) {
                int orderId = order.ID;
                List<Article> articleListForOrder = GetArticlesByOrderID(orderId);
                order.ArticleList = articleListForOrder;
            }

        }
        catch (Exception e)
        {
            Console.WriteLine("Fehler beim Verbindungsaufbau: " + e.Message);
            if (connection != null && connection.State == ConnectionState.Open)
            {
                connection.Close();
            }
        }
        return orderList;
    }

}