using ExaminationSystem.Core.Models;

namespace ExaminationSystem.MVC.ViewModels.StudentViewModels
{
  public class StudentDetailsVM
  {
	public long Ssn { get; set; }
	public string Fname { get; set; }
	public string Lname { get; set; }
	public string Email { get; set; }
	// naviagate to get data 
	public string Address { get; set; }
	public DateOnly Bd { get; set; }
	public string Gender { get; set; }
	public string PhoneNumber { get; set; }
	public bool IsActive { get; set; }
	// navigate to get data 
	public string ImageUrl { get; set; }
	public string Faculty { get; set; }
	public int GradYear { get; set; }
	public decimal Gpa { get; set; }
	public virtual ICollection<StudentIntakeBranchDepartmentStudy> StudentIntakeBranchDepartmentStudies { get; set; } = new List<StudentIntakeBranchDepartmentStudy>();



  }
}
