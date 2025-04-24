using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace ExaminationSystem.MVC.ViewModels.AccountViewModels;

public class AccountEditViewModel
{
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

  public virtual string? StudentFaculty { get; set; }


  [Column("Grad_Year")]
  public int? GradYear { get; set; }

  [Column("GPA", TypeName = "decimal(4, 3)")]
  public decimal? Gpa { get; set; }

  public string? ImageURL { get; set; }
} 


