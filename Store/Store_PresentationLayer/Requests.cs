using Store_ApplicationLayer.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;
using System.Windows;

namespace Store_PresentationLayer
{
    public static class Requests
    {
        private static readonly string apiBaseUrl = "http://127.0.0.1:7136/api/";

        public static int postRegisterRequest(string email, string password)
        {
            try
            {
                HttpClient client = new();
                client.Timeout = TimeSpan.FromSeconds(20);
                HttpRequestMessage request = new(HttpMethod.Post, $"{apiBaseUrl}ui/post/register");
                StringContent content = new($"{{\"email\":\"{email}\", \"password\":\"{password}\"}}", null, "application/json");
                request.Content = content;
                var response = client.Send(request);
                response.EnsureSuccessStatusCode();
                string answer = response.Content.ReadAsStringAsync().Result;
                int id = Int32.Parse(answer);
                if (id > 0)
                {
                    return id;
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Beim senden der Daten ist ein Fehler unterlaufen. \nBitte kontaktieren Sie den Support. \nFehler: \n{ex.Message}", "Ein Fehler ist aufgetreten!", MessageBoxButton.OK);
            }
            return -1;
        }

        public static int postLoginRequest(string email, string password)
        {
            try
            {
                HttpClient client = new();
                client.Timeout = TimeSpan.FromSeconds(10);
                HttpRequestMessage request = new(HttpMethod.Post, $"{apiBaseUrl}ui/post/login");
                StringContent content = new($"{{\"email\":\"{email}\", \"password\":\"{password}\"}}", null, "application/json");
                request.Content = content;
                var response = client.Send(request);
                response.EnsureSuccessStatusCode();
                string answer = response.Content.ReadAsStringAsync().Result;
                int id = Int32.Parse(answer);
                if (id > 0)
                {
                    return id;
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Beim senden der Daten ist ein Fehler unterlaufen. \nBitte kontaktieren Sie den Support. \nFehler: \n{ex.Message}", "Ein Fehler ist aufgetreten!", MessageBoxButton.OK);
            }
            return -1;
        }

        public static Model_Product getProductRequest(int productId)
        {
            try
            {
                var client = new HttpClient();
                var request = new HttpRequestMessage(HttpMethod.Get, $"{apiBaseUrl}ui/get/product/{productId}");
                var response = client.Send(request);
                response.EnsureSuccessStatusCode();
                string result = response.Content.ReadAsStringAsync().Result;
                Model_Product product = JsonSerializer.Deserialize<Model_Product>(result);
                return product;
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.ToString());
            }
            return null;
        }

        public static List<Model_Product> getProductsRequest()
        {
            try
            {
                HttpClient client = new();
                HttpRequestMessage request = new(HttpMethod.Get, $"{apiBaseUrl}ui/get/products");
                var response = client.Send(request);
                response.EnsureSuccessStatusCode();
                string result = response.Content.ReadAsStringAsync().Result;
                List<Model_Product> products = JsonSerializer.Deserialize<List<Model_Product>>(result);
                return products;
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.ToString());
            }
            return null;
        }

        public static int addProductRequest(int userId, int productId)
        {
            try
            {
                HttpClient client = new();
                HttpRequestMessage request = new(HttpMethod.Delete, $"{apiBaseUrl}ui/put/product/{productId}-{userId}");
                var response = client.Send(request);
                response.EnsureSuccessStatusCode();
                string result = response.Content.ReadAsStringAsync().Result;
                int amount = JsonSerializer.Deserialize<int>(result);
                return amount;
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.ToString());
            }
            return -1;
        }

        public static int deleteProductRequest(int userId, int productId)
        {
            try
            {
                HttpClient client = new();
                HttpRequestMessage request = new(HttpMethod.Delete, $"{apiBaseUrl}ui/delete/product/{productId}-{userId}");
                var response = client.Send(request);
                response.EnsureSuccessStatusCode();
                string result = response.Content.ReadAsStringAsync().Result;
                int amount = JsonSerializer.Deserialize<int>(result);
                return amount;
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.ToString());
            }
            return -1;
        }

        public static int deleteWholeProductRequest(int userId, int productId)
        {
            try
            {
                HttpClient client = new();
                HttpRequestMessage request = new(HttpMethod.Delete, $"{apiBaseUrl}ui/delete/product/all/{productId}-{userId}");
                var response = client.Send(request);
                response.EnsureSuccessStatusCode();
                string result = response.Content.ReadAsStringAsync().Result;
                int amount = JsonSerializer.Deserialize<int>(result);
                return amount;
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.ToString());
            }
            return -1;
        }

        public static Model_Cart getOrderRequest(int userId)
        {
            try
            {
                HttpClient client = new();
                HttpRequestMessage request = new(HttpMethod.Get, $"{apiBaseUrl}ui/get/cart/{userId}");
                var response = client.Send(request);
                response.EnsureSuccessStatusCode();
                string result = response.Content.ReadAsStringAsync().Result;
                Model_Cart cart = JsonSerializer.Deserialize<Model_Cart>(result);
                return cart;
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.ToString());
            }
            return null;
        }
    }
}
