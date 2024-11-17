namespace APIStore.Data;

using APIStore.Models;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

public class DataContext : IdentityDbContext<User, Role, string>
{
    public DbSet<Product> Products { get; set; }
    public DbSet<Category> Categories { get; set; }

    public DataContext(DbContextOptions<DataContext> options)
        : base(options) { }
}
