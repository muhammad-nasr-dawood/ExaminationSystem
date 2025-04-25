using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace ExaminationSystem.MVC.ViewModels.BranchViewModels;

public class BranchDisplayViewModel
{
	[Key]
	public int Id { get; set; }

	[Required]
	[StringLength(6)]
	[Unicode(false)]
	public string ZipCode { get; set; }

	[Required]
	[StringLength(5)]
	public string StreetNo { get; set; }

	public bool IsDeleted { get; set; }

	public virtual string Location { get; set; }
}
