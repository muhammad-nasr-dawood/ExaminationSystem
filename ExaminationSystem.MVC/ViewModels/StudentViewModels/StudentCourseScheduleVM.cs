using System.ComponentModel.DataAnnotations.Schema;

namespace ExaminationSystem.MVC.ViewModels.StudentViewModels
{
  public class StudentCourseScheduleVM
  {
	public string StaffName { get; set; }
	public string CourseName { get; set; }
	[Column(TypeName = "datetime")]
	public DateTime StartingDate { get; set; }

	[Column(TypeName = "datetime")]
	public DateTime? EndingDate { get; set; }

  }
}
