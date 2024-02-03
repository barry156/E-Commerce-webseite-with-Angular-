using ServiceStack;
using ServiceStack.Text.Json;
using Store_DataAccessLayer_PostgreSQL;
using Store_DataAccessLayer_PostgreSQL.Models;
using System.Text.Json;
using System.Xml.Linq;

namespace Store_DataAccessLayer_Bib_PostgreSQL
{
    public class BackEndHandler : BackEndInterface
    {
        public string Login(string json)
        {
            try
            {
                User user = new User();
                JsonDocument jsonDocument = JsonDocument.Parse(json);
                if (jsonDocument.RootElement.EnumerateObject().Count() == 1)
                {
                    JsonProperty property = jsonDocument.RootElement.EnumerateObject().First();
                    if (property.Value.ValueKind == JsonValueKind.String)
                    {
                        string email = property.Value.GetString();
                        user = DatabaseHandler.Instance.getUserFromDB(email);
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
                return JsonSerializer.Serialize(user);
            }
            catch (Exception ex)
            {
                Console.WriteLine($"DataAccessLayer Error: {ex.Message}");
            }
            Console.WriteLine("Test5");
            return JsonSerializer.Serialize(new User());
        }

        public string Register(string json)
        {
            try
            {
                User user = JsonSerializer.Deserialize<User>(json);
                User newUser = DatabaseHandler.Instance.addUserToDB(user.user_email, user.user_password);
                return JsonSerializer.Serialize(newUser.user_id);
            }
            catch (Exception ex)
            {
                Console.WriteLine($"DataAccessLayer Error: {ex.Message}");
            }
            return JsonSerializer.Serialize(new User());
        }

        public string getAllProducts()
        {
            try
            {
                List<Product> products = DatabaseHandler.Instance.getProductsFromDB();
                return JsonSerializer.Serialize(products);
            }
            catch (Exception ex)
            {
                Console.WriteLine($"DataAccessLayer Error: {ex.Message}");
            }
            return JsonSerializer.Serialize("");
        }

        public string getProduct(string json)
        {
            try
            {
                Product product = new();
                JsonDocument jsonDocument = JsonDocument.Parse(json);
                if (jsonDocument.RootElement.EnumerateObject().Count() == 1)
                {
                    JsonProperty property = jsonDocument.RootElement.EnumerateObject().First();
                    if (property.Value.ValueKind == JsonValueKind.Number)
                    {
                        int id = property.Value.GetInt32();
                        product = DatabaseHandler.Instance.getProductFromDB(id);
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
                return JsonSerializer.Serialize(product);
            }
            catch (Exception ex)
            {
                Console.WriteLine($"DataAccessLayer Error: {ex.Message}");
            }
            return JsonSerializer.Serialize(new Product());
        }

        public string addProductToCart(string json)
        {
            try
            {
                int userId = 0;
                int productId = 0;
                int amount = -1;
                JsonDocument jsonDocument = JsonDocument.Parse(json);
                if (jsonDocument.RootElement.EnumerateObject().Count() == 2)
                {
                    JsonProperty propertyUser = jsonDocument.RootElement.EnumerateObject().First();
                    if (propertyUser.Value.ValueKind == JsonValueKind.Number)
                    {
                        userId = propertyUser.Value.GetInt32();
                    }
                    JsonProperty propertyProduct = jsonDocument.RootElement.EnumerateObject().First();
                    if (propertyProduct.Value.ValueKind == JsonValueKind.Number)
                    {
                        productId = propertyProduct.Value.GetInt32();
                    }
                    amount = DatabaseHandler.Instance.addProductToOrder(userId, productId);
                }
                else
                {
                    Console.WriteLine("Das JSON enthält nicht zwei ein Property.");
                }
                return JsonSerializer.Serialize(amount);
            }
            catch (Exception ex)
            {
                Console.WriteLine($"DataAccessLayer Error: {ex.Message}");
            }
            return JsonSerializer.Serialize(-1);
        }

        public string removeProductFromCart(string json)
        {
            try
            {
                int userId = 0;
                int productId = 0;
                int amount = -1;
                JsonDocument jsonDocument = JsonDocument.Parse(json);
                if (jsonDocument.RootElement.EnumerateObject().Count() == 2)
                {
                    JsonProperty propertyUser = jsonDocument.RootElement.EnumerateObject().First();
                    if (propertyUser.Value.ValueKind == JsonValueKind.Number)
                    {
                        userId = propertyUser.Value.GetInt32();
                    }
                    JsonProperty propertyProduct = jsonDocument.RootElement.EnumerateObject().First();
                    if (propertyProduct.Value.ValueKind == JsonValueKind.Number)
                    {
                        productId = propertyProduct.Value.GetInt32();
                    }
                    amount = DatabaseHandler.Instance.removeProductFromOrder(userId, productId);
                }
                else
                {
                    Console.WriteLine("Das JSON enthält nicht zwei ein Property.");
                }
                return JsonSerializer.Serialize(amount);
            }
            catch (Exception ex)
            {
                Console.WriteLine($"DataAccessLayer Error: {ex.Message}");
            }
            return JsonSerializer.Serialize(-1);
        }

        public string getOrder(string json)
        {
            try
            {
                Order order = new();
                JsonDocument jsonDocument = JsonDocument.Parse(json);
                if (jsonDocument.RootElement.EnumerateObject().Count() == 1)
                {
                    JsonProperty property = jsonDocument.RootElement.EnumerateObject().First();
                    if (property.Value.ValueKind == JsonValueKind.Number)
                    {
                        int id = property.Value.GetInt32();
                        order = DatabaseHandler.Instance.getOrderFromDB(id);
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
                return JsonSerializer.Serialize(order);
            }
            catch (Exception ex)
            {
                Console.WriteLine($"DataAccessLayer Error: {ex.Message}");
            }
            return JsonSerializer.Serialize(new Order());
        }
    }
}
