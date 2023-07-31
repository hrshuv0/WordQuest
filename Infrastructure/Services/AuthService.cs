using AutoMapper;
using Core.Dtos;
using Core.Dtos.Identity;
using Core.Entities.Identity;
using Core.Repositories;
using Core.Services;

namespace Infrastructure.Services;

public class AuthService : IAuthService
{
    private readonly IAuthRepository _authRepository;
    private IMapper _mapper;

    public AuthService(IAuthRepository authRepository, IMapper mapper)
    {
        _authRepository = authRepository;
        _mapper = mapper;
    }

    public async Task<UserDetailsDto> Register(RegisterDto userDto)
    {
        try
        {
            userDto.UserName = userDto.UserName!.Normalize();
            userDto.Email = userDto.Email!.Normalize();
            
            if(await _authRepository.UserNameExists(userDto.UserName))
                throw new InvalidDataException("Username already exists");
            
            if(await _authRepository.UserEmailExists(userDto.Email))
                throw new InvalidDataException("Email already in used");

            var userToCreate = new ApplicationUser()
            {
                UserName = userDto.UserName,
                Email = userDto.Email,
                DisplayName = userDto.UserName
            };

            var user = await _authRepository.Register(userToCreate, userDto.Password!);

            if (user is null)
                throw new InvalidDataException("Unable to register");

            var userToReturn = new UserDetailsDto()
            {
                UserName = user.UserName,
                DisplayName = user.DisplayName,
                Email = user.Email
            };


            return userToReturn;
        }
        catch (InvalidDataException ex)
        {
            throw new InvalidDataException(ex.Message);
        }
        catch (Exception e)
        {
            throw new Exception(e.Message);
        }
    }

    public async Task<(UserDetailsForReturnDto, string)> Login(LoginDto userDto)
    {
        try
        {
            var user = await _authRepository.Login(userDto.UserName!, userDto.Password!);

            if (user is null)
                throw new InvalidDataException("Username or password did not matched");

            var userToReturn = _mapper.Map<UserDetailsForReturnDto>(user);

            var token = _authRepository.CreateToken(user);

            return (userToReturn, token);
        }
        catch (InvalidDataException ex)
        {
            throw new InvalidDataException(ex.Message);
        }
        catch (Exception ex)
        {
            throw new Exception(ex.Message);
        }
    }

    public async Task<bool> UserExists(string username)
    {
        try
        {
            return await _authRepository.UserExists(username);
        }
        catch (Exception e)
        {
            throw new Exception(e.Message);
        }
    }
}