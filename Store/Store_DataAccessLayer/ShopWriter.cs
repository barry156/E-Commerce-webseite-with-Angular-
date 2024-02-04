using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using System.Data.SqlClient;
using System.Data;
using Newtonsoft.Json;

class ShopWriter : IShopWriter
{
    private string CONNECTION_STRING = "Data Source=DESKTOP-ODAGOGJ\\SQLEXPRESS;Initial Catalog=ShopDB;User ID=shopUser;Password=123456789;";
    private SqlConnection connection;

    public ShopWriter()
    {
        connection = new SqlConnection(CONNECTION_STRING);
    }

    // Artikel
    public int AddArticle(string articleJson)
    {
        try
        {
            dynamic articleData = JsonConvert.DeserializeObject(articleJson);

            connection.Open();

            SqlCommand command = new SqlCommand("INSERT INTO Article (id, name, url, price) VALUES (@id, @name, @url, @price)", connection);
            /*
            command.Parameters.AddWithValue("@ID", articleData.id);
            command.Parameters.AddWithValue("@Name", articleData.name);
            command.Parameters.AddWithValue("@Preis", articleData.price);
            */

            command.Parameters.AddWithValue("@id", Convert.ToInt32(articleData.id));
            command.Parameters.AddWithValue("@name", articleData.name.ToString());
            command.Parameters.AddWithValue("@url", articleData.url.ToString());
            command.Parameters.AddWithValue("@price", Convert.ToDouble(articleData.price));

            command.ExecuteNonQuery();
            Console.WriteLine($"Artikel mit ID {articleData.id} erfolgreich angelegt.");

            connection.Close();
        }
        catch (Exception e)
        {
            Console.WriteLine("Fehler beim Hinzufügen des Artikels: " + e.Message);
            if (connection != null && connection.State == ConnectionState.Open)
            {
                connection.Close();
            }
            return 0;
        }
        return 1;
    }

    public int DeleteArticle(int articleID)
    {
        int rowsAffected = -1;
        try
        {
            connection.Open();

            SqlCommand command = new SqlCommand("DELETE FROM Article WHERE id = @articleid", connection);
            command.Parameters.AddWithValue("@articleid", articleID);
            rowsAffected = command.ExecuteNonQuery();

            connection.Close();

            if (rowsAffected > 0)
            {
                Console.WriteLine($"Artikel mit ID {articleID} erfolgreich gelöscht.");
            }
            else
            {
                Console.WriteLine($"Artikel mit ID {articleID} nicht gefunden oder konnte nicht gelöscht werden.");
            }
            return rowsAffected;
        }
        catch (Exception e)
        {
            Console.WriteLine("Fehler beim Löschen des Artikels: " + e.Message);
            if (connection != null && connection.State == ConnectionState.Open)
            {
                connection.Close();
            }
            return rowsAffected;
        }
    }

    // Kunde
    public int AddCustomer(string customerJson)
    {
        try
        {
            dynamic customerData = JsonConvert.DeserializeObject(customerJson);
            connection.Open();

            SqlCommand command = new SqlCommand("INSERT INTO Customer (name, id, password, email) VALUES (@name, @id, @password, @email)", connection);
            command.Parameters.AddWithValue("@id", Convert.ToInt32(customerData.id));
            command.Parameters.AddWithValue("@name", customerData.name.ToString());
            command.Parameters.AddWithValue("@password", customerData.password.ToString());
            command.Parameters.AddWithValue("@email", customerData.email.ToString());

            command.ExecuteNonQuery();
            Console.WriteLine($"Kunde mit ID {customerData.id} erfolgreich angelegt.");

            connection.Close();
        }
        catch (Exception e)
        {
            Console.WriteLine("Fehler beim Hinzufügen des Kunden: " + e.Message);
            if (connection != null && connection.State == ConnectionState.Open)
            {
                connection.Close();
            }
            return 0;
        }
        return 1;
    }

    public int DeleteCustomer(int customerId)
    {
        int rowsAffected = -1;
        try
        {
            connection.Open();

            SqlCommand command = new SqlCommand("DELETE FROM Customer WHERE id = @customerid", connection);
            command.Parameters.AddWithValue("@customerid", customerId);
            rowsAffected = command.ExecuteNonQuery();

            connection.Close();

            if (rowsAffected > 0)
            {
                Console.WriteLine($"Kunde mit ID {customerId} erfolgreich gelöscht.");
            }
            else
            {
                Console.WriteLine($"Kunde mit ID {customerId} nicht gefunden oder konnte nicht gelöscht werden.");
            }
            return rowsAffected;
        }
        catch (Exception e)
        {
            Console.WriteLine("Fehler beim Löschen des Kunden: " + e.Message);
            if (connection != null && connection.State == ConnectionState.Open)
            {
                connection.Close();
            }
            return rowsAffected;
        }
    }

