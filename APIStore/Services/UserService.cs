namespace APIStore.Services;

using APIStore.Repositories.Abstracts;
using Microsoft.AspNetCore.Identity;

public class UserService {
  public IUserRepository UserRepository { get; set; }

  public UserService(IUserRepository userRepository) {
    UserRepository = userRepository;
  }

  public async Task<IdentityResult> DeleteUser(Guid id) {
    IdentityResult? result = await UserRepository.DeleteUser(id);
    if (result == null) {
      throw new Exception("User with that Id wasn't found.");
    }

    return result;
  }
}