using ExaminationSystem.Core;
using ExaminationSystem.Core.Consts;
using ExaminationSystem.Core.Helpers;
using ExaminationSystem.Core.Models;
using ExaminationSystem.MVC.ViewModels.StaffViewModels;
using ExaminationSystem.MVC.ViewModels.StudentViewModels;
using System.Linq.Expressions;

namespace ExaminationSystem.MVC.Services
{
  public interface IStudentService
  {
	IUnitOfWork UnitOfWork { get; }
	//public List<StudentViewModel> GetAll();

	PaginatedResult<StudentVM> FindAll(
	int? pageNumber,
	int? pageSize,
	int? branchIdFilter,
	int? departmentIdFilter,
	bool? StatusFilter,
	string columnOrderBy = null,
	string orderByDirection = OrderBy.Ascending,
	string searchTerm = null);


	public Task<StudentDetailsVM> GetStdByIdAsync (long id);

	public bool Add (StudentAddVM studentAddVM);
	public User ResetPassword(long userId);
	public User ToggleUserStatus(long userId);
	public StudentDetailsVM GetById(long id);

	public Task<StudentDetailsVM> GetByEmailAsync(string email, long? Ssn);
	public Task<StudentDetailsVM> GetByPhoneNumberAsync(string phone, long? Ssn);

	public bool UpdateStudent(StudentDetailsVM stdvm);

	public Task<List<StudentBasicInfoVM>> GetStudentsByDepartmentBranchAndActiveIntake(int deptId, int branchId);

  }
}
