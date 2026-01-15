using login.Application.Services;
using login.Domain.Models;
using Microsoft.AspNetCore.Mvc;
using login.Application.Services;
using login.Domain.Models;
using Microsoft.AspNetCore.Authorization;

namespace login.Api.Controllers;

[ApiController]
[Route("api/[controller]")]

public class UserController :  ControllerBase
{
    private readonly UserService _userService;
    public UserController(UserService userService)
    {
        _userService = userService;
    }
    
    [HttpGet]
    public async Task<IActionResult> GetAllUsers()
    {
        var user = await _userService.GetAllUsersAsync();
        return Ok(user);
    }
    
    [HttpGet("{id}")]
    public async Task<IActionResult> GetUserById(int id)
    {
        var user = await _userService.GetUserAsync(id);
        return Ok(user);
    }
    
    [HttpPost]
    public async Task<IActionResult> AddUser(User user)
    {
        await _userService.AddUserAsync(user);
        return Ok();
    }
    
    [HttpPut("{id}")]
    public async Task<IActionResult> UpdateUser(int id, User user)
    {
        user.Id = id;
        await _userService.UpdateUserAsync(user);
        return Ok(user);
    }
    
    [HttpDelete("{id}")]
    public async Task<IActionResult> DeleteUser(int id)
    {
        await _userService.DeleteUserAsync(id);
        return Ok();
    }
}