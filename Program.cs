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

        List<Article> articleList = shopReaderObj.ReadAllArticles();

        foreach (var article in articleList)
        {
            Console.WriteLine($"ID: {article.ID}, Name: {article.Name}, Preis: {article.Preis}");
        }

        List <Order> orderList = shopReaderObj.ReadAllOrders();

        foreach (var order in orderList)
        {
            Console.WriteLine($"ID: {order.ID}, Payd: {order.Payd}, Preis: {order.TotalPrice}");
            List<Article> articleForOrderList = order.ArticleList;
            foreach (var article in articleForOrderList)
            {
                Console.WriteLine($"ID: {article.ID}, Name: {article.Name}, Preis: {article.Preis}");
            }
        }
    }
}
