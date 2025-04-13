using ExaminationSystem.Core;
using ExaminationSystem.Core.Consts;
using ExaminationSystem.Core.Helpers;
using ExaminationSystem.Core.Models;
using ExaminationSystem.EF;
using Microsoft.CodeAnalysis.CSharp.Syntax;
using System.Linq.Expressions;
using LinqKit;
using AutoMapper;
using ExaminationSystem.MVC.ViewModels.StaffViewModels;

namespace ExaminationSystem.MVC.Services
{
  public class StaffService : IStaffService
  {
	public IUnitOfWork UnitOfWork { get;  }
	private readonly IMapper _mapper;
	public StaffService(IUnitOfWork unitOfWork, IMapper mapper)
	{
	  UnitOfWork = unitOfWork;
	  _mapper = mapper;
	}


	public PaginatedResult<StaffGeneralDisplayVM> FindAll(
		int? pageNumber,
		int? pageSize,
		int? branchIdFilter,
		int? departmentIdFilter,
		bool? StatusFilter,
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

	  if (StatusFilter.HasValue)
	  {
		criteria = criteria.And(staff => staff.SsnNavigation.IsActive == StatusFilter);
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

	  PaginatedResult<Staff> tempRes = UnitOfWork.StaffRepo.FindAll(
		  take: pageSize,
		  skip: (pageNumber - 1) * pageSize,
		  criteria: criteria,
		  orderBy: orderBy,
		  orderByDirection: orderByDirection
	  );

	  PaginatedResult<StaffGeneralDisplayVM> res = new PaginatedResult<StaffGeneralDisplayVM>()
	  {
		Items = _mapper.Map<List<StaffGeneralDisplayVM>>(tempRes.Items),
		PageSize = tempRes.PageSize,
		CurrentPage = tempRes.CurrentPage,
		TotalPages = tempRes.TotalPages,
		TotalFilteredItems = tempRes.TotalFilteredItems,
		TotalItemsInTable = tempRes.TotalItemsInTable,
	  };

	  return res;
	}

  }
}