    // Bestellung
    public int ConnectArticleAndOrder(int articleId, int orderId)
    {
        try
        {
            connection.Open();

            SqlCommand command = new SqlCommand("INSERT INTO OrderArticle (orderid, articleid) VALUES (@orderid, @articleid)", connection);
            command.Parameters.AddWithValue("@orderid", Convert.ToInt32(orderId));
            command.Parameters.AddWithValue("@articleid", Convert.ToInt32(articleId));

            command.ExecuteNonQuery();
            Console.WriteLine($"Artikel mit der ID {articleId} mit der Bestellung mit ID {orderId} erfolgreich verbunden.");

            connection.Close();
        }
        catch (Exception e)
        {
            Console.WriteLine($"Fehler beim verbinden von Artikel mit der ID {articleId} und der Bestellung mit der ID {orderId}: " + e.Message);
            if (connection != null && connection.State == ConnectionState.Open)
            {
                connection.Close();
            }
            return 0;
        }
        return 1;
    }

    public int AddOrder(string orderJson)
    {
        /*
         {
          "id": 1,
          "payd": true,
          "totalPrice": 99.99,
          "articleList": [
            {
              "id": 1,
              "name": "Product A",
              "price": 19.99
            },
            {
              "id": 2,
              "name": "Product B",
              "price": 29.99
            }
          ]
        }
         */
        bool connectionProblems = false;
        try
        {
            dynamic orderData = JsonConvert.DeserializeObject(orderJson);

            connection.Open();

            SqlCommand command = new SqlCommand("INSERT INTO Orders (id, payd, totalPrice) VALUES (@id, @payd, @totalPrice)", connection);
            command.Parameters.AddWithValue("@id", Convert.ToInt32(orderData.id));
            command.Parameters.AddWithValue("@payd", Convert.ToBoolean(orderData.payd));
            command.Parameters.AddWithValue("@totalPrice", Convert.ToDouble(orderData.totalPrice));

            command.ExecuteNonQuery();
            Console.WriteLine($"Bestellung mit ID {orderData.id} erfolgreich angelegt.");

            connection.Close();

            //List<Article> articleListForOrder = order.ArticleList;
            foreach (var articleData in orderData.articleList)
            {
                int articleId = articleData.id;
                int orderId = orderData.id;
                int i = ConnectArticleAndOrder(articleId, orderId);
                if (i == 0)
                {
                    connectionProblems = true;
                }
            }
        }
        catch (Exception e)
        {
            Console.WriteLine("Fehler beim hinzufügen der Bestellung: " + e.Message);
            if (connection != null && connection.State == ConnectionState.Open)
            {
                connection.Close();
            }
            return 0;
        }
        if (connectionProblems)
        {
            return 0;
        }
        else
        {
            return 1;
        }
    }

    public int RemoveArticleAndOrderConnection(int orderId)
    {
        int rowsAffected = -1;
        try
        {
            connection.Open();

            SqlCommand command = new SqlCommand("DELETE FROM OrderArticle WHERE orderid = @orderid", connection);
            command.Parameters.AddWithValue("@orderid", orderId);
            rowsAffected = command.ExecuteNonQuery();

            connection.Close();

            if (rowsAffected > 0)
            {
                Console.WriteLine($"Bestellung mit ID {orderId} wurde erfolgreich von den Artikeln entkoppelt.");
            }
            else
            {
                Console.WriteLine($"Kopplungen welche die Bestellung mit ID {orderId} verwenden nicht gefunden oder konnte nicht gelöscht werden.");
            }
            return rowsAffected;
        }
        catch (Exception e)
        {
            Console.WriteLine($"Fehler beim entkoppeln aller Artikel vond der Bestellung mit der ID {orderId}: " + e.Message);
            if (connection != null && connection.State == ConnectionState.Open)
            {
                connection.Close();
            }
            return rowsAffected;
        }
    }

    public int RemoveOrder(int orderId)
    {
        int rowsAffected = -1;
        try
        {
            // Noch keine Verwendung fuer i
            int i = RemoveArticleAndOrderConnection(orderId);

            connection.Open();

            SqlCommand command = new SqlCommand("DELETE FROM Orders WHERE id = @orderid", connection);
            command.Parameters.AddWithValue("@orderid", orderId);
            rowsAffected = command.ExecuteNonQuery();

            connection.Close();

            if (rowsAffected > 0)
            {
                Console.WriteLine($"Bestellung mit ID {orderId} erfolgreich gelöscht.");
            }
            else
            {
                Console.WriteLine($"Bestellung mit ID {orderId} nicht gefunden oder konnte nicht gelöscht werden.");
            }

            return rowsAffected;
        }
        catch (Exception e)
        {
            Console.WriteLine($"Fehler beim Löschen der Bestellung mit der ID {orderId}: " + e.Message);
            if (connection != null && connection.State == ConnectionState.Open)
            {
                connection.Close();
            }
            return rowsAffected;
        }
    }

