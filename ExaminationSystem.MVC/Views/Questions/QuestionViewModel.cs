namespace ExaminationSystem.MVC.Views.Questions
{
  public class QuestionViewModel
  {
	public int Id{ get; set; }
	public byte Type { get; set; }
	public byte Level { get; set; }
	public byte TopicId { get; set; }

	public long StaffId { get; set; }
	public string Content { get; set; }
	public string CreatedDate{ get; set; }
	public byte IsDeleted { get; set; }


	public List<Choice>? Choices { get; set; }

	public List<Image>? Images { get; set; }
  }

  public class Choice
  {
	public int Index_{ get; set; }

	public int QuestionId { get; set; }

	public string content { get; set; }

	public bool IsCorrect { get; set; }

  }

  public class Image
  {
	public string Id{ get; set; }
	public int QuestionId { get; set; }
	public string Url{ get; set; }
  }

}
