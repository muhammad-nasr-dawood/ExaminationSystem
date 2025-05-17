using ExaminationSystem.Core.Helpers;
using ExaminationSystem.MVC.ViewModels.DepartmentViewModels;
using ExaminationSystem.MVC.ViewModels.StudentViewModels;

namespace ExaminationSystem.MVC.Services
{
  public interface IDepartmentService
  {
	 List<DepartmentViewModel> GetAll();
	 Task<PaginatedResult<DepartmentViewModel>> GetPagedDepartmentsAsync(string? searchTerm, int pageNumber, int pageSize, int? branchId = null);
	Task<AddEditDeptViewModel> GetDepartmentForEditAsync(int id);
	DepartmentViewModel Add(AddEditDeptViewModel model);
	void Update(AddEditDeptViewModel model);
	Task<List<DepartmentViewModel>> GetDepartmentsByBranchIdAsync(int branchId);
	bool Delete(int id);
	Task<bool> IsNameUniqueAsync(string name, int id);
	Task<bool> IsDiscUniqueAsync(string disc, int id);

  }
}
