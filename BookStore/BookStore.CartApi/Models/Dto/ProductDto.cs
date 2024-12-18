﻿namespace BookStore.CartApi.Models.Dto;

public class ProductDto
{
    public Guid Id { get; set; }

    public string Name { get; set; }

    public decimal Price { get; set; }

    public string Description { get; set; }

    public string ImageUrl { get; set; }
}