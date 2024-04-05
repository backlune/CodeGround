using IO.Swagger.Api;
using IO.Swagger.Model;

namespace BookStore.App.Server.Services;

public class CartService
{
    private CartApi client = new(Configs.CartAPIBase);

    public CartService(string apiKey)
    {
        client.Configuration.ApiKey = new Dictionary<string, string> { { "Authorization", "Bearer " + apiKey } };
    }



    public async Task<CartHeaderDto> GetCartHeaderByUserIdIdAsync<T>(Guid id)
    {
        return await client.GetCartAsync(id);
    }

    public async Task AddCart<T>(CartDto Cart)
    {
        await client.AddCartAsync(Cart);
    }

    public async Task UpdateCart<T>(CartDto Cart)
    {
        await client.UpdateCartAsync(Cart,Cart.CartHeader.Id);
    }

    public async Task DeleteCartAsync(Guid CartId)
    {
        await client.DeleteCartAsync(CartId);
    }
}