using login.Domain.Models;

namespace login.Domain.Interfaces;

public interface IUserRepositorie
{
    Task<IEnumerable<User>>  GetAllUsersAsync();
    Task<User> GetUserByIdAsync(int id);
    Task RegisterUser (User user);
    Task UpdateUserAsync(User user);
    Task DeleteUserAsync(int id);
    Task<User?> GetUserByUsernameAsync(string name);
}