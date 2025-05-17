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
	  var branches = _unitOfWork.BranchesRepo
		  .FindAllQueryable(b => !b.IsDeleted)
		  .Include(b => b.ZipCodeNavigation) 
		  .AsNoTracking()
		  .Select(b => new BranchViewModel
		  {
			Id = b.Id,
			LocationName = b.ZipCodeNavigation.Governate
		  })
		  .ToList();

	  return branches;
	}




	public async Task<BranchEditViewModel> GetBranchForEditAsync(int id)
	{
	  var branch = await _unitOfWork.BranchesRepo.FindAsync(b => b.Id == id);
	  if (branch == null) return null;

	  var viewModel = _mapper.Map<BranchEditViewModel>(branch);

	  var activeIntakeId = await _unitOfWork.IntakeRepo.GetActiveIntakeIdAsync();
	  if (activeIntakeId == 0) return viewModel;

	  var assignedDeptIds = _unitOfWork.BranchDeptRepo
		  .FindAllQueryable(bd => bd.BranchId == id && bd.IntakeId == activeIntakeId)
		  .Select(bd => bd.DeptId)
		  .ToList(); 

	  viewModel.SelectedDepartmentIds = assignedDeptIds;

	  viewModel.AvailableDepartments = await _unitOfWork.DepartmentRepo
		  .FindAllQueryable(d => !d.IsDeleted)
		  .AsNoTracking()
		  .ProjectTo<DepartmentViewModel>(_mapper.ConfigurationProvider)
		  .ToListAsync();

	  return viewModel;
	}




	public void Update(BranchEditViewModel viewModel)
	{
	  var branch = _unitOfWork.BranchesRepo.GetById(viewModel.Id);
	  if (branch != null)
	  {
		_mapper.Map(viewModel, branch);
		_unitOfWork.Complete();

		var activeIntakeId = _unitOfWork.IntakeRepo.GetActiveIntakeIdAsync().Result;

		var existingDeptLinks = _unitOfWork.BranchDeptRepo
			.FindAll(bd => bd.BranchId == branch.Id && bd.IntakeId == activeIntakeId)
			.ToList();

		foreach (var link in existingDeptLinks)
		  _unitOfWork.BranchDeptRepo.Delete(link);

		foreach (var deptId in viewModel.SelectedDepartmentIds)
		{
		  _unitOfWork.BranchDeptRepo.Add(new BranchDept
		  {
			BranchId = branch.Id,
			DeptId = deptId,
			IntakeId = activeIntakeId
		  });
		}

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

	public async Task<bool> DeleteManagerByBranchId(int branchId)
	{
	  var manager = await _unitOfWork.StaffBranchManageRepo.GetByBranchId(branchId);
	  if (manager == null) return false;

	  _unitOfWork.StaffBranchManageRepo.Delete(manager);

	  
	  var staff = await _unitOfWork.StaffRepo.FindAsync(s => s.Ssn == manager.StaffSsn);
	  var managerRole = await _unitOfWork.RoleRepo.FindAsync(r => r.Name == "branch_manager");
	  if (staff != null && managerRole != null && staff.Roles.Contains(managerRole))
	  {
		staff.Roles.Remove(managerRole);
	  }

	  await _unitOfWork.CompleteAsync();

	  return true;
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
	 
	  var branch = await _unitOfWork.BranchesRepo.FindAsync(b => b.Id == branchId);
	  var newStaff = await _unitOfWork.StaffRepo.FindAsync(s => s.Ssn == staffSsn);
	  if (branch == null || newStaff == null)
		return null;

	
	  var existing = await _unitOfWork.StaffBranchManageRepo.GetByBranchId(branchId);
	  if (existing != null)
	  {
		var oldStaff = await _unitOfWork.StaffRepo.FindAsync(s => s.Ssn == existing.StaffSsn);
		var managerRole = await _unitOfWork.RoleRepo.FindAsync(r => r.Name == "branch_manager");

		if (oldStaff != null && managerRole != null && oldStaff.Roles.Contains(managerRole))
		{
		  oldStaff.Roles.Remove(managerRole);
		}

		_unitOfWork.StaffBranchManageRepo.Delete(existing);
	  }

	  var activeIntakeId = await _unitOfWork.IntakeRepo.GetActiveIntakeIdAsync();
	  if (activeIntakeId == 0)
		return null;

	  
	  var newManager = new StaffBranchManage
	  {
		StaffSsn = staffSsn,
		BranchId = branchId,
		IntakeId = activeIntakeId,
		HiringDate = DateOnly.FromDateTime(DateTime.Now)
	  };

	  await _unitOfWork.StaffBranchManageRepo.AddAsync(newManager);

	  var roleToAdd = await _unitOfWork.RoleRepo.FindAsync(r => r.Name == "branch_manager");
	  if (roleToAdd != null && !newStaff.Roles.Contains(roleToAdd))
	  {
		newStaff.Roles.Add(roleToAdd);
	  }

	  await _unitOfWork.CompleteAsync();
	  return _mapper.Map<StaffGeneralDisplayVM>(newStaff)?.FullName;
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

	  var activeIntakeId = _unitOfWork.IntakeRepo.GetActiveIntakeIdAsync().Result;
	  if (activeIntakeId == 0)
	  {
		throw new InvalidOperationException("No active intake found.");
	  }

	  foreach (var deptId in viewModel.SelectedDepartmentIds)
	  {
		_unitOfWork.BranchDeptRepo.Add(new BranchDept
		{
		  BranchId = newBranch.Id,
		  DeptId = deptId,
		  IntakeId = activeIntakeId
		});
	  }

	  _unitOfWork.Complete();

	  var location = _unitOfWork.LocationRepo.Find(l => l.ZipCode == newBranch.ZipCode);
	  var result = _mapper.Map<BranchViewModel>(newBranch);
	  result.NumberOfDepartments = viewModel.SelectedDepartmentIds.Count;

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


