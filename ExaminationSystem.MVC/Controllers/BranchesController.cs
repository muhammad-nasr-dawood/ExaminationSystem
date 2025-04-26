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


  public async Task< IActionResult>Edit(int id)
  {
	var branch = _branchService.GetBranchForEdit(id);
	if (branch == null)
	{
	  return NotFound();
	}


	var Locations = await _branchService.GetLocations(id);
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

	
	  var updatedBranch = _branchService.GetBranchForEdit(viewModel.Id);

	 
	  return Json(new { success = true, id = updatedBranch.Id, branch = updatedBranch });
	}


	var errors = ModelState.Values.SelectMany(v => v.Errors.Select(e => e.ErrorMessage)).ToList();
	return Json(new { success = false, message = string.Join(", ", errors) });
  }


  
  [HttpGet]
  public IActionResult Delete(int id)
  {
	var branch = _branchService.GetBranchForEdit(id);

	
	if (branch == null)
	{
	  return Json(new { success = false, message = "Branch not found." });
	}

	return PartialView("DeleteBranchModel", branch);
  }

 
  [HttpPost]
  public IActionResult DeleteConfirmed(int id)
  {
	try
	{
	  
	  var branch = _branchService.GetBranchForEdit(id);

	  if (branch == null)
	  {
		return Json(new { success = false, message = "Branch not found." });
	  }

	  _branchService.Delete(id);

	  return Json(new { success = true, message = "Branch deleted successfully" });
	}
	catch (KeyNotFoundException ex)
	{
	  
	  return Json(new { success = false, message = ex.Message });
	}
	catch (Exception ex)
	{
	  
	  return Json(new { success = false, message = "An error occurred while deleting the branch." });
	}
  }


  [HttpGet]
  public async Task<IActionResult> AssignManager(int id)
  {
	
	var unassignedStaff = await _branchService.GetUnassignedStaffAsync(id);


	return PartialView("AssignBranchManagerModal", unassignedStaff);
  }



 [HttpPost]
public async Task<IActionResult> AssignManager(int id, long staffSsn)
{


    var success = await _branchService.AddBranchManager(id, staffSsn);

    if (success)
    {
       
       var branch =  _branchService.GetBranchForEdit(id);
	  

        return Json(new { success = true,Id=id, message = "Manager assigned successfully.", managerName = branch?.BranchManagerName });
    }

    return Json(new { success = false, message = "Error: Could not assign manager." });
}

  [HttpGet]
  public async Task< IActionResult> DeleteManager(int id)
  {
	var branch = await _branchService.GetBranchThatOwnStaffByID(id);

	if (branch == null)
	{
	  return Json(new { success = false, message = "Branch not found." });
	}


	return PartialView("DeleteManagerModal", branch);
  }

  [HttpPost]
  public async Task<IActionResult> DeleteManagerConfirmed(int id)
  {
	
	  var branch = await _branchService.GetBranchThatOwnStaffByID(id); 
	  if (branch == null)
	  {
		return Json(new { success = false, message = "Branch not found." });
	  }

	  bool ok = await _branchService.DeleteManagerByBranchId(id);
	  if (ok)
	  {
		return Json(new { success = true, message = "Staff deleted successfully" });
	  }
	  else
	  {
		return Json(new { success = false, message = "An error occurred while deleting the staff." });
	  }

  }

  public async Task<IActionResult> Add()
  {
	var viewModel = new BranchEditViewModel();
	ViewBag.Locations = await _branchService.GetLocations();
	return PartialView("_EditBranchModal", viewModel);
  }

  [HttpPost]
  public IActionResult Add(BranchEditViewModel viewModel)
  {
	if (ModelState.IsValid)
	{
	  
	  var branch = _branchService.Add(viewModel);

	  
	  return Json(new { success = true, id = 0, branch = branch });
	}

	
	var errors = ModelState.Values.SelectMany(v => v.Errors.Select(e => e.ErrorMessage)).ToList();
	return Json(new { success = false, message = string.Join(", ", errors) });
  }


}

