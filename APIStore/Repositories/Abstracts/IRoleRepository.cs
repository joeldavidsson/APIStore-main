namespace APIStore.Repositories.Abstracts;

using System.Security.Claims;
using Microsoft.AspNetCore.Identity;

public interface IRoleRepository {
  Task<IdentityResult?> AddRole(string role);
  Task<IdentityResult?> AddRoleToUser(ClaimsPrincipal principal, string role);
}