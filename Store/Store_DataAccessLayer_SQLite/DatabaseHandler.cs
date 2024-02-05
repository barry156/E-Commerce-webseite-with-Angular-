using ServiceStack.OrmLite;
using Store_DataAccessLayer_SQLite.Models;

namespace Store_DataAccessLayer_SQLite
{
    public sealed class DatabaseHandler
    {
        OrmLiteConnectionFactory dbFactory;
        private readonly string DATABASE_FILENAME = "Store_Postgre_DB.db";
        private readonly string DATABASE_PATH = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.Desktop), "PostgreSQL_DB");
        private static readonly DatabaseHandler instance = new();

        static DatabaseHandler() { }

        public static DatabaseHandler Instance { get { return instance; } }

        private DatabaseHandler()
        {
            try
            {
                if (!Directory.Exists(DATABASE_PATH))
                {
                    Directory.CreateDirectory(DATABASE_PATH);
                }
                string path = Path.Combine(DATABASE_PATH, DATABASE_FILENAME);
                dbFactory = new OrmLiteConnectionFactory(path, SqliteDialect.Provider);
                bool alreadyCreated = false;
                using (var db = dbFactory.Open())
                {
                    db.CreateTableIfNotExists<User>();
                    alreadyCreated = db.CreateTableIfNotExists<Product>();
                    db.CreateTableIfNotExists<Order>();
                }
                if (alreadyCreated)
                {
                    addDefaultProducts();
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.ToString());
            }
            Console.WriteLine("Datenbank angelegt");
        }

        private void addDefaultProducts()
        {
            using (var db = dbFactory.Open())
            {
                db.Insert(new Product() { product_name = "product1", product_price = 100, product_url = "assets/img/product-1.jpg", created_at = DateTime.Now });
                db.Insert(new Product() { product_name = "product2", product_price = 99, product_url = "assets/img/product-2.jpg", created_at = DateTime.Now });
                db.Insert(new Product() { product_name = "product3", product_price = 123, product_url = "assets/img/product-3.jpg", created_at = DateTime.Now });
                db.Insert(new Product() { product_name = "product4", product_price = 120, product_url = "assets/img/product-4.jpg", created_at = DateTime.Now });
                db.Insert(new Product() { product_name = "product5", product_price = 10, product_url = "assets/img/product-5.jpg", created_at = DateTime.Now });
                db.Insert(new Product() { product_name = "product6", product_price = 74.5, product_url = "assets/img/product-6.jpg", created_at = DateTime.Now });
            }
        }

        public User getUserFromDB(string email)
        {
            try
            {
                using (var db = dbFactory.Open())
                {
                    return db.Select<User>(x => x.user_email == email).First();
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
            return null;
        }

        public User addUserToDB(string email, string password)
        {
            try
            {
                using (var db = dbFactory.Open())
                {
                    User user = new User() { user_email = email, user_password = password };
                    db.Insert(user);
                    return user;
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
            return null;
        }

        public List<Product> getProductsFromDB()
        {
            try
            {
                using (var db = dbFactory.Open())
                {
                    return db.Select<Product>();
                }
            }
            catch
            (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
            return null;
        }

        public Product getProductFromDB(int id)
        {
            try
            {
                using (var db = dbFactory.Open())
                {
                    return db.Select<Product>(x => x.product_id == id).First();
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
            return null;
        }

        public int addProductToOrder(int userId, int productId)
        {
            try
            {
                using (var db = dbFactory.Open())
                {
                    Order order = db.Select<Order>(x => x.user_id == userId).FirstOrDefault();
                    if (order == null)
                    {
                        order = new Order() { created_at = DateTime.Now, user_id = userId, order_entitys = new() { new() { amount = 1, product_id = productId } } };
                        db.Insert(order);
                        return 1;
                    }
                    else
                    {
                        int amount = 0;
                        bool found = false;
                        foreach (OrderEntity entity in order.order_entitys)
                        {
                            if (entity.product_id == productId)
                            {
                                entity.amount += 1;
                                amount = entity.amount;
                                found = true;
                                break;
                            }
                        }
                        if (!found)
                        {
                            order.order_entitys.Add(new() { amount = 1, product_id = productId });
                        }
                        db.Update(order);
                        return amount;
                    }
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
            return -1;
        }

        public int removeProductFromOrder(int userId, int productId)
        {
            try
            {
                using (var db = dbFactory.Open())
                {
                    Order order = db.Select<Order>(x => x.user_id == userId).FirstOrDefault();
                    if (order == null)
                    {
                        return 0;
                    }
                    else
                    {
                        int amount = 0;
                        OrderEntity removeEntity = null;
                        foreach (OrderEntity entity in order.order_entitys)
                        {
                            if (entity.product_id == productId)
                            {
                                if (entity.amount <= 1)
                                {
                                    removeEntity = entity;
                                }
                                entity.amount -= 1;
                                amount = entity.amount;
                                break;
                            }
                        }
                        if (removeEntity != null)
                        {
                            order.order_entitys.Remove(removeEntity);
                        }
                        db.Update(order);
                        return amount;
                    }
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
            return -1;
        }

        public List<OrderEntity> getOrderFromDB(int id)
        {
            try
            {
                using (var db = dbFactory.Open())
                {
                    Order order = db.Select<Order>(x => x.user_id == id).FirstOrDefault();
                    return order.order_entitys;
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
            return null;
        }
    }
}
