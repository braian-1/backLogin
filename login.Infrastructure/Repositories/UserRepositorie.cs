using login.Domain.Interfaces;
using login.Domain.Models;
using login.Infrastructure.Data;
using Microsoft.EntityFrameworkCore;

namespace login.Infrastructure.Repositories;

public class UserRepositorie : IUserRepositorie
{
    private readonly AppDbContext _context;
    public UserRepositorie(AppDbContext context)
    {
        _context = context;
    }
    
    public async Task<IEnumerable<User>> GetAllUsersAsync()
    {
        return await _context.Users.ToListAsync();
    }

    public async Task<User> GetUserByIdAsync(int id)
    {
        return await _context.Users.FindAsync(id);
    }

    public async Task RegisterUser(User user)
    {
        _context.Users.Add(user);
        await _context.SaveChangesAsync();
    }

    public async Task UpdateUserAsync(User user)
    {
        _context.Users.Update(user);
        await _context.SaveChangesAsync();
    }

    public async Task DeleteUserAsync(int id)
    {
        var deletedUser = await _context.Users.FindAsync(id);
        _context.Users.Remove(deletedUser);
        await _context.SaveChangesAsync();
    }

    public async Task<User?> GetUserByUsernameAsync(string name)
    {
        return await _context.Users.FirstOrDefaultAsync(u => u.name == name);
    }
}