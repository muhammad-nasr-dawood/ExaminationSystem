using ExaminationSystem.Core.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ExaminationSystem.MVC.ViewModels.StaffViewModels
{
  public class StaffDisplayDetailViewModel
  {
	[Display(Name = "SSN")]
	[Column("SSN")]
	[Remote("IsSSNExist", "Staff", ErrorMessage = "SSN already exists!")]
	public long Ssn { get; set; }

	[Display(Name = "First Name")]
	[Required]
	[StringLength(50)]
	public string Fname { get; set; }

	[Display(Name = "Last Name")]
	[Required]
	[StringLength(50)]
	public string Lname { get; set; }

	[Display(Name = "Email Address")]
	[Required]
	[StringLength(100)]
	[Unicode(false)]
	[Remote("IsEmailExist", "Staff", ErrorMessage = "Email already exists", AdditionalFields = "Ssn")]
	public string Email { get; set; }

	[Display(Name = "Street Number")]
	[Required]
	[StringLength(5)]
	[Unicode(false)]
	public string StreetNo { get; set; }

	[Display(Name = "Birth Date")]
	[Column("BD")]
	public DateOnly Bd { get; set; }

	[Display(Name = "Gender")]
	[Required]
	[StringLength(1)]
	[Unicode(false)]
	public string Gender { get; set; }

	[Display(Name = "Phone Number")]
	[Required]
	[StringLength(11)]
	[Unicode(false)]
	[Remote("IsPhoneNumberExist", "Staff", ErrorMessage = "Phone number already in use!", AdditionalFields = "Ssn")]
	[RegularExpression(@"^(010|011|012|015)\d{8}$",
			ErrorMessage = "Phone must be 11 digits starting with 010, 011, 012, or 015")]
	public string PhoneNumber { get; set; }

	[Display(Name = "Active Status")]
	public bool IsActive { get; set; }

	[Display(Name = "Profile Image")]
	public virtual string? ImageURL { get; set; }

	[Display(Name = "Zip Code")]
	[Required]
	[StringLength(6)]
	[Unicode(false)]
	public string ZipCode { get; set; }

	[Display(Name = "Location")]
	public virtual string? Location { get; set; }

	[Display(Name = "Salary")]
	[Column(TypeName = "decimal(10, 2)")]
	public decimal? Salary { get; set; }

	[Display(Name = "Roles")]
	public virtual List<string> Roles { get; set; } = new List<string>();
	[Display(Name = "Branch")]
	public int? BranchId { get; set; } // optional
  }
}
