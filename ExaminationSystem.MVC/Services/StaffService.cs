using ExaminationSystem.Core;
using ExaminationSystem.Core.Consts;
using ExaminationSystem.Core.Helpers;
using ExaminationSystem.Core.Models;
using ExaminationSystem.EF;
using Microsoft.CodeAnalysis.CSharp.Syntax;
using System.Linq.Expressions;
using LinqKit;

namespace ExaminationSystem.MVC.Services
{
  public class StaffService : IStaffService
  {
	public IUnitOfWork UnitOfWork { get;  }
	public StaffService(IUnitOfWork unitOfWork)
	{
	  UnitOfWork = unitOfWork;
	}


	public PaginatedResult<Staff> FindAll(
		int? pageNumber,
		int? pageSize,
		int? branchIdFilter,
		int? departmentIdFilter,
		string columnOrderBy = null,
		string orderByDirection = OrderBy.Ascending,
		string searchTerm = null)
	{
	  pageNumber ??= 1;  
	  pageSize ??= 10;   
	  Expression<Func<Staff, bool>> criteria = staff => true;

	  if (branchIdFilter.HasValue)
	  {
		criteria = criteria.And(staff => staff.StaffBranchDepartmentWorksFors.Any(w => w.BranchId == branchIdFilter));
	  }

	  if (departmentIdFilter.HasValue)
	  {
		criteria = criteria.And(staff => staff.StaffBranchDepartmentWorksFors.Any(w => w.DepartmentId == departmentIdFilter));
	  }

	  if (!string.IsNullOrWhiteSpace(searchTerm))
	  {
		criteria = criteria.And(staff => staff.SsnNavigation.Fname.Contains(searchTerm) || staff.SsnNavigation.Lname.Contains(searchTerm));
	  }

	  Expression<Func<Staff, object>> orderBy = null;
	  if (!string.IsNullOrEmpty(columnOrderBy))
	  {
		if (columnOrderBy.Equals("First Name", StringComparison.OrdinalIgnoreCase))
		{
		  orderBy = staff => staff.SsnNavigation.Fname;
		}
		else if (columnOrderBy.Equals("Last Name", StringComparison.OrdinalIgnoreCase))
		{
		  orderBy = staff => staff.SsnNavigation.Lname;
		}
	  }

	  return UnitOfWork.StaffRepo.FindAll(
		  take: pageSize,
		  skip: (pageNumber - 1) * pageSize,
		  criteria: criteria,
		  orderBy: orderBy,
		  orderByDirection: orderByDirection
	  );
	}

  }
}
