namespace APIStore.Controllers;

using APIStore.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

[ApiController]
[Route("Users")]
public class UserController : ControllerBase {
  public UserService UserService { get; set; }

  public UserController(UserService userService) {
    UserService = userService;
  }

  [HttpDelete("{id:guid}")]
  [Authorize("Admin")]
  public async Task<ActionResult<IdentityResult>> DeleteUser(Guid id) {
    try {
      IdentityResult result = await UserService.DeleteUser(id);
      return Ok(result);
    } catch (Exception) {
      return NotFound("User with that id wasn't found.");
    }
  }
}