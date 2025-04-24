using ExaminationSystem.Core.Models;
using ExaminationSystem.MVC.ViewModels.BranchViewModels;
using ExaminationSystem.MVC.ViewModels.StaffViewModels;
using ExaminationSystem.MVC.ViewModels.StudentViewModels;
using Microsoft.CodeAnalysis.Operations;

namespace ExaminationSystem.MVC.Services
{
  public interface IBranchService
  {
	List<BranchViewModel> GetAll();
	List<LocationViewModel> GetLocations();
	BranchEditViewModel GetBranchForEdit(int id);
	void Update(BranchEditViewModel viewModel);
	void Delete(int id);
	public Task<bool> DeleteManagerByBranchId(int id);
	Task<bool> AddBranchManager(int branchId, long staffSsn);
	Task<StaffBranchManage> GetBranchThatOwnStaffByID(int branchId);
	Task<BranchManagerViewModel> GetUnassignedStaffAsync(int branchId);

  }

  } 
