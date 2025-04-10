using AutoMapper;
using ExaminationSystem.Core;
using ExaminationSystem.MVC.ViewModels.AuthViewModels;

namespace ExaminationSystem.MVC.Services
{
  public class AuthService : IAuthService
  {
	public  IUnitOfWork UnitOfWork { get; }
	private IMapper _mapper;

	public AuthService(IUnitOfWork unitOfWork, IMapper mapper)
	{
		UnitOfWork = unitOfWork;
		_mapper = mapper;
	}

	public UserLoginViewModel ValidateLoginByEmailAndPassword(string email, string password)
	{
	  var user = UnitOfWork.AuthRepo.ValidateLoginByEmailAndPassword(email, password);
	  return _mapper.Map<UserLoginViewModel>(user);
	}
  }
}
