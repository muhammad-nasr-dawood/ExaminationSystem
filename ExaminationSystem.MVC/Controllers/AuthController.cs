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
		// 1. Build the full list of claims
		var claims = new List<Claim>
			{
				new Claim(ClaimTypes.NameIdentifier, user.Ssn.ToString()),
				new Claim(ClaimTypes.Name, $"{user.Fname} {user.Lname}"),
				new Claim(ClaimTypes.Email, user.Email),
				new Claim(ClaimTypes.Role, user.UserType) // main role (e.g., "super_admin", "Staff", etc.)
            };

		// 2. Add extra staff roles if applicable
		if (user.UserType == "Staff" && user.StaffRoles != null)
		{
		  foreach (var role in user.StaffRoles)
		  {
			claims.Add(new Claim(ClaimTypes.Role, role));
		  }
		}


		string highestRole = user.UserType;
		if (user.UserType == "Staff" && user.StaffRoles != null && user.StaffRoles.Any())
		{
		  var rolePriority = new[] { "super_admin", "branch_manager", "dept_manager", "instructor", "Staff" };
		  var allRoles = new List<string> { user.UserType };
		  allRoles.AddRange(user.StaffRoles);
		  highestRole = rolePriority.FirstOrDefault(r => allRoles.Contains(r)) ?? user.UserType;
		}
		highestRole = highestRole.Replace('_', ' ');
		claims.Add(new Claim("HighestRole", highestRole));
		claims.Add(new Claim("ImageURL", user.ImageURL));

		// 3. Create identity with correct auth scheme
		var identity = new ClaimsIdentity(claims, CookieAuthenticationDefaults.AuthenticationScheme);
		var principal = new ClaimsPrincipal(identity);

		// 4. Create authentication properties
		var authProperties = new AuthenticationProperties
		{
		  IsPersistent = true,
		  ExpiresUtc = DateTimeOffset.UtcNow.AddDays(7)
		};

		// 5. Sign the user in
		await HttpContext.SignInAsync(
			CookieAuthenticationDefaults.AuthenticationScheme,
			principal,
			authProperties
		);

		// 6. Redirect to home or dashboard
		return RedirectToAction("Index", "Home");
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
