using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Threading;
using System.Threading.Tasks;
using Traning.AspNetCore.Microservices.Products.Abstractions.Models;

namespace Traning.AspNetCore.Microservices.Products.Abstractions.Clients
{
    public class ProductsClient : IProductsClient
    {
        private readonly HttpClient _httpClient;

        public ProductsClient(HttpClient httpClient)
        {
            _httpClient = httpClient;
        }

        public Task<Guid> CreateProductAsync(ProductCreateDto product, CancellationToken cancellationToken = default)
        {
            throw new NotImplementedException();
        }

        public Task DeleteProductAsync(Guid productId, CancellationToken cancellationToken = default)
        {
            throw new NotImplementedException();
        }

        public Task<ProductViewDto> GetProductAsync(Guid productId, CancellationToken cancellationToken = default)
        {
            throw new NotImplementedException();
        }

        public async Task<IEnumerable<ProductViewDto>> GetProductsAsync(CancellationToken cancellationToken = default)
        {
            var url = "products";
            using (var response = await _httpClient.GetAsync(url, cancellationToken))
            {
                var responseString = await response.Content.ReadAsStringAsync();
                response.EnsureSuccessStatusCode();
                var result = JsonConvert.DeserializeObject<IEnumerable<ProductViewDto>>(responseString);
                return result;
            }
        }

        public Task UpdateProductAsync(ProductViewDto product, CancellationToken cancellationToken = default)
        {
            throw new NotImplementedException();
        }
    }
}
