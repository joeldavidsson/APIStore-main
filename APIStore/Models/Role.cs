namespace APIStore.Models;

using Microsoft.AspNetCore.Identity;

public class Role : IdentityRole {
  public Role(string name) : base(name) {}
}