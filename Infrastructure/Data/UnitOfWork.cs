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

    #endregion

    #region Service
    public IVocabularyService VocabularyService { get; }

    #endregion
    
    public UnitOfWork(ApplicationDbContext dbContext)
    {
        _dbContext = dbContext;

        #region Repo
        VocabularyRepository = new VocabularyRepository(dbContext);
        #endregion

        VocabularyService = new VocabularyService(VocabularyRepository);
    }

    #region Helper

    public void SaveChanges()
    {
        _dbContext.SaveChanges();
    }

    public IDbContextTransaction BeginTransaction()
    {
        throw new NotImplementedException();
    }

    public async Task SaveChangesAsync()
    {
        await _dbContext.SaveChangesAsync();
    }

    public Task<bool> SaveAllAsync()
    {
        throw new NotImplementedException();
    }

    public Task<IDbContextTransaction> BeginTransactionAsync()
    {
        throw new NotImplementedException();
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