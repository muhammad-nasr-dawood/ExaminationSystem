using ExaminationSystem.Core.Helpers;
using ExaminationSystem.Core.Models;
using ExaminationSystem.MVC.ViewModels.BranchViewModels;
using ExaminationSystem.MVC.ViewModels.DepartmentViewModels;
using ExaminationSystem.MVC.ViewModels.StaffViewModels;
using ExaminationSystem.MVC.ViewModels.StudentViewModels;
using Microsoft.CodeAnalysis.Operations;

namespace ExaminationSystem.MVC.Services
{
  public interface IBranchService
  {
	List<BranchViewModel> GetAll();
    Task<List<LocationViewModel>> GetLocations(int? id = null);
	public BranchViewModel Add(BranchEditViewModel viewModel);
	BranchEditViewModel GetBranchForEdit(int id);
	void Update(BranchEditViewModel viewModel);
	void Delete(int id);
	public Task<bool> DeleteManagerByBranchId(int id);
	Task<string?> AddBranchManager(int branchId, long staffSsn);
	Task<StaffBranchManage> GetBranchThatOwnStaffByID(int branchId);
	Task<BranchManagerViewModel> GetUnassignedStaffAsync(int branchId);
	public List<DepartmentViewModel> GetDepartmentsByBranch(int branchId);
	List<DepartmentViewModel> GetDepartmentsWithCapacitiesByBranch(int branchId);
    Task<PaginatedResult<BranchViewModel>> GetPagedBranchesAsync(string? searchTerm, int pageNumber, int pageSize);


	}

  } 
