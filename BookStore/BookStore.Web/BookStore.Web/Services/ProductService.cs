using IO.Swagger.Api;
using IO.Swagger.Client;
using IO.Swagger.Model;


namespace BookStore.Web.Services
{
    public class ProductService
    {
        private ProductApi client;

        public ProductService()
        {
            client = new ProductApi(SD.ProductAPIBase);
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
