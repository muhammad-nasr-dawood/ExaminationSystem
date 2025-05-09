using ExaminationSystem.MVC.ViewModels.CourseViewModels;
using System.ComponentModel.DataAnnotations;

namespace ExaminationSystem.MVC.ViewModels.TopicViewModels
{
  public class TopicAddEditViewModel
  {
	public int? Id { get; set; }

	[Required]
	[StringLength(30, ErrorMessage = "Name cannot exceed 30 characters.")]
	public string Name { get; set; }

	[Display(Name = "Courses")]
	public List<int>? SelectedCourseIds { get; set; } = new();

	public List<CourseDisplayViewModel> AvailableCourses { get; set; } = new();
  }

}
