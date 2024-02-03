using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using System.Data.SqlClient;
using Newtonsoft.Json;

class Program
{
    static void Main(string[] args)
    {
        Console.WriteLine("Try to build Tables ...");
        IDatabaseTableCreator creator = new DatabaseTableCreator();
        if (creator.CreateTables() == 1)
        {
            Console.WriteLine("... Tables where created.");
        }
        else
        {
            Console.WriteLine("... Tables allready exist or Error, see console.");
        }

        // Test Reader
        Console.WriteLine("Test Reader");
        // Setup
        IShopReader shopReaderObj = new ShopReader();
        Console.WriteLine("Reader Setup Done");

        // Test ReadAllCustomer
        Console.WriteLine("Test ReadAllCustomer");
        //List <Customer> customerList = shopReaderObj.ReadAllCustomer();
        dynamic customerList = JsonConvert.DeserializeObject(shopReaderObj.ReadAllCustomer());
        foreach (var customer in customerList)
        {
            Console.WriteLine($"ID: {customer.id}, Name: {customer.name}, Email: {customer.email}, Bestellungen: {customer.orders}");
        }

        // Test ReadAllArticles
        Console.WriteLine("Test ReadAllArticles");
        //List<Article> articleList = shopReaderObj.ReadAllArticles();
        dynamic articleList = JsonConvert.DeserializeObject(shopReaderObj.ReadAllArticles());
        foreach (var article in articleList)
        {
            Console.WriteLine($"ID: {article.id}, Name: {article.name}, Preis: {article.price}");
        }

        // Test ReadAllOrders + GetArticlesByOrderID
        Console.WriteLine("Test ReadAllOrders + GetArticlesByOrderID");
        //List<Order> orderList = shopReaderObj.ReadAllOrders();
        dynamic orderList = JsonConvert.DeserializeObject(shopReaderObj.ReadAllOrders());
        foreach (var order in orderList)
        {
            Console.WriteLine($"ID: {order.id}, Payd: {order.payd}, Preis: {order.totalPrice}");
            //List<Article> articleForOrderList = order.ArticleList;
            dynamic articleForOrderList = order.articleList;
            foreach (var article in articleForOrderList)
            {
                Console.WriteLine($"ID: {article.id}, Name: {article.name}, Preis: {article.price}");
            }
        }

        // Test ReadArticleByID
        Console.WriteLine("Test ReadArticleByID");
        //Article articleAlone = shopReaderObj.ReadArticleByID(1);
        dynamic articleAlone = JsonConvert.DeserializeObject(shopReaderObj.ReadArticleByID(1));
        Console.WriteLine($"ID: {articleAlone.id}, Name: {articleAlone.name}, Preis: {articleAlone.price}");

        // Test Writer
        Console.WriteLine("Test Writer");
        // Setup
        IShopWriter shopWriterObj = new ShopWriter();
        // Artikels
        Console.WriteLine("Test Artikel");
        int i = shopWriterObj.AddArticle("{\"id\":999,\"name\":\"DeleteNameTest\",\"price\":999.99}");
        int j = shopWriterObj.DeleteArticle(999);
        Console.WriteLine($"ADD: {i}, DELETE: {j}");

        // Kunden
        Console.WriteLine("Test Kunden");
        int n = shopWriterObj.AddCustomer("{\"id\":999,\"name\":\"DeleteCustomerTest\",\"password\":\"123456789\",\"email\":\"test@mail.com\"}");
        int m = shopWriterObj.DeleteCustomer(999);
        Console.WriteLine($"ADD: {n}, DELETE: {m}");

        // Bestellungen
        Console.WriteLine("Test Bestellungen");
        //List<Article> articleListForOrderTest = new List<Article>();
        List<dynamic> articleListForOrderTest = new List<dynamic>();
        dynamic articleWriter = new
        {
            id = 1,
            name = "DeleteNameTest",
            price = 999.99m
        };
        articleListForOrderTest.Add(articleWriter);

        dynamic orderWriter = new
        {
            id = 999,
            articleList = articleListForOrderTest,
            payd = false,
            totalPrice = 999.99m
        };
        int p = shopWriterObj.AddOrder(JsonConvert.SerializeObject(orderWriter, Formatting.Indented));
        int q = shopWriterObj.RemoveOrder(999);
        Console.WriteLine($"ADD: {p}, DELETE: {q}");
    }
}
