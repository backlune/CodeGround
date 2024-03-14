using BookStore.CartApi.Models;
using Microsoft.EntityFrameworkCore;

public class CartDbContext : DbContext
{
    public CartDbContext(DbContextOptions<CartDbContext> options) : base(options)
    {
        
    }
    
    public DbSet<CartHeader> CartHeaders { get; set; }
    public DbSet<CartDetails> CartDetails { get; set; }
    public DbSet<Product> Products { get; set; }
}