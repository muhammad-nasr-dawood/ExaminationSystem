using System.ComponentModel.DataAnnotations;

namespace ExaminationSystem.MVC.ViewModels.DepartmentViewModels
{
  public class DepartmentViewModel
  {
	public int Id { get; set; }
	public string Name { get; set; }
	public string Disc { get; set; }
	public bool IsDeleted { get; set; }
	public int TotalCapacity { get; set; }    
	public int BranchCapacity { get; set; }
  }
}
