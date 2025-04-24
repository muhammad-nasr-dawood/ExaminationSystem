using ExaminationSystem.MVC.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;

namespace ExaminationSystem.MVC.Controllers;

//[Authorize]
public class AccountController : Controller
{
    private readonly IAccountService _accountService;
    public AccountController(IAccountService accountService)
    {
        _accountService = accountService;

    }
    public IActionResult Index()
    {
        string? userId = User.FindFirst(ClaimTypes.NameIdentifier)?.Value;

        if (string.IsNullOrEmpty(userId))
        {
            return BadRequest("User ID is missing.");
        }

        ViewBag.Locations = _accountService.UnitOfWork.LocationRepo.GetAll();
        var account = _accountService.GetAccount(long.Parse(userId));
        return View(account);
    }



  [HttpPost]
  public async Task<IActionResult> Update(AccountEditViewModel model)
  {
	if (!ModelState.IsValid)
	{
	  ViewBag.Locations = _accountService.UnitOfWork.LocationRepo.GetAll();
	  return View("Index", model);
	}

	string? userId = User.FindFirst(ClaimTypes.NameIdentifier)?.Value;
	var roles = User.FindAll(ClaimTypes.Role).Select(c => c.Value).ToList();


	bool success = await _accountService.UpdateAccount(long.Parse(userId), roles, model);


	return Json(new
	{
	  success = success,
	  redirectUrl = Url.Action("Index", "Staff")
	});
  }


  [HttpPost]
  [ValidateAntiForgeryToken] 
  public async Task<IActionResult> UploadImage([FromForm] IFormFile file)
    {
        if (file == null || file.Length == 0)
            return BadRequest("No file uploaded.");

        string? userId = User.FindFirst(ClaimTypes.NameIdentifier)?.Value;

        if (string.IsNullOrEmpty(userId))
        {
            return BadRequest("User ID is missing.");
        }

        // Fix for CS1061: Ensure UpdateImage is awaited properly by marking it as Task<string> in IAccountService
        string imagePath = await _accountService.UpdateImage(file, long.Parse(userId));

        return Json(new { imageUrl = imagePath });
    }


  [HttpPost]
  [ValidateAntiForgeryToken]
  public async Task<IActionResult> RemoveImage()
  {
	string? userId = User.FindFirst(ClaimTypes.NameIdentifier)?.Value;
	var imagePath = await _accountService.DeleteImage(long.Parse(userId));

	return Json(new { imageUrl = imagePath });
  }

 

}
