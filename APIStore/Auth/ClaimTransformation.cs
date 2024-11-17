namespace APIStore.Auth;

using System.Security.Claims;
using APIStore.Models;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Identity;

public class ClaimTransformation : IClaimsTransformation {
  UserManager<User> UserManager { get; set; }

  public ClaimTransformation(UserManager<User> userManager) {
    UserManager = userManager;
  }

  public async Task<ClaimsPrincipal> TransformAsync(ClaimsPrincipal principal) {
    ClaimsIdentity claims = new ClaimsIdentity();

    var id = principal.FindFirstValue(ClaimTypes.NameIdentifier);
    if (id != null) {
      var user = await UserManager.FindByIdAsync(id);
      if (user != null) {
        var userRoles = await UserManager.GetRolesAsync(user);
        foreach (string userRole in userRoles) {
          claims.AddClaim(new Claim(ClaimTypes.Role, userRole));
        }
      }
    }

    principal.AddIdentity(claims);
    return await Task.FromResult(principal);
  }
}