    public int Register(string email, string password)
    {
        int customerId = -1;
        try
        {
            connection.Open();

            SqlCommand command = new SqlCommand("INSERT INTO Customer (password, email) OUTPUT INSERTED.id VALUES (@password, @email)", connection);
            command.Parameters.AddWithValue("@password", password);
            command.Parameters.AddWithValue("@email", email);

            customerId = Convert.ToInt32(command.ExecuteScalar());
            Console.WriteLine($"Kunde mit Email {email} erfolgreich angelegt unter ID {customerId}.");

            connection.Close();
        }
        catch (Exception e)
        {
            Console.WriteLine("(Register) Fehler beim Hinzufügen des Kunden: " + e.Message);
            if (connection != null && connection.State == ConnectionState.Open)
            {
                connection.Close();
            }
            return -1;
        }
        return customerId;
    }

    public int AddProductToCart(int customerId, int articleId)
    {
        try
        {
            int orderId = GetOrderIdByCustomerId(customerId);

            if (orderId != -1)
            {
                Console.WriteLine("Order Existiert");
                // Order existiert
                //Artikel in der Order?
                bool articleExistsInOrder = ArticleExistsInOrder(orderId, articleId);
                if (articleExistsInOrder)
                {
                    Console.WriteLine("Artikel Existiert in Order");
                    connection.Open();
                    SqlCommand command = new SqlCommand("UPDATE OrderArticle SET articleAmount = articleAmount + 1 OUTPUT INSERTED.articleAmount WHERE orderid = @orderid AND articleid = @articleid", connection);
                    command.Parameters.AddWithValue("@orderid", orderId);
                    command.Parameters.AddWithValue("@articleId", articleId);
                    int updatedAmount = Convert.ToInt32(command.ExecuteScalar());
                    connection.Close();
                    return updatedAmount;
                }
                else
                {
                    Console.WriteLine("Artikel wird in Order angelegt");
                    connection.Open();
                    SqlCommand command = new SqlCommand("INSERT INTO OrderArticle (orderid, articleid, articleAmount) VALUES (@orderid, @articleid, 1);", connection);
                    command.Parameters.AddWithValue("@orderid", orderId);
                    command.Parameters.AddWithValue("@articleid", articleId);
                    command.ExecuteNonQuery();
                    connection.Close();
                    return 1;
                }
            }
            else
            {
                Console.WriteLine("Order Existiert nicht");
                int newOrderId = CreateOrder();
                if (newOrderId != -1)
                {
                    Console.WriteLine("Order neu erstellt");
                    // Verknüpfung in der Tabelle CustomerOrder erstellen
                    connection.Open();
                    SqlCommand customerOrderCommand = new SqlCommand("INSERT INTO CustomerOrder (customerid, orderid) VALUES (@customerid, @orderid);", connection);
                    customerOrderCommand.Parameters.AddWithValue("@customerid", customerId);
                    customerOrderCommand.Parameters.AddWithValue("@orderid", newOrderId);
                    customerOrderCommand.ExecuteNonQuery();
                    connection.Close();

                    // Verknüpfung in der Tabelle OrderArticle erstellen
                    connection.Open();
                    SqlCommand orderArticleCommand = new SqlCommand("INSERT INTO OrderArticle (orderid, articleid, articleAmount) VALUES (@orderid, @articleid, 1);", connection);
                    orderArticleCommand.Parameters.AddWithValue("@orderid", newOrderId);
                    orderArticleCommand.Parameters.AddWithValue("@articleid", articleId);
                    orderArticleCommand.ExecuteNonQuery();
                    connection.Close();
                    return 1;
                }
                else
                {
                    // Fehler beim Erstellen der Bestellung
                    Console.WriteLine("Fehler beim Erstellen einer neuen Bestellung.");
                    return -1;
                }
            }
        }
        catch (Exception e)
        {
            Console.WriteLine("(AddProductToCart) Fehler beim Hinzufügen des Produkts zum Warenkorb: " + e.Message);
            if (connection != null && connection.State == ConnectionState.Open)
            {
                connection.Close();
            }
            return -1;
        }
    }

