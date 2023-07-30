using API.Controllers;
using Core.Common;
using Core.Common.Enums;
using Core.Entities;
using Core.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace API.Areas.User.Controllers;

[Area("User")]
public class VocabController : BaseMvcController
{
    private readonly IUnitOfWork _unitOfWork;

    public VocabController(ILoggerFactory factory, IUnitOfWork unitOfWork)
    {
        _logger = factory.CreateLogger<VocabController>();
        _unitOfWork = unitOfWork;
    }

    // GET
    public async Task<IActionResult> Index()
    {
        IList<Word> wordList = new List<Word>();

        try
        {
            (wordList, _, _, _) = await _unitOfWork.VocabularyService.LoadAsync(c => c);
            wordList.Shuffle();
            
        }
        catch (Exception e)
        {
            ShowMessage("Something went wrong. Please try again later.", MessageType.Error);
            _logger.LogError(e.Message);
        }

        return View(wordList);
    }
}