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

        public Model_Cart
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
