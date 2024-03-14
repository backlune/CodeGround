using System.ComponentModel.DataAnnotations;

namespace BookStore.CartApi.Models
{
    public class Product
    {
        public Guid ProductId { get; set; }

        [Required]
        public string Name { get; set; }

        public decimal Price { get; set; }

        public string Description { get; set; }

        public string ImageUrl { get; set; }
    }

}
