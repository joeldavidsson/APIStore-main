namespace APIStore.Services;

using System.Security.Claims;
using APIStore.Repositories.Abstracts;
using Microsoft.AspNetCore.Identity;

public class RoleService {
  public IRoleRepository RoleRepository { get; set; }

  public RoleService(IRoleRepository roleRepository) {
    RoleRepository = roleRepository;
  }

  public async Task<IdentityResult> AddRole(string role) {
    IdentityResult? result = await RoleRepository.AddRole(role);
    if (result == null) {
      throw new Exception();
    }

    return result;
  }

  public async Task<IdentityResult> AddRoleToUser(ClaimsPrincipal principal, string role) {
     IdentityResult? result = await RoleRepository.AddRoleToUser(principal, role);
     if (result == null) {
      throw new Exception();
     }

     return result;
  }
}