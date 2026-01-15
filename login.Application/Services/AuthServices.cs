using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using login.Domain.Interfaces;
using login.Domain.Models;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;

namespace login.Application.Services;

public class AuthServices
{
    private readonly IUserRepositorie _userRepository;
    private readonly IConfiguration _configuration;

    public AuthServices(IUserRepositorie userRepository, IConfiguration configuration)
    {
        _userRepository = userRepository;
        _configuration = configuration;
    }

    public async Task<bool> Register(string name, string password, string email, string role)
    {
        var existingUser = await _userRepository.GetUserByUsernameAsync(name);
        if (existingUser != null)
            return false;

        var user = new User
        {
            name = name,
            PasswordHash = BCrypt.Net.BCrypt.HashPassword(password),
            Email = email,
            Role = role
        };

        await _userRepository.RegisterUser(user);
        return true;
    }

    public async Task<object?> Authenticate(string name, string password)
    {
        var user = await _userRepository.GetUserByUsernameAsync(name);
        if(user == null) return null;

        bool passwordValid = BCrypt.Net.BCrypt.Verify(password, user.PasswordHash);
        if (!passwordValid) return null;

        var claims = new[]
        {
            new Claim(JwtRegisteredClaimNames.Sub, user.name),
            new Claim(ClaimTypes.Role, user.Role),
            new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString())
        };
    
        var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_configuration["Jwt:key"]!));
        var cred = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);

        var token = new JwtSecurityToken(
            issuer: _configuration["Jwt:issuer"],
            audience: _configuration["Jwt:issuer"],
            claims: claims,
            expires: DateTime.Now.AddMinutes(30),
            signingCredentials: cred);

        // Devuelve directamente un objeto anónimo
        return new {
            id = user.Id,
            name = user.name,
            email = user.Email,
            role = user.Role,
            token = new JwtSecurityTokenHandler().WriteToken(token)
        };
    }

}