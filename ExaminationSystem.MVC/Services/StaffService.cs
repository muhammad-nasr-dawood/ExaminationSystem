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
using ExaminationSystem.MVC.ViewModels.TeachingViewModels;
using System.Runtime.CompilerServices;

namespace ExaminationSystem.MVC.Services;

public class StaffService : IStaffService
{
	public IUnitOfWork UnitOfWork { get;  }
	private readonly IMapper _mapper;
	private readonly IPasswordService _passwordService;
	public StaffService(IUnitOfWork unitOfWork, IMapper mapper, IPasswordService passwordService)
	{
	  UnitOfWork = unitOfWork;
	  _mapper = mapper;
	  _passwordService = passwordService;
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
		criteria = criteria.And(staff => staff.StaffBranchIntakeWorksFors.Any(w => w.BranchId == branchIdFilter));
	  }

	 // if (departmentIdFilter.HasValue)
	 // {
		//criteria = criteria.And(staff => staff.StaffBranchDepartmentWorksFors.Any(w => w.DepartmentId == departmentIdFilter));
	 // }

	  if (StatusFilter.HasValue)
	  {
		criteria = criteria.And(staff => staff.SsnNavigation.IsActive == StatusFilter);
	  }

	  if (!string.IsNullOrWhiteSpace(searchTerm))
	  {
		criteria = criteria.And(staff => staff.SsnNavigation.Fname.Contains(searchTerm) || staff.SsnNavigation.Lname.Contains(searchTerm) || staff.SsnNavigation.Ssn.ToString().Contains(searchTerm.ToString())); // search is done by name and ssn 
	  }

