using System;
using System.Threading;
using System.Threading.Tasks;
using Traning.AspNetCore.EntityFramework.Logic.Models;

namespace Traning.AspNetCore.EntityFramework.Logic.Interfaces
{
    public interface IProductManager
    {
        Task<PagedResult<ProductDto>> GetProductsAsync(string search, int? fromIndex = default, int? toIndex = default, CancellationToken cancellationToken = default);
        Task<ProductDto> GetProductAsync(Guid productId, CancellationToken cancellationToken = default);
        Task<ProductDto> CreateProductAsync(ProductDto product, CancellationToken cancellationToken = default);
        Task<ProductDto> UpdateProductAsync(ProductDto product, CancellationToken cancellationToken = default);
        Task DeleteProductAsync(Guid productId, bool force, CancellationToken cancellationToken = default);
    }
}
