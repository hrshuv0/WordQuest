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
    public IActionResult Index()
    {

        return View();
    }


    #region API Calls

    
    public async Task<IActionResult> GetRandomWord()
    {
        var isSuccess = false;
        var word = new Word();

        try
        {
            word = await _unitOfWork.VocabularyService.GetRandomWord();
            ViewBag.Word = word;
            
            isSuccess = true;
            return PartialView("Partial/_RandomWordPartial", word);
        }
        catch (Exception e)
        {
            _logger.LogError(e.Message);
        }

        return new JsonResult(isSuccess);
    }

    #endregion
}