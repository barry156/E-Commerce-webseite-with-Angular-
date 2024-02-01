using Microsoft.AspNetCore.Mvc;
using System.Net.Mail;
using System.Net;
using Store_ApplicationLayer.Models;
using System.Text.Json;

namespace Store_ApplicationLayer.Controller
{
    public class UI_Controller : ControllerBase
    {
        [HttpPost]
        [Route("api/ui/post/login")]
        public IActionResult PostLoginRequest([FromBody] Model_Login login_body)
        {
            try
            {
                Console.WriteLine($"Post login request");
                if (login_body == null)
                {
                    throw new Exception("login body is null");
                }
                string user_id = "1234"; //ToDo Aus test gründen
                //Backend/databasestuff
                if (user_id != string.Empty)
                {
                    return Ok(user_id);
                }
                return Unauthorized();
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex);
            }
            return BadRequest();
        }

        [HttpPost]
        [Route("api/ui/post/register")]
        public IActionResult PostRegisterRequest([FromBody] Model_Login register_body)
        {
            try
            {
                Console.WriteLine($"Post login request");
                if (register_body == null)
                {
                    throw new Exception("login body is null");
                }
                string user_id = "1234"; //ToDo Aus test gründen
                //Backend/databasestuff
                if (user_id != string.Empty)
                {
                    return Ok(user_id);
                }
                return Unauthorized();
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex);
            }
            return BadRequest();
        }

        [HttpGet]
        [Route("api/ui/get/products")]
        public IActionResult GetProductsRequest()
        {
            try
            {
                Console.WriteLine($"Get products request");
                List<Model_Product> products = new();
                //Backend/databasestuff
                //ToDO Aus test gründen
                products.Add(new() { id = 123456, name = "someKindOfCloths", price = 2.50, url = "data:image/jpeg;base64,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" });
                products.Add(new() { id = 654321, name = "someKindOfRag", price = 222.49, url = "https://encrypted-tbn0.gstatic.com/images?q=tbn:ANd9GcTMk9KlHbNVoPApkVe2akBJ40yDEiFuLXdS8A&usqp=CAU" });
                return Ok(JsonSerializer.Serialize(products));
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex);
            }
            return BadRequest();
        }

        [HttpGet]
        [Route("api/ui/get/product/{id}")]
        public IActionResult GetProductRequest([FromRoute] int id)
        {
            try
            {
                if (id == null)
                {
                    throw new Exception("id is null");
                }
                Console.WriteLine($"Get price request, id: {id}");
                //Backend/databasestuff
                //ToDO Aus test gründen
                Model_Product product = new() { id = 654321, name = "someKindOfRag", price = 222.49, url = "https://encrypted-tbn0.gstatic.com/images?q=tbn:ANd9GcTMk9KlHbNVoPApkVe2akBJ40yDEiFuLXdS8A&usqp=CAU" };
                return Ok(JsonSerializer.Serialize(product));
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex);
            }
            return BadRequest();
        }

        [HttpPut]
        [Route("api/ui/put/product/{productId}-{userId}")]
        public IActionResult PutProductToCartRequest([FromRoute] int productId, [FromRoute] int userId)
        {
            try
            {
                if (productId == null || userId == null)
                {
                    throw new Exception("userId or productId is null");
                }
                Console.WriteLine($"Post product request, productId: {productId}, userId: {userId}");
                bool success = true; //ToDo Aus test gründen
                //Backend/databasestuff
                if (success)
                {
                    return Ok();
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex);
            }
            return BadRequest();
        }

        [HttpDelete]
        [Route("api/ui/delete/product/{productId}-{userId}")]
        public IActionResult DeleteProductFromCartRequest([FromRoute] int productId, [FromRoute] int userId)
        {
            try
            {
                if (productId == null || userId == null)
                {
                    throw new Exception("userId or productId is null");
                }
                Console.WriteLine($"Delete product request, productId: {productId}, userId: {userId}");
                bool success = true; //ToDo Aus test gründen
                //Backend/databasestuff
                if (success)
                {
                    return Ok();
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex);
            }
            return BadRequest();
        }

        [HttpGet]
        [Route("api/ui/get/cart/{userId}")]
        public IActionResult GetCartFromUserRequest([FromRoute] int userId)
        {
            try
            {
                if (userId == null)
                {
                    throw new Exception("userId is null");
                }
                Console.WriteLine($"Get cart request, userId: {userId}");
                //Backend/databasestuff
                //ToDO Aus test gründen
                Model_Cart cart = new();
                cart.id = userId;
                cart.products.Add(new Model_Product() { id = 654321, name = "someKindOfRag", price = 222.49, url = "https://encrypted-tbn0.gstatic.com/images?q=tbn:ANd9GcTMk9KlHbNVoPApkVe2akBJ40yDEiFuLXdS8A&usqp=CAU" });
                return Ok(JsonSerializer.Serialize(cart));
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex);
            }
            return BadRequest();
        }
    }
}
