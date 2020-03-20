using Microsoft.AspNetCore.Mvc;
using System.Threading;
using System.Threading.Tasks;
using Traning.AspNetCore.EntityFramework.Logic.Interfaces;
using Traning.AspNetCore.EntityFramework.Logic.Models;

namespace Traning.AspNetCore.EntityFramework.API.Controllers
{
    [Route("[controller]")]
    [ApiController]
    public class ProductReviewsController : ControllerBase
    {
        private readonly IProductReviewManager _productReviewManager;

        public ProductReviewsController(IProductReviewManager productReviewManager)
        {
            _productReviewManager = productReviewManager;
        }

        [HttpPost()]
        public async Task<IActionResult> CreateAsync(
            ProductReviewDto productReview,
            CancellationToken cancellationToken = default)
        {
            var result = await _productReviewManager.CreateProductReviewAsync(productReview, cancellationToken);
            return Ok(result);
        }
    }
}