using ExaminationSystem.MVC.Services;
using ExaminationSystem.MVC.ViewModels.DepartmentViewModels;
using Microsoft.AspNetCore.Mvc;

namespace ExaminationSystem.MVC.Controllers
{
  public class DepartmentsController : Controller
  {
	private readonly IDepartmentService _departmentService;
	private readonly IBranchService _branchService;
	public DepartmentsController(IDepartmentService departmentService, IBranchService branchService)
	{
	  _departmentService = departmentService;
	  _branchService = branchService;
	}
	[HttpGet]
	public IActionResult Add()
	{
	  var branches = _branchService.GetAll();

	  var viewModel = new AddEditDeptViewModel
	  {
		AvailableBranches = branches
	  };

	  return PartialView("_AddEditModal", viewModel);
	}



	[HttpPost]
	[ValidateAntiForgeryToken]
	public IActionResult Add(AddEditDeptViewModel model)
	{
	  if (ModelState.IsValid)
	  {
		try
		{
		  var dept = _departmentService.Add(model);
		  return PartialView("_DeptCardPartial", dept);
		}
		catch (InvalidOperationException ex)
		{
		  return StatusCode(500, ex.Message);
		}
	  }
	  return BadRequest("Please check your inputs and try again.");
	}




	[HttpGet]
	public async Task<IActionResult> Edit(int id)
	{
	  var model = await _departmentService.GetDepartmentForEditAsync(id);
	  if (model == null)
		return NotFound();

	  return PartialView("_AddEditModal", model);
	}

	[HttpPost]
	[ValidateAntiForgeryToken]
	public async Task<IActionResult> Edit(AddEditDeptViewModel model)
	{
	  if (ModelState.IsValid)
	  {
	
		  _departmentService.Update(model);
		  var updatedDept = await _departmentService.GetDepartmentForEditAsync(model.Id);
		  return Json(new { success = true, id = updatedDept.Id, department = updatedDept });
		
	  }

	  return Json(new { success = false, message = "Validation failed. Please check your inputs." });
	}



	[HttpPost]
	public IActionResult DeleteConfirmed(int id)
	{
	  var success = _departmentService.Delete(id);

	  if (success)
		return Json(new { success = true });

	  return Json(new { success = false, message = "Error occurred while deleting." });
	}

	[HttpGet]
	public async Task<IActionResult> Index(string? search, int page = 1, int pageSize = 12, bool isPartial = false, int? branchId = null)
	{
	  ViewBag.BranchId = branchId;

	  var paginated = await _departmentService.GetPagedDepartmentsAsync(search, page, pageSize, branchId);

	  if (isPartial)
	  {
		return PartialView("_DeptCardListPartial", paginated);
	  }

	  return View(paginated);
	}
	[HttpGet]
	public async Task<IActionResult> IsDeptNameUnique(string name, int id)
	{
	  var isUnique = await _departmentService.IsNameUniqueAsync(name, id);
	  return Json(isUnique);
	}

	[HttpGet]
	public async Task<IActionResult> IsDeptDiscUnique(string disc, int id)
	{
	  var isUnique = await _departmentService.IsDiscUniqueAsync(disc, id);
	  return Json(isUnique);
	}



  }
}
