using API.Helpers;
using Core.Interfaces;
using Infrastructure.Data;
using Microsoft.EntityFrameworkCore;

namespace API.Extensions;

public static class ApplicationServiceExtensions
{
    public static async Task AddApplicationServices(this IServiceCollection services, IConfiguration config)
    {
        #region Database CONFIG
        var connectionString = config.GetConnectionString("WordQuestDb")!;
        
        services.AddDbContext<ApplicationDbContext>(options =>
        {
            options.UseMySQL(connectionString, conn => 
                conn.EnableRetryOnFailure(
                    maxRetryCount:5,
                    maxRetryDelay: TimeSpan.FromSeconds(30),
                    errorNumbersToAdd: null
                ));
        });

        #endregion

        services.AddAutoMapper(typeof(MappingProfiles));
        services.AddScoped<IUnitOfWork, UnitOfWork>();
        
        var loggerFactory = services.BuildServiceProvider().GetRequiredService<ILoggerFactory>();

        try
        {
            // Migrate and Seed Data
            
            var context = services.BuildServiceProvider().GetRequiredService<ApplicationDbContext>();
            await context.Database.MigrateAsync();

            //await AppDbInitializer.SeedAsync(context, loggerFactory);
        }
        catch (Exception e)
        {
            var logger = loggerFactory.CreateLogger<Program>();
            logger.LogError(e, "An error occured during migration");
        }
    }
}