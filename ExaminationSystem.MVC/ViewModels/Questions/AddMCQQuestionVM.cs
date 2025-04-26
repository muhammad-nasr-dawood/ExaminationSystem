namespace ExaminationSystem.MVC.ViewModels.Questions
{
  public class AddMCQQuestionVM
  {
	public long StaffId { get; set; }
	public byte Level { get; set; }
	public string Content { get; set; }
	public int TopicId { get; set; }
	public byte AnswerIndex { get; set; }
	public List<Image>? Images { get; set; }
	public List<string> Answers { get; set; }
  }
}
