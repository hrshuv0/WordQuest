using API.Helpers;
using API.Helpers.Pagination;
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
    
    [HttpGet]
    public async Task<IActionResult> Index([FromQuery] PaginationParams pagination)
    {
        IList<Word> wordList = new List<Word>();
        Word word = new Word();

        try
        {
            var total = 0;
            var totalFiltered = 0;
            var totalPages = 0;
            
            (wordList, total, totalFiltered, totalPages) = await _unitOfWork.VocabularyService.LoadAsync(c => c);
            wordList.Shuffle();
            
            //Response.AddPagination(pagination.PageNumber, pagination.PageSize, total, totalFiltered, totalPages);
            return Ok(new
            {
                total,
                totalFiltered,
                totalPages,
                data = wordList
            });
        }
        catch (Exception e)
        {
            _logger.LogError(e.Message);
        }
        
        return BadRequest("Failed To Load Word.");
    }
}