using Microsoft.AspNetCore.Mvc;
using Store_ApplicationLayer.Models;
using System.Text.Json;
using Store_ApplicationLayer.Logic;

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
                Model_Logic_Login answer = Store_Logic.checkLoginData(login_body);
                switch (answer.answer)
                {
                    case Logic_Answer.OK:
                        return Ok(answer.id);
                    case Logic_Answer.WRONG_PASSWORD:
                        return Unauthorized();
                    case Logic_Answer.NOT_FOUND:
                    case Logic_Answer.ERROR:
                    case Logic_Answer.BAD_ARGUMENTS:
                    default:
                        return BadRequest();
                }
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
                Model_Logic_Login answer = Store_Logic.checkRegisterData(register_body);
                switch (answer.answer)
                {
                    case Logic_Answer.OK:
                        return Ok(answer.id);
                    case Logic_Answer.ALREADY_EXISTS:
                    case Logic_Answer.NOT_FOUND:
                    case Logic_Answer.ERROR:
                    case Logic_Answer.BAD_ARGUMENTS:
                    default:
                        return BadRequest();
                }
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
                List<Model_Product> products = Store_Logic.getAllProductsFromDB();
                if (products != null)
                {
                    return Ok(JsonSerializer.Serialize(products));
                }
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
                Model_Product product = Store_Logic.getProductFromDB(id);
                if (product != null)
                {
                    return Ok(JsonSerializer.Serialize(product));
                }
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
                int amount = Store_Logic.addProductToCart(userId, productId);
                if (amount >= 0)
                {
                    return Ok(amount);
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
                int amount = Store_Logic.removeProductFromCart(userId, productId);
                if (amount >= 0)
                {
                    return Ok(amount);
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
                Model_Cart cart = Store_Logic.getCartFromDB(userId);
                cart.id = userId;
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
