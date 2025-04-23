using ExaminationSystem.MVC.Services;
using ExaminationSystem.MVC.Views.Questions;
using Microsoft.AspNetCore.Mvc;

namespace ExaminationSystem.MVC.Controllers
{
  public class QuestionsController : Controller
  {
	private IQuestionService _questionService;

	public QuestionsController(IQuestionService questionService)
	{
	  _questionService = questionService;
	}


	[HttpGet]
	public async Task<IActionResult> GetByTopic(int topicId, int order, byte type, byte level, int page, int limit)
	{
	  try
	  {
		PaginatedQuestionsViewModel PaginateResult = await _questionService.GetByTopic(topicId, order, type, level, page, limit);
		return View(PaginateResult);
	  }
	  catch (Exception ex)
	  {
		return BadRequest(ex.Message);
	  }

	}

  }
}

