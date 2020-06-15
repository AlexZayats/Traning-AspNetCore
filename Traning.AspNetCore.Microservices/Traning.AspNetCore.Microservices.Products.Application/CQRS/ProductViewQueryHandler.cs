using MediatR;
using System.Threading;
using System.Threading.Tasks;
using Traning.AspNetCore.Microservices.Products.Abstractions.Models;

namespace Traning.AspNetCore.Microservices.Products.Application.CQRS
{
    public class ProductViewQueryHandler : IRequestHandler<ProductViewQuery, ProductViewDto>
    {
        public async Task<ProductViewDto> Handle(ProductViewQuery request, CancellationToken cancellationToken)
        {
            return new ProductViewDto();
        }
    }
}
