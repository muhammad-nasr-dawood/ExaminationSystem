namespace ExaminationSystem.MVC.ViewModels.TopicViewModels
{
  public class TopicDisplayViewModel
  {
	public int Id { get; set; }
	public string Name { get; set; }
	public int NumberOfCourses { get; set; }
	public bool IsDeleted { get; set; }
  }
}
