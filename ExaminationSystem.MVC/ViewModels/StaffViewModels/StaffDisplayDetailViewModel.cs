using ExaminationSystem.Core.Models;
using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ExaminationSystem.MVC.ViewModels.StaffViewModels
{
  public class StaffDisplayDetailViewModel
  {

	[Column("SSN")]
	public long Ssn { get; set; }

	[Required]
	[StringLength(50)]
	public string Fname { get; set; }

	[Required]
	[StringLength(50)]
	public string Lname { get; set; }

	[Required]
	[StringLength(100)]
	[Unicode(false)]
	public string Email { get; set; }


	[Required]
	[StringLength(5)]
	[Unicode(false)]
	public string StreetNo { get; set; }

	[Column("BD")]
	public DateOnly Bd { get; set; }

	[Required]
	[StringLength(1)]
	[Unicode(false)]
	public string Gender { get; set; }

	[Required]
	[StringLength(11)]
	[Unicode(false)]
	public string PhoneNumber { get; set; }


	public bool IsActive { get; set; }


	public virtual string? ImageURL { get; set; }

	[Required]
	[StringLength(6)]
	[Unicode(false)]
	public string ZipCode { get; set; }
	public virtual string? Location { get; set; }

	[Column(TypeName = "decimal(10, 2)")]
	public decimal? Salary { get; set; }


	public virtual List<string> Roles { get; set; } = new List<string>();
  }
}
