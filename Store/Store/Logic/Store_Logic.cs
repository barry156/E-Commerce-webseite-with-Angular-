using DataAccessLayer_Interface;
using Store_ApplicationLayer.Models;
using Store_DataAccessLayer;
using System.Text.Json;
using System.Text.RegularExpressions;

namespace Store_ApplicationLayer.Logic
{
    public static class Store_Logic
    {
        private static readonly DataAccessLayerInterface db = new BackEndHandler_SQL();

        #region Login & Register
        public static Model_Logic_Login checkLoginData(Model_Login userFromRequest)
        {
            try
            {
                if (!isEmail(userFromRequest.email))
                {
                    Console.WriteLine("Email is not in email format");
                    return new Model_Logic_Login(Logic_Answer.BAD_ARGUMENTS);
                }
                Model_Login userFromDB = getUserFromDB(userFromRequest.email);
                if (userFromDB != null)
                {
                    if (userFromDB.password.Equals(userFromRequest.password))
                    {
                        return new Model_Logic_Login(Logic_Answer.OK, userFromDB.id);
                    }
                    else
                    {
                        return new Model_Logic_Login(Logic_Answer.WRONG_PASSWORD);
                    }
                }
                else
                {
                    return new Model_Logic_Login(Logic_Answer.NOT_FOUND);
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex);
            }
            return new Model_Logic_Login(Logic_Answer.ERROR);
        }

        public static Model_Logic_Login checkRegisterData(Model_Login userFromRequest)
        {
            try
            {
                if (!isEmail(userFromRequest.email))
                {
                    Console.WriteLine("Email is not in email format");
                    return new Model_Logic_Login(Logic_Answer.BAD_ARGUMENTS);
                }
                Model_Login userFromDB = getUserFromDB(userFromRequest.email);
                if (userFromDB == null)
                {
                    int id = createUserInDB(userFromRequest);
                    if (id == -1)
                    {
                        return new Model_Logic_Login(Logic_Answer.ERROR);
                    }
                    else
                    {
                        return new Model_Logic_Login(Logic_Answer.OK, id);
                    }
                }
                else
                {
                    return new Model_Logic_Login(Logic_Answer.ALREADY_EXISTS);
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex);
            }
            return new Model_Logic_Login(Logic_Answer.ERROR); ;
        }

        private static bool isEmail(string email)
        {
            try
            {
                string pattern = @"^[a-zA-Z0-9._-]+@[a-zA-Z0-9.-]+\.[a-zA-Z]{2,4}$";
                Regex regex = new(pattern);
                return regex.IsMatch(email);
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex);
            }
            return false;
        }

        private static Model_Login getUserFromDB(string email)
        {
            try
            {
                string resultJson = db.Login(JsonSerializer.Serialize(new Model_UserRequest() { email = email }));
                if (resultJson == null || resultJson == "{}")
                {
                    return null;
                }
                return JsonSerializer.Deserialize<Model_Login>(resultJson);
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex);
            }
            return null;
        }

        private static int createUserInDB(Model_Login user)
        {
            try
            {
                string resultJson = db.Register(JsonSerializer.Serialize(user));
                return JsonSerializer.Deserialize<int>(resultJson);
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex);
            }
            return -1;
        }
        #endregion

        #region Products
        public static List<Model_Product> getAllProductsFromDB()
        {
            try
            {
                string resultJson = db.getAllProducts();
                return JsonSerializer.Deserialize<List<Model_Product>>(resultJson);
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex);
            }
            return null;
        }

        public static Model_Product getProductFromDB(int id)
        {
            try
            {
                List<Model_Product> products = getAllProductsFromDB();
                foreach (Model_Product product in products)
                {
                    if (product.id == id)
                    {
                        return product;
                    }
                }
                return null;
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex);
            }
            return null;
        }
        #endregion

        #region Cart
        public static int addProductToCart(int userId, int productId)
        {
            try
            {
                string resultJson = db.addProductToCart(JsonSerializer.Serialize(new Model_ProductRequest() { product_id = productId, user_id = userId }));
                return JsonSerializer.Deserialize<int>(resultJson);
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex);
            }
            return -1;
        }

        public static int removeProductFromCart(int userId, int productId)
        {
            try
            {
                string resultJson = db.removeProductFromCart(JsonSerializer.Serialize(new Model_ProductRequest() { product_id = productId, user_id = userId }));
                return JsonSerializer.Deserialize<int>(resultJson);
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex);
            }
            return -1;
        }

        public static Model_UICart getCartFromDB(int userId)
        {
            try
            {
                double totalPrice = 0;
                Model_UICart model_UICart = new();
                string resultJson = db.getOrder(JsonSerializer.Serialize(userId));
                List<ProductFromOrder> cart = JsonSerializer.Deserialize<List<ProductFromOrder>>(resultJson);
                foreach (ProductFromOrder product in cart)
                {
                    Model_Product compProduct = getProductFromDB(product.id);
                    if (compProduct != null)
                    {
                        totalPrice += (compProduct.price * product.amount);
                        compProduct.amount = product.amount;
                        model_UICart.products.Add(compProduct);
                    }
                }
                model_UICart.total_price = totalPrice;
                return model_UICart;
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex);
            }
            return null;
        }
        #endregion
    }
}
