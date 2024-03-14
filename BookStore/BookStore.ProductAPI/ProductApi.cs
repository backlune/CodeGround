using AutoMapper;
using BookStore.Common;
using BookStore.ProductAPI.Data;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace BookStore.ProductAPI;

public static class ProductApi
{
    public static RouteGroupBuilder MapProductsApi(this RouteGroupBuilder group)
    {
        group.MapPost("", async ([FromBody] ProductDto productDto, [FromServices] ProductDbContext dbContext, [FromServices] IMapper mapper) =>
            {
                var product = mapper.Map<ProductDto, Product>(productDto);

                dbContext.Products.Add(product);
                await dbContext.SaveChangesAsync();

                return TypedResults.Ok(mapper.Map<Product, ProductDto>(product));
            })
            .WithName("AddProduct")
            .RequireAuthorization()
            .WithOpenApi();

        group.MapGet("{id}", async ([FromRoute] Guid id, [FromServices] ProductDbContext dbContext, [FromServices] IMapper mapper) =>
            {
                var product = await dbContext.Products.FirstOrDefaultAsync(p => p.Id == id);
                if (product != null)
                {
                    return TypedResults.Ok(mapper.Map<Product, ProductDto>(product));
                }

                return Results.NotFound(); ;
            })
            .WithName("GetProduct")
            .RequireAuthorization()
            .WithOpenApi()
            .Produces<ProductDto>(StatusCodes.Status200OK)
            .Produces(StatusCodes.Status404NotFound);

        group.MapGet("", async ([FromServices] ProductDbContext dbContext, [FromServices] IMapper mapper) =>
            {
                var products = await dbContext.Products.ToListAsync();
                return products.Select(mapper.Map<Product, ProductDto>);
            })
            .WithName("GetProducts")
            .RequireAuthorization()
            .WithOpenApi();

        group.MapDelete("{id}",
                async ([FromRoute] Guid id, [FromServices] ProductDbContext dbContext, [FromServices] IMapper mapper) =>
                {
                    var product = await dbContext.Products.FirstOrDefaultAsync(p => p.Id == id);
                    if (product == null)
                    {
                        return Results.NotFound();
                    }

                    dbContext.Products.Remove(product);
                    await dbContext.SaveChangesAsync();
                    return Results.Ok();

                })
            .WithName("DeleteProduct")
            .RequireAuthorization("Admin")
            .WithOpenApi()
            .Produces(StatusCodes.Status200OK)
            .Produces(StatusCodes.Status404NotFound);

        return group;
    }
}