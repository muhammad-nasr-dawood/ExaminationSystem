using Microsoft.AspNetCore.Mvc;
using System.ComponentModel.DataAnnotations;

namespace ExaminationSystem.MVC.ViewModels.AccountViewModels;
public class ChangePasswordViewModel
{
  [Required]
  [DataType(DataType.Password)]
  [Remote(action: "VerifyOldPassword", controller: "Account", ErrorMessage = "Incorrect password.")]
  public string OldPassword { get; set; }

  [Required]
  [DataType(DataType.Password)]
  [RegularExpression(
	  @"^(?=.*[a-z])(?=.*[\d\W\s]).{8,}$",
	  ErrorMessage = "Password must be 8+ chars with at least 1 lowercase letter and 1 number/symbol.")]
  public string NewPassword { get; set; }

  [Required]
  [DataType(DataType.Password)]
  [Compare("NewPassword", ErrorMessage = "Passwords don't match.")]
  public string ConfirmNewPassword { get; set; }
}
