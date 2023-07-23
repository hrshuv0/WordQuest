using System.Linq.Expressions;
using Core.Entities;
using Core.Repositories;
using Infrastructure.Data;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Query;

namespace Infrastructure.Repositories;

public abstract class BaseRepository<TEntity, TKey> : IBaseRepository<TEntity, TKey> 
    where TEntity : class, IBaseEntity<TKey>
{
    #region CONFIG

    private ApplicationDbContext _dbContext;
    private DbSet<TEntity> _dbSet;

    protected BaseRepository(ApplicationDbContext dbContext)
    {
        _dbContext = dbContext;
        _dbSet = _dbContext.Set<TEntity>();
    }

    #endregion

    #region LINQ Async
    public async Task<IList<TResult>> GetAsync<TResult>(Expression<Func<TEntity, TResult>> selector, Expression<Func<TEntity, bool>>? predicate = null, Func<IQueryable<TEntity>, IOrderedQueryable<TEntity>>? orderBy = null, Func<IQueryable<TEntity>, IIncludableQueryable<TEntity, object>>? include = null,
        bool disableTracking = true)
    {
        IQueryable<TEntity> query = _dbSet.AsQueryable();

        if (include is not null)
            query = include(query);

        if (predicate is not null)
            query = query.Where(predicate);

        if (orderBy is not null)
            query = orderBy(query);

        if (disableTracking)
            query = query.AsNoTracking();

        var result = await query.Select(selector).ToListAsync();

        return result;
    }

    public async Task<(IList<TResult> Items, int Total, int TotalFilter, int totalPages)> LoadAsync<TResult>(Expression<Func<TEntity, TResult>> selector, Expression<Func<TEntity, bool>>? predicate = null, Func<IQueryable<TEntity>, IOrderedQueryable<TEntity>>? orderBy = null, Func<IQueryable<TEntity>, IIncludableQueryable<TEntity, object>>? include = null,
        int pageIndex = 1, int pageSize = 10, bool disableTracking = true)
    {
        IQueryable<TEntity> query = _dbSet.AsQueryable();

        int total = await query.CountAsync();
        int totalFilter = total;

        if (include is not null)
            query = include(query);

        if (predicate is not null)
        {
            query = query.Where(predicate);
            totalFilter = await query.CountAsync();
        }

        if (orderBy is not null)
            query = orderBy(query);

        if (disableTracking)
            query = query.AsNoTracking();

        var result = await query.Skip((pageIndex - 1) * pageSize).Take(pageSize).Select(selector).ToListAsync();
        
        var totalPages = (int)Math.Ceiling(total / (double)pageSize);

        return (result, total, totalFilter, totalPages);
    }

    public async Task<TResult> GetFirstOrDefaultAsync<TResult>(Expression<Func<TEntity, TResult>> selector, Expression<Func<TEntity, bool>>? predicate = null, Func<IQueryable<TEntity>, IIncludableQueryable<TEntity, object>>? include = null,
        bool disableTracking = true)
    {
        IQueryable<TEntity> query = _dbSet.AsQueryable();

        if (include != null)
            query = include(query);

        if (predicate != null)
            query = query.Where(predicate);

        if (disableTracking)
            query = query.AsNoTracking();

        var result = await query.Select(selector).FirstOrDefaultAsync();

        return result!;
    }

    public async Task<TEntity> GetByIdAsync(TKey id)
    {
        var result = await _dbSet.FindAsync(id);
        
        return result!;
    }

    public async Task<int> GetCountAsync(Expression<Func<TEntity, bool>>? predicate = null)
    {
        var query = _dbSet.AsQueryable();

        if (predicate != null)
        {
            query = query.Where(predicate);
        }

        return await query.CountAsync();
    }

    public async Task<bool> IsExistsAsync(Expression<Func<TEntity, bool>> predicate)
    {
        var query = _dbSet.AsQueryable();

        return await query.AnyAsync(predicate);
    }

    public async Task AddAsync(TEntity entity)
    {
        await _dbSet.AddAsync(entity);
    }

    public async Task AddRangeAsync(IList<TEntity> entities)
    {
        await _dbSet.AddRangeAsync(entities);
    }

    public async Task UpdateAsync(TEntity entity)
    {
        _dbSet.Attach(entity);
        _dbContext.Entry(entity).State = EntityState.Modified;
    }

    public async Task UpdateRangeAsync(IList<TEntity> entities)
    {
        _dbContext.UpdateRange(entities);
    }

    public async Task DeleteAsync(TKey id)
    {
        var entity = await _dbSet.FindAsync(id);

        await DeleteAsync(entity);
    }

    public async Task DeleteAsync(TEntity entity)
    {
        if (_dbContext.Entry(entity).State == EntityState.Detached)
        {
            _dbSet.Attach(entity);
        }

        _dbSet.Remove(entity);
    }

    public async Task DeleteAsync(Expression<Func<TEntity, bool>> predicate)
    {
        var query = _dbSet.AsQueryable().Where(predicate);

        if (query.Any())
        {
            _dbSet.RemoveRange(query);
        }
    }

    public async Task DeleteRangeAsync(IList<TEntity> entities)
    {
        _dbContext.RemoveRange(entities);
    }

    #endregion

    #region LINQ

    

    #endregion
    
    #region SQL
    
    public IList<TEntity> ExecuteSqlQuery(string sql, params object[] parameters)
    {
        throw new NotImplementedException();
    }

    public int ExecuteSqlCommand(string sql, params object[] parameters)
    {
        throw new NotImplementedException();
    }

    public IList<dynamic> GetFromSql(string sql, Dictionary<string, object> parameters, bool isStoredProcedure = false)
    {
        throw new NotImplementedException();
    }

    public (IList<TEntity> Items, int Total, int TotalFilter) GetFromSql(string sql, IList<(string Key, object Value, bool IsOut)> parameters, bool isStoredProcedure = true)
    {
        throw new NotImplementedException();
    }

    

    #endregion
    
}