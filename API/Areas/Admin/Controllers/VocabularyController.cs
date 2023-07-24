using Core.Entities;
using Microsoft.AspNetCore.Mvc;

namespace API.Areas.Admin.Controllers;

[Area("Admin")]
public class VocabularyController : Controller
{
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
}