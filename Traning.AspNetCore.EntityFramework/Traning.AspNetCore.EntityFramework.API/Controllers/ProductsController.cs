using Microsoft.AspNetCore.Mvc;
using System;
using System.Threading;
using System.Threading.Tasks;
using Traning.AspNetCore.EntityFramework.Data.Models;
using Traning.AspNetCore.EntityFramework.Logic.Interfaces;
using Traning.AspNetCore.EntityFramework.Logic.Models;

namespace Traning.AspNetCore.EntityFramework.API.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class ProductsController : ControllerBase
    {
        private readonly IProductManager _productManager;

        public ProductsController(IProductManager productManager)
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

        [HttpGet("{productId}")]
        public async Task<IActionResult> GetAsync(
            Guid productId,
            CancellationToken cancellationToken = default)
        {
            var result = await _productManager.GetProductAsync(productId, cancellationToken);
            return Ok(result);
        }

        [HttpPost()]
        public async Task<IActionResult> CreateAsync(
            ProductDto product,
            CancellationToken cancellationToken = default)
        {
            var result = await _productManager.CreateProductAsync(product, cancellationToken);
            return Ok(result);
        }

        [HttpPut()]
        public async Task<IActionResult> UpdateAsync(
            ProductDto product,
            CancellationToken cancellationToken = default)
        {
            var result = await _productManager.UpdateProductAsync(product, cancellationToken);
            return Ok(result);
        }

        [HttpDelete("{productId}")]
        public async Task<IActionResult> DeleteAsync(
            Guid productId,
            [FromQuery] bool force = false,
            CancellationToken cancellationToken = default)
        {
            await _productManager.DeleteProductAsync(productId, force, cancellationToken);
            return Ok();
        }
    }
}
