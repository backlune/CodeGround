using AutoMapper;
using BookStore.ProductAPI.Data;
using Microsoft.EntityFrameworkCore;

namespace BookStore.ProductAPI;

public class ProductService
{
    public static async Task<Product> AddProduct(IMapper mapper, ProductDto productDto, ProductDbContext dbContext)
    {
        var product = mapper.Map<ProductDto, Product>(productDto);

        dbContext.Products.Add(product);
        await dbContext.SaveChangesAsync();
        return product;
    }

    public static async Task<Product> GetProduct(Guid id, ProductDbContext dbContext, IMapper mapper)
    {
        var product = await dbContext.Products.FirstOrDefaultAsync(p => p.Id == id);
        // throw ProductNotFoundException exception if product is null
        if (product == null)
        {
            throw new ProductNotFoundException();
        }
        return product;
    }

    public static async Task DeleteProduct(Guid id, ProductDbContext dbContext)
    {
        var product = await dbContext.Products.FirstOrDefaultAsync(p => p.Id == id);
        if (product == null)
        {
            throw new ProductNotFoundException();
        }
        dbContext.Products.Remove(product);
        await dbContext.SaveChangesAsync();
    }
}