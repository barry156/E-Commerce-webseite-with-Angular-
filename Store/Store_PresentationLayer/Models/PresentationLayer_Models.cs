using System.Collections.Generic;

namespace Store_ApplicationLayer.Models
{
    public class Model_Login
    {
        public string email { get; set; }
        public string password { get; set; }
        public int id { get; set; }
    }

    public class Model_Product
    {
        public string name { get; set; }
        public int id { get; set; }
        public double price { get; set; }
        public string url { get; set; }
        public int amount { get; set; } = 0;
    }

    public class Model_Cart
    {
        public int id { get; set; }
        public List<Model_Product> products { get; set; } = new();
        public double total_price { get; set; }
    }
}
