using AutoMapper;
using ExaminationSystem.Core;
using ExaminationSystem.Core.Consts;
using ExaminationSystem.Core.Helpers;
using ExaminationSystem.Core.Models;
using ExaminationSystem.EF;
using ExaminationSystem.MVC.ViewModels.StudentViewModels;
using LinqKit;
using System.Linq.Expressions;
using System.Threading.Tasks;

namespace ExaminationSystem.MVC.Services
{
  // those services are being used for auto-mapping and in a lot of other functionalities as u see below
  // it deals also with the repos that has direct access to the db
  public class StudentService : IStudentService
  {
	public IUnitOfWork UnitOfWork { get; }
	private readonly IMapper _mapper;
	private readonly IPasswordService _passwordService;

	public StudentService(
	  IUnitOfWork _unitOfWork,
	  IMapper mapper,
	  IPasswordService passwordService)
	{
	  UnitOfWork = _unitOfWork;
	  _mapper = mapper;
	  _passwordService = passwordService;
	}


	public PaginatedResult<StudentVM> FindAll(
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
	  Expression<Func<Student, bool>> criteria = student => true;

	  if (branchIdFilter.HasValue)
	  {
		criteria = criteria.And(student => student.StudentIntakeBranchDepartmentStudies.Any(w => w.BranchId == branchIdFilter));
	  }

	  if (departmentIdFilter.HasValue)
	  {
		criteria = criteria.And(student => student.StudentIntakeBranchDepartmentStudies.Any(w => w.DepartmentId == departmentIdFilter));
	  }

	  if (StatusFilter.HasValue)
	  {
		criteria = criteria.And(student => student.SsnNavigation.IsActive == StatusFilter);
	  }

	  if (!string.IsNullOrWhiteSpace(searchTerm))
	  {
		criteria = criteria.And(student => student.SsnNavigation.Fname.Contains(searchTerm) || student.SsnNavigation.Lname.Contains(searchTerm));
	  }

	  Expression<Func<Student, object>> orderBy = null;
	  if (!string.IsNullOrEmpty(columnOrderBy))
	  {
		if (columnOrderBy.Equals("First Name", StringComparison.OrdinalIgnoreCase))
		{
		  orderBy = student => student.SsnNavigation.Fname;
		}
		else if (columnOrderBy.Equals("Last Name", StringComparison.OrdinalIgnoreCase))
		{
		  orderBy = student => student.SsnNavigation.Lname;
		}
	  }

	  PaginatedResult<Student> tempRes = UnitOfWork.StudentRepo.FindAll(
		  take: pageSize,
		  skip: (pageNumber - 1) * pageSize,
		  criteria: criteria,
		  orderBy: null,
		  orderByDirection: orderByDirection
	  );

	  PaginatedResult<StudentVM> res = new PaginatedResult<StudentVM>()
	  {
		Items = _mapper.Map<List<StudentVM>>(tempRes.Items),
		PageSize = tempRes.PageSize,
		CurrentPage = tempRes.CurrentPage,
		TotalPages = tempRes.TotalPages,
		TotalFilteredItems = tempRes.TotalFilteredItems,
		TotalItemsInTable = tempRes.TotalItemsInTable,
	  };

	  return res;
	}

	//public List<StudentViewModel> GetAll()
	//{
	//  var stds = UnitOfWork.StudentRepo.GetAll();

	//  return _mapper.Map<List<StudentViewModel>>(stds); // List<StudentViewModel> is the destination object // stds is the source object
	//}





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
	  int totalItems = await UnitOfWork.StudentRepo.CountAsync();

	  // Calculate total number of pages
	  int totalPages = (int)Math.Ceiling((double)totalItems / currentSize);

	  // Retrieve paginated data with includes
	  IEnumerable<Student> students = await UnitOfWork.StudentRepo.FindAllAsync(
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

	  Student std = await UnitOfWork.StudentRepo.GetByIdAsync(id);

	  if (std == null)
		throw new NullReferenceException("No Student With that SSN");
	  return _mapper.Map<StudentDetailsVM>(std);
	}

	public bool Add(StudentAddVM studentAddVM)
	{
	  var userEntity = _mapper.Map<User>(studentAddVM);
	  if (userEntity != null)
	  {
		userEntity.PasswordHash = _passwordService.HashPassword(userEntity.Ssn.ToString()); // user ssn will be his password
	  }
	  var studentEntity = _mapper.Map<Student>(studentAddVM);

	  UnitOfWork.UserRepo.Add(userEntity);
	  UnitOfWork.StudentRepo.Add(studentEntity);

	  var numOfRowsAffected = UnitOfWork.Complete();

	  return numOfRowsAffected == 2;
	}
  }
}
