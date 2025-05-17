using ExaminationSystem.Core.Consts;
using ExaminationSystem.MVC.Services;
using ExaminationSystem.MVC.ViewModels.CourseViewModels;
using ExaminationSystem.MVC.ViewModels.TopicViewModels;
using Microsoft.AspNetCore.Mvc;

namespace ExaminationSystem.MVC.Controllers
{
  public class TopicsController : Controller
  {

	private readonly ITopicService _topicService;

	public TopicsController(ITopicService topicService)
	{
	  _topicService = topicService;
	}
	public IActionResult Index(int? courseId)
	{
	  ViewBag.Courses = _topicService.GetAllCourses();
	  ViewBag.CourseIdFilter = courseId;
	  return View();
	}


	[HttpPost]
	public async Task<IActionResult> GetAllTopics()
	{
	  try
	  {
		var draw = int.Parse(Request.Form["draw"].FirstOrDefault() ?? "1");
		var start = int.Parse(Request.Form["start"].FirstOrDefault() ?? "0");
		var length = int.Parse(Request.Form["length"].FirstOrDefault() ?? "10");

		var searchValue = Request.Form["search[value]"].FirstOrDefault();

		var filterDeleted = bool.TryParse(Request.Form["isDeleted"].FirstOrDefault(), out var isDel) ? isDel : (bool?)null;
		var courseId = int.TryParse(Request.Form["courseId"].FirstOrDefault(), out var cId) ? cId : (int?)null;

		var orderColumnIndex = int.Parse(Request.Form["order[0][column]"].FirstOrDefault() ?? "0");
		var orderDir = Request.Form["order[0][dir]"].FirstOrDefault() ?? "asc";

		string[] columnNames = { "Name", "NumberOfCourses", "IsDeleted" };
		string orderBy = columnNames.ElementAtOrDefault(orderColumnIndex) ?? "Name";

		int pageNumber = (start / length) + 1;
		int pageSize = length;

		var topicResult = await _topicService.FindAll(
			pageNumber: pageNumber,
			pageSize: pageSize,
			courseIdFilter: courseId,
			isDeletedFilter: filterDeleted,
			searchTerm: searchValue,
			columnOrderBy: orderBy,
			orderByDirection: orderDir == "asc" ? OrderBy.Ascending : OrderBy.Descending
		);

		return Json(new
		{
		  draw = draw,
		  recordsTotal = topicResult.TotalItemsInTable,
		  recordsFiltered = topicResult.TotalFilteredItems,
		  data = topicResult.Items
		});
	  }
	  catch
	  {
		return Json(new
		{
		  draw = 1,
		  recordsTotal = 0,
		  recordsFiltered = 0,
		  data = new List<object>()
		});
	  }
	}


	[HttpPost]
	public IActionResult RestoreTopic(int id)
	{
	  var success = _topicService.RestoreTopic(id);
	  return Json(new { success });
	}


	[HttpPost]
	public IActionResult Delete(int id)
	{
	  try
	  {
		var course = _topicService.Delete(id);

		return Json(new { success = true, isActive = course.IsDeleted });
	  }
	  catch (Exception ex)
	  {

		return Json(new { success = false, message = "An error occurred while toggling status." });
	  }
	}

	[HttpGet]
	public IActionResult GetAvailableCourses()
	{
	  var Crs = _topicService.GetAvailableCourses();
	  return Json(Crs);
	}

	[HttpGet]
	public IActionResult GetTopicForEdit(int id)
	{
	  var topic = _topicService.GetTopicForEdit(id);
	  if (topic == null)
		return Json(new { success = false, message = "Topic not found." });

	  return Json(topic);
	}

	[HttpPost]
	public IActionResult AddTopic(TopicAddEditViewModel model)
	{
	  if (!ModelState.IsValid)
		return Json(new { success = false, message = "Invalid data." });

	  var success = _topicService.AddTopic(model);
	  return Json(success);
	}
	[HttpPost]
	public IActionResult EditTopic(TopicAddEditViewModel model)
	{
	  if (!ModelState.IsValid || !model.Id.HasValue)
		return Json(new { success = false, message = "Invalid data." });

	  var success = _topicService.EditTopic(model);
	  return Json(success);
	}
	[HttpGet]
	public async Task<IActionResult> IsTopicNameUnique(string name, int? id)
	{
	  var result = await _topicService.CheckTopicNameStatusAsync(name, id);

	  return result == null ? Json(true) : Json(result);
	}


  }
}
