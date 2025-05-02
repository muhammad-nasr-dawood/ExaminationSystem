using System.ComponentModel.DataAnnotations;

namespace ExaminationSystem.MVC.ViewModels.StudentViewModels
{
  public class StudentVM
  {
	[Display(Name ="Full Name")]
	public string FullName { get; set; }
	public string Email { get; set; }
	public string Gender { get; set; }
	public bool IsActive { get; set; }
	public string Faculty { get; set; }
	public string ImageUrl { get; set; }

	public long Ssn { get; set; }

  }
}
