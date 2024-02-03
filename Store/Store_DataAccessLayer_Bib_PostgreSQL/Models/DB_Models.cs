using ServiceStack.DataAnnotations;
using System.Text.Json.Serialization;

namespace Store_DataAccessLayer_PostgreSQL.Models
{
    public class User
    {
        [AutoIncrement]
        [JsonPropertyName("id")]
        public int user_id { get; set; }

        [JsonPropertyName("email")]
        public string user_email { get; set; }

        [JsonPropertyName("password")]
        public string user_password { get; set; }

        [JsonIgnore]
        public DateTime created_at { get; set; }
    }

    public class Product
    {
        [AutoIncrement]
        [JsonPropertyName("id")]
        public int product_id { get; set; }

        [JsonPropertyName("name")]
        public string product_name { get; set; }

        [JsonPropertyName("price")]
        public double product_price { get; set; }

        [JsonPropertyName("url")]
        public string product_url { get; set; }

        [JsonIgnore]
        public DateTime created_at { get; set; }
    }

    public class Order
    {
        [AutoIncrement]
        [JsonIgnore]
        public int order_id { get; set; }

        [JsonIgnore]
        public int user_id { get; set; }

        [JsonPropertyName("")]
        public List<OrderEntity> order_entitys { get; set; }

        [JsonIgnore]
        public DateTime created_at { get; set; }
    }

    public class OrderEntity
    {
        [JsonPropertyName("productId)")]
        public int product_id { get; set; }

        public int amount { get; set; }
    }
}
