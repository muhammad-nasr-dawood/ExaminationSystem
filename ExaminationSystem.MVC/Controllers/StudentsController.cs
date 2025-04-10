using ExaminationSystem.Core;
using ExaminationSystem.Core.Models;
using ExaminationSystem.MVC.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace ExaminationSystem.MVC.Controllers;

[Authorize]
public class StudentsController : Controller
{
  IStudentService _studentService;
  public StudentsController(IStudentService studentService)
  {
	_studentService = studentService; // controller layer will only deal with the service layer any dirty work will be within the service layer // in order to keep our controller simple and clean
  }
  public IActionResult Index()
  {
    return View();
  }

  public IActionResult GetAllStudents()
  {
    var stds = _studentService.GetAll();
    return View(stds);
  }
}
