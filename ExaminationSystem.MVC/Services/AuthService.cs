using AutoMapper;
using ExaminationSystem.Core;
using ExaminationSystem.Core.Models;
using ExaminationSystem.MVC.ViewModels.AuthViewModels;
using Microsoft.AspNetCore.Mvc;

namespace ExaminationSystem.MVC.Services
{
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



  }
}
