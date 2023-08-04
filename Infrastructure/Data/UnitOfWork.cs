using Core.Interfaces;
using Core.Repositories;
using Core.Services;
using Infrastructure.Repositories;
using Infrastructure.Services;
using Microsoft.EntityFrameworkCore.Storage;

namespace Infrastructure.Data;

public class UnitOfWork : IUnitOfWork
{
    private bool _disposed;
    private readonly ApplicationDbContext _dbContext;

    #region Repository
    public IVocabularyRepository VocabularyRepository { get; }
    public IUserRepository UserRepository { get; }

    #endregion

    #region Service
    public IVocabularyService VocabularyService { get; }
    public IUserService UserService { get; }

    #endregion
    
    public UnitOfWork(ApplicationDbContext dbContext)
    {
        _dbContext = dbContext;

        #region Repo
        VocabularyRepository = new VocabularyRepository(dbContext);
        UserRepository = new UserRepository(dbContext);
        #endregion

        VocabularyService = new VocabularyService(VocabularyRepository);
        UserService = new UserService(UserRepository);
    }

    #region Helper

    public void SaveChanges()
    {
        _dbContext.SaveChanges();
    }
    

    public async Task SaveChangesAsync()
    {
        await _dbContext.SaveChangesAsync();
    }

    public async Task<bool> SaveAllAsync()
    {
        return await _dbContext.SaveChangesAsync() > 0;
    }

    public async Task<IDbContextTransaction> BeginTransactionAsync()
    {
        return await _dbContext.Database.BeginTransactionAsync();
    }
    #endregion



    #region Dispose
    ~UnitOfWork()
    {
        Dispose(false);
    }
    public void Dispose()
    {
        Dispose(true);
        GC.SuppressFinalize(this);
    }

    private void Dispose(bool disposing)
    {
        if(!_disposed && disposing)
            _dbContext.Dispose();
        _disposed = true;
    }
    #endregion
    
}