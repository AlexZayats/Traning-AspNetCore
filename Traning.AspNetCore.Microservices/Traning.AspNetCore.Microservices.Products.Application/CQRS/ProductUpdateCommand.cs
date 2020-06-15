using MediatR;
using Traning.AspNetCore.Microservices.Products.Abstractions.Models;

namespace Traning.AspNetCore.Microservices.Products.Application.CQRS
{
    public class ProductUpdateCommand : ProductUpdateDto, IRequest
    {
    }
}
