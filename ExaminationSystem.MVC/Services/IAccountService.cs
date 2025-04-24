using ExaminationSystem.Core;
using ExaminationSystem.MVC.ViewModels.AccountViewModels;

namespace ExaminationSystem.MVC.Services;

public interface IAccountService
{
  IUnitOfWork UnitOfWork { get; }

  AccountEditViewModel GetAccount(long id);
  Task<string> UpdateImage(IFormFile file,  long userId);

  Task<string> DeleteImage(long id);
  Task<bool> UpdateAccount(long userId, List<string> roles, AccountEditViewModel model);

  Task<bool> VerifyOldPassword(long userId, string oldPassword);

  Task<bool> ChangePassword(long userId, string oldPassword, string newPassword);

  Task<bool> VerifyEmail(long userId, string email);

  Task<bool> VerifyPhone(long userId, string phoneNumber);
  Task<bool> VerifySSN(long userId);
}
