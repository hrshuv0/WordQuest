using Microsoft.AspNetCore.Mvc;

namespace API.Areas.Admin.Controllers;

[Area("admin")]
public class WordQuestController : Controller
{
    // GET
    public IActionResult Index()
    {
        return View();
    }
}