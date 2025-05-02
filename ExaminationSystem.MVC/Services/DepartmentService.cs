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

	  var departments = _unitOfWork.DepartmentRepo.GetAll().ToList();
	  return _mapper.Map<List<DepartmentViewModel>>(departments);
	}


  }
}
