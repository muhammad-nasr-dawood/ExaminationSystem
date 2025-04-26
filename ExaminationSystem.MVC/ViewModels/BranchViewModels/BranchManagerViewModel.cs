using ExaminationSystem.MVC.ViewModels.StaffViewModels;

namespace ExaminationSystem.MVC.ViewModels.BranchViewModels
{
  public class BranchManagerViewModel
  {
	public IEnumerable<StaffGeneralDisplayVM> UnassignedStaff { get; set; }
	public long? AssignedStaffSsn { get; set; } 
  }
}

