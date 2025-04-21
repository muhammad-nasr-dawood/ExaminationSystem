using ExaminationSystem.Core;
using ExaminationSystem.Core.Consts;
using ExaminationSystem.Core.Helpers;
using ExaminationSystem.Core.Models;
using ExaminationSystem.MVC.Services;
using ExaminationSystem.MVC.ViewModels.StaffViewModels;
using ExaminationSystem.MVC.ViewModels.StudentViewModels;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Data.Entity.Validation;

namespace ExaminationSystem.MVC.Controllers;

//[Authorize]
public class StudentsController : Controller
{
  IStudentService _studentService;
  public StudentsController(IStudentService studentService)
  {
	_studentService = studentService; // controller layer will only deal with the service layer any dirty work will be within the service layer // in order to keep our controller simple and clean
  }
  // public async Task<IActionResult> Index()
  // {
  //PaginatedResult<StudentVM> res = await _studentService.GetAllAsync(1,10,std => std.SsnNavigation);

  //   return View(res);
  // }

  // public IActionResult GetAllStudents()
  // {
  //   var stds = _studentService.GetAll();
  //   return View(stds);
  // }
  // // Get Student Details
  // public async Task<IActionResult> StudentDetails (long ssn)
  // {
  //if (ssn == 0)
  //  return NotFound();

  //StudentDetailsVM std = await _studentService.GetStdByIdAsync(ssn);

  //if (std == null)
  //  return NotFound();

  //return View(std);
  // }

  public IActionResult Index()
  {
	// Get branches and departments for filters
	ViewBag.Departments = _studentService.UnitOfWork.DepartmentRepo.GetAll();
	ViewBag.Branches = _studentService.UnitOfWork.BranchesRepo.GetAll();
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

		return Json(new { success = true, message = "Staff added successfully!" });
	  }
	  catch (Exception ex)
	  {
		return Json(new { success = false, message = ex.Message });
	  }
	}

	return Json(new { success = false, message = "Invalid data." });
  }




}
