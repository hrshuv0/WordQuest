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
}