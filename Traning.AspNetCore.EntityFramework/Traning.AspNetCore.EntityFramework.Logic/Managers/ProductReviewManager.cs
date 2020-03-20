using AutoMapper;
using System.Threading;
using System.Threading.Tasks;
using Traning.AspNetCore.EntityFramework.Data;
using Traning.AspNetCore.EntityFramework.Data.Models;
using Traning.AspNetCore.EntityFramework.Logic.Interfaces;
using Traning.AspNetCore.EntityFramework.Logic.Models;

namespace Traning.AspNetCore.EntityFramework.Logic.Managers
{
    public class ProductReviewManager : IProductReviewManager
    {
        private readonly IShopContext _shopContext;
        private readonly IMapper _mapper;

        public ProductReviewManager(IShopContext shopContext, IMapper mapper)
        {
            _shopContext = shopContext;
            _mapper = mapper;
        }

        public async Task<ProductReviewDto> CreateProductReviewAsync(ProductReviewDto productReview, CancellationToken cancellationToken = default)
        {
            var add = _mapper.Map<ProductReview>(productReview);
            _shopContext.ProductReviews.Add(add);
            await _shopContext.SaveChangesAsync(cancellationToken);
            return _mapper.Map<ProductReviewDto>(productReview);
        }
    }
}
