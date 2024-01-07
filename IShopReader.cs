using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using System.Collections.Generic;

public interface IShopReader
{
    List<Customer> ReadAllCustomer();
    List<Article> ReadAllArticles();
}