using Core.Entities.Identity;

namespace Core.Repositories;

public interface IAuthRepository
{
    Task<ApplicationUser> Register(ApplicationUser user, string password);
    Task<ApplicationUser> Login(string username, string password);
    Task<bool> UserExists(string username);
    Task<bool> UserNameExists(string username);
    Task<bool> UserEmailExists(string email);
    string CreateToken(ApplicationUser user);
}