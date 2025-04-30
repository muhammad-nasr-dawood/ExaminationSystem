using ExaminationSystem.MVC.ViewModels.TopicViewModels;
using System.ComponentModel.DataAnnotations;

namespace ExaminationSystem.MVC.ViewModels.CourseViewModels
{
  public class CourseAddEditViewModel
  {
	public int? Id { get; set; } 

	[Required]
	[StringLength(30, ErrorMessage = "Name cannot exceed 30 characters.")]
	public string Name { get; set; }

	[Required(ErrorMessage = "Duration is required.")]
	[Range(1, int.MaxValue, ErrorMessage = "Invalid Duration Number.")]
	public int Duration { get; set; }

	[Required(ErrorMessage = "Please select at least one topic.")]
	[Display(Name = "Course Topics")]
	public List<int> SelectedTopicIds { get; set; }



	public List<TopicDisplayViewModel> AvailableTopics { get; set; } = new();
  }
}
