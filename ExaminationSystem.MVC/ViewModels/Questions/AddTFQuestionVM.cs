using ExaminationSystem.MVC.CustomValidation.Questions;
using System.ComponentModel.DataAnnotations;

namespace ExaminationSystem.MVC.ViewModels.Questions
{
  public class AddTFQuestionVM
  {
	  public long? StaffId { get; set; }

	  [Range(0,2)]
	  public byte? Level { get; set; }

	  [MinLength(30)]
	  public string Content { get; set; }

	  public int? TopicId { get; set; }

	  public bool? IsTrue { get; set; }

	[MaxFileSize(2)]
	public List<IFormFile>? Images { get; set; }

  }

	 

  
}
