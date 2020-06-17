using MediatR;
using System;
using System.Threading;
using System.Threading.Tasks;
using Traning.AspNetCore.Microservices.Basket.Abstractions.Models;

namespace Traning.AspNetCore.Microservices.Basket.Application.CQRS
{
    public class BasketViewQueryHandler : IRequestHandler<BasketViewQuery, BasketViewDto>
    {
        private readonly IBasketDbContext _context;

        public BasketViewQueryHandler(IBasketDbContext context)
        {
            _context = context;
        }

        public Task<BasketViewDto> Handle(BasketViewQuery request, CancellationToken cancellationToken)
        {
            throw new NotImplementedException();
        }
    }
}
