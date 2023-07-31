

using Core.Entities.Identity;
using Core.Repositories;
using Core.Services;

namespace Infrastructure.Services;

public class UserService : IUserService
{
    #region CONFIG
    private readonly IUserRepository _entityRepository;

    public UserService(IUserRepository entityRepository)
    {
        _entityRepository = entityRepository;
    }

    #endregion
    
    public Task Add<T>(T entity)
    {
        try
        {
            return _entityRepository.Add(entity);
        }
        catch (Exception e)
        {
            throw new Exception(e.Message);
        }
    }

    public Task Delete<T>(T entity)
    {
        return _entityRepository.Delete(entity);
    }

    public async Task<bool> SaveAll()
    {
        return await _entityRepository.SaveAll();
    }

    public Task<ApplicationUser?> Get(string id)
    {
        return _entityRepository.Get(id);
    }

    public Task<List<ApplicationUser>> Load(int pageNumber, int pageSize)
    {
        return _entityRepository.Load(pageNumber, pageSize);
    }

}