using Microsoft.AspNetCore.Mvc;

namespace Traning.AspNetCore.Ocelot.Second.API.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class HomeController : ControllerBase
    {
        [HttpGet]
        public IActionResult Get()
        {
            return Ok("Second API");
        }
    }
}
