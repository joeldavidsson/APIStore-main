namespace APIStore.Repositories.Implementations;

using APIStore.Models;
using APIStore.Repositories.Abstracts;
using Microsoft.AspNetCore.Identity;

public class UserRepository : IUserRepository {
  public UserManager<User> UserManager { get; set; }

  public UserRepository(UserManager<User> userManager) {
    UserManager = userManager;
  }

  public async Task<IdentityResult?> DeleteUser(Guid id) {
    User? user = await UserManager.FindByIdAsync(id.ToString());
    if (user == null) {
      return null;
    }

    IdentityResult result = await UserManager.DeleteAsync(user);
    return result;
  }
}