using System.ComponentModel.DataAnnotations;

namespace ExaminationSystem.MVC.ViewModels.CourseViewModels
{
  public class CourseDisplayViewModel
  {
	public int Id { get; set; }

	public string Name { get; set; }

	public int Duration { get; set; }

	public int NumberOfTopics { get; set; }

	public bool IsDeleted { get; set; }
  }
}
