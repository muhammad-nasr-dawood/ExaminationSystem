using System.ComponentModel.DataAnnotations;

namespace ExaminationSystem.MVC.ViewModels.AuthViewModels
{
  public class LoginViewModel
  {
	[Required(ErrorMessage = "Email is required.")]
	[EmailAddress(ErrorMessage = "Invalid email format.")]
	public string Email { get; set; }

	[Required(ErrorMessage = "Password is required.")]
	//[RegularExpression(@"^(?=.*[A-Z])(?=.*[a-z])(?=.*\d)(?=.*[@$!%*?&])[A-Za-z\d@$!%*?&]{8,}$",
	//	ErrorMessage = "Password must be at least 8 characters long, contain at least one uppercase letter, one lowercase letter, one number, and one special character.")]
	[DataType(DataType.Password)]
	public string Password { get; set; }
  }
}
