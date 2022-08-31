using Authentication.Core.DTOs;
using Authentication.Core.Services;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Authentication.API.Controllers;

[Route("api/[controller]/[Action]")]
[ApiController]
public class UserController : CustomBaseController
{
    private readonly IUserService _userService;

    public UserController(IUserService userService)
    {
        _userService = userService;
    }

    //api/user
    [HttpPost]
    public async Task<IActionResult> CreateUser(CreateUserDto createUserDto)
    {
        return ActionResultInstance(await _userService.CreateUserAsync(createUserDto));
    }

    [Authorize]
    [HttpGet]
    public async Task<IActionResult> GetUser()
    {
        return ActionResultInstance(await _userService.GetUserByNameAsync(HttpContext.User.Identity.Name));
    }

    
    
    [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme, Roles = "Admin")]
    [HttpPost]
    public async Task<IActionResult> CreateRoleUser(CreateRoleDto roleName)
    {
        return ActionResultInstance(await _userService.CreateRole(roleName));
    }
    
    [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme, Roles = "Admin")]
    [HttpPost]
    public async Task<IActionResult> AddUserRole(UserRoleMappingDto userRoleMappingDto)
    {
        return ActionResultInstance(await _userService.AddUserRoleMapping(userRoleMappingDto));
    }
    
    [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme, Roles = "Admin")]
    [HttpGet]
    public async Task<IActionResult> GetUserRole(string email)
    {
        return ActionResultInstance(await _userService.GetUserRoles(email));
    }
    
    [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme, Roles = "Admin")]
    [HttpPost]
    public async Task<IActionResult> RemoveUserFromRole(UserRoleMappingDto userRoleMappingDto)
    {
        return ActionResultInstance(await _userService.RemoveUserFromRole(userRoleMappingDto));
    }
    
}