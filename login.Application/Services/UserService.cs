using login.Domain.Interfaces;
using login.Domain.Models;
using Microsoft.EntityFrameworkCore.Query;

namespace login.Application.Services;

public class UserService
{
    private readonly IUserRepositorie _repositorie;

    public UserService(IUserRepositorie repository)
    {
        _repositorie = repository;
    }

    public async Task<IEnumerable<User>> GetAllUsersAsync()
    {
        return await _repositorie.GetAllUsersAsync();
    }

    public async Task<User> GetUserAsync(int id)
    {
        return await _repositorie.GetUserByIdAsync(id);
    }

    public async Task AddUserAsync(User user)
    {
        await _repositorie.RegisterUser(user);
    }

    public async Task UpdateUserAsync(User user)
    {
        await _repositorie.UpdateUserAsync(user);
    }

    public async Task DeleteUserAsync(int id)
    {
        await _repositorie.DeleteUserAsync(id);
    }
    
    public async Task<bool> VerifyLoginAsync(string username, string password)
    {
        var user = await _repositorie.GetUserByUsernameAsync(username);
        if (user == null) return false;
        return user.PasswordHash == password;
    }
}