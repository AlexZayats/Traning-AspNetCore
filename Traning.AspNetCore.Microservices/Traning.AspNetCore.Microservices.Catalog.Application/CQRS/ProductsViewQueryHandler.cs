using MediatR;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using Traning.AspNetCore.Microservices.Catalog.Abstractions.Models;

namespace Traning.AspNetCore.Microservices.Catalog.Application.CQRS
{
    public class ProductsViewQueryHandler : IRequestHandler<ProductsViewQuery, IEnumerable<ProductViewDto>>
    {
        public async Task<IEnumerable<ProductViewDto>> Handle(ProductsViewQuery request, CancellationToken cancellationToken)
        {
            return null;
        }
    }
}
