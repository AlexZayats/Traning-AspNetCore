using Microsoft.AspNetCore.Mvc;

namespace Traning.AspNetCore.Microservices.Products.API.Controllers
{
    [Route("[controller]")]
    [ApiController]
    public class ProductsController : ControllerBase
    {
        [HttpGet]
        public IActionResult Get()
        {
            return Ok();
        }
    }
}
