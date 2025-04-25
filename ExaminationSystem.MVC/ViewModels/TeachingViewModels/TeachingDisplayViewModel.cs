using ExaminationSystem.Core.Models;
using System.ComponentModel.DataAnnotations.Schema;

namespace ExaminationSystem.MVC.ViewModels.TeachingViewModels;

public class TeachingDisplayViewModel
{
	public long StaffSsn { get; set; }

	public int BranchId { get; set; }

	public int DepartmentId { get; set; }

	public int CourseId { get; set; }

	public int IntakeId { get; set; }

	public string StartingDate { get; set; }

	public string EndingDate { get; set; }

	
	public virtual string Branch { get; set; }

	public virtual string Course { get; set; }

	public virtual string Department { get; set; }

	//public virtual string Intake { get; set; }
	public virtual bool IsInstructorCurrentBranch { get; set; }
}
