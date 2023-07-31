using Core.Entities.Identity;

namespace Core.Repositories;

public interface IUserRepository
{
    Task Add<T>(T entity);
    Task Delete<T>(T entity);
    Task<bool> SaveAll();
    Task<ApplicationUser> Get(string id);
    Task<List<ApplicationUser>> Load(int pageNumber, int pageSize);

} 