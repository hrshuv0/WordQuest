using Microsoft.AspNetCore.Mvc;

namespace API.Areas.User.Controllers;

[Area("user")]
public class HomeController : Controller
{
    // GET
    public IActionResult Index()
    {
        return View();
    }
}