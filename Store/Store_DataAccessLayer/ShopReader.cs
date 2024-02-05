using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using System.Data.SqlClient;
using System.Data;
using Newtonsoft.Json;

//namespace ConsoleApp1
//{
class ShopReader : IShopReader
{
    // Verbindungszeichenfolge zur lokalen SQL Server-Datenbank
    private string connectionString = "Data Source=(localdb)\\Local;Initial Catalog=ShopDB;Integrated Security=True;";
    private SqlConnection connection;

    public ShopReader()
    {
        connection = new SqlConnection(connectionString);
    }

    //SPEZIAL ABFRAGEN
    public string GetArticlesByOrderID(int orderID)
    {
        //List<Article> articleList = new List<Article>();
        List<dynamic> articleList = new List<dynamic>();

        try
        {
            connection.Open();

            SqlCommand command = new SqlCommand(
                "SELECT A.* " +
                "FROM Article A " +
                "JOIN OrderArticle OA ON A.id = OA.articleid " +
                "WHERE OA.orderid = @orderid", connection);
            command.Parameters.AddWithValue("@orderid", orderID);
            SqlDataReader reader = command.ExecuteReader();

            while (reader.Read())
            {
                dynamic article = new
                {
                    id = Convert.ToInt32(reader["id"]),
                    name = reader["name"].ToString(),
                    url = reader["url"].ToString(),
                    price = Convert.ToDecimal(reader["price"])
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
        //return articleList;
        return JsonConvert.SerializeObject(articleList, Formatting.Indented);
    }

    //EINZEL ABFRAGEN
    public string ReadArticleByID(int articleID)
    {
        dynamic article = null;

        try
        {
            connection.Open();

            SqlCommand command = new SqlCommand("SELECT * FROM Article WHERE id = @articleid", connection);
            command.Parameters.AddWithValue("@articleid", articleID);
            SqlDataReader reader = command.ExecuteReader();

            if (reader.Read())
            {
                article = new
                {
                    id = Convert.ToInt32(reader["id"]),
                    name = reader["name"].ToString(),
                    url = reader["url"].ToString(),
                    price = Convert.ToDecimal(reader["price"])
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
        //return article;
        return JsonConvert.SerializeObject(article, Formatting.Indented);
    }

    // LISTEN ABFRAGEN
    public string ReadAllCustomer()
    {
        //List<Customer> customerList = new List<Customer>();
        List<dynamic> customerList = new List<dynamic>();

        try
        {
            connection.Open();

            SqlCommand command = new SqlCommand("SELECT * FROM Customer", connection);
            SqlDataReader reader = command.ExecuteReader();

            while (reader.Read())
            {
                dynamic customer = new
                {
                    id = Convert.ToInt32(reader["id"]),
                    name = reader["name"].ToString(),
                    password = reader["password"].ToString(),
                    email = reader["email"].ToString(),
                    orders = "" //reader["bestellungen"].ToString()
                };
                customerList.Add(customer);
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
        //return customerList;
        //return new List<Customer>();
        return JsonConvert.SerializeObject(customerList, Formatting.Indented);
    }

    public string ReadAllArticles()
    {
        //List<Article> articleList = new List<Article>();
        List<dynamic> articleList = new List<dynamic>();

        try
        {
            connection.Open();

            SqlCommand command = new SqlCommand("SELECT * FROM Article", connection);
            SqlDataReader reader = command.ExecuteReader();

            while (reader.Read())
            {
                dynamic article = new
                {
                    id = Convert.ToInt32(reader["id"]),
                    name = reader["name"].ToString(),
                    url = reader["url"].ToString(),
                    price = Convert.ToDecimal(reader["price"])
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
        //return articleList;
        return JsonConvert.SerializeObject(articleList, Formatting.Indented);
    }

    public string ReadAllOrders()
    {
        //List<Order> orderList = new List<Order>();
        List<dynamic> orderList = new List<dynamic>();
        List<dynamic> orderListReturn = new List<dynamic>();

        try
        {
            connection.Open();

            SqlCommand command = new SqlCommand("SELECT * FROM Orders", connection);
            SqlDataReader reader = command.ExecuteReader();

            while (reader.Read())
            {
                int orderId = Convert.ToInt32(reader["id"]);
                //List<Article> articleList = new List<Article>(); 
                //List<dynamic> articleList = new List<dynamic>();

                //dynamic articleList = JsonConvert.DeserializeObject(GetArticlesByOrderID(orderId));

                dynamic order = new
                {
                    id = orderId,
                    //articleList = articleList, // GetArticlesByOrderID(orderId), //dynamic list
                    payd = Convert.ToBoolean(reader["payd"]),
                    totalPrice = Convert.ToDecimal(reader["totalPrice"])
                };
                orderList.Add(order);
            }
            connection.Close();

            foreach (var order in orderList)
            {
                int orderId = order.id;
                dynamic articleListForOrder = JsonConvert.DeserializeObject(GetArticlesByOrderID(orderId));
                dynamic orderReturn = new
                {
                    id = order.id,
                    articleList = articleListForOrder, // GetArticlesByOrderID(orderId), //dynamic list
                    payd = order.payd,
                    totalPrice = order.totalPrice
                };
                orderListReturn.Add(orderReturn);
                //order.articleList = articleListForOrder;
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
        //return orderList;
        return JsonConvert.SerializeObject(orderListReturn, Formatting.Indented);
    }

    public string Login(string email)
    {
        dynamic customer = null;
        string customerJson = "{}";
        try
        {
            connection.Open();

            SqlCommand command = new SqlCommand("SELECT * FROM Customer WHERE email = @email", connection);
            command.Parameters.AddWithValue("@email", email);

            SqlDataReader reader = command.ExecuteReader();

            while (reader.Read())
            {
                customer = new
                {
                    id = Convert.ToInt32(reader["id"]),
                    password = reader["password"].ToString(),
                    email = reader["email"].ToString()
                };
            }
            connection.Close();

            customerJson = JsonConvert.SerializeObject(customer, Formatting.Indented);
        }
        catch (Exception e)
        {
            Console.WriteLine("Fehler beim Verbindungsaufbau: " + e.Message);
            if (connection != null && connection.State == ConnectionState.Open)
            {
                connection.Close();
            }
            return "{}";
        }
        return customerJson; // JsonConvert.SerializeObject(customerList, Formatting.Indented);
    }

    public string getOrder(int customerId)
    {
        try
        {
            int orderId = GetOrderForCustomer(customerId);
            if (orderId == -1)
            {
                return "{}";
            }
            List<dynamic> orderList = new List<dynamic>();

            connection.Open();
            SqlCommand command = new SqlCommand("SELECT articleid, articleAmount FROM OrderArticle WHERE orderid = @orderid", connection);
            command.Parameters.AddWithValue("@orderid", orderId);
            SqlDataReader reader = command.ExecuteReader();

            while (reader.Read())
            {
                dynamic orderItem = new
                {
                    id = Convert.ToInt32(reader["articleid"]),
                    amount = Convert.ToInt32(reader["articleAmount"])
                };

                orderList.Add(orderItem);
            }
            connection.Close();

            return JsonConvert.SerializeObject(orderList, Formatting.Indented);
        }
        catch (Exception e)
        {
            Console.WriteLine("Fehler beim Abrufen der Bestellung für den Kunden: " + e.Message);
            if (connection != null && connection.State == ConnectionState.Open)
            {
                connection.Close();
            }
            return "{}";
        }
    }

    // Hilfsfunktionen
    private int GetOrderForCustomer(int customerId)
    {
        int orderId = -1;

        try
        {
            connection.Open();
            SqlCommand command = new SqlCommand("SELECT TOP 1 orderid FROM CustomerOrder WHERE customerid = @customerid", connection);
            command.Parameters.AddWithValue("@customerid", customerId);
            object result = command.ExecuteScalar();

            if (result != null)
            {
                orderId = Convert.ToInt32(result);
            }
            connection.Close();
            return orderId;
        }
        catch (Exception e)
        {
            Console.WriteLine("Fehler beim Abrufen der Bestellung für den Kunden: " + e.Message);
            if (connection != null && connection.State == ConnectionState.Open)
            {
                connection.Close();
            }
            return -1;
        }
    }
}