using ExaminationSystem.MVC.Services;
using Microsoft.AspNetCore.Mvc;

namespace ExaminationSystem.MVC.Controllers;

public class StaffController : Controller
{
  IStaffService _staffService;
  public StaffController(IStaffService staffService)
  {
	  _staffService = staffService;
  }
  public IActionResult Index()
  {
      return View();
  }

  public IActionResult GetAllStaff()
  {
	var staff =  _staffService.FindAll(
	  pageNumber: 1,
	  pageSize: 2,
	  branchIdFilter: null,
	  departmentIdFilter: null,
	  columnOrderBy: "First Name"

	  );
	return View(staff.Items);
  }
}
