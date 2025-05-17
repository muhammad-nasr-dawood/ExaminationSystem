using ExaminationSystem.MVC.ViewModels.BranchViewModels;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using System.ComponentModel.DataAnnotations;

namespace ExaminationSystem.MVC.ViewModels.DepartmentViewModels
{
  public class AddEditDeptViewModel
  {
	public int Id { get; set; }
	[Required]
	[StringLength(10, ErrorMessage = "Name cannot exceed 10 characters.")]
	[Remote("IsDeptNameUnique", "Departments", AdditionalFields = "Id", ErrorMessage = "This department name already exists.")]
	public string Name { get; set; }

	[Required]
	[StringLength(30, ErrorMessage = "Description cannot exceed 30 characters.")]
	[Remote("IsDeptDiscUnique", "Departments", AdditionalFields = "Id", ErrorMessage = "This department description already exists.")]
	public string Disc { get; set; }
	public int Capacity { get;  }

	[Display(Name = "Branches (Optional)")]
	public List<int>? SelectedBranchIds { get; set; } = new List<int>();
	public List<BranchViewModel> AvailableBranches { get; set; } = new();

  }
}
