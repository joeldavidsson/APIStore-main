namespace Tests.Factory;

using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc.Testing;
using Microsoft.AspNetCore.TestHost;
using Microsoft.Extensions.DependencyInjection.Extensions;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Authentication;
using Microsoft.Extensions.Options;
using Microsoft.Extensions.Logging;
using System.Text.Encodings.Web;
using System.Security.Claims;
using Microsoft.AspNetCore.Identity;
using APIStore.Data;
using APIStore.Models;

public class ApplicationFactory<T> : WebApplicationFactory<T> where T : class
{
  protected override void ConfigureWebHost(IWebHostBuilder builder)
  {
    builder.ConfigureTestServices(services =>
    {
      var dbContextDescriptor = services.SingleOrDefault(
      d => d.ServiceType == typeof(DbContextOptions<DataContext>));
      services.Remove(dbContextDescriptor);

      var dbConnDescriptor = services.SingleOrDefault(
          d => d.ServiceType == typeof(DataContext)
      );
      services.Remove(dbConnDescriptor);
      
      services.AddDbContext<DataContext>(options =>
      {
        var path = Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData);
        options.UseSqlite($"Data Source={Path.Join(path, "APIStoreTestDb.db")}");
      });

      services.AddAuthentication("TestScheme").AddScheme<AuthenticationSchemeOptions, TestAuthHandler>(
        "TestScheme",
        options => { }
      );

      var provider = services.BuildServiceProvider();
      var scope = provider.CreateScope();
      var context = scope.ServiceProvider.GetRequiredService<DataContext>();

      context.Database.EnsureDeleted();
      context.Database.EnsureCreated();

      User user = new User
      {
        Email = "test@example.com",
        Id = "userId"
      };

      context.Users.Add(user);
      context.SaveChanges();
    });
  }
}

public class TestAuthHandler : AuthenticationHandler<AuthenticationSchemeOptions>
{
  public TestAuthHandler(IOptionsMonitor<AuthenticationSchemeOptions> options, ILoggerFactory logger, UrlEncoder encoder) : base(options, logger, encoder) { }

  protected override Task<AuthenticateResult> HandleAuthenticateAsync()
  {
    var claims = new[] {
      new Claim(ClaimTypes.Name, "user"),
      new Claim(ClaimTypes.NameIdentifier, "userId")
    };

    var identity = new ClaimsIdentity(claims, "Test");
    var principal = new ClaimsPrincipal(identity);
    var ticket = new AuthenticationTicket(principal, "TestScheme");

    var result = AuthenticateResult.Success(ticket);
    return Task.FromResult(result);
  }
}