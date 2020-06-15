using MediatR;
using System;
using Traning.AspNetCore.Microservices.Products.Abstractions.Models;

namespace Traning.AspNetCore.Microservices.Products.Application.CQRS
{
    public class ProductViewQuery : IRequest<ProductViewDto>
    {
        public Guid ProductId { get; set; }
    }
}
