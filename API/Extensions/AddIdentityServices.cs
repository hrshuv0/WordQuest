using System.Text;
using Core.Entities.Identity;
using Core.Repositories;
using Core.Services;
using Infrastructure.Data;
using Infrastructure.Repositories;
using Infrastructure.Services;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;

namespace API.Extensions;

public static class IdentityServiceExtension
{
    public static async Task AddIdentityServices(this IServiceCollection services, IConfiguration config)
    {
        services.AddScoped<IAuthRepository, AuthRepository>();
        services.AddScoped<IAuthService, AuthService>();
        
        services.AddIdentity<ApplicationUser, IdentityRole>(options =>
        {
            // Password configuration
            options.Password.RequireDigit = false;
            options.Password.RequiredLength = 4;
            options.Password.RequireLowercase = false;
            options.Password.RequireUppercase = false;
            options.Password.RequireNonAlphanumeric = false;
            options.Password.RequiredUniqueChars = 1;
        })
            .AddEntityFrameworkStores<ApplicationDbContext>()
            .AddDefaultTokenProviders();

        services.AddAuthentication(options =>
        {
            options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
            options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
            options.DefaultScheme = JwtBearerDefaults.AuthenticationScheme;
        }).AddJwtBearer(options =>
        {
            options.TokenValidationParameters = new TokenValidationParameters()
            {
                ValidateIssuerSigningKey = true,
                IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(config["Token:Key"]!)),
                ValidIssuer = config["Token:Issuer"],
                ValidateIssuer = true,
                ValidateAudience = false
            };
        });
        
        var loggerFactory = services.BuildServiceProvider().GetRequiredService<ILoggerFactory>();

        try
        {
            // Migrate and Seed Data
            
            var context = services.BuildServiceProvider().GetRequiredService<ApplicationDbContext>();
            // await context.Database.MigrateAsync();
            
            var userManager = services.BuildServiceProvider().GetRequiredService<UserManager<ApplicationUser>>();
            
            // await SeedUserData.SeedUserAsync(context, userManager, loggerFactory);

        }
        catch (Exception ex)
        {
            var logger = loggerFactory.CreateLogger<Program>();
            logger.LogError(ex, "An error occured during migration");
        }
    }

}