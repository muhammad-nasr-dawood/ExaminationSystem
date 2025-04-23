namespace ExaminationSystem.MVC.Views.Questions
{
  public class PaginatedQuestionsViewModel
  {
	public List<QuestionViewModel> Questions { get; set; }

	public int Total { get; set; }

	public int page { get; set; }

	public int limit { get; set; }

 
  }
}
