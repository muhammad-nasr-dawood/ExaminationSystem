using ExaminationSystem.Core.Consts;
using ExaminationSystem.Core.Models;
using ExaminationSystem.MVC.Services;
using ExaminationSystem.MVC.ViewModels.CourseViewModels;
using Microsoft.AspNetCore.Mvc;

namespace ExaminationSystem.MVC.Controllers
{
  public class CoursesController : Controller
  {
	private readonly ICourseService _courseService;

	public CoursesController(ICourseService courseService)
	{
	  _courseService = courseService;
	}

	public IActionResult Index(int? branchId,int?deptId)
	{
	  ViewBag.Departments = _courseService.GetAllDepartments();
	  ViewBag.DeptIdFilter = deptId;
	  ViewBag.BranchIdFilter = branchId;
	  return View();
	}

	[HttpPost]
	public async Task< IActionResult> GetAllCourses()
	{
	  try
	  {
		var draw = int.Parse(Request.Form["draw"].FirstOrDefault() ?? "1");
		var start = int.Parse(Request.Form["start"].FirstOrDefault() ?? "0");
		var length = int.Parse(Request.Form["length"].FirstOrDefault() ?? "10");

		var searchValue = Request.Form["search[value]"].FirstOrDefault();

		var filterDeleted = bool.TryParse(Request.Form["isDeleted"].FirstOrDefault(), out var isDel) ? isDel : (bool?)null;
		var deptId = int.TryParse(Request.Form["deptId"].FirstOrDefault(), out var dId) ? dId : (int?)null;

		var orderColumnIndex = int.Parse(Request.Form["order[0][column]"].FirstOrDefault() ?? "0");
		var orderDir = Request.Form["order[0][dir]"].FirstOrDefault() ?? "asc";

		string[] columnNames = { "Name", "Duration", "NumberOfTopics", "IsDeleted" };
		string orderBy = columnNames.ElementAtOrDefault(orderColumnIndex) ?? "Name";

		int pageNumber = (start / length) + 1;
		int pageSize = length;

		var courseResult =await  _courseService.FindAll(
			pageNumber: pageNumber,
			pageSize: pageSize,
			departmentIdFilter: deptId,
			isDeletedFilter: filterDeleted,
			searchTerm: searchValue,
			columnOrderBy: orderBy,
			orderByDirection: orderDir == "asc" ? OrderBy.Ascending : OrderBy.Descending
		);

		return Json(new
		{
		  draw = draw,
		  recordsTotal = courseResult.TotalItemsInTable,
		  recordsFiltered = courseResult.TotalFilteredItems,
		  data = courseResult.Items
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
	public IActionResult RestoreCourse(int id)
	{
	  var success = _courseService.RestoreCourse(id);
	  return Json(new { success });
	}




	[HttpPost]
	public IActionResult Delete(int id)
	{
	  try
	  {
		var course = _courseService.Delete(id);

		return Json(new { success = true, isActive = course.IsDeleted});
	  }
	  catch (Exception ex)
	  {
		
		return Json(new { success = false, message = "An error occurred while toggling status." });
	  }
	}

	[HttpGet]
	public IActionResult GetAvailableTopics()
	{
	  var topics = _courseService.GetAvailableTopics(); 
	  return Json(topics);
	}

	[HttpGet]
	public IActionResult GetCourseForEdit(int id)
	{
	  var course = _courseService.GetCourseForEdit(id);
	  if (course == null)
		return Json(new { success = false, message = "Course not found." });

	  return Json(course);
	}

	[HttpPost]
	public IActionResult AddCourse(CourseAddEditViewModel model)
	{
	  if (!ModelState.IsValid)
		return Json(new { success = false, message = "Invalid data." });

	  var result = _courseService.AddCourse(model);
	  return Json(result);
	}


	[HttpPost]
	public IActionResult EditCourse(CourseAddEditViewModel model)
	{
	  if (!ModelState.IsValid || !model.Id.HasValue)
		return Json(new { success = false, message = "Invalid data." });

	  var success = _courseService.EditCourse(model);
	  return Json(success);
	}

	[HttpGet]
	public IActionResult Details(int id)
	{
	  return RedirectToAction("Index", "Topics", new { courseId = id });
	}
	[HttpGet]
	public async Task<IActionResult> IsCourseNameUnique(string name, int? id)
	{
	  var result = await _courseService.CheckCourseNameUniquenessAsync(name, id);
	  return Json(result.IsValid ? true : result.Message);
	}








  }
}
