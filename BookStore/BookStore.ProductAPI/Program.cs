using AutoMapper;
using BookStore.ProductAPI;
using BookStore.ProductAPI.Data;
using Microsoft.AspNetCore.Http.HttpResults;
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

var mapper = MappingConfig.RegisterMaps().CreateMapper();
builder.Services.AddSingleton(mapper);
builder.Services.AddAutoMapper(AppDomain.CurrentDomain.GetAssemblies());

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();


app.MapPost("/products", async ([FromBody] ProductDto productDto, [FromServices] ProductDbContext dbContext, [FromServices] IMapper mapper) =>
    {
        var product = mapper.Map<ProductDto, Product>(productDto);

        dbContext.Products.Add(product);
        await dbContext.SaveChangesAsync();
    })
    .WithName("AddProduct")
    .WithOpenApi();

app.MapGet("/products/{id}", async ([FromRoute] Guid id, [FromServices] ProductDbContext dbContext, [FromServices] IMapper mapper) =>
    {
        var product =  await dbContext.Products.FirstOrDefaultAsync(p => p.Id == id);
        if (product != null) return mapper.Map<Product, ProductDto>(product);

        return null;
    })
    .WithName("GetProduct")
    .WithOpenApi();

app.MapGet("/products", async ([FromServices] ProductDbContext dbContext, [FromServices] IMapper mapper) =>
    {
        var products = await dbContext.Products.ToListAsync();
        return products.Select(mapper.Map<Product, ProductDto>);
    })
    .WithName("GetProducts")
    .WithOpenApi();

app.Run();


