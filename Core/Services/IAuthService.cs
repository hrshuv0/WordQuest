using Core.Dtos;
using Core.Dtos.Identity;

namespace Core.Services;

public interface IAuthService
{
    Task<UserDetailsDto> Register(RegisterDto userDto);
    Task<(UserDetailsForReturnDto, string)> Login(LoginDto userDto);
    Task<bool> UserExists(string username);
}