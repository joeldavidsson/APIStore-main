namespace Tests;

using Tests.Factory;
using System.Net.Http.Json;
using Microsoft.AspNetCore.Mvc.Testing;
using APIStore.Data;
using APIStore.Dtos;
using APIStore.Models;
using APIStore.Services;
using System.Security.Claims;
using System.Text.Encodings.Web;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.TestHost;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;

public class IntegrationTests : IClassFixture<ApplicationFactory<APIStore.Program>>
{
    private readonly ApplicationFactory<APIStore.Program> Factory;

    public IntegrationTests(ApplicationFactory<APIStore.Program> factory)
    {
        Factory = factory;
    }

    [Fact]
    public async Task DeleteCategory()
    {
        var scope = Factory.Services.CreateScope();
        var context = scope.ServiceProvider.GetRequiredService<DataContext>();
        var client = Factory.CreateClient();

        context.Database.EnsureDeleted();
        context.Database.EnsureCreated();

        var categoryService = scope.ServiceProvider.GetRequiredService<CategoryService>();
        string name = "Godis";
        categoryService.CreateCategory(name);
        var category = new Category(name);

        var response = await client.DeleteAsync($"/categories/delete/{category.Id}");

        Assert.True(response.IsSuccessStatusCode);
    }

    [Fact]
    public async Task CreateProduct()
    {
        var scope = Factory.Services.CreateScope();
        var context = scope.ServiceProvider.GetRequiredService<DataContext>();

        context.Database.EnsureDeleted();
        context.Database.EnsureCreated();

        var categoryService = scope.ServiceProvider.GetRequiredService<CategoryService>();
        categoryService.CreateCategory("Leksak");
        var client = Factory.CreateClient();
        var category = new Category("Leksak");
        var product = new Product("Test", 1, 5, category);
        var dto = new ProductDto(product);

        var response = await client.PostAsJsonAsync<ProductDto>("/products/add", dto);
        var result = await response.Content.ReadFromJsonAsync<ProductDto>();

        response.EnsureSuccessStatusCode();
        Assert.NotNull(result);
        Assert.Equal("Test", result.Name);
        Assert.Equal(1, result.Price);
        Assert.Equal(5, result.Amount);
    }
}