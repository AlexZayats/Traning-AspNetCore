using AutoMapper;
using Traning.AspNetCore.Microservices.Products.Abstractions.Models;
using Traning.AspNetCore.Microservices.Products.Application.CQRS;

namespace Traning.AspNetCore.Microservices.Products.Application.Mapping
{
    public class ProductProfile : Profile
    {
        public ProductProfile()
        {
            CreateMap<ProductCreateDto, ProductCreateCommand>();
            CreateMap<ProductUpdateDto, ProductUpdateCommand>();
        }
    }
}
