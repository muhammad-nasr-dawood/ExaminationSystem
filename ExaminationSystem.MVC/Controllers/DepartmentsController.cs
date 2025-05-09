using ExaminationSystem.MVC.Services;
using ExaminationSystem.MVC.ViewModels.DepartmentViewModels;
using Microsoft.AspNetCore.Mvc;

namespace ExaminationSystem.MVC.Controllers
{
  public class DepartmentsController : Controller
  {
	private readonly IDepartmentService _departmentService;
	public DepartmentsController(IDepartmentService departmentService)
	{
	  _departmentService = departmentService;
	}
	[HttpGet]
	public IActionResult Add()
	{
	  var model = new AddEditDeptViewModel();
	  return PartialView("_AddEditModal", model);
	}

	[HttpPost]
	public IActionResult Add(AddEditDeptViewModel model)
	{
	  if (!ModelState.IsValid)
	  {
		var errors = ModelState.Values
			.SelectMany(v => v.Errors.Select(e => e.ErrorMessage))
			.ToList();

		return BadRequest(string.Join(", ", errors));
	  }

	  var dept = _departmentService.Add(model);

	  
	  return PartialView("_DeptCardPartial", dept);
	}


	[HttpGet]
	public IActionResult Edit(int id)
	{
	  var model = _departmentService.GetDepartmentForEdit(id);
	  if (model == null)
		return NotFound();

	  return PartialView("_AddEditModal", model);
	}

	[HttpPost]
	public IActionResult Edit(AddEditDeptViewModel model)
	{
	  if (ModelState.IsValid)
	  {
		_departmentService.Update(model);
		var updatedDept = _departmentService.GetDepartmentForEdit(model.Id);
		return Json(new { success = true, id = updatedDept.Id, department = updatedDept });

	  }

	  return Json(new { success = false, message = "Invalid data." });
	}
	[HttpGet]
	public IActionResult Delete(int id)
	{
	  var model = _departmentService.GetDepartmentForEdit(id);
	  if (model == null)
		return NotFound();

	  return PartialView("DeleteDepartmentModal", model);
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


  }
}
