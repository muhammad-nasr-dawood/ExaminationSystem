using AutoMapper;
using ExaminationSystem.Core;
using ExaminationSystem.Core.Models;
using ExaminationSystem.EF;
using ExaminationSystem.MVC.ViewModels.StudentViewModels;

namespace ExaminationSystem.MVC.Services
{
  // those services are being used for auto-mapping and in a lot of other functionalities as u see below
  // it deals also with the repos that has direct access to the db
  public class StudentService : IStudentService
  {
	private readonly IUnitOfWork _unitOfWork;
	private readonly IMapper _mapper;
	public StudentService(IUnitOfWork unitOfWork, IMapper mapper) {
	  _unitOfWork = unitOfWork;
	  _mapper = mapper;
	}
	public List<StudentViewModel> GetAll()
	{
	  var stds = _unitOfWork.Students.GetAll();

	  return _mapper.Map<List<StudentViewModel>>(stds); // List<StudentViewModel> is the destination object // stds is the source object
	}
  }
}
