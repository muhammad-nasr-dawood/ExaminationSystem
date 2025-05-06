using ExaminationSystem.Core.Models;
using ExaminationSystem.MVC.Services;
using ExaminationSystem.MVC.ViewModels.BranchViewModels;
using ExaminationSystem.MVC.ViewModels.CourseViewModels;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;

public class BranchesController : Controller
{
  private readonly IBranchService _branchService;
  private readonly IStaffService _staffService;
  private readonly ICourseService _courseService;
  private readonly IDepartmentService _departmentService;
  private readonly IMapper _mapper;
  public BranchesController(IBranchService branchService, IStaffService staffService, IMapper mapper, IDepartmentService departmentService, ICourseService courseService)
  {
	_branchService = branchService;
	_staffService = staffService;
	_courseService = courseService;
	_departmentService = departmentService;
	_mapper = mapper;
  }


  public async Task<IActionResult> Edit(int id)
  {
	var branch = _branchService.GetBranchForEdit(id);
	if (branch == null)
	{
	  return NotFound();
	}


	var Locations = await _branchService.GetLocations(id);
	ViewBag.Locations = Locations;

	return PartialView("_EditBranchModal", branch);
  }


  [HttpPost]
  [ValidateAntiForgeryToken]
  public IActionResult Edit(BranchEditViewModel viewModel)
  {
	if (ModelState.IsValid)
	{

	  _branchService.Update(viewModel);


	  var updatedBranch = _branchService.GetBranchForEdit(viewModel.Id);


	  return Json(new { success = true, id = updatedBranch.Id, branch = updatedBranch });
	}


	var errors = ModelState.Values.SelectMany(v => v.Errors.Select(e => e.ErrorMessage)).ToList();
	return Json(new { success = false, message = string.Join(", ", errors) });
  }



  [HttpGet]
  public IActionResult Delete(int id)
  {
	var branch = _branchService.GetBranchForEdit(id);


	if (branch == null)
	{
	  return Json(new { success = false, message = "Branch not found." });
	}

	return PartialView("DeleteBranchModel", branch);
  }


  [HttpPost]
  public IActionResult DeleteConfirmed(int id)
  {
	try
	{

	  var branch = _branchService.GetBranchForEdit(id);

	  if (branch == null)
	  {
		return Json(new { success = false, message = "Branch not found." });
	  }

	  _branchService.Delete(id);

	  return Json(new { success = true, message = "Branch deleted successfully" });
	}
	catch (KeyNotFoundException ex)
	{

	  return Json(new { success = false, message = ex.Message });
	}
	catch (Exception ex)
	{

	  return Json(new { success = false, message = "An error occurred while deleting the branch." });
	}
  }


  [HttpGet]
  public async Task<IActionResult> AssignManager(int id)
  {

	var unassignedStaff = await _branchService.GetUnassignedStaffAsync(id);

	ViewBag.BranchId = id;
	return PartialView("AssignBranchManagerModal", unassignedStaff);
  }



  [HttpPost]
  public async Task<IActionResult> AssignManager(int id, long staffSsn)
  {
	
	var managerName = await _branchService.AddBranchManager(id, staffSsn);

	if (!string.IsNullOrEmpty(managerName))
	{
	  return Json(new
	  {
		success = true,
		Id = id,
		message = "Manager assigned successfully.",
		managerName = managerName
	  });
	}

	return Json(new { success = false, message = "Error: Could not assign manager." });
  }


  [HttpGet]
  public async Task<IActionResult> DeleteManager(int id)
  {
	var branch = await _branchService.GetBranchThatOwnStaffByID(id);

	if (branch == null)
	{
	  return Json(new { success = false, message = "Branch not found." });
	}


	return PartialView("DeleteManagerModal", branch);
  }

  [HttpPost]
  public async Task<IActionResult> DeleteManagerConfirmed(int id)
  {

	var branch = await _branchService.GetBranchThatOwnStaffByID(id);
	if (branch == null)
	{
	  return Json(new { success = false, message = "Branch not found." });
	}

	bool ok = await _branchService.DeleteManagerByBranchId(id);
	if (ok)
	{
	  return Json(new { success = true, message = "Staff deleted successfully" });
	}
	else
	{
	  return Json(new { success = false, message = "An error occurred while deleting the staff." });
	}

  }

  public async Task<IActionResult> Add()
  {
	var viewModel = new BranchEditViewModel();
	ViewBag.Locations = await _branchService.GetLocations();
	return PartialView("_EditBranchModal", viewModel);
  }

  [HttpPost]
  public IActionResult Add(BranchEditViewModel viewModel)
  {
	if (ModelState.IsValid)
	{
	  var branch = _branchService.Add(viewModel);

	  
	  return PartialView("_BranchCardPartial", branch); 
	}

	var errors = ModelState.Values.SelectMany(v => v.Errors.Select(e => e.ErrorMessage)).ToList();
	return BadRequest(string.Join(", ", errors));
  }



 
  public IActionResult ShowDepartments(int branchId)
  {
	var departments = _branchService.GetDepartmentsWithCapacitiesByBranch(branchId);
	ViewBag.BranchId = branchId;
	return View("~/Views/Departments/Index.cshtml", departments);
  }


  public IActionResult ShowBranchStaff(int branchId)
  {
	ViewBag.IsBranchView = true;  
	ViewBag.BranchId = branchId;  
	ViewBag.Branches = _mapper.Map<List<BranchViewModel>>(_staffService.UnitOfWork.BranchesRepo.GetAll());
	ViewBag.Departments = _staffService.UnitOfWork.DepartmentRepo.GetAll();
	ViewBag.Locations = _staffService.UnitOfWork.LocationRepo.GetAll();

	return View("~/Views/Staff/Index.cshtml"); 
  }
  public IActionResult ShowStudents(int branchId, int deptId)
  {
	ViewBag.BranchId = branchId;
	ViewBag.DeptId = deptId;
	ViewBag.Branches = _mapper.Map<List<BranchViewModel>>(_staffService.UnitOfWork.BranchesRepo.GetAll());
	ViewBag.Departments = _staffService.UnitOfWork.DepartmentRepo.GetAll();
	ViewBag.Locations = _staffService.UnitOfWork.LocationRepo.GetAll();

	return View("~/Views/Students/Index.cshtml"); 
  }


  public IActionResult ShowCourses(int deptId,int branchId)
  {
	var department = _departmentService.GetDepartmentForEdit(deptId);
	if (department == null)
	  return NotFound();


	return RedirectToAction("Index", "Courses", new { BranchId= branchId,DeptId=deptId });
  }

  [HttpGet]
  public async Task<IActionResult> Index(string? search, int page = 1, int pageSize = 9, bool isPartial = false)
  {
	var paginated = await _branchService.GetPagedBranchesAsync(search, page, pageSize);

	if (isPartial)
	{
	  return PartialView("_BranchCardListPartial", paginated);
	}

	return View(paginated);
  }







}

