using BookStore.App.Server;
using BookStore.App.Server.Services;
using Duende.Bff.Yarp;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Mvc;
using System.IdentityModel.Tokens.Jwt;
using Microsoft.AspNetCore.Http.HttpResults;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.AddAuthorization();

builder.Services
    .AddBff()
    .AddRemoteApis();

JwtSecurityTokenHandler.DefaultMapInboundClaims = false;

builder.Services
    .AddAuthentication(options =>
    {
        options.DefaultScheme = "Cookies";
        options.DefaultChallengeScheme = "oidc";
        options.DefaultSignOutScheme = "oidc";
    })
    .AddCookie("Cookies")
    .AddOpenIdConnect("oidc", options =>
    {
        options.Authority = builder.Configuration["ServiceUrls:IdentityApi"];
        options.ClientId = "bff";
        options.ClientSecret = "secret";
        options.ResponseType = "code";
        options.Scope.Add("productApi");
        options.SaveTokens = true;
        options.GetClaimsFromUserInfoEndpoint = true;
        options.TokenValidationParameters.NameClaimType = "name";
        options.TokenValidationParameters.NameClaimType = "role";
    });

var app = builder.Build();

Configs.ProductAPIBase = builder.Configuration["ServiceUrls:ProductApi"] ??
                    throw new InvalidOperationException("ServiceUrls:ProductApi not found");
Configs.IdentityApi = builder.Configuration["ServiceUrls:IdentityApi"] ??
                 throw new InvalidOperationException("ServiceUrls:IdentityApi not found");

app.UseDefaultFiles();
app.UseStaticFiles();

app.UseRouting();
app.UseAuthentication();
app.UseBff();
app.UseAuthorization();


// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.MapBffManagementEndpoints();


app.MapGet("/products", async (HttpContext context) =>
{
    var accessToken = await context.GetTokenAsync("access_token") ?? throw new UnauthorizedAccessException("No token");
    var service = new ProductService(accessToken);
    return await service.GetAllProductsAsync();
})
.WithName("GetProducts")
.WithOpenApi();

app.MapDelete("/products/{id}", async (HttpContext context, [FromRoute] Guid id) =>
    {
        var accessToken = await context.GetTokenAsync("access_token") ?? throw new UnauthorizedAccessException("No token");
        var service = new ProductService(accessToken);
        await service.DeleteProductAsync(id);
    })
    .WithName("DeleteProducts")
    .WithOpenApi();

app.MapGet("/bff/users2", () => Results.Ok())
    .WithName("GetUSers")
    .WithOpenApi();

app.MapFallbackToFile("/index.html");

app.Run();