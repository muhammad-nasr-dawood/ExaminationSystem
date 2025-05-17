using AutoMapper;
using AutoMapper.QueryableExtensions;
using ExaminationSystem.Core;
using ExaminationSystem.Core.Consts;
using ExaminationSystem.Core.Helpers;
using ExaminationSystem.EF;
using ExaminationSystem.MVC.ViewModels.BranchViewModels;
using ExaminationSystem.MVC.ViewModels.CourseViewModels;
using ExaminationSystem.MVC.ViewModels.DepartmentViewModels;
using ExaminationSystem.MVC.ViewModels.GenericViewModels;
using ExaminationSystem.MVC.ViewModels.TopicViewModels;
using LinqKit;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;


namespace ExaminationSystem.MVC.Services
{
  public class CourseService : ICourseService
  {
	private readonly IUnitOfWork _unitOfWork;
	private readonly IMapper _mapper;
	private readonly IDepartmentService _departmentService;

	public CourseService(IUnitOfWork unitOfWork, IMapper mapper, IDepartmentService departmentService)
	{
	  _unitOfWork = unitOfWork;
	  _mapper = mapper;
	  _departmentService = departmentService;
	}

	public async Task<PaginatedResult<CourseDisplayViewModel>> FindAll(
		int? pageNumber,
		int? pageSize,
		int? departmentIdFilter,
		bool? isDeletedFilter,
		string columnOrderBy = null,
		string orderByDirection = OrderBy.Ascending,
		string searchTerm = null)
	{
	  pageNumber ??= 1;
	  pageSize ??= 10;

	  Expression<Func<Course, bool>> criteria = c => true;

	  if (departmentIdFilter.HasValue)
		criteria = criteria.And(c => c.IntakeDeptCourses.Any(d => d.DeptId == departmentIdFilter));

	  if (isDeletedFilter.HasValue)
		criteria = criteria.And(c => c.IsDeleted == isDeletedFilter);

	  if (!string.IsNullOrWhiteSpace(searchTerm))
		criteria = criteria.And(c => c.Name.Contains(searchTerm));

	  var query = _unitOfWork.CoursesRepo
		  .FindAllQueryable(criteria)
		  .AsNoTracking();

	  var totalFilteredItems = await query.CountAsync();

	  switch (columnOrderBy?.ToLower())
	  {
		case "name":
		  query = orderByDirection == OrderBy.Ascending
			  ? query.OrderBy(c => c.Name)
			  : query.OrderByDescending(c => c.Name);
		  break;

		case "duration":
		  query = orderByDirection == OrderBy.Ascending
			  ? query.OrderBy(c => c.Duration)
			  : query.OrderByDescending(c => c.Duration);
		  break;

		default:
		  query = orderByDirection == OrderBy.Ascending
			  ? query.OrderBy(c => c.Id)
			  : query.OrderByDescending(c => c.Id);
		  break;
	  }

	  var items = await query
		  .Select(c => new CourseDisplayViewModel
		  {
			Id = c.Id,
			Name = c.Name,
			Duration = c.Duration,
			IsDeleted = c.IsDeleted,
			NumberOfTopics = c.Tops.Count
		  })
		  .Skip((pageNumber.Value - 1) * pageSize.Value)
		  .Take(pageSize.Value)
		  .ToListAsync();

	  return new PaginatedResult<CourseDisplayViewModel>
	  {
		Items = items,
		PageSize = pageSize.Value,
		CurrentPage = pageNumber.Value,
		TotalFilteredItems = totalFilteredItems,
		TotalItemsInTable = totalFilteredItems,
		TotalPages = (int)Math.Ceiling((double)totalFilteredItems / pageSize.Value)
	  };
	}






	public List<DepartmentViewModel> GetAllDepartments()
	{

	  return _departmentService.GetAll();
	}

	public bool RestoreCourse(int id)
	{
	  var course = _unitOfWork.CoursesRepo.GetById(id);
	  if (course == null) return false;

	  course.IsDeleted = false;
	  _unitOfWork.Complete();
	  return true;
	}


