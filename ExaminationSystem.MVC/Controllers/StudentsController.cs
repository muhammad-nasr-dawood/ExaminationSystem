using ExaminationSystem.Core;
using ExaminationSystem.Core.Helpers;
using ExaminationSystem.Core.Models;
using ExaminationSystem.MVC.Services;
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
  public async Task<IActionResult> Index()
  {
	PaginatedResult<StudentVM> res = await _studentService.GetAllAsync(1,10,std => std.SsnNavigation);

    return View(res);
  }

  public IActionResult GetAllStudents()
  {
    var stds = _studentService.GetAll();
    return View(stds);
  }
  // Get Student Details
  public async Task<IActionResult> StudentDetails (long ssn)
  {
	if (ssn == 0)
	  return NotFound();

	StudentDetailsVM std = await _studentService.GetStdByIdAsync(ssn);

	if (std == null)
	  return NotFound();

	return View(std);
  }

}
