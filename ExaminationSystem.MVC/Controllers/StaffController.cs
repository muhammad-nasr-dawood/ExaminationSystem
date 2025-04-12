using ExaminationSystem.Core.Consts;
using ExaminationSystem.MVC.Services;
using Microsoft.AspNetCore.Authorization;
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

  public IActionResult GetAllStaff(int PageNumber = 1, int PageSize = 3, int? branchId = null, int? DeptId = null, string orderBy = null, string SearchTerm = null, string orderByDir = OrderBy.Ascending)
  {
	var staff =  _staffService.FindAll(
	  pageNumber: PageNumber,
	  pageSize: PageSize,
	  branchIdFilter: branchId,
	  departmentIdFilter: DeptId,
	  columnOrderBy: orderBy,
	  searchTerm: SearchTerm,
	  orderByDirection: orderByDir
	  );
	return View(staff.Items);
  }
}
