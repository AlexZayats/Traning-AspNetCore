using AutoMapper;
using Traning.AspNetCore.EntityFramework.Data.Models;
using Traning.AspNetCore.EntityFramework.Logic.Models;

namespace Traning.AspNetCore.EntityFramework.API.Profiles
{
    public class ProductProfile : Profile
    {
        public ProductProfile()
        {
            CreateMap<Product, ProductDto>();
            CreateMap<ProductDto, Product>();
        }
    }
}
