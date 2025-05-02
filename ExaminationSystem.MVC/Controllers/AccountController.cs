using AspNetCoreGeneratedDocument;
using ExaminationSystem.Core.Consts;
using ExaminationSystem.Core.Models;
using ExaminationSystem.MVC.Services;
using ExaminationSystem.MVC.ViewModels.AuthViewModels;
using ExaminationSystem.MVC.ViewModels.BranchViewModels;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;

namespace ExaminationSystem.MVC.Controllers;

//[Authorize]
public class AccountController : Controller
{
    private readonly IAccountService _accountService;
	private readonly IStaffService _staffService;
	private readonly IMapper _mapper;
    public AccountController(IAccountService accountService, IStaffService staffService, IMapper mapper)
    {
        _accountService = accountService;
		_staffService = staffService;
		_mapper = mapper;
    }

	public IActionResult Index()
	{
	  ViewBag.Branches = _mapper.Map<List<BranchDisplayViewModel>>(_staffService.UnitOfWork.BranchesRepo.GetAll());
	  ViewBag.Departments = _staffService.UnitOfWork.DepartmentRepo.GetAll();

	  ViewBag.Locations = _staffService.UnitOfWork.LocationRepo.GetAll();
	  ViewBag.Courses = _staffService.UnitOfWork.CoursesRepo.GetAll();
	  var userId = User.FindFirst(ClaimTypes.NameIdentifier)?.Value;
	  var account = _accountService.GetAccount(long.Parse(userId));
	  return View(account);
	}

    public IActionResult AccountSettings()
    {
        string? userId = User.FindFirst(ClaimTypes.NameIdentifier)?.Value;

        if (string.IsNullOrEmpty(userId))
        {
            return BadRequest("User ID is missing.");
        }

        ViewBag.Locations = _accountService.UnitOfWork.LocationRepo.GetAll();
        var account = _accountService.GetAccount(long.Parse(userId));
        return View(account);
    }



  [HttpPost]
  public async Task<IActionResult> Update(AccountEditViewModel model)
  {
	if (!ModelState.IsValid)
	{
	  ViewBag.Locations = _accountService.UnitOfWork.LocationRepo.GetAll();
	  return View("Index", model);
	}

	string? userId = User.FindFirst(ClaimTypes.NameIdentifier)?.Value;
	var roles = User.FindAll(ClaimTypes.Role).Select(c => c.Value).ToList();


	bool success = await _accountService.UpdateAccount(long.Parse(userId), roles, model);


	return Json(new
	{
	  success = success,
	  redirectUrl = Url.Action("Index")
	});
  }


  [HttpPost]
  [ValidateAntiForgeryToken] 
  public async Task<IActionResult> UploadImage([FromForm] IFormFile file)
    {
        if (file == null || file.Length == 0)
            return BadRequest("No file uploaded.");

        string? userId = User.FindFirst(ClaimTypes.NameIdentifier)?.Value;

        if (string.IsNullOrEmpty(userId))
        {
            return BadRequest("User ID is missing.");
        }

        // Fix for CS1061: Ensure UpdateImage is awaited properly by marking it as Task<string> in IAccountService
        string imagePath = await _accountService.UpdateImage(file, long.Parse(userId));

        return Json(new { imageUrl = imagePath });
    }


  [HttpPost]
  [ValidateAntiForgeryToken]
  public async Task<IActionResult> RemoveImage()
  {
	string? userId = User.FindFirst(ClaimTypes.NameIdentifier)?.Value;
	var imagePath = await _accountService.DeleteImage(long.Parse(userId));

	return Json(new { imageUrl = imagePath });
  }

  public async Task<IActionResult> VerifyEmail(string Email)
  {
	string? userId = User.FindFirst(ClaimTypes.NameIdentifier)?.Value;
	if (userId == null)
	{
	  return Json("User not found."); // Optional: Custom error message
	}

	bool isSuccess = await _accountService.VerifyEmail(long.Parse(userId), Email);

	// Return Json(true) if valid, Json(false) if invalid
	return Json(isSuccess);
  }

  public async Task<IActionResult> VerifyPhone(string PhoneNumber)
  {
	string? userId = User.FindFirst(ClaimTypes.NameIdentifier)?.Value;
	if (userId == null)
	{
	  return Json("User not found."); // Optional: Custom error message
	}

	bool isSuccess = await _accountService.VerifyPhone(long.Parse(userId), PhoneNumber);

	// Return Json(true) if valid, Json(false) if invalid
	return Json(isSuccess);
  }

  [HttpGet] // remote validation requires HTTP GET
  public async Task<IActionResult> VerifyOldPassword(string OldPassword)
  {
	string? userId = User.FindFirst(ClaimTypes.NameIdentifier)?.Value;

	if (userId == null)
	{
	  return Json("User not found."); // Optional: Custom error message
	}

	bool isSuccess = await _accountService.VerifyOldPassword(long.Parse(userId), OldPassword);

	// Return Json(true) if valid, Json(false) if invalid
	return Json(isSuccess);
  }

  [HttpPost]
  public async Task<IActionResult> ChangePassword(ChangePasswordViewModel model)
  {
	if (!ModelState.IsValid)
	{
	  return Json(new { success = false, message = "Validation failed." });
	}

	var userId = User.FindFirst(ClaimTypes.NameIdentifier)?.Value;
	if (userId == null)
	{
	  return Json(new { success = false, message = "User not found." });
	}

	var succeeded = await _accountService.ChangePassword(long.Parse(userId), model.OldPassword, model.NewPassword);

	if (succeeded)
	{
	  return Json(new { success = true });
	}

	// Handle errors
	return Json(new
	{
	  success = false,
	  message = string.Join(" ", "Something went very wrong!")
	});
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

	  var staffSsn = long.Parse( User.FindFirst(ClaimTypes.NameIdentifier)?.Value);

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


}
