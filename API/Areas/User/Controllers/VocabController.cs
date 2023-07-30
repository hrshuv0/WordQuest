using API.Controllers;
using Microsoft.AspNetCore.Mvc;

namespace API.Areas.User.Controllers;

[Area("User")]
public class VocabController : BaseMvcController
{
    // GET
    public IActionResult Index()
    {
        return View();
    }
}