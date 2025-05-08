using ExaminationSystem.Core.Helpers;
using ExaminationSystem.MVC.ViewModels.DepartmentViewModels;
using ExaminationSystem.MVC.ViewModels.StudentViewModels;

namespace ExaminationSystem.MVC.Services
{
  public interface IDepartmentService
  {
	 List<DepartmentViewModel> GetAll();
	 Task<PaginatedResult<DepartmentViewModel>> GetPagedDepartmentsAsync(string? searchTerm, int pageNumber, int pageSize, int? branchId = null);
	AddEditDeptViewModel GetDepartmentForEdit(int id);
	DepartmentViewModel Add(AddEditDeptViewModel model);
	void Update(AddEditDeptViewModel model);
	bool Delete(int id);
  }
}
