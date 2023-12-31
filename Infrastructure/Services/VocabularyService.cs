﻿using Core.Common;
using Core.Entities;
using Core.Repositories;
using Core.Services;

namespace Infrastructure.Services;

public class VocabularyService : BaseService<Word, long>, IVocabularyService
{
    #region CONFIG
    
    private readonly IVocabularyRepository _entityRepository;
    
    public VocabularyService(IVocabularyRepository entityRepository) : base(entityRepository)
    {
        _entityRepository = entityRepository;
    }
    #endregion
    
    #region METHODS
    
    public override Task UpdateAsync(Word entity)
    {
        try
        {
            entity.UpdatedTime = DateTime.Now;
            return base.UpdateAsync(entity);
        }
        catch (Exception e)
        {
            throw new Exception(e.Message);
        }
        
    }

    public async Task<Word> GetRandomWord()
    {
        try
        {
            var (wordList, _, _, _) = await _entityRepository.LoadAsync(c => c);
            wordList.Shuffle();
            
            return wordList.FirstOrDefault()!;
        }
        catch (Exception e)
        {
            throw new Exception(e.Message);
        }
    }

    #endregion
    
}