using ExaminationSystem.Core.Consts;
using ExaminationSystem.MVC.Services;
using ExaminationSystem.MVC.ViewModels;
using ExaminationSystem.MVC.ViewModels.BranchViewModels;
using ExaminationSystem.MVC.ViewModels.StaffViewModels;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Linq;

namespace ExaminationSystem.MVC.Controllers;

[Authorize]
public class StaffController : Controller
{
  private readonly IStaffService _staffService;
  private readonly IAccountService _accountService;
  private readonly IMapper _mapper;
  public StaffController(
	  IStaffService staffService, IMapper mapper, IAccountService accountService)
  {
	_staffService = staffService;
	_mapper = mapper;
	_accountService = accountService;
  }
  [HttpGet]
  public async Task<IActionResult> IsEmailExist(string Email, long Ssn)
  {
	var isSuccess = await _accountService.VerifyEmail(Ssn, Email);
	return Json(isSuccess);
  }
  [HttpGet]
  public async Task<IActionResult> IsPhoneNumberExist(string PhoneNumber, long Ssn)
  {
	 var isSuccess = await _accountService.VerifyPhone(Ssn, PhoneNumber);
	return Json(isSuccess);
  }

  [HttpGet]
  public async Task<IActionResult> IsSSNExist(long Ssn)
  {
	var isSuccess = await _accountService.VerifySSN(Ssn);
	return Json(isSuccess);
  }

  public IActionResult Index()
  {
	ViewBag.Branches = _mapper.Map<List<BranchDisplayViewModel>>(_staffService.UnitOfWork.BranchesRepo.GetAll()) ;
	ViewBag.Departments = _staffService.UnitOfWork.DepartmentRepo.GetAll();

	ViewBag.Locations = _staffService.UnitOfWork.LocationRepo.GetAll();

	return View();
  }

  [HttpPost]
  public IActionResult GetAllStaff() // will only be used throw ajax call
  {
	try
	{
	  var draw = int.Parse(Request.Form["draw"].FirstOrDefault() ?? "1");
	  var start = int.Parse(Request.Form["start"].FirstOrDefault() ?? "0");
	  var length = int.Parse(Request.Form["length"].FirstOrDefault() ?? "10");

	  var searchValue = Request.Form["search[value]"].FirstOrDefault();

	  var filterStatus = bool.TryParse( Request.Form["statusFilter"].FirstOrDefault(), out var fStatus) ? fStatus : (bool?) null;

	  var branchId = int.TryParse(Request.Form["branchId"].FirstOrDefault(), out var bId) ? bId : (int?)null;
	  var deptId = int.TryParse(Request.Form["DeptId"].FirstOrDefault(), out var dId) ? dId : (int?)null;

	  var orderColumnIndex = int.Parse(Request.Form["order[0][column]"].FirstOrDefault() ?? "0");
	  var orderDir = Request.Form["order[0][dir]"].FirstOrDefault() ?? "asc";

	  string[] columnNames = { "FullName", "Ssn", "Salary", "", "" }; 
	  string orderBy = columnNames[orderColumnIndex];

	  int pageNumber = (start / length) + 1;
	  int pageSize = length;

	  var staffResult = _staffService.FindAll(
		  pageNumber: pageNumber,
		  pageSize: pageSize,
		  branchIdFilter: branchId,
		  departmentIdFilter: deptId,
		  StatusFilter: filterStatus,
		  columnOrderBy: orderBy,
		  searchTerm: searchValue,
		  orderByDirection: orderDir == "asc" ? OrderBy.Ascending : OrderBy.Descending
	  );

	  int recordsTotal = staffResult.TotalItemsInTable;
	  int recordsFiltered = staffResult.TotalFilteredItems;

	  return Json(new
	  {
		draw = draw,
		recordsTotal = recordsTotal,
		recordsFiltered = recordsFiltered,
		data = staffResult.Items
	  });
	}
	catch (Exception ex)
	{
	  return Json(new
	  {
		draw = 1,
		recordsTotal = 0,
		recordsFiltered = 0,
		data = new List<StaffGeneralDisplayVM>()
	  });
	}
  }

