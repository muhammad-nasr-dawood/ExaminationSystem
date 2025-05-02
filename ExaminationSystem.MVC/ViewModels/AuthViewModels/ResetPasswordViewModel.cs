using System.ComponentModel.DataAnnotations;

namespace ExaminationSystem.MVC.ViewModels.AuthViewModels;

public class ResetPasswordViewModel
{
  [Required]
  public string Token { get; set; }

  [Required]
  [EmailAddress]
  public string Email { get; set; }

  [Required(ErrorMessage = "Password is required.")]
  [RegularExpression(@"^(?=.*[A-Z])(?=.*[a-z])(?=.*\d)(?=.*[@$!%*?&])[A-Za-z\d@$!%*?&]{8,}$",
	ErrorMessage = "Password must be at least 8 characters long, contain at least one uppercase letter, one lowercase letter, one number, and one special character.")]
  [DataType(DataType.Password)]
  public string NewPassword { get; set; }

  [Required(ErrorMessage = "Please confirm your password.")]
  [Compare("NewPassword", ErrorMessage = "The password and confirmation password do not match.")]
  [DataType(DataType.Password)]
  public string ConfirmPassword { get; set; }
}
