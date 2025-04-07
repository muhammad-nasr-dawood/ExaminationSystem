using ExaminationSystem.MVC.ViewModels.StudentViewModels;

namespace ExaminationSystem.MVC.Services
{
  public interface IStudentService
  {
	public List<StudentViewModel> GetAll();
  }
}
