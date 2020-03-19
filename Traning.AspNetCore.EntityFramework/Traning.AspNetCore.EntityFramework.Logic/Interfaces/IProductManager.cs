using System.Threading;
using System.Threading.Tasks;
using Traning.AspNetCore.EntityFramework.Data.Models;
using Traning.AspNetCore.EntityFramework.Logic.Models;

namespace Traning.AspNetCore.EntityFramework.Logic.Interfaces
{
    public interface IProductManager
    {
        Task<PagedResult<Product>> GetProductsAsync(string search, int? fromIndex = default, int? toIndex = default, CancellationToken cancellationToken = default);
    }
}
