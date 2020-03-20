using AutoMapper;
using Traning.AspNetCore.EntityFramework.Data.Models;
using Traning.AspNetCore.EntityFramework.Logic.Models;

namespace Traning.AspNetCore.EntityFramework.API.Profiles
{
    public class ProductReviewProfile : Profile
    {
        public ProductReviewProfile()
        {
            CreateMap<ProductReview, ProductReviewDto>();
            CreateMap<ProductReviewDto, ProductReview>();
        }
    }
}
