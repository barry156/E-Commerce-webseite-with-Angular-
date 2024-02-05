using DataAccessLayer_Interface;
using System.Text.Json;

namespace Store_DataAccessLayer
{
    public class BackEndHandler_SQL : DataAccessLayerInterface
    {
        IDatabaseTableCreator creator;
        IShopWriter shopWriterObj;
        IShopReader shopReaderObj;

        public BackEndHandler_SQL()
        {
            creator = new DatabaseTableCreator();
            shopWriterObj = new ShopWriter();
            shopReaderObj = new ShopReader();
            if (creator.CreateTables() == 1)
            {
                Console.WriteLine("... Tables where created.");
            }
            else
            {
                Console.WriteLine("... Tables allready exist or Error, see console.");
            }
            bool skipInit = true;
            try
            {
                string test = shopReaderObj.ReadArticleByID(0);
                if (test == "null")
                {
                    skipInit = false;
                }
            }
            catch { skipInit = false; }
            if (!skipInit)
            {
                shopWriterObj.AddArticle("{\"name\":\"product1\", \"url\":\"assets/img/product-1.jpg\", \"price\":\"100\"}");
                shopWriterObj.AddArticle("{\"name\":\"product2\", \"url\":\"assets/img/product-2.jpg\", \"price\":\"99\"}");
                shopWriterObj.AddArticle("{\"name\":\"product3\", \"url\":\"assets/img/product-3.jpg\", \"price\":\"123\"}");
                shopWriterObj.AddArticle("{\"name\":\"product4\", \"url\":\"assets/img/product-4.jpg\", \"price\":\"120\"}");
                shopWriterObj.AddArticle("{\"name\":\"product5\", \"url\":\"assets/img/product-5.jpg\", \"price\":\"10\"}");
                shopWriterObj.AddArticle("{\"name\":\"product6\", \"url\":\"assets/img/product-6.jpg\", \"price\":\"74.5\"}");
            }
        }

        public string Login(string json)
        {
            try
            {
                JsonDocument jsonDocument = JsonDocument.Parse(json);
                if (jsonDocument.RootElement.EnumerateObject().Count() == 1)
                {
                    JsonProperty property = jsonDocument.RootElement.EnumerateObject().First();
                    if (property.Value.ValueKind == JsonValueKind.String)
                    {
                        string email = property.Value.GetString();
                        return shopReaderObj.Login(email);
                    }
                    else
                    {
                        Console.WriteLine("Der Wert ist kein String.");
                    }
                }
                else
                {
                    Console.WriteLine("Das JSON enthält nicht genau ein Property.");
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"DataAccessLayer Error: {ex.Message}");
            }
            return string.Empty;
        }

        public string Register(string json)
        {
            try
            {
                JsonDocument jsonDocument = JsonDocument.Parse(json);
                string email = jsonDocument.RootElement.GetProperty("email").GetString();
                string password = jsonDocument.RootElement.GetProperty("password").GetString();
                int newId = shopWriterObj.Register(email, password);
                return JsonSerializer.Serialize(newId);
            }
            catch (Exception ex)
            {
                Console.WriteLine($"DataAccessLayer Error: {ex.Message}");
            }
            return string.Empty;
        }

        public string getAllProducts()
        {
            try
            {
                return shopReaderObj.ReadAllArticles();
            }
            catch (Exception ex)
            {
                Console.WriteLine($"DataAccessLayer Error: {ex.Message}");
            }
            return string.Empty;
        }

        public string getProduct(string json)
        {
            try
            {
                JsonDocument jsonDocument = JsonDocument.Parse(json);
                if (jsonDocument.RootElement.EnumerateObject().Count() == 1)
                {
                    JsonProperty property = jsonDocument.RootElement.EnumerateObject().First();
                    if (property.Value.ValueKind == JsonValueKind.Number)
                    {
                        int id = property.Value.GetInt32();
                        return shopReaderObj.ReadArticleByID(id);
                    }
                    else
                    {
                        Console.WriteLine("Der Wert ist kein int.");
                    }
                }
                else
                {
                    Console.WriteLine("Das JSON enthält nicht genau ein Property.");
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"DataAccessLayer Error: {ex.Message}");
            }
            return string.Empty;
        }

        public string addProductToCart(string json)
        {
            try
            {
                int userId = 0;
                int productId = 0;
                JsonDocument jsonDocument = JsonDocument.Parse(json);
                if (jsonDocument.RootElement.EnumerateObject().Count() == 2)
                {
                    JsonProperty propertyUser = jsonDocument.RootElement.EnumerateObject().Last();
                    if (propertyUser.Value.ValueKind == JsonValueKind.Number)
                    {
                        userId = propertyUser.Value.GetInt32();
                    }
                    JsonProperty propertyProduct = jsonDocument.RootElement.EnumerateObject().First();
                    if (propertyProduct.Value.ValueKind == JsonValueKind.Number)
                    {
                        productId = propertyProduct.Value.GetInt32();
                    }
                    int articleAmount = shopWriterObj.AddProductToCart(userId, productId);
                    return JsonSerializer.Serialize(articleAmount);
                }
                else
                {
                    Console.WriteLine("Das JSON enthält nicht zwei ein Property.");
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"DataAccessLayer Error: {ex.Message}");
            }
            return string.Empty;
        }

        public string removeProductFromCart(string json)
        {
            try
            {
                int userId = 0;
                int productId = 0;
                JsonDocument jsonDocument = JsonDocument.Parse(json);
                if (jsonDocument.RootElement.EnumerateObject().Count() == 2)
                {
                    JsonProperty propertyUser = jsonDocument.RootElement.EnumerateObject().Last();
                    if (propertyUser.Value.ValueKind == JsonValueKind.Number)
                    {
                        userId = propertyUser.Value.GetInt32();
                    }
                    JsonProperty propertyProduct = jsonDocument.RootElement.EnumerateObject().First();
                    if (propertyProduct.Value.ValueKind == JsonValueKind.Number)
                    {
                        productId = propertyProduct.Value.GetInt32();
                    }
                    int articleAmount = shopWriterObj.RemoveProductFromCart(userId, productId);
                    return JsonSerializer.Serialize(articleAmount);
                }
                else
                {
                    Console.WriteLine("Das JSON enthält nicht zwei ein Property.");
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"DataAccessLayer Error: {ex.Message}");
            }
            return string.Empty; ;
        }

        public string getOrder(string json)
        {
            try
            {
                int id = JsonSerializer.Deserialize<int>(json);
                return shopReaderObj.getOrder(id);
            }
            catch (Exception ex)
            {
                Console.WriteLine($"DataAccessLayer Error: {ex.Message}");
            }
            return string.Empty;
        }
    }
}
