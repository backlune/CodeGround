using AutoMapper;
using BookStore.CartApi.Models;
using BookStore.CartApi.Models.Dto;

public static class MappingConfig
{
    public static MapperConfiguration RegisterMaps()
    {
        var mappingConfig = new MapperConfiguration(config =>
        {
            config.CreateMap<CartDto, Cart>();
            config.CreateMap<Cart, CartDto>();


            config.CreateMap<CartHeaderDto, CartHeader>();
            config.CreateMap<CartHeader, CartHeaderDto>();

            config.CreateMap<CartDetailsDto, CartDetails>();
            config.CreateMap<CartDetails, CartDetailsDto>();

            config.CreateMap<ProductDto, Product>();
            config.CreateMap<Product, ProductDto>();

        });

        return mappingConfig;
    }
}