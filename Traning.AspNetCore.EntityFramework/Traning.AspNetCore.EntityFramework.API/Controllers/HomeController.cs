using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Threading;
using System.Threading.Tasks;
using Traning.AspNetCore.EntityFramework.Data;

namespace Traning.AspNetCore.EntityFramework.API.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class HomeController : ControllerBase
    {
        private readonly IShopContext _shopContext;

        public HomeController(IShopContext shopContext)
        {
            _shopContext = shopContext;
        }

        [HttpGet]
        public async Task<IActionResult> GetAsync(CancellationToken cancellationToken)
        {
            var result = await _shopContext.Products.ToArrayAsync(cancellationToken);
            return Ok(result);
        }
    }
}
