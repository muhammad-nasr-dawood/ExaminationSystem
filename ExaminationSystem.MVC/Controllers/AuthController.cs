using Microsoft.AspNetCore.Mvc;

namespace ExaminationSystem.MVC.Controllers;

public class AuthController : Controller
{
    public IActionResult Index()
    {
        return View();
    }

	public IActionResult LoginCover() => View();
}
