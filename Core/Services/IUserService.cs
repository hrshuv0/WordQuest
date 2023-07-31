

using Core.Entities.Identity;

namespace Core.Services;

public interface IUserService
{
    Task Add<T>(T entity);
    Task Delete<T>(T entity);
    Task<ApplicationUser?> Get(string id);
    Task<List<ApplicationUser>> Load(int pageNumber, int pageSize);

    
    Task<bool> SaveAll();
}