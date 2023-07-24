using Core.Entities;
using Core.Repositories;
using Infrastructure.Data;

namespace Infrastructure.Repositories;

public class VocabularyRepository : BaseRepository<Word, long>,  IVocabularyRepository
{
    public VocabularyRepository(ApplicationDbContext dbContext) : base(dbContext)
    {
    }
}