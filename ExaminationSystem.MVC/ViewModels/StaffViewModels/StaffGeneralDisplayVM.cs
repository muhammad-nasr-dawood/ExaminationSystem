using ExaminationSystem.Core.Models;
using Microsoft.AspNetCore.Mvc;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ExaminationSystem.MVC.ViewModels.StaffViewModels
{
  public class StaffGeneralDisplayVM
  {
	[Required]
	public long Ssn { get; set; }

	[Column(TypeName = "decimal(10, 2)")]
	public decimal? Salary { get; set; }
	[Required]
	
	public string FullName { get; set; }
	[Required]
	public string ImageURL { get; set; }
	[Required]
	public bool IsActive { get; set; }	
  }
}
