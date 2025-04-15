using ExaminationSystem.Core.Models;
using ExaminationSystem.MVC.Services;
using ExaminationSystem.MVC.ViewModels.BranchViewModels;
using Microsoft.AspNetCore.Mvc;

public class BranchesController : Controller
{
  private readonly IBranchService _branchService;
  public BranchesController(IBranchService branchService)
  {
	_branchService = branchService;
  }

  public IActionResult Index()
  {
	var branches = _branchService.GetAll();

	return View(branches);
  }


  public IActionResult Edit(int id)
  {
	var branch = _branchService.GetById(id);
	if (branch == null)
	{
	  return NotFound();
	}


	var locations = _branchService.GetLocations();
	branch.Locations = locations;


	return PartialView("_EditBranchModal", branch);
  }


  [HttpPost]
  [ValidateAntiForgeryToken]
  public IActionResult Edit(BranchEditViewModel viewModel)
  {
	if (ModelState.IsValid)
	{

	  _branchService.Update(viewModel);

	
	  var updatedBranch = _branchService.GetById(viewModel.Id);

	
	  //var locations = _branchService.GetLocations();

	 
	  return Json(new { success = true, id = updatedBranch.Id, branch = updatedBranch });
	}


	var errors = ModelState.Values.SelectMany(v => v.Errors.Select(e => e.ErrorMessage)).ToList();
	return Json(new { success = false, message = string.Join(", ", errors) });
  }
}

