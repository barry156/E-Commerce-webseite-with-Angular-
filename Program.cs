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
        // Test Reader
        Console.WriteLine("Test Reader");
        // Setup
        IShopReader shopReaderObj = new ShopReader();
        Console.WriteLine("Reader Setup Done");

        // Test ReadAllCustomer
        Console.WriteLine("Test ReadAllCustomer");
        List <Customer> customerList = shopReaderObj.ReadAllCustomer();
        foreach (var customer in customerList)
        {
            Console.WriteLine($"ID: {customer.ID}, Name: {customer.Name}, Email: {customer.Email}, Bestellungen: {customer.Bestellungen}");
        }

        // Test ReadAllArticles
        Console.WriteLine("Test ReadAllArticles");
        List<Article> articleList = shopReaderObj.ReadAllArticles();
        foreach (var article in articleList)
        {
            Console.WriteLine($"ID: {article.ID}, Name: {article.Name}, Preis: {article.Preis}");
        }

        // Test ReadAllOrders + GetArticlesByOrderID
        Console.WriteLine("Test ReadAllOrders + GetArticlesByOrderID");
        List<Order> orderList = shopReaderObj.ReadAllOrders();
        foreach (var order in orderList)
        {
            Console.WriteLine($"ID: {order.ID}, Payd: {order.Payd}, Preis: {order.TotalPrice}");
            List<Article> articleForOrderList = order.ArticleList;
            foreach (var article in articleForOrderList)
            {
                Console.WriteLine($"ID: {article.ID}, Name: {article.Name}, Preis: {article.Preis}");
            }
        }

        // Test ReadArticleByID
        Console.WriteLine("Test ReadArticleByID");
        Article articleAlone = shopReaderObj.ReadArticleByID(1);
        Console.WriteLine($"ID: {articleAlone.ID}, Name: {articleAlone.Name}, Preis: {articleAlone.Preis}");

        // Test Writer
        Console.WriteLine("Test Writer");
        // Setup
        IShopWriter shopWriterObj = new ShopWriter();
        // Artikels
        Console.WriteLine("Test Artikel");
        int i = shopWriterObj.AddArticle(new Article
            {
                ID      = 999,
                Name    = "DeleteNameTest",
                Preis   = 999.99m
            }
        );
        int j = shopWriterObj.DeleteArticle(999);
        Console.WriteLine($"ADD: {i}, DELETE: {j}");

        // Kunden
        Console.WriteLine("Test Kunden");
        int n = shopWriterObj.AddCustomer(new Customer 
            {
                ID          = 999,
                Name        = "DeleteCustomerTest",
                Password    = "123456789",
                Email       = "test@mail.com"
                // Keine Bestellung da ich nicht weis wie die implementiert wird
            }
        );
        int m = shopWriterObj.DeleteCustomer(999);
        Console.WriteLine($"ADD: {n}, DELETE: {m}");

        // Bestellungen
        Console.WriteLine("Test Bestellungen");
        List<Article> articleListForOrderTest = new List<Article>();
        articleListForOrderTest.Add(new Article
            {
                ID = 1, //Artikel muss existieren
                Name = "DeleteNameTest",
                Preis = 999.99m
            }
        );
        int p = shopWriterObj.AddOrder(new Order 
            {
                ID = 999,
                ArticleList = articleListForOrderTest,
                Payd = false,
                TotalPrice = 999.99m
            }
        );
        int q = shopWriterObj.RemoveOrder(999);
        Console.WriteLine($"ADD: {p}, DELETE: {q}");
    }
}
