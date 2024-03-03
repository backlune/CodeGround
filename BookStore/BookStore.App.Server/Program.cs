using BookStore.App.Server;
using BookStore.App.Server.Services;
using Microsoft.AspNetCore.Mvc;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();

SD.ProductAPIBase = builder.Configuration["ServiceUrls:ProductApi"] ?? throw new InvalidOperationException("ServiceUrls:ProductApi not found");

app.UseDefaultFiles();
app.UseStaticFiles();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();



app.MapGet("/products", async () =>
{
    var service = new ProductService();
    return await service.GetAllProductsAsync();
})
.WithName("GetProducts")
.WithOpenApi();

app.MapDelete("/products/{id}", async ([FromRoute] Guid id) =>
    {
        var service = new ProductService();
        await service.DeleteProductAsync(id);
    })
    .WithName("DeleteProducts")
    .WithOpenApi();

app.MapFallbackToFile("/index.html");

app.Run();


