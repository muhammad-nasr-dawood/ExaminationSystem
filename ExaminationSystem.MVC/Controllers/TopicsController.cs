using ExaminationSystem.MVC.IService;
using Microsoft.AspNetCore.Mvc;

namespace ExaminationSystem.MVC.Controllers
{
  public class TopicsController : Controller
  {
	public ITopicService TopicService { get; set; }

	public IActionResult Index()
	{
	  return View();
	}


	public TopicsController(ITopicService topicService)
	{
	  TopicService = topicService;
	}


	[HttpGet]
	public async Task<IActionResult> GetTopicsByCourse(int courseId, int? pageSize, int? pageNumber)
	{
	  try
	  {
		var res = await TopicService.GetTopicsByCourse(courseId, pageSize, pageNumber);
		return Json(res);
	  }
	  catch (Exception ex)
	  {
		// Log the exception (not implemented here)
		return BadRequest(ex.Message);
	  }

	}


  }
}
