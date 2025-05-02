using ExaminationSystem.Core.Models;
using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using Microsoft.AspNetCore.Mvc;

namespace ExaminationSystem.MVC.ViewModels.StaffViewModels;

public class StaffAddViewModel
{
  [Display(Name = "SSN")]
  [Remote("IsSSNExist", "Staff", ErrorMessage = "SSN already exists")]
  [Required]
  [RegularExpression(@"^[2-3]\d{13}$",
		  ErrorMessage = "SSN must be 14 digits starting with 2 or 3")]
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

  [Display(Name = "Zip Code")]
  [Required]
  [StringLength(6)]
  [Unicode(false)]
  public string ZipCode { get; set; }

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

  public int? BranchId { get; set; } // optional
}
