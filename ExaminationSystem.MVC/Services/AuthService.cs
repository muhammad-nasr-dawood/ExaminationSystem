using AutoMapper;
using Azure.Core;
using ExaminationSystem.Core;
using ExaminationSystem.Core.Models;
using ExaminationSystem.MVC.ViewModels.AuthViewModels;
using Microsoft.AspNetCore.Mvc;

namespace ExaminationSystem.MVC.Services;

public class AuthService : IAuthService
{
	public  IUnitOfWork UnitOfWork { get; }
	private IMapper _mapper;
	private IPasswordService _passwordService;
	public AuthService(IUnitOfWork unitOfWork, IMapper mapper, IPasswordService passwordService)
	{
		UnitOfWork = unitOfWork;
		_mapper = mapper;
		_passwordService = passwordService;
	}

	public UserLoginViewModel ValidateLoginByEmailAndPassword(string email, string password)
	{
	  var user = UnitOfWork.AuthRepo.ValidateLoginByEmail(email);
	  if (user is null ) return null;

	  bool isValidPassword = _passwordService.VerifyPassword(password, user.PasswordHash);
	  if (!isValidPassword) return null;

	  return _mapper.Map<UserLoginViewModel>(user);
	}


  public async Task<string> CreateTempTokenForNewPassword(string email)
  {
	var user = await FindUserByEmail(email);

	if (user is null) return null;

	// 1. Generate a random token
	var token = Guid.NewGuid().ToString();

	// 2. Save token + expiry in database
	user.PasswordResetToken = token;
	user.PasswordResetTokenExpiry = DateTime.UtcNow.AddHours(1); // 1 hour valid
	UnitOfWork.Complete();

	return token;
  }

  public async Task<User> FindUserByEmail(string email)
  {
	Expression<Func<User, bool>> criteria = user => user.Email == email;
	return await UnitOfWork.UserRepo.FindAsync(criteria);
  }

  public async Task ResetPasswordAfterVerification(string email, string password)
  {
	var user = await FindUserByEmail(email);
	if (user is null) return;

	var newHashedPassword = _passwordService.HashPassword(password);
	user.PasswordHash = newHashedPassword;
	user.PasswordResetToken = null;
	user.PasswordResetTokenExpiry = null;
	UnitOfWork.Complete();
  }
}
