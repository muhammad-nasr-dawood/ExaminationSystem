using ExaminationSystem.Core.Models;
using ExaminationSystem.MVC.Services;
using ExaminationSystem.MVC.ViewModels.AuthViewModels;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;

namespace ExaminationSystem.MVC.Controllers;

public class AuthController : Controller
{
  private readonly IAuthService _authService;
  public AuthController(IAuthService authService)
  {
	  _authService = authService;
  }
  public IActionResult Index()
  {
      return View();
  }

  public IActionResult LoginCover() => View();

  [HttpPost]
  public async Task<IActionResult> LoginCover(LoginViewModel model)
  {
	
	if (ModelState.IsValid)
	{
	  UserLoginViewModel user = _authService.ValidateLoginByEmailAndPassword(model.Email, model.Password);
	  if (user != null)
	  {
		Claim nameClaim = new Claim(ClaimTypes.Name, $"{user.Fname} {user.Lname}");
		Claim emailClaim = new Claim(ClaimTypes.Email, user.Email);
		List<Claim> rolesClaims = new List<Claim>();
		rolesClaims.Add(new Claim(ClaimTypes.Role, user.UserType));
		if(user.UserType == "Staff")
		{
		  foreach(var role in user?.StaffRoles)
			rolesClaims.Add(new Claim(ClaimTypes.Role, role));
		}
		ClaimsIdentity card = new ClaimsIdentity(CookieAuthenticationDefaults.AuthenticationScheme);
		card.AddClaim(nameClaim);
		card.AddClaim(emailClaim);

		foreach(var roleClaim in rolesClaims)
		  card.AddClaim(roleClaim);

		ClaimsPrincipal principal = new ClaimsPrincipal();
		principal.AddIdentity(card);

		var authProperties = new AuthenticationProperties
		{
		  IsPersistent = true, // Makes the cookie persist across browser sessions
		  ExpiresUtc = DateTimeOffset.UtcNow.AddDays(7) // Cookie expires after 7 days
		};

		await HttpContext.SignInAsync(
			CookieAuthenticationDefaults.AuthenticationScheme,
			principal,
			authProperties
		);

		return View("Index");
	  }
	  else
	  {
		ModelState.AddModelError("", "Invalid Email or password!");
	  }
	}
	return View(model);
  }

  [Authorize] 
  public async Task<IActionResult> Logout()
  {
	await HttpContext.SignOutAsync(CookieAuthenticationDefaults.AuthenticationScheme);
	return RedirectToAction("LoginCover"); 
  }


}
