using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace Store_PresentationLayer
{
    public static class Requests
    {
        private static readonly string apiBaseUrl = "http://127.0.0.1:7136/api/";

        public static bool postRegister(string email, string password)
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
                    return true;
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Beim senden der Daten ist ein Fehler unterlaufen. \nBitte kontaktieren Sie den Support. \nFehler: \n{ex.Message}", "Ein Fehler ist aufgetreten!", MessageBoxButton.OK);
            }
            return false;
        }

        public static bool postLogin(string email, string password)
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
                    return true;
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Beim senden der Daten ist ein Fehler unterlaufen. \nBitte kontaktieren Sie den Support. \nFehler: \n{ex.Message}", "Ein Fehler ist aufgetreten!", MessageBoxButton.OK);
            }
            return false;
        }

        private void Test_get_product()
        {
            try
            {

                Console.WriteLine("Test Get Product");
                int id = 1234;
                client = new();
                client.Timeout = TimeSpan.FromSeconds(10);
                var request = new HttpRequestMessage(HttpMethod.Get, $"{GlobalConstants.apiBaseUrl}ui/get/product/{id}");
                var response = client.Send(request);
                response.EnsureSuccessStatusCode();
                Console.WriteLine(response.StatusCode.ToString());
                Console.WriteLine(response.Content.ReadAsStringAsync().Result);
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.ToString());
            }
        }

        private void Test_get_products()
        {
            try
            {

                Console.WriteLine("Test Get Products");
                client = new();
                client.Timeout = TimeSpan.FromSeconds(10);
                var request = new HttpRequestMessage(HttpMethod.Get, $"{GlobalConstants.apiBaseUrl}ui/get/products");
                var response = client.Send(request);
                response.EnsureSuccessStatusCode();
                Console.WriteLine(response.StatusCode.ToString());
                Console.WriteLine(response.Content.ReadAsStringAsync().Result);
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.ToString());
            }
        }

        private void Test_put_product()
        {
            try
            {

                Console.WriteLine("Test put Product");
                int userId = 1234;
                int productId = 54321;
                client = new();
                client.Timeout = TimeSpan.FromSeconds(10);
                var request = new HttpRequestMessage(HttpMethod.Put, $"{GlobalConstants.apiBaseUrl}ui/put/product/{productId}-{userId}");
                var response = client.Send(request);
                response.EnsureSuccessStatusCode();
                Console.WriteLine(response.StatusCode.ToString());
                Console.WriteLine(response.Content.ReadAsStringAsync().Result);
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.ToString());
            }
        }

        private void Test_delete_product()
        {
            try
            {

                Console.WriteLine("Test Delete Product");
                int userId = 1234;
                int productId = 54321;
                client = new();
                client.Timeout = TimeSpan.FromSeconds(10);
                var request = new HttpRequestMessage(HttpMethod.Delete, $"{GlobalConstants.apiBaseUrl}ui/delete/product/{productId}-{userId}");
                var response = client.Send(request);
                response.EnsureSuccessStatusCode();
                Console.WriteLine(response.StatusCode.ToString());
                Console.WriteLine(response.Content.ReadAsStringAsync().Result);
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.ToString());
            }
        }

        private void Test_get_cart()
        {
            try
            {

                Console.WriteLine("Test Get Cart");
                int userId = 1234;
                client = new();
                client.Timeout = TimeSpan.FromSeconds(10);
                var request = new HttpRequestMessage(HttpMethod.Get, $"{GlobalConstants.apiBaseUrl}ui/get/cart/{userId}");
                var response = client.Send(request);
                response.EnsureSuccessStatusCode();
                Console.WriteLine(response.StatusCode.ToString());
                Console.WriteLine(response.Content.ReadAsStringAsync().Result);
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.ToString());
            }
        }
    }
}
