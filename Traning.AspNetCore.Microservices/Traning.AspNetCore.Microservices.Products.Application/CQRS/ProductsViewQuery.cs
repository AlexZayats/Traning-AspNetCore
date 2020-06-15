using MediatR;
using System.Collections.Generic;
using Traning.AspNetCore.Microservices.Products.Abstractions.Models;

namespace Traning.AspNetCore.Microservices.Products.Application.CQRS
{
    public class ProductsViewQuery : IRequest<IEnumerable<ProductViewDto>>
    {
    }
}
