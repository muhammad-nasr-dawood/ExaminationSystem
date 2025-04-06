using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace ExaminationSystem.MVC.ViewModels
{
  public class StudentViewModel
  {
	public long Ssn { get; set; }

	[Required]
	[StringLength(100)]
	public string Faculty { get; set; }

	[Required]
	[Column("Grad_Year")]
	public int GradYear { get; set; }
	[Required]
	[Column(TypeName = "decimal(4, 3)")]
	public decimal Gpa { get; set; }
  }
}
