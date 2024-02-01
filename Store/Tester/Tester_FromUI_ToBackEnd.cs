using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace Tester
{
    public class Tester_FromUI_ToBackEnd
    {

        HttpClient client;

        public Tester_FromUI_ToBackEnd()
        {
            Console.WriteLine("Tests From UI to BackEnd \n");
            Test_post_register();
            Test_post_login();
            Test_get_product();
            Test_get_products();
            Test_put_product();
            Test_delete_product();
            Test_get_cart();
        }

        private void Test_post_register()
        {
            try
            {
                Console.WriteLine("Test Post Register");
                string email = "test.tester@email.de";
                string password = "passwordImKlartext";
                client = new();
                client.Timeout = TimeSpan.FromSeconds(10);
                HttpRequestMessage request = new(HttpMethod.Post, $"{GlobalConstants.apiBaseUrl}ui/post/register");
                StringContent content = new($"{{\"email\":\"{email}\", \"password\":\"{password}\"}}", null, "application/json");
                request.Content = content;
                var response = client.Send(request);
                response.EnsureSuccessStatusCode();
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.ToString());
            }
        }

        private void Test_post_login()
        {
            try
            {
                Console.WriteLine("Test Post Login");
                string email = "test.tester@email.de";
                string password = "passwordImKlartext";
                client = new();
                client.Timeout = TimeSpan.FromSeconds(10);
                HttpRequestMessage request = new(HttpMethod.Post, $"{GlobalConstants.apiBaseUrl}ui/post/login");
                StringContent content = new($"{{\"email\":\"{email}\", \"password\":\"{password}\"}}", null, "application/json");
                request.Content = content;
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
