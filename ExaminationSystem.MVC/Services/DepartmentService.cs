using AutoMapper;
using ExaminationSystem.Core;
using ExaminationSystem.Core.Helpers;
using ExaminationSystem.MVC.ViewModels.BranchViewModels;
using ExaminationSystem.MVC.ViewModels.DepartmentViewModels;
using ExaminationSystem.MVC.ViewModels.StudentViewModels;

using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Mvc.Rendering;
using AutoMapper.QueryableExtensions;

namespace ExaminationSystem.MVC.Services
{
  public class DeparmentService : IDepartmentService
  {
	private readonly IUnitOfWork _unitOfWork;
	private readonly IMapper _mapper;
	public DeparmentService(IUnitOfWork unitOfWork, IMapper mapper)
	{
	  _unitOfWork = unitOfWork;
	  _mapper = mapper;
	}
	public List<DepartmentViewModel> GetAll()
	{
	  var departments = _unitOfWork.DepartmentRepo
		  .FindAllQueryable(d => !d.IsDeleted)
		  .AsNoTracking() 
		  .ProjectTo<DepartmentViewModel>(_mapper.ConfigurationProvider) 
		  .ToList();

	  return departments;
	}


	public async Task<AddEditDeptViewModel> GetDepartmentForEditAsync(int id)
	{
	  var dept = await _unitOfWork.DepartmentRepo.FindAsync(d => d.Id == id);
	  if (dept == null) return null;

	  var viewModel = _mapper.Map<AddEditDeptViewModel>(dept);

	  var activeIntakeId = await _unitOfWork.IntakeRepo.GetActiveIntakeIdAsync();
	  if (activeIntakeId != 0)
	  {
		viewModel.SelectedBranchIds = await _unitOfWork.BranchDeptRepo
			.FindAllQueryable(x => x.DeptId == id && x.IntakeId == activeIntakeId)
			.Select(x => x.BranchId)
			.ToListAsync();
	  }

	  viewModel.AvailableBranches = await _unitOfWork.BranchesRepo
		  .FindAllQueryable(b => !b.IsDeleted)
		  .Include(b => b.ZipCodeNavigation)
		  .AsNoTracking()
		  .Select(b => new BranchViewModel
		  {
			Id = b.Id,
			LocationName = b.ZipCodeNavigation.Governate
		  })
		  .ToListAsync();


	  return viewModel;
	}




	public DepartmentViewModel Add(AddEditDeptViewModel model)
	{
	  var entity = _mapper.Map<Department>(model);
	  _unitOfWork.DepartmentRepo.Add(entity);
	  _unitOfWork.Complete();

	  if (model.SelectedBranchIds != null && model.SelectedBranchIds.Any())
	  {
		var activeIntakeId = _unitOfWork.IntakeRepo.GetActiveIntakeIdAsync().Result;
		if (activeIntakeId == 0)
		  throw new InvalidOperationException("No active intake found.");

		foreach (var branchId in model.SelectedBranchIds)
		{
		  _unitOfWork.BranchDeptRepo.Add(new BranchDept
		  {
			BranchId = branchId,
			DeptId = entity.Id,
			IntakeId = activeIntakeId
		  });
		}
		_unitOfWork.Complete();
	  }

	  return _mapper.Map<DepartmentViewModel>(entity);
	}






	public void Update(AddEditDeptViewModel model)
	{
	  var entity = _unitOfWork.DepartmentRepo.GetById(model.Id);
	  if (entity == null)
		throw new Exception("Department not found.");

	  _mapper.Map(model, entity);
	  _unitOfWork.Complete();

	  var activeIntakeId = _unitOfWork.IntakeRepo.GetActiveIntakeIdAsync().Result;

	  var existingLinks = _unitOfWork.BranchDeptRepo
		  .FindAll(bd => bd.DeptId == entity.Id && bd.IntakeId == activeIntakeId)
		  .ToList();

	  foreach (var link in existingLinks)
		_unitOfWork.BranchDeptRepo.Delete(link);

	  if (model.SelectedBranchIds != null && model.SelectedBranchIds.Any())
	  {
		foreach (var branchId in model.SelectedBranchIds)
		{
		  _unitOfWork.BranchDeptRepo.Add(new BranchDept
		  {
			DeptId = entity.Id,
			BranchId = branchId,
			IntakeId = activeIntakeId
		  });
		}
	  }

	  _unitOfWork.Complete();
	}





	public bool Delete(int id)
	{
	  var entity = _unitOfWork.DepartmentRepo.GetById(id);
	  if (entity == null)
		return false;

	  entity.IsDeleted = true;
	  _unitOfWork.Complete();
	  return true;
	}

	public async Task<PaginatedResult<DepartmentViewModel>> GetPagedDepartmentsAsync(string? searchTerm, int pageNumber, int pageSize, int? branchId)
	{
	  var query = _unitOfWork.DepartmentRepo.FindAllQueryable(d =>
		  !d.IsDeleted &&
		  (string.IsNullOrEmpty(searchTerm) || d.Name.Contains(searchTerm) || d.Disc.Contains(searchTerm)) &&
		  (!branchId.HasValue || d.StudentIntakeBranchDepartmentStudies.Any(b => b.BranchId == branchId))
	  );

	  int total = await query.CountAsync();

	  var pagedItems = await query
		  .OrderBy(d => d.Name)
		  .Skip((pageNumber - 1) * pageSize)
		  .Take(pageSize)
		  .Select(d => new DepartmentViewModel
		  {
			Id = d.Id,
			Name = d.Name,
			Disc = d.Disc,
			IsDeleted = d.IsDeleted,
			TotalCapacity = d.StudentIntakeBranchDepartmentStudies.Count
		  })
		  .ToListAsync();

	  return new PaginatedResult<DepartmentViewModel>
	  {
		Items = pagedItems,
		CurrentPage = pageNumber,
		PageSize = pageSize,
		TotalFilteredItems = total,
		TotalItemsInTable = total,
		TotalPages = (int)Math.Ceiling((double)total / pageSize)
	  };
	}




	public async Task<List<DepartmentViewModel>> GetDepartmentsByBranchIdAsync(int branchId)
	{
	  var branch = _unitOfWork.BranchesRepo.GetById(branchId);
	  if (branch == null)
	  {
		return new List<DepartmentViewModel>();
	  }
		Expression<Func<Department, bool>> fillters = dept => dept.BranchDepts.Any(bd => bd.BranchId == branchId);
		var branchDepts = await _unitOfWork.DepartmentRepo.FindAllAsync(criteria: fillters);

	  var departments = _mapper.Map<List<DepartmentViewModel>>(branchDepts);
	  return departments;

	}
	public async Task<bool> IsNameUniqueAsync(string name, int id)
	{
	  Expression<Func<Department, bool>> criteria =
		  dept => dept.Name == name && dept.Id != id && !dept.IsDeleted;

	  var exists = await _unitOfWork.DepartmentRepo.FindAsync(criteria);
	  return exists == null; 
	}

	public async Task<bool> IsDiscUniqueAsync(string disc, int id)
	{
	  Expression<Func<Department, bool>> criteria =
		  dept => dept.Disc == disc && dept.Id != id && !dept.IsDeleted;

	  var exists = await _unitOfWork.DepartmentRepo.FindAsync(criteria);
	  return exists == null;
	}

  }


}