	  Expression<Func<Staff, object>> orderBy = null;
	  if (!string.IsNullOrEmpty(columnOrderBy))
	  {
		if (columnOrderBy.Equals("FullName", StringComparison.OrdinalIgnoreCase))
		{
		  orderBy = staff => staff.SsnNavigation.Fname;
		}
		else if (columnOrderBy.Equals("Ssn", StringComparison.OrdinalIgnoreCase))
		{
		  orderBy = staff => staff.SsnNavigation.Ssn;
		}
	  else if (columnOrderBy.Equals("Salary", StringComparison.OrdinalIgnoreCase))
		{
		  orderBy = staff => staff.Salary;
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


	public async Task<bool> Add(StaffAddViewModel model)
	{
	  var userEntity = _mapper.Map<User>(model);
	  if (userEntity != null)
	  {
		userEntity.PasswordHash = _passwordService.HashPassword(userEntity.Ssn.ToString()); // user ssn will be his password
	  }
	  var staffEntity = _mapper.Map<Staff>(model);

	  UnitOfWork.UserRepo.Add(userEntity);
	  UnitOfWork.StaffRepo.Add(staffEntity);

	  if(model.BranchId is not null)
	  {
		Expression<Func<StaffBranchIntakeWorksFor, bool>> criteria = sw => sw.StaffSsn == model.Ssn;
		var tempStaffWorksfor = await UnitOfWork.WorksForRepo.FindAsync(criteria);
	    if(tempStaffWorksfor != null)
		{
		  throw new Exception("This staff already exist");
		}
		else
		{
		  var intakes = await UnitOfWork.IntakeRepo.GetAllAsync();
		  var currentIntake = -1;
		  foreach (var intake in intakes)
		  {
			currentIntake = Math.Max(intake.Id, currentIntake);
		  }
		  var newStaffWorksFor = new StaffBranchIntakeWorksFor()
		  {
			StaffSsn = model.Ssn,
			BranchId = (int)model.BranchId,
			IntakeId = currentIntake,
			HiringDate = DateOnly.FromDateTime(DateTime.Today)
		  };

		  await UnitOfWork.WorksForRepo.AddAsync(newStaffWorksFor);
		}
	
		
	  }

	  var numOfRowsAffected = await UnitOfWork.CompleteAsync();

	  return numOfRowsAffected >= 2 ;
	}

	public StaffDisplayDetailViewModel GetById(long id)
	{
	  var staff = UnitOfWork.StaffRepo.GetById(id);
	  var staffMapped = _mapper.Map<StaffDisplayDetailViewModel>(staff);

	  return staffMapped;
	}

	public async Task<bool> UpdateById(StaffDisplayDetailViewModel model)
	{

	  var user = await UnitOfWork.UserRepo.GetByIdAsync(model.Ssn);
	  var staff = await UnitOfWork.StaffRepo.GetByIdAsync(model.Ssn);

	  _mapper.Map(model, user); // will only update the values in the automapper configuration and leave every thing else as they are --> that's why i passed the two models the source and the destiantion
	  _mapper.Map(model, staff);

	  if (model.BranchId is not null)
	  {
		  var currentIntakeId = (await UnitOfWork.IntakeRepo.GetAllAsync()).Max(i => i.Id);

		  Expression<Func<StaffBranchIntakeWorksFor, bool>> criteria = sw => sw.StaffSsn == model.Ssn && sw.IntakeId == currentIntakeId;

		  var tempStaffWorksfor = await UnitOfWork.WorksForRepo.FindAsync(criteria);
		if (tempStaffWorksfor != null)
		{
		  /*
		  // tempStaffWorksfor.BranchId = (int)model.BranchId;
		  //tempStaffWorksfor.HiringDate = DateOnly.FromDateTime(DateTime.Today);
		  those lines won't work dirctly since we are modifing in a primary key we have to remove the row first
		  */
		  // remove the old row
		  UnitOfWork.WorksForRepo.Delete(tempStaffWorksfor);

		  // insert the new one
		  var replacement = new StaffBranchIntakeWorksFor
		  {
			StaffSsn = model.Ssn,
			BranchId = (int)model.BranchId,
			IntakeId = currentIntakeId,
			HiringDate = DateOnly.FromDateTime(DateTime.Today)
		  };
		  await UnitOfWork.WorksForRepo.AddAsync(replacement);
		}
		else
		{
		  var newStaffWorksFor = new StaffBranchIntakeWorksFor()
		  {
			StaffSsn = model.Ssn,
			BranchId = (int)model.BranchId,
			IntakeId = currentIntakeId,
			HiringDate = DateOnly.FromDateTime(DateTime.Today)
		  };

		  await UnitOfWork.WorksForRepo.AddAsync(newStaffWorksFor);
		}


	  }

	try
	{
	  await UnitOfWork.CompleteAsync();
	}
	catch (Exception ex)
	{
	  // ex.Message will contain "Database update failed: ..." 
	  // and ex.InnerException will be the original exception if you need more detail.
	  // Return or log it as needed.
	  throw;
	}
	//await UnitOfWork.CompleteAsync(); // it will save changes in the user and staff that you get by id and they will be updated according to the mapped values in auto mapper

	  return true;
	}


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
		string searchTerm = null)
	{
	  pageNumber ??= 1;
	  pageSize ??= 10;
	  Expression<Func<StaffBranchIntakeDepartmentCourseTeach, bool>> criteria = teach => true;

	  criteria = criteria.And(teach => teach.StaffSsn == StaffSnn);

	  if (courseFilter.HasValue)
	  {
		criteria = criteria.And(teach => teach.CourseId == courseFilter);
	  }
	  if (branchIdFilter.HasValue)
	  {
		criteria = criteria.And(teach => teach.BranchId == branchIdFilter);
	  }

	  if (departmentIdFilter.HasValue)
	  {
		criteria = criteria.And(teach => teach.DepartmentId == departmentIdFilter);
	  }
	  if (status.HasValue)
	  {
		if(status == true) // finished
		{
		  criteria = criteria.And(teach => teach.EndingDate != null);
		}
		else
		{
		  criteria = criteria.And(teach => teach.EndingDate == null);
		}
	  }

	  if (!string.IsNullOrWhiteSpace(searchTerm))
	  {
		criteria = criteria.And(teach => teach.Department.Name.Contains(searchTerm) || teach.Branch.ZipCodeNavigation.Governate.Contains(searchTerm) || teach.Course.Name.Contains(searchTerm));
	  }

	  Expression<Func<StaffBranchIntakeDepartmentCourseTeach, object>> orderBy = null;
	  if (!string.IsNullOrEmpty(columnOrderBy))
	  {
		if (columnOrderBy.Equals("Course", StringComparison.OrdinalIgnoreCase))
		{
		  orderBy = teach => teach.Course.Name;
		}
		else if (columnOrderBy.Equals("StartingDate", StringComparison.OrdinalIgnoreCase))
		{
		  orderBy = teach => teach.StartingDate;
		}
	  }

	  PaginatedResult<StaffBranchIntakeDepartmentCourseTeach> tempRes = UnitOfWork.TeachingRepo.FindAll(
		  take: pageSize,
		  skip: (pageNumber - 1) * pageSize,
		  criteria: criteria,
		  orderBy: orderBy,
		  orderByDirection: orderByDirection
	  );

	  PaginatedResult<TeachingDisplayViewModel> res = new PaginatedResult<TeachingDisplayViewModel>()
	  {
		Items = _mapper.Map<List<TeachingDisplayViewModel>>(tempRes.Items),
		PageSize = tempRes.PageSize,
		CurrentPage = tempRes.CurrentPage,
		TotalPages = tempRes.TotalPages,
		TotalFilteredItems = tempRes.TotalFilteredItems,
		TotalItemsInTable = tempRes.TotalItemsInTable,
	  };

	  return res;
	}

	public User ToggleUserStatus(long userId)
	{
	  var user = UnitOfWork.UserRepo.GetById(userId);

	  user.IsActive = !user.IsActive;

	  UnitOfWork.Complete();
	  return user;
	}

  public User ResetPassword(long userId)
  {
	var user = UnitOfWork.UserRepo.GetById(userId);
	user.PasswordHash = _passwordService.HashPassword(userId.ToString());
	UnitOfWork.Complete();
	return user;
  }

}
