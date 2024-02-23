using System.ComponentModel.DataAnnotations;

namespace BookStore.ProductAPI.Data
{
    public class Product
    {
        public Guid Id { get; set; }

        [Required]
        public string Name { get; set; }

        public decimal Price { get; set; }

        public string Description { get; set; }

        public string ImageUrl { get; set; }
    }

    public class ProductDto
    {
        public Guid Id { get; set; }

        public string Name { get; set; }
    }

    public class ResponseDto<T>
    {
        public bool IsSuccess { get; set; }

        public T Result { get; set; }

        public string DisplayMessage { get; set; }
    }

}
