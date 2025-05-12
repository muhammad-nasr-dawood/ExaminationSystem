using ExaminationSystem.Core;
using ExaminationSystem.Core.Consts;
using ExaminationSystem.Core.Helpers;
using ExaminationSystem.Core.Models;
using ExaminationSystem.EF;
using ExaminationSystem.MVC.Services;
using ExaminationSystem.MVC.ViewModels.StudentViewModels;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.VisualStudio.Web.CodeGenerators.Mvc.Templates.BlazorIdentity.Pages;
using System.Data.Entity.Validation;
using System.Data.SqlTypes;
using System.Threading.Tasks;

namespace ExaminationSystem.MVC.Controllers;

//[Authorize]
public class StudentsController : Controller
{
  IStudentService _studentService;
  private readonly IDepartmentService departmentService;
  private readonly IBranchService branchService;

  public StudentsController(
	IStudentService studentService,
	IDepartmentService departmentService,
	IBranchService branchService
	)
  {
	_studentService = studentService; // controller layer will only deal with the service layer any dirty work will be within the service layer // in order to keep our controller simple and clean
	this.departmentService = departmentService;
	this.branchService = branchService;
  }

  public IActionResult Index()
  {
	// Get branches and departments for filters
	ViewBag.Departments = departmentService.GetAll();
	ViewBag.Branches = branchService.GetAll();
	ViewBag.Locations = _studentService.UnitOfWork.LocationRepo.GetAll();

	return View();
  }
  [HttpPost]
  public IActionResult GetAllStudent() // will only be used throw ajax call
  {
	try
	{
	  var draw = int.Parse(Request.Form["draw"].FirstOrDefault() ?? "1");
	  var start = int.Parse(Request.Form["start"].FirstOrDefault() ?? "0");
	  var length = int.Parse(Request.Form["length"].FirstOrDefault() ?? "10");

	  var searchValue = Request.Form["search[value]"].FirstOrDefault();

	  var filterStatus = bool.TryParse(Request.Form["statusFilter"].FirstOrDefault(), out var fStatus) ? fStatus : (bool?)null;

	  var branchId = int.TryParse(Request.Form["branchId"].FirstOrDefault(), out var bId) ? bId : (int?)null;
	  var deptId = int.TryParse(Request.Form["DeptId"].FirstOrDefault(), out var dId) ? dId : (int?)null;

	  var orderColumnIndex = int.Parse(Request.Form["order[0][column]"].FirstOrDefault() ?? "0");
	  var orderDir = Request.Form["order[0][dir]"].FirstOrDefault() ?? "asc";

	  string[] columnNames = { "FullName", "Ssn", "Salary", "", "" };
	  string orderBy = columnNames[orderColumnIndex];

	  int pageNumber = (start / length) + 1;
	  int pageSize = length;

	  var studentResult = _studentService.FindAll(
		  pageNumber: pageNumber,
		  pageSize: pageSize,
		  branchIdFilter: branchId,
		  departmentIdFilter: deptId,
		  StatusFilter: filterStatus,
		  columnOrderBy: orderBy,
		  searchTerm: searchValue,
		  orderByDirection: orderDir == "asc" ? OrderBy.Ascending : OrderBy.Descending
	  );

	  int recordsTotal = studentResult.TotalItemsInTable;
	  int recordsFiltered = studentResult.TotalFilteredItems;

	  return Json(new
	  {
		draw = draw,
		recordsTotal = recordsTotal,
		recordsFiltered = recordsFiltered,
		data = studentResult.Items
	  });
	}
	catch (Exception ex)
	{
	  return Json(new
	  {
		draw = 1,
		recordsTotal = 0,
		recordsFiltered = 0,
		data = new List<StudentVM>()
	  });
	}
  }


  [HttpPost]
  public JsonResult Add(StudentAddVM model)
  {
	if (ModelState.IsValid)
	{
	  try
	  {
		var IsSucceeded = _studentService.Add(model);
		if (!IsSucceeded)
		  return Json(new { success = false, message = "Something went very wrong!" });

		return Json(new { success = true, message = "Student added successfully!" });
	  }
	  catch (Exception ex)
	  {
		return Json(new { success = false, message = ex.Message });
	  }
	}

	return Json(new { success = false, message = "Invalid data." });
  }

  public async Task<IActionResult> Details(long id)
  {
	ViewBag.StudentCourseSchedule = _studentService.GetStudentCourseSchedule(id);
	if (id == 0)
	  return NotFound();
	ViewBag.Locations = _studentService.UnitOfWork.LocationRepo.GetAll();

	StudentDetailsVM std = await _studentService.GetStdByIdAsync(id);

	if (std == null)
	  return NotFound();

	return View(std);
  }

  [HttpPost]
  public IActionResult ResetPassword(long ssn)
  {

	var user = _studentService.ResetPassword(ssn);

	return Json(new
	{
	  success = true,
	  message = "Password reset successfully.",
	});
  }

  [HttpPost]
  public IActionResult ToggleUserStatus(long ssn)
  {
	try
	{
	  var user = _studentService.ToggleUserStatus(ssn);

	  return Json(new { success = true, isActive = user.IsActive });
	}
	catch (Exception ex)
	{
	  // log the error if needed
	  return Json(new { success = false, message = "An error occurred while toggling status." });
	}
  }

  public IActionResult GetUserSidebar(long userId)
  {
	var studentDetails = _studentService.GetById(userId); 
	return PartialView("_StudentSidebar", studentDetails);
  }

  [HttpPost]
  public IActionResult Update(StudentDetailsVM model)
  {
	if (!ModelState.IsValid)
	{
	  return BadRequest(ModelState);
	}
	_studentService.UpdateStudent(model);
	// Save changes to DB...

	return Ok(new { message = "Student updated" });
  }


  public async Task<IActionResult> IsEmailExist(string email, long? Ssn)
  {
	var model = await _studentService.GetByEmailAsync(email, Ssn);
	if (model == null)
	  return Json(true);
	return Json("This email is already in use");
  }

  public async Task<IActionResult> IsSSNExist(long ssn)
  {
	var model = await _studentService.GetStdByIdAsync(ssn);
	if (model == null)
	{
	  return Json(true); 
	}
	else
	{
	  return Json("This SSN is already in use"); 
	}
  }
  public async Task<IActionResult> isPhoneNumberExist(string PhoneNumber, long? Ssn)
	{
	var model = await _studentService.GetByPhoneNumberAsync(PhoneNumber, Ssn);
	if (model == null)
	{
	  return Json(true);
	}
	else
	{
	  return Json("This Phone Number is already in use");
	}
  }

  [HttpGet]
  public async Task<IActionResult> GetDepartmentsByBranch(int branchId)
  {
	var departments = await departmentService.GetDepartmentsByBranchIdAsync(branchId);

	return Json(departments);
  }


  [HttpGet]
  public IActionResult GetExams(string filterType, long studentId)
  {
	try
	{
	  List<StudentExamVM> exams;

	  if (filterType == "1") // Pending Exams
	  {
		exams = _studentService.GetStudentExams(studentId, true);
	  }
	  else if (filterType == "2") // Old Exams
	  {
		exams = _studentService.GetStudentExams(studentId,false);
	  }
	  else
	  {
		return BadRequest("Invalid filter type");
	  }

	  return Ok(exams);
	}
	catch (Exception ex)
	{
	  return StatusCode(500, "Error retrieving exam data");
	}
  }


}
