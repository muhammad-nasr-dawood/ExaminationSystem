using ExaminationSystem.MVC.ViewModels.DepartmentViewModels;
using ExaminationSystem.MVC.ViewModels.StudentViewModels;

namespace ExaminationSystem.MVC.Services
{
  public interface IDepartmentService
  {
	 List<DepartmentViewModel> GetAll();
	AddEditDeptViewModel GetDepartmentForEdit(int id);
	AddEditDeptViewModel Add(AddEditDeptViewModel model);
	void Update(AddEditDeptViewModel model);
	bool Delete(int id);
  }
}
