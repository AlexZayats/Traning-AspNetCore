using Microsoft.AspNetCore.Mvc;

namespace Traning.AspNetCore.Ocelot.First.API.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class HomeController : ControllerBase
    {
        [HttpGet]
        public IActionResult Get()
        {
            return Ok("First API");
        }
    }
}
