using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using Traning.AspNetCore.Microservices.Catalog.Abstractions.Models;

namespace Traning.AspNetCore.Microservices.Catalog.Abstractions.Clients
{
    public class ProductsClient : IProductsClient
    {
        private const string URL = "products";

        private readonly HttpClient _httpClient;

        public ProductsClient(HttpClient httpClient)
        {
            _httpClient = httpClient;
        }

        public async Task<IEnumerable<ProductViewDto>> GetProductsAsync(Guid[] productIds = default, CancellationToken cancellationToken = default)
        {
            var uriBuilder = new UriBuilder(URL);
            if (productIds != null)
            {
                uriBuilder.Query += $"productIds={string.Join(",", productIds)}";
            }
            using (var response = await _httpClient.GetAsync(uriBuilder.Uri, cancellationToken))
            {
                var responseString = await response.Content.ReadAsStringAsync();
                response.EnsureSuccessStatusCode();
                var result = JsonConvert.DeserializeObject<IEnumerable<ProductViewDto>>(responseString);
                return result;
            }
        }

        public async Task<ProductViewDto> GetProductAsync(Guid productId, CancellationToken cancellationToken = default)
        {
            using (var response = await _httpClient.GetAsync($"{URL}/{productId}", cancellationToken))
            {
                var responseString = await response.Content.ReadAsStringAsync();
                response.EnsureSuccessStatusCode();
                var result = JsonConvert.DeserializeObject<ProductViewDto>(responseString);
                return result;
            }
        }

        public async Task<Guid> CreateProductAsync(ProductCreateDto product, CancellationToken cancellationToken = default)
        {
            if (product == null) throw new ArgumentNullException(nameof(product));
            using (var request = new HttpRequestMessage(HttpMethod.Post, URL))
            {
                var data = JsonConvert.SerializeObject(product);
                request.Content = new StringContent(data, Encoding.UTF8, "application/json");
                using (var response = await _httpClient.SendAsync(request, cancellationToken))
                {
                    response.EnsureSuccessStatusCode();
                    var content = await response.Content.ReadAsStringAsync();
                    return JsonConvert.DeserializeObject<Guid>(content);
                }
            }
        }

        public async Task UpdateProductAsync(ProductViewDto product, CancellationToken cancellationToken = default)
        {
            if (product == null) throw new ArgumentNullException(nameof(product));
            using (var request = new HttpRequestMessage(HttpMethod.Put, URL))
            {
                var data = JsonConvert.SerializeObject(product);
                request.Content = new StringContent(data, Encoding.UTF8, "application/json");
                using (var response = await _httpClient.SendAsync(request, cancellationToken))
                {
                    response.EnsureSuccessStatusCode();
                }
            }
        }

        public async Task DeleteProductAsync(Guid productId, CancellationToken cancellationToken = default)
        {
            using (var request = new HttpRequestMessage(HttpMethod.Delete, $"{URL}/{productId}"))
            using (var response = await _httpClient.SendAsync(request, cancellationToken))
            {
                response.EnsureSuccessStatusCode();
            }
        }
    }
}
