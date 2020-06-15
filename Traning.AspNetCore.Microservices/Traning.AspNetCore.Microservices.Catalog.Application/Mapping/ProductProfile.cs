using AutoMapper;
using Traning.AspNetCore.Microservices.Catalog.Abstractions.Models;
using Traning.AspNetCore.Microservices.Catalog.Application.CQRS;

namespace Traning.AspNetCore.Microservices.Catalog.Application.Mapping
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
