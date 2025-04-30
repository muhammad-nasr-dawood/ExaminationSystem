using AutoMapper;
using ExaminationSystem.Core;
using ExaminationSystem.MVC.ViewModels.BranchViewModels;
using ExaminationSystem.MVC.ViewModels.DepartmentViewModels;
using ExaminationSystem.MVC.ViewModels.StudentViewModels;

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

	public AddEditDeptViewModel Add(AddEditDeptViewModel model)
	{
	  var entity = _mapper.Map<Department>(model);
	  _unitOfWork.DepartmentRepo.Add(entity);
	  _unitOfWork.Complete();

	  return _mapper.Map<AddEditDeptViewModel>(entity);
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

  }


}

