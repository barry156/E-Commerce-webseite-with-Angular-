namespace DataAccessLayer_Interface
{
    public interface DataAccessLayerInterface
    {
        public string Login(string json);
        public string Register(string json);
        public string getAllProducts();
        public string getProduct(string json);
        public string addProductToCart(string json);
        public string removeProductFromCart(string json);
        public string getOrder(string json);
    }
}
