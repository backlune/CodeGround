namespace BookStore.CartApi.Models;

public class CartDetails
{
    public Guid Id { get; set; }

    public Guid CartHeaderId  { get; set; }
    public CartHeader CartHeader { get; set; }

    public Guid ProductId { get; set; }
    public Product Product { get; set; }

    public int Count { get; set; }
}