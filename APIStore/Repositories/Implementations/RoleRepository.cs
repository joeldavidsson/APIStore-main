namespace APIStore.Repositories.Implementations;

using System.Security.Claims;
using APIStore.Data;
using APIStore.Models;
using APIStore.Repositories.Abstracts;
using Microsoft.AspNetCore.Identity;

public class RoleRepository : IRoleRepository
{
    public DataContext Context { get; set; }
    public UserManager<User> UserManager { get; set; }
    public RoleManager<Role> RoleManager { get; set; }

    public RoleRepository(DataContext context, UserManager<User> userManager, RoleManager<Role> roleManager) {
      Context = context;
      UserManager = userManager;
      RoleManager = roleManager;
    }

    public async Task<IdentityResult?> AddRole(string role)
    {
        IdentityResult newRole = await RoleManager.CreateAsync(new Role(role));
        return newRole;
    }

    public async Task<IdentityResult?> AddRoleToUser(ClaimsPrincipal principal, string role)
    {
        string? userId = principal.FindFirstValue(ClaimTypes.NameIdentifier);
        if (string.IsNullOrEmpty(userId)) {
          return null;
        }

        User? user = await UserManager.FindByIdAsync(userId);
        if (user == null) {
          return null;
        }

        IdentityResult result = await UserManager.AddToRoleAsync(user, role);
        return result;
    }
}