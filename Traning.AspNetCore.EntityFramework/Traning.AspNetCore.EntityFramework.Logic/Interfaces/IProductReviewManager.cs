using System.Threading;
using System.Threading.Tasks;
using Traning.AspNetCore.EntityFramework.Logic.Models;

namespace Traning.AspNetCore.EntityFramework.Logic.Interfaces
{
    public interface IProductReviewManager
    {
        Task<ProductReviewDto> CreateProductReviewAsync(ProductReviewDto product, CancellationToken cancellationToken = default);
    }
}
