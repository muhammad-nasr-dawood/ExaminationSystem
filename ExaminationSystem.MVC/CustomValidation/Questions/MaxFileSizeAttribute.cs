using System.ComponentModel.DataAnnotations;
using Microsoft.AspNetCore.Http;
namespace ExaminationSystem.MVC.CustomValidation.Questions
{

  public class MaxFileSizeAttribute : ValidationAttribute
  {
	private readonly long _maxFileSizeInBytes;

	public MaxFileSizeAttribute(long maxFileSizeInMB)
	{
	  _maxFileSizeInBytes = maxFileSizeInMB * 1024 * 1024;
	}

	protected override ValidationResult IsValid(object value, ValidationContext validationContext)
	{
	  var files = value as List<IFormFile>;

	  if (files != null) //allow null
	  {
		foreach (var file in files)
		{
		  if (file != null && file.Length > _maxFileSizeInBytes) // don 't excceed max size
		  {
			return new ValidationResult($"File size must not exceed {_maxFileSizeInBytes / (1024 * 1024)} MB.");
		  }
		}
	  }

	  return ValidationResult.Success;
	}
  }

}
