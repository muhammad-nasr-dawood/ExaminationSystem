using ExaminationSystem.MVC.ViewModels.DepartmentViewModels;
using ExaminationSystem.MVC.ViewModels.StudentViewModels;

namespace ExaminationSystem.MVC.Services
{
  public interface IDepartmentService
  {
	public List<DepartmentViewModel> GetAll();
  }
}
