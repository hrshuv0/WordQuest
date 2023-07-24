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
}