using API.Helpers.Pagination;
using Core.Entities;
using Core.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace API.Areas.Admin.Controllers;

[Area("Admin")]
public class VocabularyController : Controller
{
    #region Init

    private readonly IUnitOfWork _unitOfWork;

    public VocabularyController(IUnitOfWork unitOfWork)
    {
        _unitOfWork = unitOfWork;
    }

    #endregion
    
    // GET
    public IActionResult Index()
    {
        return View();
    }

    public IActionResult CreateEdit()
    {
        Word model = new();

        return View(model);
    }

    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> CreateEdit(Word model)
    {
        try
        {
            await _unitOfWork.VocabularyService.AddAsync(model);
            await _unitOfWork.SaveChangesAsync();
            
            return RedirectToAction(nameof(Index));
        }
        catch (Exception e)
        {
            Console.WriteLine(e);
        }
        return View(model);
    }


    #region API Calls

    [HttpPost]
    public async Task<IActionResult> GetAll([FromQuery] PaginationParams pagination)
    {
        IList<Word> result = new List<Word>();
        
        var total = 0;
        var totalFiltered = 0;
        var totalPages = 0;
            
        (result, total, totalFiltered, totalPages) = await _unitOfWork.VocabularyService.LoadAsync(v => v, null, null, null, pagination.PageNumber,
            pagination.PageSize, false);
        
        return Json(new {data = result});
    }

    #endregion
}