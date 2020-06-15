using MediatR;
using System.Collections.Generic;
using Traning.AspNetCore.Microservices.Catalog.Abstractions.Models;

namespace Traning.AspNetCore.Microservices.Catalog.Application.CQRS
{
    public class ProductsViewQuery : IRequest<IEnumerable<ProductViewDto>>
    {
    }
}
