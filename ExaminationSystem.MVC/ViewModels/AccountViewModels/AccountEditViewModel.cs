using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using Microsoft.AspNetCore.Mvc;

namespace ExaminationSystem.MVC.ViewModels.AccountViewModels;

public class AccountEditViewModel
{
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
  [Remote(action: "VerifyEmail", controller: "Account", ErrorMessage = "Email already exists.")]
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
  [Remote(action: "VerifyPhone", controller: "Account", ErrorMessage = "Phone number already in use.")]
  [RegularExpression(@"^(010|011|012|015)\d{8}$",
		  ErrorMessage = "Phone must be 11 digits starting with 010, 011, 012, or 015")]
  public string PhoneNumber { get; set; }

  [Display(Name = "Faculty")]
  public virtual string? StudentFaculty { get; set; }

  [Display(Name = "Graduation Year")]
  [Column("Grad_Year")]
  public int? GradYear { get; set; }

  [Display(Name = "GPA")]
  [Column("GPA", TypeName = "decimal(4, 3)")]
  public decimal? Gpa { get; set; }

  [Display(Name = "Image URL")]
  public string? ImageURL { get; set; }
}
