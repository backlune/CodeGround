using BookStore.CartApi.Models;

public class CartDto
{
    public CartHeader CartHeader { get; set; }

    public IEnumerable<CartDetails> CartDetails { get; set; }
}