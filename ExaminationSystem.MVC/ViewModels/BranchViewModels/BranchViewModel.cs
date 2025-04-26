using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace ExaminationSystem.MVC.ViewModels.BranchViewModels
{
  public class BranchViewModel
  {
	public int Id { get; set; }

	[Required]
	[StringLength(6)]
	[Column(TypeName = "varchar(6)")]
	public string ZipCode { get; set; }

	public string? LocationName { get; set; }

	[Required]
	[StringLength(5)]
	public string StreetNo { get; set; }

	public bool IsDeleted { get; set; }

	public string? BranchManagerName { get; set; }
	public int NumberOfDepartments { get; set; }

	public List<LocationViewModel> Locations { get; set; }

  }
}
