using Microsoft.AspNetCore.Mvc;
using System.Threading;
using System.Threading.Tasks;
using Traning.AspNetCore.EntityFramework.Logic.Interfaces;

namespace Traning.AspNetCore.EntityFramework.API.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class HomeController : ControllerBase
    {
        private readonly IProductManager _productManager;

        public HomeController(IProductManager productManager)
        {
            _productManager = productManager;
        }

        [HttpGet]
        public async Task<IActionResult> GetAsync(
            [FromQuery] string search,
            [FromQuery] int? fromIndex = default,
            [FromQuery] int? toIndex = default,
            CancellationToken cancellationToken = default)
        {
            var result = await _productManager.GetProductsAsync(search, fromIndex, toIndex, cancellationToken);
            return Ok(result);
        }
    }
}
