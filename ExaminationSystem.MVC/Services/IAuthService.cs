using ExaminationSystem.Core;
using ExaminationSystem.Core.Models;
using ExaminationSystem.MVC.ViewModels.AuthViewModels;

namespace ExaminationSystem.MVC.Services
{
  public interface IAuthService
  {
	IUnitOfWork UnitOfWork { get; }
	UserLoginViewModel ValidateLoginByEmailAndPassword(string email, string password);
  }
}
