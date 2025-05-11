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

	  return _mapper.Map<StudentDetailsVM>(std);
	}

	public bool Add(StudentAddVM studentAddVM)
	{
	  if (studentAddVM.PhoneNumber != null)
		studentAddVM.PhoneNumber = NormalizePhoneNumber(studentAddVM.PhoneNumber);

	  var userEntity = _mapper.Map<User>(studentAddVM);
	  if (userEntity != null)
	  {
		userEntity.PasswordHash = _passwordService.HashPassword(userEntity.Ssn.ToString()); // user ssn will be his password
	  }
	  var studentEntity = _mapper.Map<Student>(studentAddVM);


	  UnitOfWork.UserRepo.Add(userEntity);
	  UnitOfWork.StudentRepo.Add(studentEntity);
	  var numOfRowsAffected = UnitOfWork.Complete();

	  var latestIntake = (UnitOfWork.IntakeRepo.GetAll())
				  .OrderByDescending(i => i.Id)
				  .FirstOrDefault();

	  var assignStdToDept = new StudentIntakeBranchDepartmentStudy()
	  {
		BranchId = studentAddVM.SelectedBranchId.Value,
		DepartmentId = studentAddVM.SelectedDepartmentId.Value,
		IntakeId = latestIntake.Id,
		StudentSsn = studentAddVM.Ssn
	  };

	  UnitOfWork.StudentIntakeBranchDepartmentStudyRepo.Add(assignStdToDept);

	  UnitOfWork.Complete();

	  return numOfRowsAffected == 2;
	}
    public User ResetPassword(long userId)
	{
	  var user = UnitOfWork.UserRepo.GetById(userId);
	  user.PasswordHash = _passwordService.HashPassword(userId.ToString());
	  UnitOfWork.Complete();
	  return user;
	}
	public User ToggleUserStatus(long userId)
	{
	  var user = UnitOfWork.UserRepo.GetById(userId);

	  user.IsActive = !user.IsActive;

	  UnitOfWork.Complete();
	  return user;
	}

	public StudentDetailsVM GetById(long id)
	{
	  var student = UnitOfWork.StudentRepo.GetById(id);
	  var StudentMapped = _mapper.Map<StudentDetailsVM>(student);

	  return StudentMapped;
	}


	public async Task<StudentDetailsVM> GetByEmailAsync(string email, long? Ssn)
	{
	  Expression<Func<Student, bool>> criteria = std =>
		  std.SsnNavigation.Email == email &&
		  (!Ssn.HasValue || std.SsnNavigation.Ssn != Ssn.Value);

	  var student = await UnitOfWork.StudentRepo.FindAsync(criteria);
	  return _mapper.Map<StudentDetailsVM>(student);
	}


	public async Task<StudentDetailsVM> GetByPhoneNumberAsync(string phone, long? Ssn)
	{
	  var normalizedPhone = NormalizePhoneNumber(phone);

	  Expression<Func<Student, bool>> criteria = std =>
		  std.SsnNavigation.PhoneNumber == normalizedPhone &&
		  (!Ssn.HasValue || std.SsnNavigation.Ssn != Ssn.Value);

	  var student = await UnitOfWork.StudentRepo.FindAsync(criteria);

	  return _mapper.Map<StudentDetailsVM>(student);
	}


	private string NormalizePhoneNumber(string phone)
	{
	  if (string.IsNullOrWhiteSpace(phone))
		return phone;

	  phone = phone.Replace(" ", "").Replace("-", "").Replace("(", "").Replace(")", "");

	  if (phone.StartsWith("+"))
	  {
		phone = phone.Substring(1);
		phone = phone.TrimStart("0123456789".ToCharArray()); 
	  }
	  else if (phone.StartsWith("00"))
	  {
		phone = phone.Substring(2);
		phone = phone.TrimStart("0123456789".ToCharArray()); 
	  }

	  // If there's still a leading country code (e.g., 20 for Egypt), remove first few digits
	  // Assuming local numbers are always 10 or 11 digits in your DB
	  if (phone.Length > 11)
		phone = phone.Substring(phone.Length - 11); // take last 11 digits only

	  return phone;
	}


	public bool UpdateStudent (StudentDetailsVM stdvm)
	{
	  var std = UnitOfWork.StudentRepo.GetById(stdvm.Ssn);

	  std.Faculty = stdvm.Faculty;
	  std.SsnNavigation.Fname = stdvm.Fname;
	  std.SsnNavigation.Lname = stdvm.Lname;
	  std.SsnNavigation.Email = stdvm.Email;
	  std.Gpa = (decimal)stdvm.Gpa;
	  std.GradYear = stdvm.GradYear;
	  std.SsnNavigation.Bd = stdvm.Bd;
	  std.SsnNavigation.Gender = stdvm.Gender;
	  std.SsnNavigation.StreetNo = stdvm.StreetNo;
	  std.SsnNavigation.ZipCode = stdvm.ZipCode;


	  UnitOfWork.StudentRepo.Update(std);

	  UnitOfWork.Complete();

	  return true;
	}

	public async Task<List<StudentBasicInfoVM>> GetStudentsByDepartmentBranchAndActiveIntakeAsync(int deptId, int branchId)
	{
	  var branch = UnitOfWork.BranchesRepo.GetById(branchId);
	  if (branch == null)
		throw new Exception("No Branch with that Id");

	  var dept = UnitOfWork.DepartmentRepo.GetById(deptId);
	  if (dept == null)
		throw new Exception("No Department With that Id");

	  Expression<Func<Student, bool>> filter = std =>
		  std.StudentIntakeBranchDepartmentStudies.Any(rel =>
			  rel.BranchId == branchId &&
			  rel.DepartmentId == deptId &&
			  rel.Intake.IsRunning == 1 &&
			  rel.StudentSsnNavigation.SsnNavigation.IsActive == true);

	  var students = await UnitOfWork.StudentRepo.FindAllAsync(filter);

	  return _mapper.Map<List<StudentBasicInfoVM>>(students);
	}

	public List<StudentExamVM> GetStudentExams(long studentSSN, bool isPending)
	{
	  DateOnly today = DateOnly.FromDateTime(DateTime.Now);
	  Expression<Func<StudentExamModel, bool>> filters;
	  if (isPending)
		filters = std => std.StudentId == studentSSN && std.ExamModel.Pool.Configuration.Date >= today;
	  else
		filters = std => std.StudentId == studentSSN && std.ExamModel.Pool.Configuration.Date < today;
	  var exams = UnitOfWork.StudentExamModelRepo.FindAll(criteria: filters);
	  return _mapper.Map<List<StudentExamVM>>(exams);
	}

  }
}
