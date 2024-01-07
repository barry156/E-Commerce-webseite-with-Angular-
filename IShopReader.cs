using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using System.Collections.Generic;

public interface IShopReader
{
    List<Article> GetArticlesByOrderID(int orderID);

    Article ReadArticleByID(int articleID);

    List<Customer> ReadAllCustomer();
    List<Article> ReadAllArticles();
    List<Order> ReadAllOrders();
}