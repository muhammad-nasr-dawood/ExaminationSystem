using AutoMapper;
using ExaminationSystem.Core;
using ExaminationSystem.Core.Helpers;
using ExaminationSystem.MVC.ViewModels.BranchViewModels;
using ExaminationSystem.MVC.ViewModels.DepartmentViewModels;
using ExaminationSystem.MVC.ViewModels.StudentViewModels;

using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Mvc.Rendering;

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
	  var departments = _unitOfWork.DepartmentRepo.FindAll(d => !d.IsDeleted).ToList();
	  return _mapper.Map<List<DepartmentViewModel>>(departments);
	}
	public AddEditDeptViewModel GetDepartmentForEdit(int id)
	{
	  var dept = _unitOfWork.DepartmentRepo.GetById(id);
	  if (dept == null) return null;

	  return _mapper.Map<AddEditDeptViewModel>(dept);
	}

	public DepartmentViewModel Add(AddEditDeptViewModel model)
	{
	  var entity = _mapper.Map<Department>(model);
	  _unitOfWork.DepartmentRepo.Add(entity);
	  _unitOfWork.Complete();

	  return _mapper.Map<DepartmentViewModel>(entity);
	}

	public void Update(AddEditDeptViewModel model)
	{
	  var entity = _unitOfWork.DepartmentRepo.GetById(model.Id);
	  _mapper.Map(model, entity);
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
  }


}

