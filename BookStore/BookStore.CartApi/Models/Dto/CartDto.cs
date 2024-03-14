namespace BookStore.CartApi.Models.Dto;

class CartDto
{
    public CartHeaderDto CartHeader { get; set; }

    public IEnumerable<CartDetailsDto> CartDetails { get; set; }
}