namespace BookStore.CartApi.Models.Dto;

public class CartHeaderDto
{
    public Guid Id { get; set; }

    public Guid UserId { get; set; }

    public string? CouponCode { get; set; }
}