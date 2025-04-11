using ExaminationSystem.Core;
using ExaminationSystem.Core.Consts;
using ExaminationSystem.Core.Helpers;
using ExaminationSystem.Core.Models;

namespace ExaminationSystem.MVC.Services
{
  public interface IStaffService
  {
	IUnitOfWork UnitOfWork { get; }
	PaginatedResult<Staff> FindAll(
		int? pageNumber,
		int? pageSize,
		int? branchIdFilter,
		int? departmentIdFilter,
		string columnOrderBy = null,
		string orderByDirection = OrderBy.Ascending,
		string searchTerm = null);
  }
}
