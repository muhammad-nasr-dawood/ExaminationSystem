using System.ComponentModel.DataAnnotations;

namespace ExaminationSystem.MVC.ViewModels.DepartmentViewModels
{
  public class AddEditDeptViewModel
  {
	public int Id { get; set; }
	[Required]
	[StringLength(10, ErrorMessage = "Name cannot exceed 10 characters.")]
	public string Name { get; set; }

	[Required]
	[StringLength(30, ErrorMessage = "Description cannot exceed 30 characters.")]
	public string Disc { get; set; }
	public int Capacity { get;  }
  }
}
