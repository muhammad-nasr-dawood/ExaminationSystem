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
	public IActionResult Index()
	{
	  var depts = _departmentService.GetAll();
	  return View(depts);
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
	  if (ModelState.IsValid)
	  {
		var dept = _departmentService.Add(model);
		return Json(new { success = true, id = 0, department = dept });
	  }

	  return Json(new { success = false, message = "Invalid data." });
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
  }
}
