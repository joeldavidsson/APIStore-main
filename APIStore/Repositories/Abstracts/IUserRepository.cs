namespace APIStore.Repositories.Abstracts;

using APIStore.Models;
using Microsoft.AspNetCore.Identity;

public interface IUserRepository {
  Task<IdentityResult?> DeleteUser(Guid id);
}