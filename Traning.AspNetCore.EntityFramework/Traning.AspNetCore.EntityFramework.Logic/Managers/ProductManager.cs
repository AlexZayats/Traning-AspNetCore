using AutoMapper;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
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
        private readonly IMapper _mapper;

        public ProductManager(IShopContext shopContext, IMapper mapper)
        {
            _shopContext = shopContext;
            _mapper = mapper;
        }

        public async Task<ProductDto> CreateProductAsync(ProductDto product, CancellationToken cancellationToken = default)
        {
            var add = _mapper.Map<Product>(product);
            _shopContext.Products.Add(add);
            await _shopContext.SaveChangesAsync(cancellationToken);
            return _mapper.Map<ProductDto>(product);
        }

        public async Task DeleteProductAsync(Guid productId, bool force, CancellationToken cancellationToken = default)
        {
            var product = await _shopContext.Products.IgnoreQueryFilters().FirstOrDefaultAsync(x => x.Id == productId, cancellationToken);
            if (product != null)
            {
                if (force)
                {
                    _shopContext.Products.Remove(product);
                }
                else
                {
                    product.IsDeleted = true;
                }
                await _shopContext.SaveChangesAsync(cancellationToken);
            }
        }

        public async Task<ProductDto> GetProductAsync(Guid productId, CancellationToken cancellationToken = default)
        {
            var product = await _shopContext.Products.Include(x => x.Reviews).AsNoTracking().FirstOrDefaultAsync(x => x.Id == productId, cancellationToken);
            return _mapper.Map<ProductDto>(product);
        }

        public async Task<PagedResult<ProductDto>> GetProductsAsync(string search, int? fromIndex = null, int? toIndex = null, CancellationToken cancellationToken = default)
        {
            var query = _shopContext.Products.AsNoTracking();
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
            query = query.OrderBy(x => x.Name);
            var total = await query.CountAsync(cancellationToken);
            if (fromIndex.HasValue && toIndex.HasValue)
            {
                query = query.Skip(fromIndex.Value).Take(toIndex.Value - fromIndex.Value + 1);
            }
            
            var items = await _mapper.ProjectTo<ProductDto>(query).ToArrayAsync(cancellationToken);
            return new PagedResult<ProductDto> { Items = items, Total = total };
        }

        public async Task<ProductDto> UpdateProductAsync(ProductDto product, CancellationToken cancellationToken = default)
        {
            var update = await _shopContext.Products.FirstOrDefaultAsync(x => x.Id == product.Id, cancellationToken);
            if (update != null)
            {
                _mapper.Map(product, update);
            }
            await _shopContext.SaveChangesAsync(cancellationToken);
            return product;
        }
    }
}
