using AutoMapper;
using BookStore.CartApi.Models;
using BookStore.CartApi.Models.Dto;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace BookStore.CartApi
{
    public static class CartApi
    {
        public static RouteGroupBuilder MapCartApi(this RouteGroupBuilder group)
        {
            group.MapGet("/{userId}", async ([FromServices] CartDbContext dbContext, [FromServices] IMapper mapper, Guid userId) =>
                {
                    var cart = await dbContext.CartHeaders.SingleAsync(c => c.UserId == userId); // Can be more than one but shouldn't
                    return Results.Ok(mapper.Map<CartHeader, CartHeaderDto>(cart));
                })
                .WithName("GetCart")
                .RequireAuthorization()
                .WithOpenApi();


            group.MapPost("/",
                    async ([FromServices] CartDbContext dbContext, [FromServices] IMapper mapper, [FromBody] CartDto cartDto) =>
                    {
                        var cart = mapper.Map<Cart>(cartDto);
                        
                        await dbContext.CartHeaders.AddAsync(cart.CartHeader);
                        await dbContext.CartDetails.AddRangeAsync(cart.CartDetails);
                        await dbContext.SaveChangesAsync();

                        return Results.Ok(cartDto); // Cheating!
                    })  
                .WithName("AddCart")
                .RequireAuthorization()
                .WithOpenApi();

            group.MapPut("/{cartHeaderId}",
                    async ([FromServices] CartDbContext dbContext, [FromServices] IMapper mapper, Guid cartHeaderId, [FromBody] CartDto cartDto) =>
                    {
                        var cart = mapper.Map<Cart>(cartDto);

                        await dbContext.CartHeaders.AddAsync(cart.CartHeader);
                        await dbContext.CartDetails.AddRangeAsync(cart.CartDetails);
                        await dbContext.SaveChangesAsync();

                        return Results.Ok(cartDto); // Cheating!
                    })
                .WithName("UpdateCart")
                .RequireAuthorization()
                .WithOpenApi();

            group.MapDelete("/{cartHeaderId}",
                    async ([FromServices] CartDbContext dbContext, [FromServices] IMapper mapper, Guid cartHeaderId) =>
                    {
                        var cartHeader = await dbContext.CartHeaders.SingleAsync(c => c.Id == cartHeaderId);
                        dbContext.CartHeaders.Remove(cartHeader);
                        await dbContext.SaveChangesAsync();

                        return Results.Ok(); 
                    })
                .WithName("DeleteCart")
                .RequireAuthorization()
                .WithOpenApi();

            return group;
        }
    }
}
