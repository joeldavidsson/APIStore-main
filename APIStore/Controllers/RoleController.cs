namespace APIStore.Controllers;


using APIStore.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

[ApiController]
[Route("roles")]
public class RoleController : ControllerBase {
  public RoleService RoleService { get; set; }

  public RoleController(RoleService roleService) {
    RoleService = roleService;
  }

  [HttpPost("")]
  [Authorize]
  public async Task<ActionResult<IdentityResult>> AddRole([FromQuery] string role) {
    try
    {
      IdentityResult result = await RoleService.AddRole(role);
      return Ok(result); 
    }
    catch (Exception)
    {
      return BadRequest("Failed to add the specified role.");
    }
  }

  [HttpPut("")]
  [Authorize]
  public async Task<ActionResult<IdentityResult>> AddRoleToUser([FromQuery] string role) {
    try
    {
      IdentityResult result = await RoleService.AddRoleToUser(User, role);
      return Ok(result);
    }
    catch (Exception)
    {
      return BadRequest("Failed to add role to user.");
    }
  }
}