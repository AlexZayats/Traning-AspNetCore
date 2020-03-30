using System;
using System.Net.Http;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;
using Traning.AspNetCore.EntityFramework.Logic.Models;

namespace Traning.AspNetCore.Blazor.Web.Data
{
    public class ProductsService
    {
        private JsonSerializerOptions _options = new JsonSerializerOptions
        {
            PropertyNamingPolicy = JsonNamingPolicy.CamelCase
        };

        public async Task<PagedResult<ProductDto>> GetProducts(string search = default)
        {
            using (var client = new HttpClient())
            {
                var url = "https://localhost:44327/products";
                if (!string.IsNullOrEmpty(search))
                {
                    url += $"?search={Uri.EscapeUriString(search)}";
                }
                var response = await client.GetAsync(url);
                if (response.IsSuccessStatusCode)
                {
                    var content = await response.Content.ReadAsStringAsync();
                    var result = JsonSerializer.Deserialize<PagedResult<ProductDto>>(content, _options);
                    return result;
                }
                return null;
            }
        }

        public async Task<ProductDto> GetProductAsync(string productId)
        {
            using (var client = new HttpClient())
            {
                var response = await client.GetAsync($"https://localhost:44327/products/{productId}");
                if (response.IsSuccessStatusCode)
                {
                    var content = await response.Content.ReadAsStringAsync();
                    var result = JsonSerializer.Deserialize<ProductDto>(content, _options);
                    return result;
                }
                return null;
            }
        }

        public async Task<ProductDto> CreateProductAsync(ProductDto product)
        {
            using (var client = new HttpClient())
            {
                var requestContent = new StringContent(JsonSerializer.Serialize(product, _options), Encoding.UTF8, "application/json");
                var response = await client.PostAsync($"https://localhost:44327/products", requestContent);
                if (response.IsSuccessStatusCode)
                {
                    var responseContent = await response.Content.ReadAsStringAsync();
                    var result = JsonSerializer.Deserialize<ProductDto>(responseContent, _options);
                    return result;
                }
                return null;
            }
        }

        public async Task<ProductDto> UpdateProductAsync(ProductDto product)
        {
            using (var client = new HttpClient())
            {
                var requestContent = new StringContent(JsonSerializer.Serialize(product, _options), Encoding.UTF8, "application/json");
                var response = await client.PutAsync($"https://localhost:44327/products", requestContent);
                if (response.IsSuccessStatusCode)
                {
                    var responseContent = await response.Content.ReadAsStringAsync();
                    var result = JsonSerializer.Deserialize<ProductDto>(responseContent, _options);
                    return result;
                }
                return null;
            }
        }
    }
}
