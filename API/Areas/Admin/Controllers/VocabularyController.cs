using System.Linq.Expressions;
using API.Controllers;
using API.Helpers.Pagination;
using Core.Entities;
using Core.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace API.Areas.Admin.Controllers;

public class VocabularyController : BaseMvcController
{
    #region Init

    private readonly IUnitOfWork _unitOfWork;

    public VocabularyController(ILoggerFactory factory,IUnitOfWork unitOfWork)
    {
        _logger = factory.CreateLogger<VocabularyController>();
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
    public async Task<IActionResult> GetAll()
    {
        IList<Word> result = new List<Word>();

        try
        {
            PaginationParams pagination = new(Request);
            Expression<Func<Word, bool>> filter = null!;
        
            if(string.IsNullOrWhiteSpace(pagination.SearchText) == false)
                filter = word => word.Name.Contains(pagination.SearchText);
        
            (result, _, _, _) = await _unitOfWork.VocabularyService.LoadAsync(v => v, filter, null, null, pagination.PageNumber,
                pagination.PageSize, false);
        }
        catch (Exception e)
        {
            // ignored
        }

        return Json(new {data = result});
    }

    #endregion
}