    public int RemoveProductFromCart(int customerId, int articleId)
    {
        try
        {
            int orderId = GetOrderIdByCustomerId(customerId);
            if (orderId == -1)
            {
                return -1;
            }
            else
            {
                bool articleExistsInOrder = ArticleExistsInOrder(orderId, articleId);
                if (articleExistsInOrder)
                {
                    connection.Open();
                    SqlCommand command = new SqlCommand("UPDATE OrderArticle SET articleAmount = articleAmount - 1 OUTPUT INSERTED.articleAmount WHERE orderid = @orderid AND articleid = @articleid", connection);
                    command.Parameters.AddWithValue("@orderid", orderId);
                    command.Parameters.AddWithValue("@articleid", articleId);
                    int updatedAmount = Convert.ToInt32(command.ExecuteScalar());
                    connection.Close();

                    if (updatedAmount == 0)
                    {
                        RemoveArticleFromOrder(orderId, articleId);
                        return updatedAmount;
                    }
                    else
                    {
                        return updatedAmount;
                    }
                }
                else
                {
                    return -1;
                }
            }
        }
        catch (Exception e)
        {
            Console.WriteLine("(RemoveProductFromCart) Fehler beim Hinzufügen des Produkts zum Warenkorb: " + e.Message);
            if (connection != null && connection.State == ConnectionState.Open)
            {
                connection.Close();
            }
            return -1;
        }
    }

    // Hilfsfunktionen
    private int RemoveArticleFromOrder(int orderId, int articleId)
    {
        try
        {
            connection.Open();
            SqlCommand deleteCommand = new SqlCommand("DELETE FROM OrderArticle WHERE orderid = @orderid AND articleid = @articleid", connection);
            deleteCommand.Parameters.AddWithValue("@orderid", orderId);
            deleteCommand.Parameters.AddWithValue("@articleid", articleId);
            deleteCommand.ExecuteNonQuery();
            connection.Close();
            return 1;
        }
        catch (Exception e)
        {
            Console.WriteLine("(RemoveArticleFromOrder) Fehler beim Entfernen des Artikels von der Bestellung: " + e.Message);
            if (connection != null && connection.State == ConnectionState.Open)
            {
                connection.Close();
            }
            return -1;
        }
    }

    private int CreateOrder()
    {
        try
        {
            connection.Open();
            SqlCommand command = new SqlCommand("INSERT INTO Orders (payd, totalPrice) VALUES (0, 0); SELECT SCOPE_IDENTITY();", connection);
            int newOrderId = Convert.ToInt32(command.ExecuteScalar());
            connection.Close();

            return newOrderId;
        }
        catch (Exception e)
        {
            Console.WriteLine("(CreateOrder) Fehler beim Erstellen einer neuen Bestellung: " + e.Message);
            if (connection != null && connection.State == ConnectionState.Open)
            {
                connection.Close();
            }
            return -1;
        }
    }

    private bool ArticleExistsInOrder(int orderId, int articleId)
    {
        try
        {
            string checkArticleQuery = @"
            SELECT COUNT(*)
            FROM OrderArticle
            WHERE orderid = @OrderId AND articleid = @ArticleId";

            using (SqlCommand checkArticleCommand = new SqlCommand(checkArticleQuery, connection))
            {
                checkArticleCommand.Parameters.AddWithValue("@OrderId", orderId);
                checkArticleCommand.Parameters.AddWithValue("@ArticleId", articleId);

                connection.Open();
                int count = Convert.ToInt32(checkArticleCommand.ExecuteScalar());
                connection.Close();

                if (count > 0)
                {
                    return true;
                }
                else
                {
                    return false;
                }
            }
        }
        catch (Exception e)
        {
            Console.WriteLine("(ArticleExistsInOrder) Fehler beim Überprüfen des Artikels in der Bestellung: " + e.Message);
            if (connection != null && connection.State == ConnectionState.Open)
            {
                connection.Close();
            }
            return false;
        }
    }

    private int GetOrderIdByCustomerId(int customerId)
    {
        try
        {
            string getOrderQuery = @"
            SELECT orderid
            FROM CustomerOrder
            WHERE customerid = @CustomerId";

            using (SqlCommand getOrderCommand = new SqlCommand(getOrderQuery, connection))
            {
                getOrderCommand.Parameters.AddWithValue("@CustomerId", customerId);

                connection.Open();
                object result = getOrderCommand.ExecuteScalar();
                connection.Close();

                return result != null ? Convert.ToInt32(result) : -1;
            }
        }
        catch (Exception e)
        {
            Console.WriteLine("(GetOrderIdByCustomerId) Fehler beim Abrufen der OrderID: " + e.Message);
            if (connection != null && connection.State == ConnectionState.Open)
            {
                connection.Close();
            }
            return -1;
        }
    }
}
