using AutoMapper;
using BookStore.CartApi.Models;
using BookStore.CartApi.Models.Dto;
using Microsoft.EntityFrameworkCore;

namespace BookStore.CartApi;

public class CartService
{
    public static async Task<CartHeaderDto> GetCart(Guid userId, CartDbContext dbContext, IMapper mapper)
    {
        var cartHeader = await dbContext.CartHeaders.SingleAsync(c => c.UserId == userId); // Can be more than one but shouldn't
        return mapper.Map<CartHeaderDto>(cartHeader);
    }

    public static async Task<CartDto> AddCart(CartDto cartDto, CartDbContext dbContext, IMapper mapper)
    {
        var cart = mapper.Map<Cart>(cartDto);
        await dbContext.CartHeaders.AddAsync(cart.CartHeader);
        await dbContext.CartDetails.AddRangeAsync(cart.CartDetails);
        await dbContext.SaveChangesAsync();
        return cartDto;
    }

    // Implement UpdateCart method
    public static async Task<CartDto> UpdateCart(Guid cartHeaderId, CartDto cartDto, CartDbContext dbContext,
        IMapper mapper)
    {
        var cart = await dbContext.CartHeaders.Include(c => c.CartDetails).SingleAsync(c => c.Id == cartHeaderId);
            
        if (cart.UserId != cartDto.CartHeader.UserId)
        {
            throw new InvalidOperationException("UserId cannot be changed");
        }

        cart.CartDetails = cartDto.CartDetails.Select(mapper.Map<CartDetails>).ToList();
        await dbContext.SaveChangesAsync();

        return cartDto;
    }

    // Implement DeleteCart method
    public static async Task DeleteCart(Guid cartHeaderId, CartDbContext dbContext)
    {
        var cartHeader = await dbContext.CartHeaders.SingleAsync(c => c.Id == cartHeaderId);
        dbContext.CartHeaders.Remove(cartHeader);
        await dbContext.SaveChangesAsync();
    }
}