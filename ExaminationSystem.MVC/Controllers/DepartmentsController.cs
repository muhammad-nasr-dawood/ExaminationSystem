using ExaminationSystem.MVC.Services;
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
  }
}
