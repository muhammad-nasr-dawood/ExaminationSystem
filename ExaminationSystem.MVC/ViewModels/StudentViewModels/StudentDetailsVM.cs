using ExaminationSystem.Core.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ExaminationSystem.MVC.ViewModels.StudentViewModels
{
  public class StudentDetailsVM
  {
	[Required(ErrorMessage = "SSN is required")]
	[Remote("IsSSNExist", controller: "Students", ErrorMessage = "This SSN is already in use", AdditionalFields = "Email")]
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
	[Remote("IsEmailExist", "Students", ErrorMessage = "This email is already in use", AdditionalFields = "Ssn")]
	public string Email { get; set; }



	[Required(ErrorMessage = "Zip code is required")]
	[StringLength(6, ErrorMessage = "Zip code must not exceed 6 characters")]
	[Unicode(false)]
	public string ZipCode { get; set; }

	[Required(ErrorMessage = "Street number is required")]
	[StringLength(5, ErrorMessage = "Street number must not exceed 5 characters")]
	[Unicode(false)]
	public string StreetNo { get; set; }



	[Required(ErrorMessage = "Faculty is required")]
	public string Faculty { get; set; }

	[Required(ErrorMessage = "GPA is required")]
	[Range(0.0, 4.0, ErrorMessage = "GPA must be between 0.0 and 4.0")]
	public double Gpa { get; set; }

	[Required(ErrorMessage = "Graduation year is required")]
	[Range(2000, 2025, ErrorMessage = "Graduation year must be between 2000 and 2025")]
	public int GradYear { get; set; }



	[Column("BD")]
	public DateOnly Bd { get; set; }

	[Required(ErrorMessage = "Gender is required")]
	[StringLength(1, ErrorMessage = "Gender must be a single character (M/F)")]
	[Unicode(false)]
	public string Gender { get; set; }

	[Required(ErrorMessage = "Phone number is required")]
	[StringLength(11, ErrorMessage = "Phone number must not exceed 11 digits")]
	[Unicode(false)]
	[Remote("isPhoneNumberExist", "Students", ErrorMessage = "This Phone Number is already in use", AdditionalFields = "Ssn")]
	public string PhoneNumber { get; set; }

	public bool IsActive { get; set; }


	public virtual string? ImageURL { get; set; }



	public virtual string? Location { get; set; }
	[Display(Name = "Student Department")]
	public List<string>? StudentDepartment { get; set; } = new List<string>();
  }
}
