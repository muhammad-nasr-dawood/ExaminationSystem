namespace ExaminationSystem.MVC.ViewModels.Questions
{
  public class PaginatedQuestionsVM
  {
	public List<QuestionVM> Questions { get; set; }

	public int Total { get; set; }

	public int page { get; set; }

	public int limit { get; set; }

 
  }
}
