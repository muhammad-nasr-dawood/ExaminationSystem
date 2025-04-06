using ExaminationSystem.MVC.ViewModels;

namespace ExaminationSystem.MVC.Services
{
  public interface IStudentService
  {
	public List<StudentViewModel> GetAll();
  }
}