	public Course Delete(int id)
	{
	  var course = _unitOfWork.CoursesRepo.GetById(id);

	  course.IsDeleted = !course.IsDeleted;

	  _unitOfWork.Complete();
	  return course;
	}

	public CourseAddEditViewModel GetCourseForEdit(int id)
	{
	  var course = _unitOfWork.CoursesRepo.GetById(id);

	  var vm = _mapper.Map<CourseAddEditViewModel>(course);

	  vm.AvailableTopics = _unitOfWork.TopicRepo
		  .FindAllQueryable(t => !t.IsDeleted)
		  .AsNoTracking()
		  .ProjectTo<TopicDisplayViewModel>(_mapper.ConfigurationProvider)
		  .ToList();

	  vm.SelectedTopicIds = course.Tops.Select(t => t.Id).ToList();

	  return vm;
	}


	public List<TopicDisplayViewModel> GetAvailableTopics()
	{
	  return _unitOfWork.TopicRepo
		  .FindAllQueryable(t => !t.IsDeleted)
		  .AsNoTracking()
		  .ProjectTo<TopicDisplayViewModel>(_mapper.ConfigurationProvider)
		  .ToList();
	}


	public object AddCourse(CourseAddEditViewModel model)
	{
	  var course = _mapper.Map<Course>(model);
	  course.IsDeleted = false;

	  var selectedTopics = _unitOfWork.TopicRepo
		  .FindAllQueryable(t => model.SelectedTopicIds.Contains(t.Id))
		  .AsNoTracking()
		  .ToList(); 

	  foreach (var topic in selectedTopics)
	  {
		course.Tops.Add(topic);
	  }

	  _unitOfWork.CoursesRepo.Add(course);
	  _unitOfWork.Complete();

	  return new { success = true, message = "Course added successfully." };
	}






	public object EditCourse(CourseAddEditViewModel model)
	{
	  var course = _unitOfWork.CoursesRepo.GetById(model.Id.Value);
	  if (course == null)
		return new { success = false, message = "Course not found." };

	  _mapper.Map(model, course);

	  course.Tops.Clear();

	  var selectedTopics = _unitOfWork.TopicRepo
		  .FindAllQueryable(t => model.SelectedTopicIds.Contains(t.Id))
		  .AsNoTracking()  
		  .ToList();      

	  foreach (var topic in selectedTopics)
	  {
		course.Tops.Add(topic);
	  }

	  _unitOfWork.Complete();

	  return new { success = true, message = "Course updated successfully." };
	}



	public List<CourseDisplayViewModel> GetAll()
	{
	  var courses = _unitOfWork.CoursesRepo
		  .FindAllQueryable(c => !c.IsDeleted)
		  .AsNoTracking()
		  .ProjectTo<CourseDisplayViewModel>(_mapper.ConfigurationProvider)
		  .ToList();

	  return courses;
	}

	public List<Topic> GetTopicsByCourse(int courseId)
	{
	  return _unitOfWork.TopicRepo
		  .FindAllQueryable(t => t.Crs.Any(c => c.Id == courseId) && !t.IsDeleted)
		  .AsNoTracking()
		  .Select(t => new Topic { Id = t.Id, Name = t.Name }) 
		  .ToList();
	}

	public async Task<ValidationResultViewModel> CheckCourseNameUniquenessAsync(string name, int? id)
	{
	  var course = await _unitOfWork.CoursesRepo
		  .FindAllQueryable(c => c.Name == name && (!id.HasValue || c.Id != id.Value))
		  .Select(c => new { c.IsDeleted })
		  .FirstOrDefaultAsync();

	  if (course == null)
		return new ValidationResultViewModel { IsValid = true };

	  if (!course.IsDeleted)
		return new ValidationResultViewModel { IsValid = false, Message = "Course name already exists." };

	  return new ValidationResultViewModel
	  {
		IsValid = false,
		Message = "Course name exists but is inactive. Please restore it instead."
	  };
	}






  }
}
