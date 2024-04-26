using AutoMapper;
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
                var product = await ProductService.AddProduct(mapper, productDto, dbContext);

                return TypedResults.Ok(mapper.Map<Product, ProductDto>(product));
            })
            .WithName("AddProduct")
            .RequireAuthorization()
            .WithOpenApi();

        group.MapGet("{id}", async ([FromRoute] Guid id, [FromServices] ProductDbContext dbContext, [FromServices] IMapper mapper) =>
            {
                // return BadResult if ProductNotFoundException is thrown
                try
                {
                    var product = await ProductService.GetProduct(id, dbContext, mapper);

                    return TypedResults.Ok(mapper.Map<Product, ProductDto>(product));
                }
                catch (ProductNotFoundException)
                {
                    return Results.NotFound();
                }

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
                    // Use ProductService.DeleteProduct to delete the product and return NotFound if ProductNotFoundException is thrown
                    try
                    {
                        await ProductService.DeleteProduct(id, dbContext);
                        return Results.Ok();
                    }
                    catch (ProductNotFoundException)
                    {
                        return Results.NotFound();
                    }
                })
            .WithName("DeleteProduct")
            .RequireAuthorization("Admin")
            .WithOpenApi()
            .Produces(StatusCodes.Status200OK)
            .Produces(StatusCodes.Status404NotFound);

        return group;
    }

    
}