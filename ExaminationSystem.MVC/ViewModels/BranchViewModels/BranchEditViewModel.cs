using System.ComponentModel.DataAnnotations;

namespace ExaminationSystem.MVC.ViewModels.BranchViewModels
{
  public class BranchEditViewModel
  {
	[Required]
	public int Id { get; set; } 

	[Required]
	public string ZipCode { get; set; }
	public string? BranchManagerName { get; set; }
	public string? LocationName { get; set; }

	[Required]
	[StringLength(5, ErrorMessage = "Street Number must be 5 characters long.")]
	[Range(1, int.MaxValue, ErrorMessage = "Street Number must be greater than 0.")]
	public string StreetNo { get; set; }

  }
}
