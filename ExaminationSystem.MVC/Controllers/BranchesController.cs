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


	var Locations = _branchService.GetLocations();
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

	
	  var updatedBranch = _branchService.GetById(viewModel.Id);

	 
	  return Json(new { success = true, id = updatedBranch.Id, branch = updatedBranch });
	}


	var errors = ModelState.Values.SelectMany(v => v.Errors.Select(e => e.ErrorMessage)).ToList();
	return Json(new { success = false, message = string.Join(", ", errors) });
  }


  // GET: Show the Delete Confirmation Modal
  [HttpGet]
  public IActionResult Delete(int id)
  {
	var branch = _branchService.GetById(id);

	// If branch is not found, return a failure message
	if (branch == null)
	{
	  return Json(new { success = false, message = "Branch not found." });
	}




	// Return the delete modal as a partial view with the branch data
	return PartialView("DeleteBranchModel", branch);
  }

  // POST: Delete the branch
  [HttpPost]
  public IActionResult DeleteConfirmed(int id)
  {
	try
	{
	  // Call the service to delete the branch by ID
	  var branch = _branchService.GetById(id);

	  if (branch == null)
	  {
		return Json(new { success = false, message = "Branch not found." });
	  }

	  // Delete the branch
	  _branchService.Delete(id);

	  return Json(new { success = true, message = "Branch deleted successfully" });
	}
	catch (KeyNotFoundException ex)
	{
	  // Handle the case where the branch ID is not found
	  return Json(new { success = false, message = ex.Message });
	}
	catch (Exception ex)
	{
	  // Handle any other exceptions
	  return Json(new { success = false, message = "An error occurred while deleting the branch." });
	}
  }



}

