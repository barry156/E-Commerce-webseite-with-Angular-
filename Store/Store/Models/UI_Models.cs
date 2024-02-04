using Store_DataAccessLayer_PostgreSQL.Models;

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
        public int id { get; set; }
        public string name { get; set; }
        public double price { get; set; }
        public string url { get; set; }
        public int amount { get; set; } = 0;
    }

    public class Model_Cart
    {
        public List<ProductFromOrder> products { get; set; } = new();
    }

    public class Model_UICart
    {
        public List<Model_Product> products { get; set; } = new();
        public double total_price { get; set; }
    }

    public class Model_Logic_Login
    {
        public int id { set; get; }
        public Logic_Answer answer { get; set; }

        public Model_Logic_Login(Logic_Answer answer, int id = -1)
        {
            this.id = id;
            this.answer = answer;
        }
    }

    public class Model_ProductRequest
    {
        public int product_id { set; get; }
        public int user_id { set; get; }
    }

    public class Model_UserRequest
    {
        public string email { set; get; }
    }

    public class ProductFromOrder
    {
        public int id { set; get; }
        public int amount { set; get; }
    }

    public enum Logic_Answer
    {
        OK,
        NOT_FOUND,
        BAD_ARGUMENTS,
        WRONG_PASSWORD,
        ALREADY_EXISTS,
        ERROR
    }
}
