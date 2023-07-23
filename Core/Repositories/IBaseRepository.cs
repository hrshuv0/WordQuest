using System.Linq.Expressions;
using Core.Entities;
using Microsoft.EntityFrameworkCore.Query;

namespace Core.Repositories;

public interface IBaseRepository<TEntity, TKey> where TEntity : IBaseEntity<TKey>
{
    #region LINQ Async

    Task<IList<TResult>> GetAsync<TResult>(Expression<Func<TEntity, TResult>> selector,
        Expression<Func<TEntity, bool>>? predicate = null,
        Func<IQueryable<TEntity>, IOrderedQueryable<TEntity>>? orderBy = null,
        Func<IQueryable<TEntity>, IIncludableQueryable<TEntity, object>>? include = null,
        bool disableTracking = true);

    Task<(IList<TResult> Items, int Total, int TotalFilter, int totalPages)> LoadAsync<TResult>(Expression<Func<TEntity, TResult>> selector, 
        Expression<Func<TEntity, bool>>? predicate= null,
        Func<IQueryable<TEntity>, IOrderedQueryable<TEntity>>? orderBy = null,
        Func<IQueryable<TEntity>, IIncludableQueryable<TEntity, object>>? include = null,
        int pageIndex = 1, int pageSize = 10,
        bool disableTracking = true);

    Task<TResult> GetFirstOrDefaultAsync<TResult>(Expression<Func<TEntity, TResult>> selector,
        Expression<Func<TEntity, bool>>? predicate=null,
        Func<IQueryable<TEntity>, IIncludableQueryable<TEntity, object>>? include = null, 
        bool disableTracking = true);

    Task<TEntity> GetByIdAsync(TKey id);
    Task<int> GetCountAsync(Expression<Func<TEntity, bool>>? predicate = null);
    Task<bool> IsExistsAsync(Expression<Func<TEntity, bool>> predicate);
    Task AddAsync(TEntity entity);
    Task AddRangeAsync(IList<TEntity> entities);
    Task UpdateAsync(TEntity entity);
    Task UpdateRangeAsync(IList<TEntity> entities);
    Task DeleteAsync(TKey id);
    Task DeleteAsync(TEntity entity);
    Task DeleteAsync(Expression<Func<TEntity, bool>> predicate);
    Task DeleteRangeAsync(IList<TEntity> entities);


    #endregion

    #region LINQ


    #endregion

    #region SQL

    IList<TEntity> ExecuteSqlQuery(string sql, params object[] parameters);
    int ExecuteSqlCommand(string sql, params object[] parameters);
    IList<dynamic> GetFromSql(string sql, Dictionary<string, object> parameters, bool isStoredProcedure = false);

    (IList<TEntity> Items, int Total, int TotalFilter) GetFromSql(string sql,
        IList<(string Key, object Value, bool IsOut)> parameters, bool isStoredProcedure = true);

    #endregion
}