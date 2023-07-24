using Core.Entities;
using Core.Repositories;
using Infrastructure.Data;

namespace Infrastructure.Repositories;

public class VocabularyRepository : BaseRepository<Word, Guid>,  IVocabularyRepository
{
    public VocabularyRepository(ApplicationDbContext dbContext) : base(dbContext)
    {
    }
}