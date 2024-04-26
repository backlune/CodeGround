using AutoMapper;
using Xunit;
using Microsoft.EntityFrameworkCore;
using BookStore.CartApi;
using BookStore.CartApi.Models;
using BookStore.CartApi.Models.Dto;

namespace BookStore.Tests;

public class CartServiceTests
{

    // Implement tests for the CartService.GetCartById method
    [Fact]
    public async Task GetCartById_ReturnsCartHeader()
    {
        // Arrange
        var options = new DbContextOptionsBuilder<CartDbContext>()
            .UseInMemoryDatabase(databaseName: "GetCartById_ReturnsCartHeader")
            .Options;

        using (var dbContext = new CartDbContext(options))
        {
            dbContext.CartHeaders.Add(new CartHeader { Id = Guid.NewGuid(), UserId = Guid.NewGuid() });
            await dbContext.SaveChangesAsync();
        }

        using (var dbContext = new CartDbContext(options))
        {
            var mapper = BookStore.CartApi.MappingConfig.RegisterMaps().CreateMapper();

            // Act
            var cartHeader = await CartService.GetCart(Guid.NewGuid(), dbContext, mapper);

            // Assert
            Assert.NotNull(cartHeader);
        }
    }

    // Implement tests for the CartService.AddCart method
    [Fact]
    public async Task AddCart_ReturnsCartDto()
    {
        // Arrange
        var options = new DbContextOptionsBuilder<CartDbContext>()
            .UseInMemoryDatabase(databaseName: "AddCart_ReturnsCartDto")
            .Options;

        var cartDto = new CartDto
        {
            CartHeader = new CartHeaderDto { UserId = Guid.NewGuid() },
            CartDetails = new List<CartDetailsDto>
            {
                new CartDetailsDto { ProductId = Guid.NewGuid(), Count = 1 }
            }
        };

        using (var dbContext = new CartDbContext(options))
        {
            var mapper = BookStore.CartApi.MappingConfig.RegisterMaps().CreateMapper();

            // Act
            var result = await CartService.AddCart(cartDto, dbContext, mapper);

            // Assert
            Assert.NotNull(result);
        }
    }

}