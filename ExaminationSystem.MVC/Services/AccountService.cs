using ExaminationSystem.Core;
using ExaminationSystem.Core.Models;
using ExaminationSystem.MVC.ViewModels.AccountViewModels;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authentication;
using Microsoft.DiaSymReader;
using System.Security.Claims;
using Microsoft.AspNetCore.Http;
using System.Security.Policy;

namespace ExaminationSystem.MVC.Services;

public class AccountService: IAccountService
{

  public IUnitOfWork UnitOfWork {  get; }
  private readonly IMapper _mapper;
  private readonly IImageService _imageService;
  private readonly IPasswordService _passwordService;
  private readonly IUserClaimService _userClaimService;
  public AccountService(IUnitOfWork unitOfWork, IMapper mapper, IImageService imageService, IPasswordService passwordService, IUserClaimService userClaimService	)
  {
	UnitOfWork = unitOfWork;
	_mapper = mapper;
	_imageService = imageService;
	_passwordService = passwordService;
	_userClaimService = userClaimService;
  }

  public AccountEditViewModel GetAccount(long id)
  {
	var account = UnitOfWork.UserRepo.GetById(id);

	var mappedAccount = _mapper.Map<AccountEditViewModel>(account);

	return mappedAccount;
  }

  public async Task<string> UpdateImage(IFormFile file, long userId)
  {
	//var (id, url) = await _imageService.UploadImageAsync(file);
	//nasser changing
	string folderName = "/ExaminationImagesFolder";

	// Pass the folderName argument to the UploadImageAsync method  
	var (id, url) = await _imageService.UploadImageAsync(file, folderName);

	var user = UnitOfWork.UserRepo.GetById(userId);
	ProfileImage currentImage;
	if(user.ImageId is not null)
	{
	  currentImage = UnitOfWork.ProfileImageRepo.GetById(user.ImageId);
	  var tempImageId = user.ImageId;
	  UnitOfWork.ProfileImageRepo.Delete(currentImage);
	  await _imageService.DeleteImageAsync(tempImageId); 
	}

	  currentImage = new ProfileImage { ImageId = id, ImageUrl = url };
	  UnitOfWork.ProfileImageRepo.Add(currentImage);
	  user.ImageId = id;

	UnitOfWork.Complete();
	await _userClaimService.RefreshUserClaim("ImageURL", currentImage.ImageUrl);
	return currentImage.ImageUrl;
  }

  public async Task<string> DeleteImage(long userId)
  {
	var user = UnitOfWork.UserRepo.GetById(userId);
	ProfileImage currentImage;
	if (user.ImageId is not null)
	{
	  currentImage = UnitOfWork.ProfileImageRepo.GetById(user.ImageId);
	  var tempImageId = user.ImageId;
	  UnitOfWork.ProfileImageRepo.Delete(currentImage);
	  await _imageService.DeleteImageAsync(tempImageId);
	  UnitOfWork.Complete();
	}
	await _userClaimService.RefreshUserClaim("ImageURL", "/img/defaultImages/defaultImage.png");
	return "/img/defaultImages/defaultImage.png";
  }

  public async Task<bool> UpdateAccount(long userId, List<string> roles, AccountEditViewModel model)
  {
	var user = await UnitOfWork.UserRepo.GetByIdAsync(userId);
	bool isStudent = roles.Any(role => role == "Student");
	_mapper.Map(model, user);
	if (isStudent)
	{
	  var student = await UnitOfWork.StudentRepo.GetByIdAsync(userId);
	  _mapper.Map(model, student);
	}

	UnitOfWork.Complete();
	await _userClaimService.RefreshUserClaim(ClaimTypes.Name, $"{model.Fname} {model.Lname}" );
	return true;

  }

  public async Task<bool> VerifyOldPassword(long userId, string oldPassword)
  {
	var user = await UnitOfWork.UserRepo.GetByIdAsync(userId);
	var isSuccess = _passwordService.VerifyPassword(oldPassword, user.PasswordHash);
	return isSuccess;
  }

  public async Task<bool> ChangePassword(long userId, string oldPassword, string newPassword)
  {
	var isSuccess = await VerifyOldPassword(userId, oldPassword);
	if(isSuccess)
	{
	  var newHashedPassword = _passwordService.HashPassword(newPassword);
	  var user = await UnitOfWork.UserRepo.GetByIdAsync(userId);
	  user.PasswordHash = newHashedPassword;
	  UnitOfWork.Complete();
	  return true;
	}
	return false;
  }

  public async Task<bool> VerifyEmail(long userId, string email)
  {
	Expression<Func<User, bool>> criteria = user => user.Email == email; // we used expression in order to deal with the database --> cannot use delgates directly when dealing with database
	var user = await UnitOfWork.UserRepo.FindAsync(criteria);

	if(user is not null && user.Ssn != userId) return false; // it means that email already taken by another user

	return true;
  }

  public async Task<bool> VerifyPhone(long userId, string phoneNumber)
  {
	Expression<Func<User, bool>> criteria = user => user.PhoneNumber == phoneNumber; // we used expression in order to deal with the database --> cannot use delgates directly when dealing with database input is user and return is bool
	var user = await UnitOfWork.UserRepo.FindAsync(criteria);

	if (user is not null && user.Ssn != userId) return false; // it means that email already taken by another user

	return true;
  }


  public async Task<bool> VerifySSN(long userId)
  {
	var user = await UnitOfWork.UserRepo.GetByIdAsync(userId);
	if (user is null) return true;
	return false;
  }


  public async Task RefreshUserClaim(HttpContext httpContext, string claimType, string newValue)
  {
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
