using Core.Common;
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

    #region OVERRIDE

    public override async Task AddAsync(Word entity)
    {
        await using var transaction = await _entityRepository.BeginTransactionAsync();
        try
        {
            await base.AddAsync(entity);
            
            await transaction.CommitAsync();
        }
        catch (Core.Common.Exceptions.InvalidDataException e)
        {
            await transaction.RollbackAsync();
            throw new Core.Common.Exceptions.InvalidDataException(e.Message);
        }
        catch (Exception e)
        {
            await transaction.RollbackAsync();
            throw new Exception(e.Message);
        }
    }
    
    public override async Task UpdateAsync(Word model)
    {
        await using var transaction = await _entityRepository.BeginTransactionAsync();
        
        try
        {
            var entity = model;
            
            entity.UpdatedTime = DateTime.Now;
           
            
            await base.UpdateAsync(entity);
            
            await transaction.CommitAsync();
        }
        catch (Exception e)
        {
            await transaction.RollbackAsync();
            throw new Exception(e.Message);
        }
        
    }

    #endregion

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