using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

public class Order
{
    public int ID { get; set; }
    public List<Article> ArticleList { get; set; }
    public bool Payd { get; set; }
    public decimal TotalPrice { get; set; }
}
