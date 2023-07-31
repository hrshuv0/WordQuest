using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using Core.Entities.Identity;
using Core.Repositories;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;

namespace Infrastructure.Repositories;

public class AuthRepository : IAuthRepository
{
    private readonly UserManager<ApplicationUser> _userManager;
    private readonly SignInManager<ApplicationUser> _signInManager;
    private readonly IConfiguration _config;
    private readonly SymmetricSecurityKey _key;

    public AuthRepository(UserManager<ApplicationUser> userManager, SignInManager<ApplicationUser> signInManager, IConfiguration config)
    {
        _userManager = userManager;
        _signInManager = signInManager;
        _config = config;
        _key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_config["Token:Key"]!));
    }

    
    public async Task<ApplicationUser> Register(ApplicationUser user, string password)
    {
        var result = await _userManager.CreateAsync(user, password);

        return result.Succeeded ? user : null!;
    }

    public async Task<ApplicationUser> Login(string username, string password)
    {
        var user = await _userManager.Users
            .FirstOrDefaultAsync(u => u.UserName == username) ?? await _userManager.Users
            .FirstOrDefaultAsync(u => u.Email == username);

        if (user is null) return null!;

        var result = await _signInManager.CheckPasswordSignInAsync(user, password, false);
        
        return result.Succeeded ? user : null!;
    }

    public async Task<bool> UserExists(string username)
    {
        var user = await _userManager.FindByNameAsync(username) ?? 
                   await _userManager.FindByEmailAsync(username);

        return user is not null;
    }

    public async Task<bool> UserNameExists(string username)
    {
        var user = await _userManager.FindByNameAsync(username);

        return user is not null;
    }

    public async Task<bool> UserEmailExists(string email)
    {
        var user = await _userManager.FindByEmailAsync(email);

        return user is not null;
    }

    public string CreateToken(ApplicationUser user)
    {
        var claims = new List<Claim>()
        {
            new Claim(ClaimTypes.NameIdentifier, user.Id),
            new Claim(ClaimTypes.Name, user.UserName!),
            new Claim(ClaimTypes.Email, user.Email!)
        };
        
        var credentials = new SigningCredentials(_key, SecurityAlgorithms.HmacSha512Signature);

        var tokenDescriptor = new SecurityTokenDescriptor()
        {
            Subject = new ClaimsIdentity(claims),
            Expires = DateTime.Now.AddDays(7),
            SigningCredentials = credentials,
            Issuer = _config["Token:Issuer"]
        };
        
        var tokenHandler = new JwtSecurityTokenHandler();
        var token = tokenHandler.CreateToken(tokenDescriptor);

        return tokenHandler.WriteToken(token);
    }
}