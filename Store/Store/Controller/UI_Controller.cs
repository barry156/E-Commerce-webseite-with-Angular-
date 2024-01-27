using Microsoft.AspNetCore.Mvc;
using System.Net.Mail;
using System.Net;
using Store_ApplicationLayer.Models;

namespace Store_ApplicationLayer.Controller
{
    public class UI_Controller : ControllerBase
    {
        [HttpPost]
        [Route("api/ui/post/login")]
        public IActionResult PostLoginRequest([FromBody] Model_login login_body)
        {
            try
            {
                Console.WriteLine($"Post login request");
                if (login_body == null)
                {
                    throw new Exception("login body is null");
                }
                bool success = true; //ToDo Aus test gründen
                //Backend/databasestuff
                if (success)
                {
                    return Ok();
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
        [Route("api/ui/get/product/{id}")]
        public IActionResult GetPriceRequest([FromRoute] string id)
        {
            try
            {
                if (id == null)
                {
                    throw new Exception("id is null");
                }
                Console.WriteLine($"Get price request, id: {id}");
                string result = $"{{\"name\":\"test\", \"price\":\"2,50\"}}"; //ToDo Aus test gründen
                //Backend/databasestuff
                return Ok(result);
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex);
            }
            return BadRequest();
        }

        [HttpPost]
        [Route("api/ui/post/product/{id}")]
        public IActionResult PostProductToCartRequest([FromRoute] string id)
        {
            try
            {
                if (id == null)
                {
                    throw new Exception("id is null");
                }
                Console.WriteLine($"Post product request, id: {id}");
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
    }
}
