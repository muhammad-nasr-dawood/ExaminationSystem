using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authentication;
using System.Security.Claims;

namespace ExaminationSystem.MVC.Services;

public class UserClaimService: IUserClaimService
{
	private readonly IHttpContextAccessor _httpContextAccessor;

	public UserClaimService(IHttpContextAccessor httpContextAccessor)
	{
	  _httpContextAccessor = httpContextAccessor;
	}

	public async Task RefreshUserClaim(string claimType, string newValue)
	{
	  var httpContext = _httpContextAccessor.HttpContext;
	  var identity = (ClaimsIdentity)httpContext.User.Identity;
	  var existingClaim = identity.FindFirst(claimType);
	  if (existingClaim != null)
	  {
		identity.RemoveClaim(existingClaim);
	  }
	  identity.AddClaim(new Claim(claimType, newValue));

	  var principal = new ClaimsPrincipal(identity);

	  await httpContext.SignOutAsync();
	  await httpContext.SignInAsync(CookieAuthenticationDefaults.AuthenticationScheme, principal);
	}
}
