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
class ShopReader : IShopReader{
    // Verbindungszeichenfolge zur lokalen SQL Server-Datenbank
    private string connectionString = "Data Source=DESKTOP-ODAGOGJ\\SQLEXPRESS;Initial Catalog=ShopDB;User ID=shopUser;Password=123456789;";
    private SqlConnection connection;

    public ShopReader() {
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
                "FROM Artikel A " +
                "JOIN BestellungsArtikel BA ON A.ID = BA.ArtikelID " +
                "WHERE BA.BestellungsID = @OrderID", connection);
            command.Parameters.AddWithValue("@OrderID", orderID);
            SqlDataReader reader = command.ExecuteReader();

            while (reader.Read())
            {
                dynamic article = new
                {
                    id = Convert.ToInt32(reader["ID"]),
                    name = reader["Name"].ToString(),
                    price = Convert.ToDecimal(reader["Preis"])
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

            SqlCommand command      = new SqlCommand("SELECT * FROM Artikel WHERE ID = @ArticleID", connection);
            command.Parameters.AddWithValue("@ArticleID", articleID);
            SqlDataReader reader    = command.ExecuteReader();

            if (reader.Read())
            {
                article = new
                {
                    id = Convert.ToInt32(reader["ID"]),
                    name = reader["Name"].ToString(),
                    price = Convert.ToDecimal(reader["Preis"])
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
    public string ReadAllCustomer() {
        //List<Customer> customerList = new List<Customer>();
        List<dynamic> customerList = new List<dynamic>();

        try {
            connection.Open();

            SqlCommand command      = new SqlCommand("SELECT * FROM kunden", connection);
            SqlDataReader reader    = command.ExecuteReader();

            while (reader.Read())
            {
                dynamic customer = new
                {
                    id              = Convert.ToInt32(reader["id"]),
                    name            = reader["name"].ToString(),
                    password        = reader["password"].ToString(),
                    email           = reader["email"].ToString(),
                    orders          = reader["bestellungen"].ToString()
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

            SqlCommand command      = new SqlCommand("SELECT * FROM Artikel", connection);
            SqlDataReader reader    = command.ExecuteReader();

            while (reader.Read())
            {
                dynamic article = new
                {
                    id      = Convert.ToInt32(reader["ID"]),
                    name    = reader["Name"].ToString(),
                    price   = Convert.ToDecimal(reader["Preis"])
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
            
            SqlCommand command      = new SqlCommand("SELECT * FROM Bestellungen", connection);
            SqlDataReader reader    = command.ExecuteReader();

            while (reader.Read())
            {
                int orderId = Convert.ToInt32(reader["ID"]);
                //List<Article> articleList = new List<Article>(); 
                //List<dynamic> articleList = new List<dynamic>();

                //dynamic articleList = JsonConvert.DeserializeObject(GetArticlesByOrderID(orderId));

                dynamic order = new
                {
                    id = orderId,
                    //articleList = articleList, // GetArticlesByOrderID(orderId), //dynamic list
                    payd = Convert.ToBoolean(reader["Bezahlt"]),
                    totalPrice = Convert.ToDecimal(reader["Gesamtpreis"])
                };
                orderList.Add(order);
            }
            connection.Close();

            foreach (var order in orderList) {
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

}