using IO.Swagger.Api;
using IO.Swagger.Model;

namespace BookStore.Web.Services
{
    public class ProductService
    {
        private BookStoreProductAPIApi client;

        public ProductService()
        {
            client = new BookStoreProductAPIApi(SD.ProductAPIBase);
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
    }
}
