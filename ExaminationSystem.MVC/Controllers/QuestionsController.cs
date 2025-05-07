using ExaminationSystem.MVC.IService;
using ExaminationSystem.MVC.ViewModels.Questions;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore.Metadata.Internal;
using System.Runtime.CompilerServices;

namespace ExaminationSystem.MVC.Controllers
{
  public class QuestionsController : Controller
  {
	private readonly IQuestionService _questionService;
	private readonly ITopicService _topicService;

	public QuestionsController(IQuestionService questionService, ITopicService topicService)
	{
	  _questionService = questionService;
	  _topicService = topicService;
	}


	[HttpGet]
	public async Task<IActionResult> GetByTopic(int topicId, int order, byte type, byte level, int page, int limit)
	{
	  try
	  {
		PaginatedQuestionsVM PaginateResult = await _questionService.GetByTopic(topicId, order, type, level, page, limit);
		return Json(PaginateResult); // Changed from return View() to return Json()
	  }
	  catch (Exception ex)
	  {
		return BadRequest(ex.Message);
	  }
	}


	[HttpGet]

	public async Task<IActionResult> AddTFQuestion(int courseId)
	{
	  try
	  {
		var topics = await _topicService.GetTopicsByCourse(courseId, null, null);
		ViewBag.Topics = topics;
		ViewBag.CourseId = courseId;

		AddTFQuestionVM TFQObj = new AddTFQuestionVM();
		return View(model: TFQObj);
	  }
	  catch (Exception ex)
	  {
		return BadRequest(ex.Message);
	  }
	}

	[HttpPost]
	public async Task<IActionResult> AddTFQuestion([FromForm] AddTFQuestionVM TFQObj)
	{
	  try
	  {
		if (!ModelState.IsValid)
		  return BadRequest(ModelState);

		int result = await _questionService.AddTFQueston(TFQObj);

		if (result == -1)
		{
		  return BadRequest("Failed to add question. Please try again.");
		}
		return Ok("Question added successfully! 😊");
	  }
	  catch (Exception ex)
	  {
		return BadRequest(ex.Message);
	  }
	}


	[HttpGet]

	public IActionResult AddMCQQuestion()
	{
	  AddMCQQuestionVM MCQObj = new AddMCQQuestionVM();
	  return View(model: MCQObj);
	}


	public async Task<IActionResult> AddMCQQuestion([FromForm] AddMCQQuestionVM MCQObj)
	{
	  if (!ModelState.IsValid)
		return BadRequest(ModelState);
	  int result = await _questionService.AddMCQQuestion(MCQObj);
	  // faild to add question 
	  if (result == -1)
	  {
		ModelState.AddModelError(string.Empty, "Failed to add question. Please try again.");
		return View(MCQObj); // return to same view with validation message
	  }
	  string successMessage = "Question added successfully.😊😊";

	  return RedirectToAction("Index", successMessage); // index must the incomming page till now i don't know it 

	}



  }
}

