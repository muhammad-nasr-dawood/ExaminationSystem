using ExaminationSystem.Core;
using ExaminationSystem.Core.Helpers;
using ExaminationSystem.Core.Models;
using ExaminationSystem.MVC.ViewModels.StudentViewModels;
using System.Linq.Expressions;

namespace ExaminationSystem.MVC.Services
{
  public interface IStudentService
  {
	IUnitOfWork UnitOfWork { get; }
	public List<StudentViewModel> GetAll();
	public Task<PaginatedResult<StudentVM>> GetAllAsync(
	  int? pageNumber,
	  int? pageSize,
	  params Expression<Func<Student, object>>[] includes);

	public Task<StudentDetailsVM> GetStdByIdAsync (long id);


  }
}
