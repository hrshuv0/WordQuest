using System.Linq.Expressions;
using Core.Entities;
using Core.Repositories;
using Core.Services;
using Microsoft.EntityFrameworkCore.Query;

namespace Infrastructure.Services;

public class BaseService<TEntity, TKey> : IBaseService<TEntity, TKey> 
    where TEntity : class, IBaseEntity<TKey>
{
    #region CONFIG

    private readonly IBaseRepository<TEntity, TKey> _entityRepository;

    protected BaseService(IBaseRepository<TEntity, TKey> entityRepository)
    {
        _entityRepository = entityRepository;
    }

    #endregion

    #region LINQ Async

    public Task<IList<TResult>> GetAsync<TResult>(Expression<Func<TEntity, TResult>> selector,
        Expression<Func<TEntity, bool>>? predicate = null,
        Func<IQueryable<TEntity>, IOrderedQueryable<TEntity>>? orderBy = null,
        Func<IQueryable<TEntity>, IIncludableQueryable<TEntity, object>>? include = null,
        bool disableTracking = true)
    {
        try
        {
            return _entityRepository.GetAsync(selector, predicate, orderBy, include, disableTracking);
        }
        catch (Exception ex)
        {
            throw new Exception(ex.Message);
        }
    }

    public Task<(IList<TResult> Items, int Total, int TotalFilter, int totalPages)> LoadAsync<TResult>(Expression<Func<TEntity, TResult>> selector, Expression<Func<TEntity, bool>>? predicate = null, Func<IQueryable<TEntity>, IOrderedQueryable<TEntity>>? orderBy = null, Func<IQueryable<TEntity>, IIncludableQueryable<TEntity, object>>? include = null,
        int pageIndex = 1, int pageSize = 10, bool disableTracking = true)
    {
        try
        {
            return _entityRepository.LoadAsync(selector, predicate, orderBy, include, pageIndex, pageSize, disableTracking);
        }
        catch (Exception ex)
        {
            throw new Exception(ex.Message);
        }
    }

    public Task<TResult> GetFirstOrDefaultAsync<TResult>(Expression<Func<TEntity, TResult>> selector, Expression<Func<TEntity, bool>>? predicate = null, Func<IQueryable<TEntity>, IIncludableQueryable<TEntity, object>>? include = null,
        bool disableTracking = true)
    {
        try
        {
            return _entityRepository.GetFirstOrDefaultAsync(selector, predicate, include, disableTracking);
        }
        catch (Exception ex)
        {
            throw new Exception(ex.Message);
        }
    }

    public Task<TEntity> GetByIdAsync(TKey id)
    {
        try
        {
            return _entityRepository.GetByIdAsync(id);
        }
        catch (Exception ex)
        {
            throw new Exception(ex.Message);
        }
    }

    public Task<int> GetCountAsync(Expression<Func<TEntity, bool>>? predicate = null)
    {
        try
        {
            return _entityRepository.GetCountAsync(predicate);
        }
        catch (Exception ex)
        {
            throw new Exception(ex.Message);
        }
    }

    public Task<bool> IsExistsAsync(Expression<Func<TEntity, bool>> predicate)
    {
        try
        {
            return _entityRepository.IsExistsAsync(predicate);
        }
        catch (Exception ex)
        {
            throw new Exception(ex.Message);
        }
    }

    public Task AddAsync(TEntity entity)
    {
        try
        {
            return _entityRepository.AddAsync(entity);
        }
        catch (Exception ex)
        {
            throw new Exception(ex.Message);
        }
    }

    public virtual Task AddRangeAsync(IList<TEntity> entities)
    {
        try
        {
            return _entityRepository.AddRangeAsync(entities);
        }
        catch (Exception ex)
        {
            throw new Exception(ex.Message);
        }
    }

    public virtual Task UpdateAsync(TEntity entity)
    {
        try
        {
            return _entityRepository.UpdateAsync(entity);
        }
        catch (Exception ex)
        {
            throw new Exception(ex.Message);
        }
    }

    public virtual Task UpdateRangeAsync(IList<TEntity> entities)
    {
        throw new NotImplementedException();
    }

    public Task DeleteAsync(TKey id)
    {
        try
        {
            return _entityRepository.DeleteAsync(id);
        }
        catch (Exception ex)
        {
            throw new Exception(ex.Message);
        }
    }

    public Task DeleteAsync(TEntity entity)
    {
        try
        {
            return _entityRepository.DeleteAsync(entity);
        }
        catch (Exception ex)
        {
            throw new Exception(ex.Message);
        }
    }

    public Task DeleteAsync(Expression<Func<TEntity, bool>> predicate)
    {
        try
        {
            return _entityRepository.DeleteAsync(predicate);
        }
        catch (Exception ex)
        {
            throw new Exception(ex.Message);
        }
    }

    public Task DeleteRangeAsync(IList<TEntity> entities)
    {
        throw new NotImplementedException();
    }
    
    #endregion
}