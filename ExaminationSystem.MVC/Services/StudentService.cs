using AutoMapper;
using ExaminationSystem.Core;
using ExaminationSystem.Core.Helpers;
using ExaminationSystem.Core.Models;
using ExaminationSystem.EF;
using ExaminationSystem.MVC.ViewModels.StudentViewModels;
using System.Linq.Expressions;
using System.Threading.Tasks;

namespace ExaminationSystem.MVC.Services
{
  // those services are being used for auto-mapping and in a lot of other functionalities as u see below
  // it deals also with the repos that has direct access to the db
  public class StudentService : IStudentService
  {
	public IUnitOfWork UnitOfWork { get; }

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


	//public async Task<PaginatedResult<StudentVM>> GetAllAsync(
	//  	int? pageNumber,
	//	int? pageSize,
	//	params Expression<Func<Student, object>>[] _includes
	//  )
	//{
	//  pageNumber ??= 1;
	//  pageSize ??= 10;

	//  IEnumerable<Student> stdRes = await UnitOfWork.Students.FindAllAsync(take: pageSize, skip: (pageNumber - 1) * pageSize, includes: _includes);

	//  IEnumerable<StudentVM> stdResVM = _mapper.Map<IEnumerable<StudentVM>>(stdRes);

	//  return new PaginatedResult<StudentVM>()
	//  {

	//  };
	//}

	public async Task<PaginatedResult<StudentVM>> GetAllAsync(
	int? pageNumber,
	int? pageSize,
	params Expression<Func<Student, object>>[] includes)
	{
	  // Set default values if null
	  int currentPage = pageNumber ?? 1;
	  int currentSize = pageSize ?? 10;

	  // Retrieve total number of items in the Student table
	  int totalItems = await _unitOfWork.Students.CountAsync();

	  // Calculate total number of pages
	  int totalPages = (int)Math.Ceiling((double)totalItems / currentSize);

	  // Retrieve paginated data with includes
	  IEnumerable<Student> students = await _unitOfWork.Students.FindAllAsync(
		  take: currentSize,
		  skip: (currentPage - 1) * currentSize,
		  includes: includes);

	  // Map Student entities to StudentVM view models
	  List<StudentVM> studentVMs = _mapper.Map<List<StudentVM>>(students);

	  // Construct and return the PaginatedResult
	  return new PaginatedResult<StudentVM>
	  {
		Items = studentVMs,
		CurrentPage = currentPage,
		PageSize = currentSize,
		TotalPages = totalPages,
		TotalFilteredItems = totalItems,
		TotalItemsInTable = totalItems
	  };
	}

	public async Task<StudentDetailsVM> GetStdByIdAsync(long id)
	{
	  if (id == 0)
		throw new Exception("SSN Should be Provided");

	  Student std = await _unitOfWork.Students.GetByIdAsync(id);

	  if (std == null)
		throw new NullReferenceException("No Student With that SSN");
	  return _mapper.Map<StudentDetailsVM>(std);
	}
  }
}
