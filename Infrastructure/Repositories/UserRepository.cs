using Core.Entities.Identity;
using Core.Repositories;
using Infrastructure.Data;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Repositories;

public class UserRepository : IUserRepository
{
    #region CONFIG
    private readonly ApplicationDbContext? _context;

    public UserRepository(ApplicationDbContext? context)
    {
        _context = context;
    }

    #endregion
    
    public async Task Add<T>(T entity)
    {
        await _context!.AddAsync(entity!);
    }

    public async Task Delete<T>(T entity)
    {
        _context!.Remove(entity!);
    }

    public async Task<bool> SaveAll()
    {
        return await _context!.SaveChangesAsync() > 0;
    }

    public async Task<ApplicationUser> Get(string id)
    {
        var user = await _context!.ApplicationUser!
            .FirstOrDefaultAsync(u => u.Id == id);

        return user!;
    }

    public async Task<List<ApplicationUser>> Load(int pageNumber, int pageSize)
    {
        IQueryable<ApplicationUser> query = _context!.ApplicationUser!.AsQueryable();
        
        query = query
            .Skip((pageNumber - 1) * pageSize)
            .Take(pageSize);
        
        var result = await query.ToListAsync();
        
        return result;
    }
    
}