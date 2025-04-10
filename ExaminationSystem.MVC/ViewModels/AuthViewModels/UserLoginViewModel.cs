using ExaminationSystem.Core.Models;
using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace ExaminationSystem.MVC.ViewModels.AuthViewModels
{
  public class UserLoginViewModel
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
	[StringLength(100)]
	public string PasswordHash { get; set; }

	[Required]
	[MaxLength(100)]
	public byte[] Salt { get; set; }

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

	[Required]
	[StringLength(10)]
	[Unicode(false)]
	public string UserType { get; set; }

	public bool IsActive { get; set; }

	[StringLength(30)]
	[Unicode(false)]
	public string ImageId { get; set; }

	public virtual string ImageURL { get; set; }

	public virtual List<string> StaffRoles { get; set; } = new List<string>();

	public virtual string StudentFaculty { get; set; }

	public virtual string Location { get; set; }
  }
}
