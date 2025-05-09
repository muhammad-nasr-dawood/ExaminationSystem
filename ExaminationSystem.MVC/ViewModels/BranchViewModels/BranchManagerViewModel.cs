using ExaminationSystem.MVC.ViewModels.StaffViewModels;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace ExaminationSystem.MVC.ViewModels.BranchViewModels
{
  public class BranchManagerViewModel
  {
	public IEnumerable<SelectListItem> UnassignedStaff { get; set; }
	public long? AssignedStaffSsn { get; set; }
	public int BranchId { get; set; }
  }


}

