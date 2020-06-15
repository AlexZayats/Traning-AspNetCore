using FluentValidation;

namespace Traning.AspNetCore.Microservices.Products.Application.CQRS
{
    public class ProductUpdateCommandValidator : AbstractValidator<ProductUpdateCommand>
    {
        public ProductUpdateCommandValidator()
        {
            RuleFor(x => x.Name).NotEmpty();
            RuleFor(x => x.Description).NotEmpty();
        }
    }
}
