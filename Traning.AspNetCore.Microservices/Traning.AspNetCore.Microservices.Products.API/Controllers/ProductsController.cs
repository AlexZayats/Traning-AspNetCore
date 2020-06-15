﻿using AutoMapper;
using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using Traning.AspNetCore.Microservices.Products.Abstractions.Models;
using Traning.AspNetCore.Microservices.Products.Application.CQRS;

namespace Traning.AspNetCore.Microservices.Products.API.Controllers
{
    [Route("[controller]")]
    [ApiController]
    public class ProductsController : ControllerBase
    {
        private readonly IMediator _mediator;
        private readonly IMapper _mapper;

        public ProductsController(IMediator mediator, IMapper mapper)
        {
            _mediator = mediator;
            _mapper = mapper;
        }

        [HttpGet(Name = nameof(GetProductsAsync))]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        public async Task<ActionResult<IEnumerable<ProductViewDto>>> GetProductsAsync(CancellationToken cancellationToken = default)
        {
            var result = await _mediator.Send(new ProductsViewQuery(), cancellationToken);
            return Ok(result);
        }

        [HttpGet("{productId}", Name = nameof(GetProductAsync))]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        public async Task<ActionResult<ProductViewDto>> GetProductAsync(Guid productId, CancellationToken cancellationToken = default)
        {
            var result = await _mediator.Send(new ProductViewQuery { ProductId = productId }, cancellationToken);
            return Ok(result);
        }

        [HttpPost(Name = nameof(CreateProductAsync))]
        [ProducesResponseType(StatusCodes.Status201Created)]
        public async Task<ActionResult> CreateProductAsync(ProductCreateDto model, CancellationToken cancellationToken = default)
        {
            var command = _mapper.Map<ProductCreateCommand>(model);
            var result = await _mediator.Send(command, cancellationToken);
            return CreatedAtRoute(nameof(GetProductAsync), new { productId = result }, result);
        }

        [HttpPut(Name = nameof(UpdateProductAsync))]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        public async Task<ActionResult> UpdateProductAsync(ProductCreateDto model, CancellationToken cancellationToken = default)
        {
            var command = _mapper.Map<ProductUpdateCommand>(model);
            await _mediator.Send(command, cancellationToken);
            return NoContent();
        }

        [HttpDelete("{productId}", Name = nameof(DeleteProductAsync))]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        public async Task<ActionResult> DeleteProductAsync(Guid productId, CancellationToken cancellationToken = default)
        {
            await _mediator.Send(new ProductDeleteCommand { ProductId = productId }, cancellationToken);
            return NoContent();
        }
    }
}
