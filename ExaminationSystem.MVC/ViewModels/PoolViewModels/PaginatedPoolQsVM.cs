using System.Text.Json.Serialization;

namespace ExaminationSystem.MVC.ViewModels.PoolViewModels
{

  public class PaginatedPoolQsVM
  {

	public List<Questions> Questions { get; set; } = new List<Questions>();
	public int Total { get; set; }

	public int Page { get; set; }

	public int Limit { get; set; }

  }
	public class Questions
  {
	public int Id { get; set; }
	public int Type { get; set; }
	public int Level { get; set; }
	public int TopicId { get; set; }
	public string Content { get; set; }

	[JsonPropertyName("createdDate")]
	public DateTime CreatedDate { get; set; }

	[JsonPropertyName("MCChoices")]
	public List<MultipleChoice>? MCChoices { get; set; }

	[ JsonPropertyName("TFChoices")]
	public List<TFChoices>? TFChoices { get; set; }
	public List<Image>? Images { get; set; }

  }
  public class MultipleChoice
  {
	public int Index_ { get; set; }
	public int QuestionId { get; set; }
	public string Content { get; set; }
	public bool IsCorrect { get; set; }
  }

  public class Image
  {
	public string Id { get; set; }
	public int QuestionId { get; set; }
	public string Url { get; set; }
  }

  public class TFChoices
  {
	public int QuestionId { get; set; }
	public bool IsTrue { get; set; }
  }


}
