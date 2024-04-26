namespace BookStore.CartApi.Models;

public class CartHeader
{
    public Guid Id { get; set; }

    public Guid UserId { get; set; }

    public ICollection<CartDetails> CartDetails { get; set; }
}