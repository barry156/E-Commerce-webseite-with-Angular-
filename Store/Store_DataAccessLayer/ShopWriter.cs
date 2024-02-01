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
    private string connectionString = "Data Source=DESKTOP-ODAGOGJ\\SQLEXPRESS;Initial Catalog=ShopDB;User ID=shopUser;Password=123456789;";
    private SqlConnection connection;

    public ShopWriter()
    {
        connection = new SqlConnection(connectionString);
    }

    // Artikel
    public int AddArticle(Article article) 
    {
        try
        {
            connection.Open();

            SqlCommand command = new SqlCommand("INSERT INTO Artikel (ID, Name, Preis) VALUES (@ID, @Name, @Preis)", connection);
            command.Parameters.AddWithValue("@ID", article.ID);
            command.Parameters.AddWithValue("@Name", article.Name);
            command.Parameters.AddWithValue("@Preis", article.Preis);

            command.ExecuteNonQuery();
            Console.WriteLine($"Artikel mit ID {article.ID} erfolgreich angelegt.");

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

            SqlCommand command = new SqlCommand("DELETE FROM Artikel WHERE ID = @ArticleID", connection);
            command.Parameters.AddWithValue("@ArticleID", articleID);
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
    public int AddCustomer(Customer customer)
    {
        try
        {
            connection.Open();

            SqlCommand command = new SqlCommand("INSERT INTO kunden (name, id, password, email) VALUES (@name, @id, @password, @email)", connection);
            command.Parameters.AddWithValue("@id", customer.ID);
            command.Parameters.AddWithValue("@name", customer.Name);
            command.Parameters.AddWithValue("@password", customer.Password);
            command.Parameters.AddWithValue("@email", customer.Email);

            command.ExecuteNonQuery();
            Console.WriteLine($"Kunde mit ID {customer.ID} erfolgreich angelegt.");

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

            SqlCommand command = new SqlCommand("DELETE FROM kunden WHERE id = @CustomerID", connection);
            command.Parameters.AddWithValue("@CustomerID", customerId);
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

            SqlCommand command = new SqlCommand("INSERT INTO BestellungsArtikel (BestellungsID, ArtikelID) VALUES (@BestellungsID, @ArtikelID)", connection);
            command.Parameters.AddWithValue("@BestellungsID", orderId);
            command.Parameters.AddWithValue("@ArtikelID", articleId);

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
          "iD": 1,
          "payd": true,
          "totalPrice": 99.99,
          "articleList": [
            {
              "ID": 1,
              "Name": "Product A",
              "Preis": 19.99
            },
            {
              "ID": 2,
              "Name": "Product B",
              "Preis": 29.99
            }
          ]
        }
         */
        bool connectionProblems = false;
        try
        {
            dynamic orderData = JsonConvert.DeserializeObject(orderJson);

            connection.Open();

            SqlCommand command = new SqlCommand("INSERT INTO Bestellungen (ID, Bezahlt, Gesamtpreis) VALUES (@ID, @Bezahlt, @Gesamtpreis)", connection);
            command.Parameters.AddWithValue("@ID", orderData.ID);
            command.Parameters.AddWithValue("@Bezahlt", orderData.Payd);
            command.Parameters.AddWithValue("@Gesamtpreis", orderData.TotalPrice);

            command.ExecuteNonQuery();
            Console.WriteLine($"Bestellung mit ID {orderData.ID} erfolgreich angelegt.");

            connection.Close();

            //List<Article> articleListForOrder = order.ArticleList;
            foreach (var articleData in orderData.ArticleList)
            {
                int articleId = articleData.ID;
                int orderId = orderData.ID;
                int i = ConnectArticleAndOrder(articleId, orderId);
                if (i == 0) 
                {
                    connectionProblems = true;
                }
            }
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

            SqlCommand command = new SqlCommand("DELETE FROM BestellungsArtikel WHERE BestellungsID = @orderID", connection);
            command.Parameters.AddWithValue("@orderID", orderId);
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

            SqlCommand command = new SqlCommand("DELETE FROM Bestellungen WHERE ID = @orderID", connection);
            command.Parameters.AddWithValue("@orderID", orderId);
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
}
