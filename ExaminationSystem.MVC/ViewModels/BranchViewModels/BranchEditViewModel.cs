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

	
	[Required(ErrorMessage = "Street Number is required.")]
	[Range(1, int.MaxValue, ErrorMessage = "Invalid Street Number.")]
	public int StreetNo { get; set; }


  }
}
