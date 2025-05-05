using ExaminationSystem.MVC.IService;
using Microsoft.AspNetCore.Mvc;

namespace ExaminationSystem.MVC.Controllers
{
  public class ExamController : Controller
  {
	IExamService _examService;

	public ExamController(IExamService examService)
	{
	  this._examService = examService;
	}
	public IActionResult Index()
	{
	  return View();
	}

	[HttpPost]
	public async Task<IActionResult> SetExamSession(long staffId, int poolId, DateOnly date, TimeOnly startingTime, TimeOnly endingTime, int duration)
	{
	  try
	  {
		var res = await _examService.SetExamSession(staffId, poolId, date, startingTime, endingTime, duration);

		if (res > 0)
		{
		  return Json(new { success = true, message = "Exam session set successfully." });
		}
		else
		{
		  return Json(new { success = false, message = "Failed to set exam session." });
		}

	  }
	  catch (Exception ex)
	  {
		return Json(new { success = false, message = ex.Message });
	  }

	}





  }



}
