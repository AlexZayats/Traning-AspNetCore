using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using Traning.AspNetCore.Microservices.Products.Abstractions.Models;

namespace Traning.AspNetCore.Microservices.Products.Abstractions.Clients
{
    public interface IProductsClient
    {
        Task<IEnumerable<ProductViewDto>> GetProductsAsync(CancellationToken cancellationToken = default);
        Task<ProductViewDto> GetProductAsync(Guid productId, CancellationToken cancellationToken = default);
        Task<Guid> CreateProductAsync(ProductCreateDto product, CancellationToken cancellationToken = default);
        Task UpdateProductAsync(ProductViewDto product, CancellationToken cancellationToken = default);
        Task DeleteProductAsync(Guid productId, CancellationToken cancellationToken = default);
    }
}
