using AutoMapper.QueryableExtensions;
using ExaminationSystem.Core;
using ExaminationSystem.Core.Helpers;
using ExaminationSystem.Core.Models;
using ExaminationSystem.MVC.ViewModels.BranchViewModels;
using ExaminationSystem.MVC.ViewModels.DepartmentViewModels;
using ExaminationSystem.MVC.ViewModels.StaffViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Mvc.Rendering;




namespace ExaminationSystem.MVC.Services
{
  public class BranchService : IBranchService
  {
	private readonly IUnitOfWork _unitOfWork;
	private readonly IMapper _mapper;

	public BranchService(IUnitOfWork unitOfWork, IMapper mapper)
	{
	  _unitOfWork = unitOfWork;
	  _mapper = mapper;
	}

	public List<BranchViewModel> GetAll()
	{

	  var branches = _unitOfWork.BranchesRepo.FindAll(b => !b.IsDeleted).ToList();
	  return _mapper.Map<List<BranchViewModel>>(branches);
	}


	public BranchEditViewModel GetBranchForEdit(int id)
	{
	  var branch = _unitOfWork.BranchesRepo.GetById(id);
	  if (branch == null)
	  {
		return null;
	  }

	  return _mapper.Map<BranchEditViewModel>(branch);
	}

	public void Update(BranchEditViewModel viewModel)
	{
	  var branch = _unitOfWork.BranchesRepo.GetById(viewModel.Id);
	  if (branch != null)
	  {

		_mapper.Map(viewModel, branch);
		_unitOfWork.Complete();
	  }
	}


	public async Task<List<LocationViewModel>> GetLocations(int? id = null)
	{

	  var locations = await _unitOfWork.LocationRepo.GetLocationsWithNoBranchAndIsDeletedAsync(id);
	  return _mapper.Map<List<LocationViewModel>>(locations);
	}


	public void Delete(int id)
	{
	  var branch = _unitOfWork.BranchesRepo.GetById(id);

	  if (branch == null)
	  {
		throw new KeyNotFoundException("Branch not found.");
	  }
	  else
	  {
		branch.IsDeleted = true;
		_unitOfWork.Complete();

	  }

	}

	public async Task<bool> DeleteManagerByBranchId(int id)
	{
	  var Branch = await _unitOfWork.StaffBranchManageRepo.GetByBranchId(id);
	  if (Branch == null)
		return false;

	  else
	  {
		_unitOfWork.StaffBranchManageRepo.Delete(Branch);
		_unitOfWork.Complete();
		return (Branch != null);
	  }
	}


	public async Task<BranchManagerViewModel> GetUnassignedStaffAsync(int branchId)
	{
	  var unassignedStaff = await _unitOfWork.BranchesRepo
	  .GetUnassignedStaffQueryable(branchId)
	  .Select(s => new SelectListItem
	  {
		Value = s.Ssn.ToString(),
		Text = s.SsnNavigation.Fname + " " + s.SsnNavigation.Lname
	  })
	  .ToListAsync();

	  var assignedStaff = await _unitOfWork.StaffBranchManageRepo
		  .FindAsync(sbm => sbm.BranchId == branchId);

	  return new BranchManagerViewModel
	  {
		UnassignedStaff = unassignedStaff,
		AssignedStaffSsn = assignedStaff?.StaffSsn
	  };

	}





	public async Task<string?> AddBranchManager(int branchId, long staffSsn)
	{
	  
	  var rec = await _unitOfWork.StaffBranchManageRepo.AddBranchManager(branchId, staffSsn);
	  _unitOfWork.Complete();

	  if (rec == null)
		return null;

	  var staff = await _unitOfWork.StaffRepo.FindAsync(s => s.Ssn == staffSsn);
	  return _mapper.Map<StaffGeneralDisplayVM>( staff).FullName; 
	}



	public async Task<StaffBranchManage> GetBranchThatOwnStaffByID(int branchId)
	{
	  return await _unitOfWork.StaffBranchManageRepo.GetByBranchId(branchId);
	}

	public BranchViewModel Add(BranchEditViewModel viewModel)
	{
	  var newBranch = _mapper.Map<Branch>(viewModel);
	  _unitOfWork.BranchesRepo.Add(newBranch);
	  _unitOfWork.Complete();


	  var location = _unitOfWork.LocationRepo
		  .Find(l => l.ZipCode == newBranch.ZipCode);

	  var result = _mapper.Map<BranchViewModel>(newBranch);

	  if (location != null)
	  {
		result.LocationName = location.Governate;
	  }

	  return result;
	}



	public List<DepartmentViewModel> GetDepartmentsByBranch(int branchId)
	{

	  var departments = _unitOfWork.DepartmentRepo.FindAll(d => d.BranchDepts.Any(bd => bd.BranchId == branchId) && !d.IsDeleted).ToList();


	  return _mapper.Map<List<DepartmentViewModel>>(departments);
	}
	public List<DepartmentViewModel> GetDepartmentsWithCapacitiesByBranch(int branchId)
	{
	  var departments = _unitOfWork.DepartmentRepo
		  .FindAll(d => d.BranchDepts.Any(bd => bd.BranchId == branchId) && !d.IsDeleted)
		  .ToList();

	  var departmentVMs = _mapper.Map<List<DepartmentViewModel>>(departments);

	  foreach (var deptVM in departmentVMs)
	  {
		var deptEntity = departments.First(d => d.Id == deptVM.Id);

		deptVM.TotalCapacity = deptEntity.StudentIntakeBranchDepartmentStudies.Count();
		deptVM.BranchCapacity = deptEntity.StudentIntakeBranchDepartmentStudies
			.Where(x => x.BranchId == branchId)
			.Count();
	  }

	  return departmentVMs;
	}

	public async Task<PaginatedResult<BranchViewModel>> GetPagedBranchesAsync(string? searchTerm, int pageNumber, int pageSize)
	{
	  var baseQuery = _unitOfWork.BranchesRepo
	  .FindAllQueryable(b =>
	  !b.IsDeleted &&
	  (string.IsNullOrEmpty(searchTerm) ||
	  b.ZipCodeNavigation.Governate.Contains(searchTerm) ||
	  (b.StaffBranchManage != null &&
	  (b.StaffBranchManage.StaffSsnNavigation.SsnNavigation.Fname + " " +
	  b.StaffBranchManage.StaffSsnNavigation.SsnNavigation.Lname).Contains(searchTerm)))
	  );

	  int total = await baseQuery.CountAsync();

	  var pagedItems = baseQuery.Select(b => new BranchViewModel
	  {
		Id = b.Id,
		ZipCode = b.ZipCode,
		LocationName = b.ZipCodeNavigation.Governate,
		StreetNo = b.StreetNo,
		IsDeleted = b.IsDeleted,
		NumberOfDepartments = b.BranchDepts.Count,
		BranchManagerName = b.StaffBranchManage.StaffSsnNavigation.SsnNavigation != null
	  ? b.StaffBranchManage.StaffSsnNavigation.SsnNavigation.Fname + " " +
	  b.StaffBranchManage.StaffSsnNavigation.SsnNavigation.Lname
	  : "Not Assigned"
	  }).Skip((pageNumber - 1) * pageSize)
		  .Take(pageSize)
		  .ToList();

	  return new PaginatedResult<BranchViewModel>
	  {
		Items = pagedItems,
		CurrentPage = pageNumber,
		PageSize = pageSize,
		TotalFilteredItems = total,
		TotalItemsInTable = total,
		TotalPages = (int)Math.Ceiling((double)total / pageSize)
	  };

	}

	


  }
  }


