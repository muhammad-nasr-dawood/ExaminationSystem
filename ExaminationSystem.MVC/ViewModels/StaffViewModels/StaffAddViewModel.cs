using ExaminationSystem.Core.Models;
using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using Microsoft.AspNetCore.Mvc;

namespace ExaminationSystem.MVC.ViewModels.StaffViewModels
{
  public class StaffAddViewModel
  {
	[Remote("IsSSNExist", "Staff", ErrorMessage = "SSN already exists", AdditionalFields = "Email")]
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
	[Remote("IsEmailExist", "Staff", ErrorMessage = "Email already exists", AdditionalFields = "Ssn")]
	public string Email { get; set; }

	[Required]
	[StringLength(100)]
	[DataType(DataType.Password)]
	public string Password { get; set; }
	[DataType(DataType.Password)]
	[Compare("Password")]
	[NotMapped]
	public string ConfirmPassword { get; set; }

	[Required]
	[StringLength(6)]
	[Unicode(false)]
	public string ZipCode { get; set; }

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

  }
}
