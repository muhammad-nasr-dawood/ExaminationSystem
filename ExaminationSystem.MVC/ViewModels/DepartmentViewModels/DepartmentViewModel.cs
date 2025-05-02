using System.ComponentModel.DataAnnotations;

namespace ExaminationSystem.MVC.ViewModels.DepartmentViewModels
{
  public class DepartmentViewModel
  {
	public int Id { get; set; }

	[Required]
	[StringLength(10)]
	public string Name { get; set; }

	[Required]
	[StringLength(30)]
	public string Disc { get; set; }

	public bool IsDeleted { get; set; }

	public int Capacity { get; set; }
  }
}
