using BookStore.ProductAPI;
using BookStore.ProductAPI.Data;
using Microsoft.EntityFrameworkCore;
using Xunit;


namespace BookStore.Tests
{
    public class ProductServiceTests
    {
        // Add XUnit test here for ProductService.AddProduct in BookStore.ProductAPI
        [Fact]
        public async Task AddProduct_BaseCase()
        {
            // Arrange
            var productDto = new ProductDto
            {
                Name = "Test Product",
                Description = "Test Description",
                Price = 10.00m,
                ImageUrl = "https://test.com"
            };
            var product = new Product
            {
                Name = "Test Product",
                Description = "Test Description",
                Price = 10.00m,
                ImageUrl = "https://test.com"
            };
            var dbContext = new ProductDbContext(new DbContextOptionsBuilder<ProductDbContext>()
                .UseInMemoryDatabase("BookStore").Options);
            var mapper = BookStore.ProductAPI.MappingConfig.RegisterMaps().CreateMapper();

            // Act
            var result = await ProductService.AddProduct(mapper, productDto, dbContext);

            // Assert
            Assert.Equal(product.Name, result.Name);
            Assert.Equal(product.Description, result.Description);
            Assert.Equal(product.Price, result.Price);
        }

        // Add XUnit test here for ProductService.AddProduct with missing imageurl it should throw an exception
        [Fact]
        public async Task AddProduct_MissingImageUrl()
        {
            // Arrange
            var productDto = new ProductDto
            {
                Name = "Test Product",
                Description = "Test Description",
                Price = 10.00m
            };
            var dbContext = new ProductDbContext(new DbContextOptionsBuilder<ProductDbContext>()
                .UseInMemoryDatabase("BookStore").Options);
            var mapper = BookStore.ProductAPI.MappingConfig.RegisterMaps().CreateMapper();

            // Act
            // Assert
            await Assert.ThrowsAsync<Exception>(() =>
                ProductService.AddProduct(mapper, productDto, dbContext));
        }

        // Add test for ProductService.GetProduct in BookStore.ProductAPI
        [Fact]
        public async Task GetProduct_BaseCase()
        {
            // Arrange
            var product = new Product
            {
                Name = "Test Product",
                Description = "Test Description",
                Price = 10.00m,
                ImageUrl = "https://test.com"
            };
            var dbContext = new ProductDbContext(new DbContextOptionsBuilder<ProductDbContext>()
                           .UseInMemoryDatabase("BookStore").Options);
            dbContext.Products.Add(product);
            await dbContext.SaveChangesAsync();
            var mapper = BookStore.ProductAPI.MappingConfig.RegisterMaps().CreateMapper();

            // Act
            var result = await ProductService.GetProduct(product.Id, dbContext, mapper);

            // Assert
            Assert.Equal(product.Name, result.Name);
            Assert.Equal(product.Description, result.Description);
            Assert.Equal(product.Price, result.Price);
        }

        // Add test for ProductService.GetProduct in BookStore.ProductAPI When product is not found it should throw ProductNotFoundException
        [Fact]
        public async Task GetProduct_ProductNotFound()
        {
            // Arrange
            var dbContext = new ProductDbContext(new DbContextOptionsBuilder<ProductDbContext>()
                           .UseInMemoryDatabase("BookStore").Options);
            var mapper = BookStore.ProductAPI.MappingConfig.RegisterMaps().CreateMapper();

            // Act
            // Assert
            await Assert.ThrowsAsync<ProductNotFoundException>(() =>
                               ProductService.GetProduct(Guid.NewGuid(), dbContext, mapper));
        }

        //Add test for ProductService.DeleteProduct in BookStore.ProductAPI
        [Fact]
        public async Task DeleteProduct_BaseCase()
        {
            // Arrange
            var product = new Product
            {
                Name = "Test Product",
                Description = "Test Description",
                Price = 10.00m,
                ImageUrl = "https://test.com"
            };
            var dbContext = new ProductDbContext(new DbContextOptionsBuilder<ProductDbContext>()
                                      .UseInMemoryDatabase("BookStore").Options);
            dbContext.Products.Add(product);
            await dbContext.SaveChangesAsync();

            // Act
            await ProductService.DeleteProduct(product.Id, dbContext);

            // Assert
            Assert.Empty(dbContext.Products);
        }

        // Add test for ProductService.DeleteProduct in BookStore.ProductAPI When product is not found it should throw ProductNotFoundException
        [Fact]
        public async Task DeleteProduct_ProductNotFound()
        {
            // Arrange
            var dbContext = new ProductDbContext(new DbContextOptionsBuilder<ProductDbContext>()
                                      .UseInMemoryDatabase("BookStore").Options);

            // Act
            // Assert
            await Assert.ThrowsAsync<ProductNotFoundException>(() =>
                                              ProductService.DeleteProduct(Guid.NewGuid(), dbContext));
        }
    }
}
