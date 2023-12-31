﻿using Core.Repositories;
using Core.Services;
using Microsoft.EntityFrameworkCore.Storage;

namespace Core.Interfaces;

public interface IUnitOfWork : IDisposable
{
    #region Repository

    IVocabularyRepository VocabularyRepository { get; }
    IUserRepository UserRepository { get; }

    #endregion

    #region Service

    IVocabularyService VocabularyService { get; }
    IUserService UserService { get; }

    #endregion

    void SaveChanges();
    public IDbContextTransaction BeginTransaction();

    Task SaveChangesAsync();
    Task<bool> SaveAllAsync();
    public Task<IDbContextTransaction> BeginTransactionAsync();
}