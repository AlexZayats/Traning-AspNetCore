using System.Net.Http;
using System.Text.Json;
using System.Threading.Tasks;
using Traning.AspNetCore.EntityFramework.Logic.Models;

namespace Traning.AspNetCore.Blazor.Web.Data
{
    public class ProductsService
    {
        public async Task<PagedResult<ProductDto>> GetProducts()
        {
            using (var client = new HttpClient())
            {
                var response = await client.GetAsync("https://localhost:44327/products");
                if (response.IsSuccessStatusCode)
                {
                    var options = new JsonSerializerOptions
                    {
                        PropertyNamingPolicy = JsonNamingPolicy.CamelCase
                    };
                    var content = await response.Content.ReadAsStringAsync();
                    var result = JsonSerializer.Deserialize<PagedResult<ProductDto>>(content, options);
                    return result;
                }
                return null;
            }
        }
    }
}
