using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

interface IShopWriter
{
    int AddArticle(Article article);
    int DeleteArticle(int Id);
    int AddCustomer(Customer customer);
    int DeleteCustomer(int customerId);
    int AddOrder(Order order);
    int RemoveOrder(int orderId);
}
