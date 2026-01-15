using login.Application.Services;
using Microsoft.AspNetCore.Mvc;

namespace login.Api.Controllers;

[ApiController]
[Route("api/auth")]
public class AuthController : ControllerBase
{
    private readonly AuthServices _service;

    public AuthController(AuthServices service)
    {
        _service = service;
    }

    [HttpPost("login")]
    public async Task<IActionResult> Login(LoginRequest request)
    {
        var token = await _service.Authenticate(request.Name, request.Password);
        if (token == null)
        {
            return Unauthorized(new { message = "El usuario y la contraseña no coinciden." });
        }

        return Ok(token);
    }

    public record LoginRequest(string Name, string Password);

    [HttpPost("register")]
    public async Task<IActionResult> Register(RegisterRequest request)
    {
        var sucess = await _service.Register(request.Name, request.Password, request.Email,request.Role);
        if (!sucess)
            return BadRequest(new { message = "El usuario ya existe." });
        return Ok(new { message = "Registro creado correctamente." });
    }

    public record RegisterRequest(string Name, string Password, string Email, string Role);
}