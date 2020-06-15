using MediatR;
using System;
using System.Threading;
using System.Threading.Tasks;

namespace Traning.AspNetCore.Microservices.Catalog.Application.CQRS
{
    public class ProductCreateCommandHandler : IRequestHandler<ProductCreateCommand, Guid>
    {
        public async Task<Guid> Handle(ProductCreateCommand request, CancellationToken cancellationToken)
        {
            throw new NullReferenceException();
            return Guid.NewGuid();
        }
    }
}
