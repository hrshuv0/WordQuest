using Core.Common;
using Core.Entities;
using Core.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace API.Controllers;

public class WordQuestController : BaseApiController
{
    #region Init

    private readonly IUnitOfWork _unitOfWork;

    public WordQuestController(ILoggerFactory factory, IUnitOfWork unitOfWork)
    {
        _logger = factory.CreateLogger<WordQuestController>();
        _unitOfWork = unitOfWork;
    }

    #endregion
    
    // GET
    public async Task<IActionResult> Index()
    {
        IList<Word> wordList = new List<Word>();
        Word word = new Word();

        try
        {
            (wordList, _, _, _) = await _unitOfWork.VocabularyService.LoadAsync(c => c);
            wordList.Shuffle();
            
            word = wordList.FirstOrDefault()!;
        }
        catch (Exception e)
        {
            _logger.LogError(e.Message);
        }
        return Ok(word);
    }
}