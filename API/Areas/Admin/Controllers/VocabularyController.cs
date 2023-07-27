using System.Linq.Expressions;
using API.Controllers;
using API.Helpers.Pagination;
using Core.Common;
using Core.Common.Enums;
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

    public async Task<IActionResult> CreateEdit(long id = 0)
    {
        Word model = new();
        try
        {
            if (id > 0)
            {
                model = await _unitOfWork.VocabularyService.GetByIdAsync(id);
            }
        }
        catch (Exception e)
        {
            _logger.LogError(e.Message);
        }

        return View(model);
    }

    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> CreateEdit(Word model)
    {
        var message = String.Empty;
        
        try
        {
            model.Name = model.Name!.TrimString();

            if (ModelState.IsValid == false)
            {
                var errors = GetErrorMessage(ModelState);
                throw new InvalidDataException(errors);
            }
                
            var isExist = await _unitOfWork.VocabularyService.IsExistsAsync(x => string.Equals(x.Name!, model.Name!) && x.Id != model.Id);
            if(isExist)
                throw new InvalidDataException($"{model.Name} is already exist");

            if (model.Id > 0)
            {
                await _unitOfWork.VocabularyService.UpdateAsync(model);
                message = "Updated successfully";
            }
            else
            {
                await _unitOfWork.VocabularyService.AddAsync(model);
                message = "Created successfully";
            }
            
            await _unitOfWork.SaveChangesAsync();
            ShowMessage(message, MessageType.Success);

            return RedirectToAction(nameof(Index));
        }
        catch(InvalidDataException e)
        {
            ShowMessage(e.Message, MessageType.Error);
        }
        catch (Exception e)
        {
            message = "Something went wrong";
            ShowMessage(message, MessageType.Error);
            _logger.LogError(e.Message);
        }
        
        return View(model);
    }


    #region API Calls

    public async Task<IActionResult> GetAll()
    {
        var message = string.Empty;
        
        IList<Word> result = new List<Word>();
        var total = 0;
        var totalFiltered = 0;
        var totalPages = 0;

        try
        {
            PaginationParams pagination = new(Request);
            Expression<Func<Word, bool>> filter = null!;
        
            if(string.IsNullOrWhiteSpace(pagination.SearchText) == false)
                filter = word => word.Name.Contains(pagination.SearchText);
        
            (result, total, totalFiltered, totalPages) = await _unitOfWork.VocabularyService.LoadAsync(v => v, filter, null, null, pagination.PageNumber,
                pagination.PageSize, false);
        }
        catch (Exception e)
        {
            message = "Something went wrong";
            ShowMessage(message, MessageType.Error);
            _logger.LogError(string.Empty, e.Message);
        }

        return Json(new
        {
            recordsTotal = total,
            recordsFiltered = totalFiltered,
            data = result
        });
    }

    #endregion
}