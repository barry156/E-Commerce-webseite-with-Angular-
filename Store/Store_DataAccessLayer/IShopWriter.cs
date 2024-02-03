using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

interface IShopWriter
{
    int AddArticle(string articleJson);
    int DeleteArticle(int Id);
    int AddCustomer(string customerString);
    int DeleteCustomer(int customerId);
    int AddOrder(string orderJson);
    int RemoveOrder(int orderId);

    //Wichtig
    int Register(string email, string password);
    int AddProductToCart(int customerId, int articleId);
    int RemoveProductFromCart(int customerId, int articleId);
}