  [HttpPost]
  public JsonResult Add(StaffAddViewModel model)
  {
	if (ModelState.IsValid)
	{
	  try
	  {
		var IsSucceeded = _staffService.Add(model);
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


  public IActionResult Details(long id)
  {
	ViewBag.Branches = _mapper.Map<List<BranchDisplayViewModel>>(_staffService.UnitOfWork.BranchesRepo.GetAll());
	ViewBag.Departments = _staffService.UnitOfWork.DepartmentRepo.GetAll();

	ViewBag.Locations = _staffService.UnitOfWork.LocationRepo.GetAll();
	ViewBag.Courses = _staffService.UnitOfWork.CoursesRepo.GetAll();
	var staffDetail = _staffService.GetById(id);
	return View(staffDetail);
  }

  public IActionResult GetUserSidebar(long userId)
  {
	var staffDetail = _staffService.GetById(userId); // fetch updated data
	return PartialView("_StaffSidebar", staffDetail);
  }


  [HttpPost]
  public IActionResult Update(StaffDisplayDetailViewModel model)
  {
	if (!ModelState.IsValid)
	{
	  return BadRequest(ModelState);
	}
	_staffService.UpdateById(model);
	// Save changes to DB...

	return Ok(new { message = "User updated" });
  }

  [HttpPost]
  public IActionResult GetAllRegisteredCourses()
  {
	try
	{
	  var draw = int.Parse(Request.Form["draw"].FirstOrDefault() ?? "1");
	  var start = int.Parse(Request.Form["start"].FirstOrDefault() ?? "0");
	  var length = int.Parse(Request.Form["length"].FirstOrDefault() ?? "10");

	  var searchValue = Request.Form["search[value]"].FirstOrDefault();
	  var filterStatus = bool.TryParse(Request.Form["status"].FirstOrDefault(), out var fStatus) ? fStatus : (bool?)null;
	  var branchId = int.TryParse(Request.Form["branchId"].FirstOrDefault(), out var bId) ? bId : (int?)null;
	  var deptId = int.TryParse(Request.Form["DeptId"].FirstOrDefault(), out var dId) ? dId : (int?)null;
	  var crsId = int.TryParse(Request.Form["courseId"].FirstOrDefault(), out var cId) ? cId : (int?)null;

	  var staffSsn = long.Parse(Request.Form["staffSsn"].FirstOrDefault()); // required

	  var orderColumnIndex = int.Parse(Request.Form["order[0][column]"].FirstOrDefault() ?? "0");
	  var orderDir = Request.Form["order[0][dir]"].FirstOrDefault() ?? "asc";

	  // Match column index to actual property names in your model or query
	  string[] columnNames = { "Course", "IsInstructorCurrentBranch", "StartingDate", "EndingDate" };
	  string orderBy = columnNames.ElementAtOrDefault(orderColumnIndex) ?? "Course";


	  int pageNumber = (start / length) + 1;
	  int pageSize = length;

	  var courseResult = _staffService.FindAllRegisteredCourses(
		  pageNumber: pageNumber,
		  pageSize: pageSize,
		  branchIdFilter: branchId,
		  departmentIdFilter: deptId,
		  courseFilter: crsId,
		  StaffSnn: staffSsn,
		  status: filterStatus,
		  columnOrderBy: orderBy,
		  orderByDirection: orderDir == "asc" ? OrderBy.Ascending : OrderBy.Descending,
		  searchTerm: searchValue
	  );

	  return Json(new
	  {
		draw = draw,
		recordsTotal = courseResult.TotalItemsInTable,
		recordsFiltered = courseResult.TotalFilteredItems,
		data = courseResult.Items
	  });
	}
	catch (Exception ex)
	{
	  // You can optionally log the exception here for debugging

	  return Json(new
	  {
		draw = 1,
		recordsTotal = 0,
		recordsFiltered = 0,
		data = new List<TeachingDisplayViewModel>()
	  });
	}
  }

  [HttpPost]
  public IActionResult ToggleUserStatus(long ssn)
  {
	try
	{
	  var user = _staffService.ToggleUserStatus(ssn);

	  return Json(new { success = true, isActive = user.IsActive });
	}
	catch (Exception ex)
	{
	  // log the error if needed
	  return Json(new { success = false, message = "An error occurred while toggling status." });
	}
  }

  [HttpPost]
  public IActionResult ResetPassword(long ssn)
  {

	var user = _staffService.ResetPassword(ssn);

	// Send email or return password (you can change this)
	// For demo purposes, we'll return it directly
	return Json(new
	{
	  success = true,
	  message = "Password reset successfully.",
	});
  }



  //[HttpPost]
  // public IActionResult Delete(string id)
  // {
  //try
  //{
  //  var result = _staffService.DeleteStaff(id);
  //  return Json(new { success = result.IsSuccess, message = result.Message });
  //}
  //catch (Exception ex)
  //{
  //  return Json(new { success = false, message = "An error occurred while deleting the staff member." });
  //}
  // }

  // [HttpGet]
  // public IActionResult Edit(string id)
  // {
  //var staff = _staffService.GetStaffById(id);
  //if (staff == null)
  //{
  //  return NotFound();
  //}

  //ViewBag.Branches = _branchService.GetAllBranches();
  //ViewBag.Departments = _departmentService.GetAllDepartments();

  //return View(staff);
  // }

  // [HttpPost]
  // public IActionResult Edit(StaffUpdateVM model)
  // {
  //if (!ModelState.IsValid)
  //{
  //  ViewBag.Branches = _branchService.GetAllBranches();
  //  ViewBag.Departments = _departmentService.GetAllDepartments();
  //  return View(model);
  //}

  //var result = _staffService.UpdateStaff(model);
  //if (result.IsSuccess)
  //{
  //  return RedirectToAction(nameof(Index));
  //}

  //ModelState.AddModelError("", result.Message);
  //ViewBag.Branches = _branchService.GetAllBranches();
  //ViewBag.Departments = _departmentService.GetAllDepartments();
  //return View(model);
  // }
}
