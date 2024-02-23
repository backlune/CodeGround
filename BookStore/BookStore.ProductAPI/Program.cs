using BookStore.ProductAPI.Data;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddDbContext<ProductDbContext>(options =>
    options.UseSqlite(builder.Configuration.GetConnectionString("SqliteConnection")));
//builder.Services.AddDbContextFactory<ProductDbContext>()

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();


app.MapPost("/products", async ([FromBody] ProductDto product, [FromServices] ProductDbContext dbContext) =>
{
    var p = new Product
    {
        Name = product.Name,
        Description = "A product",
        Price = 10,
        ImageUrl = "https://codeproject.freetls.fastly.net/App_Themes/CodeProject/Img/logo250x135.gif"
    };

    dbContext.Products.Add(p);
    await dbContext.SaveChangesAsync();
})
    .WithName("AddProduct")
    .WithOpenApi();

app.MapGet("/products/{id}", async ([FromRoute] Guid id, [FromServices] ProductDbContext dbContext) =>
{
    return await dbContext.Products.FirstOrDefaultAsync(p => p.Id == id);
})
    .WithName("GetProduct")
    .WithOpenApi();

app.MapGet("/products", async ([FromServices] ProductDbContext dbContext) =>
    {
        return await dbContext.Products.ToListAsync();
    })
    .WithName("GetProducts")
    .WithOpenApi();

app.Run();


