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
  private readonly IEmailService _emailService;
  public AuthController(IAuthService authService, IEmailService emailService)
  {
	  _authService = authService;
	_emailService = emailService;
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
		Claim idClaim = new Claim(ClaimTypes.NameIdentifier, user.Ssn.ToString());
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
		card.AddClaim(idClaim);
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

  [HttpGet]
  public IActionResult ForgotPassword()
  {
	return View();
  }

  [HttpPost]
  public async Task<IActionResult> ForgotPassword(ForgotPasswordViewModel model)
  {
	if (!ModelState.IsValid)
	  return View(model);

	var returnedTokenValue = await _authService.CreateTempTokenForNewPassword(model.Email);

	if (returnedTokenValue is null)
	{
	  // for security: don't reveal if user exists
	  return RedirectToAction("ForgotPasswordConfirmation");
	}

	// 3. create reset password link
	var resetLink = Url.Action(
		"ResetPassword",
		"Auth",
		new { token = returnedTokenValue, email = model.Email },
		protocol: Request.Scheme);

	// 4. send the link by email
	await _emailService.SendEmailAsync(
	model.Email,
	"Reset Your Password",
	$@"
        <div style='font-family: Arial, sans-serif; font-size: 16px; color: #333;'>
            <h2 style='color: #7864f4;'>Examination System - Password Reset Request</h2>
            <p>Dear User,</p>
            <p>We received a request to reset your password for your Examination System account.</p>
            <p>To reset your password, please click the button below:</p>
            <p style='text-align: center; margin: 30px 0;'>
                <a href='{resetLink}' style='background-color: #7864f4; color: white; padding: 12px 20px; text-decoration: none; border-radius: 5px; font-weight: bold;'>
                    Reset Password
                </a>
            </p>
            <p>If you did not request a password reset, please ignore this email. Your account is still secure.</p>
            <br/>
            <p>Best Regards,</p>
            <p><strong>Examination System Team</strong></p>
        </div>
    ");


	return RedirectToAction("ForgotPasswordConfirmation");
  }

  public IActionResult ForgotPasswordConfirmation()
  {
	return View();
  }


  [HttpGet]
  public IActionResult ResetPassword(string token, string email)
  {
	var model = new ResetPasswordViewModel { Token = token, Email = email };
	return View(model);
  }

  [HttpPost]
  public async Task<IActionResult> ResetPassword(ResetPasswordViewModel model)
  {
	if (!ModelState.IsValid)
	  return View(model);

	var user = await _authService.FindUserByEmail(model.Email);

	if (user == null || user.PasswordResetToken != model.Token || user.PasswordResetTokenExpiry < DateTime.UtcNow)
	{
	  ModelState.AddModelError("", "Invalid or expired password reset token.");
	  return View();
	}

	await _authService.ResetPasswordAfterVerification(model.Email, model.NewPassword);


	return RedirectToAction("LoginCover");
  }



}
