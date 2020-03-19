using Microsoft.EntityFrameworkCore;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Traning.AspNetCore.EntityFramework.Data;
using Traning.AspNetCore.EntityFramework.Data.Models;
using Traning.AspNetCore.EntityFramework.Logic.Interfaces;
using Traning.AspNetCore.EntityFramework.Logic.Models;

namespace Traning.AspNetCore.EntityFramework.Logic.Managers
{
    public class ProductManager : IProductManager
    {
        private readonly IShopContext _shopContext;

        public ProductManager(IShopContext shopContext)
        {
            _shopContext = shopContext;
        }

        public async Task<PagedResult<Product>> GetProductsAsync(string search, int? fromIndex = null, int? toIndex = null, CancellationToken cancellationToken = default)
        {
            var query = _shopContext.Products.AsQueryable();
            if (!string.IsNullOrWhiteSpace(search))
            {
                query = query.Where(x =>
                    x.Name.ToLower().Contains(search.ToLower()) ||
                    x.Description.ToLower().Contains(search.ToLower()));
            }
            if (fromIndex.HasValue)
            {
                query = query.Skip(fromIndex.Value);
            }

            var total = await query.CountAsync(cancellationToken);
            if (fromIndex.HasValue && toIndex.HasValue)
            {
                query = query.Skip(fromIndex.Value).Take(toIndex.Value - fromIndex.Value + 1);
            }

            var items = await query.ToArrayAsync(cancellationToken);
            return new PagedResult<Product> { Items = items, Total = total };
        }
    }
}
