using Microsoft.AspNetCore.Mvc;

namespace API.Controllers;

[Area("Admin")]
public class BaseMvcController : Controller
{
    protected ILogger _logger;
}