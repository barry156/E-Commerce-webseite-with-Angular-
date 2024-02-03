using Store_ApplicationLayer.Models;
using System.Text.RegularExpressions;

namespace Store_ApplicationLayer.Logic
{
    public static class Store_Logic
    {
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
                        new Model_Logic_Login(Logic_Answer.OK, id);
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
                //
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
                //
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
                List<Model_Product> products = new();
                //
                return products;
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

                int amount = -1;
                //
                return amount;
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

                int amount = -1;
                //
                return amount;
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex);
            }
            return -1;
        }

        public static Model_Cart getCartFromDB(int userId)
        {
            try
            {
                double totalPrice = 0;
                List<Model_Product> products = getAllProductsFromDB();
                if (products == null)
                {
                    throw new Exception("get products from db failed");
                }
                List<int> productIds = new();
                //get productIds from DB
                Model_Cart cart = new();
                foreach (int id in productIds)
                {
                    foreach (Model_Product product in products)
                    {
                        if (product.id == id)
                        {
                            totalPrice += product.price;
                            cart.products.Add(product);
                        }
                    }
                }
                cart.total_price = totalPrice;
                return cart;
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
