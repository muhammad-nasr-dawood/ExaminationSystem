namespace ExaminationSystem.MVC.ViewModels.StudentViewModels
{
  public class StudentExamVM
  {
	public int ExamModelId { get; set; }
	public DateOnly? Date { get; set; }

	public TimeOnly? StartingTime { get; set; }

	public TimeOnly? EndingTime { get; set; }
	public int? Duration { get; set; }
	public string CourseName { get; set; }
	public int ExamId { get; set; }


  }
}
