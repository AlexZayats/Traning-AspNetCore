using MediatR;
using System;
using System.Threading;
using System.Threading.Tasks;

namespace Traning.AspNetCore.Microservices.Catalog.Application.CQRS
{
    public class ProductUpdateCommandHandler : IRequestHandler<ProductUpdateCommand>
    {
        public Task<Unit> Handle(ProductUpdateCommand request, CancellationToken cancellationToken)
        {
            throw new NotImplementedException();
        }
    }
}
