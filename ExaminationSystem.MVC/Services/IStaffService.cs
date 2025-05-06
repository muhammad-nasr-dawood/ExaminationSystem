using ExaminationSystem.Core;
using ExaminationSystem.Core.Consts;
using ExaminationSystem.Core.Helpers;
using ExaminationSystem.Core.Models;
using ExaminationSystem.MVC.ViewModels.StaffViewModels;

namespace ExaminationSystem.MVC.Services;

public interface IStaffService
{
  IUnitOfWork UnitOfWork { get; }
  PaginatedResult<StaffGeneralDisplayVM> FindAll(
	  int? pageNumber,
	  int? pageSize,
	  int? branchIdFilter,
	  int? departmentIdFilter,
	  bool? StatusFilter,
	  string columnOrderBy = null,
	  string orderByDirection = OrderBy.Ascending,
	  string searchTerm = null);

  public PaginatedResult<TeachingDisplayViewModel> FindAllRegisteredCourses(
	  int? pageNumber,
	  int? pageSize,
	  int? branchIdFilter,
	  int? departmentIdFilter,
	  int? courseFilter,
	  long StaffSnn,
	  bool? status,
	  string columnOrderBy = null,
	  string orderByDirection = OrderBy.Ascending,
	  string searchTerm = null);

  Task<bool> Add(StaffAddViewModel model);

  StaffDisplayDetailViewModel GetById(long id);

  Task<bool> UpdateById(StaffDisplayDetailViewModel model);

  public User ToggleUserStatus(long userId);

  public User ResetPassword(long userId);

}
