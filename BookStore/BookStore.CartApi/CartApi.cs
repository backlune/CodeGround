using AutoMapper;
using BookStore.CartApi.Models.Dto;
using Microsoft.AspNetCore.Mvc;

namespace BookStore.CartApi
{
    public static class CartApi
    {
        public static RouteGroupBuilder MapCartApi(this RouteGroupBuilder group)
        {
            

            group.MapGet("/{userId}", async ([FromServices] CartDbContext dbContext, [FromServices] IMapper mapper, Guid userId) =>
                {
                    // Move below to a method in CartService
                    var cart = await CartService.GetCart(userId, dbContext, mapper);
                    return Results.Ok(cart);
                })
                .WithName("GetCart")
                .RequireAuthorization()
                .WithOpenApi()
                .Produces<CartHeaderDto>(StatusCodes.Status200OK);


            group.MapPost("/",
                    async ([FromServices] CartDbContext dbContext, [FromServices] IMapper mapper, [FromBody] CartDto cartDto) =>
                    {
                        // Move to CartService
                        var cart = CartService.AddCart(cartDto, dbContext, mapper);

                        return Results.Ok(cartDto);
                    })  
                .WithName("AddCart")
                .RequireAuthorization()
                .WithOpenApi()
                .Produces<CartDto>(StatusCodes.Status200OK);

            group.MapPut("/{cartHeaderId}",
                    async ([FromServices] CartDbContext dbContext, [FromServices] IMapper mapper, Guid cartHeaderId, [FromBody] CartDto cartDto) =>
                    {
                        await CartService.UpdateCart(cartHeaderId, cartDto, dbContext, mapper);
                        

                        return Results.Ok(cartDto);
                    })
                .WithName("UpdateCart")
                .RequireAuthorization()
                .WithOpenApi()
                .Produces<CartDto>(StatusCodes.Status200OK);

            group.MapDelete("/{cartHeaderId}",
                    async ([FromServices] CartDbContext dbContext, [FromServices] IMapper mapper, Guid cartHeaderId) =>
                    {
                        await CartService.DeleteCart(cartHeaderId, dbContext);

                        return Results.Ok(); 
                    })
                .WithName("DeleteCart")
                .RequireAuthorization()
                .WithOpenApi();

            return group;
        }
    }
}
