using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace ExaminationSystem.MVC.ViewModels.StudentViewModels
{
  public class StudentAddVM
  {
	[Required(ErrorMessage = "SSN is required")]
	[Remote("IsSSNExist", controller: "Staff", ErrorMessage = "This SSN is already in use", AdditionalFields = "Email")]
	public long Ssn { get; set; }

	[Required(ErrorMessage = "First name is required")]
	[StringLength(50, ErrorMessage = "First name must not exceed 50 characters")]
	public string Fname { get; set; }

	[Required(ErrorMessage = "Last name is required")]
	[StringLength(50, ErrorMessage = "Last name must not exceed 50 characters")]
	public string Lname { get; set; }

	[Required(ErrorMessage = "Email is required")]
	[StringLength(100, ErrorMessage = "Email must not exceed 100 characters")]
	[Unicode(false)]
	[Remote("IsEmailExist", "Staff", ErrorMessage = "This email is already in use", AdditionalFields = "Ssn")]
	public string Email { get; set; }

	[Required(ErrorMessage = "Zip code is required")]
	[StringLength(6, ErrorMessage = "Zip code must not exceed 6 characters")]
	[Unicode(false)]
	public string ZipCode { get; set; }

	[Required(ErrorMessage = "Street number is required")]
	[StringLength(5, ErrorMessage = "Street number must not exceed 5 characters")]
	[Unicode(false)]
	public string StreetNo { get; set; }

	[Column("BD")]
	public DateOnly Bd { get; set; }

	[Required(ErrorMessage = "Gender is required")]
	[StringLength(1, ErrorMessage = "Gender must be a single character (M/F)")]
	[Unicode(false)]
	public string Gender { get; set; }

	[Required(ErrorMessage = "Phone number is required")]
	[StringLength(11, ErrorMessage = "Phone number must not exceed 11 digits")]
	[Unicode(false)]
	public string PhoneNumber { get; set; }
	[Required(ErrorMessage = "Faculty is required")]
	[StringLength(50, ErrorMessage = "Phone number must not exceed 11 digits")]

	public string Faculty { get; set; }

  }

}
