namespace APIStore;


using APIStore.Auth;
using APIStore.Data;
using APIStore.Repositories.Abstracts;
using APIStore.Repositories.Implementations;
using APIStore.Models;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using APIStore.Services;

public class Program
{
    public static void Main(string[] args)
    {
        var builder = WebApplication.CreateBuilder(args);

        AddDb(builder);
        AddAuth(builder);
        builder.Services.AddTransient<IClaimsTransformation, ClaimTransformation>();
        
        builder.Services.AddScoped<UserService, UserService>();
        builder.Services.AddScoped<CategoryService, CategoryService>();
        builder.Services.AddScoped<ProductService, ProductService>();
        builder.Services.AddScoped<RoleService, RoleService>();
        builder.Services.AddScoped<IUserRepository, UserRepository>();
        builder.Services.AddScoped<ICategoryRepository, CategoryRepository>();
        builder.Services.AddScoped<IProductRepository, ProductRepository>();
        builder.Services.AddScoped<IRoleRepository, RoleRepository>();
        builder.Services.AddControllers();
        AddSecurity(builder);

        builder.Services.AddEndpointsApiExplorer();
        builder.Services.AddSwaggerGen();

        var app = builder.Build();

        if (app.Environment.IsDevelopment())
        {
            app.UseSwagger();
            app.UseSwaggerUI();
        }

        app.UseHttpsRedirection();
        app.UseAuthentication();
        app.UseAuthorization();
        app.MapControllers();
        app.MapIdentityApi<User>();

        app.Run();
    }

    public static void AddAuth(WebApplicationBuilder builder)
    {
        builder.Services.AddAuthentication().AddBearerToken(IdentityConstants.BearerScheme);
        builder.Services.AddAuthorization(options =>
        {
            options.AddPolicy("Auth", policy =>
            {
                policy.RequireAuthenticatedUser();
            });
            options.AddPolicy("Admin", policy =>
            {
                policy.RequireAuthenticatedUser().RequireRole("Admin");
            });
        });
    }

    public static void AddDb(WebApplicationBuilder builder)
    {
        var config = builder.Configuration;
        builder.Services.AddDbContext<DataContext>(options =>
        {
            options.UseNpgsql(config.GetConnectionString("Default"));
        });
    }

    public static void AddSecurity(WebApplicationBuilder builder) {
        builder.Services.AddIdentityCore<User>().AddRoles<Role>().AddEntityFrameworkStores<DataContext>().AddApiEndpoints();
    }
}
