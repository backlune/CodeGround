using IO.Swagger.Api;
using IO.Swagger.Model;

namespace BookStore.App.Server.Services
{
    public class ProductService
    {
        private readonly ProductApi client = new(Configs.ProductAPIBase);

        public ProductService(string apiKey)
        {
            client.Configuration.ApiKey = new Dictionary<string, string> { { "Authorization", "Bearer " + apiKey} };
        }

        public async Task<List<ProductDto>> GetAllProductsAsync()
        {
            return await client.GetProductsAsync();
        }

        public async Task<ProductDto> GetProductsByIdAsync<T>(Guid id)
        {
            return await client.GetProductAsync(id);
        }

        public async Task CreateProduct<T>(ProductDto product)
        {
            await client.AddProductAsync(product);
        }

        public async Task DeleteProductAsync(Guid productId)
        {
            await client.DeleteProductAsync(productId);
        }
    }
}
