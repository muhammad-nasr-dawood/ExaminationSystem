using System.ComponentModel.DataAnnotations;

namespace ExaminationSystem.MVC.ViewModels.AuthViewModels;

public class ForgotPasswordViewModel
{
  [Required]
  [EmailAddress]
  public string Email { get; set; }
}

