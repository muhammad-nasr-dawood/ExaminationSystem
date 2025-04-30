using AutoMapper;
using ExaminationSystem.Core;
using ExaminationSystem.Core.Consts;
using ExaminationSystem.Core.Helpers;
using ExaminationSystem.EF;
using ExaminationSystem.MVC.ViewModels.BranchViewModels;
using ExaminationSystem.MVC.ViewModels.CourseViewModels;
using ExaminationSystem.MVC.ViewModels.DepartmentViewModels;
using ExaminationSystem.MVC.ViewModels.TopicViewModels;
using LinqKit;
using Microsoft.AspNetCore.Mvc;

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

	public PaginatedResult<CourseDisplayViewModel> FindAll(
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

	  Expression<Func<Course, object>> orderBy = c => c.Id;

	  if (!string.IsNullOrEmpty(columnOrderBy))
	  {
		switch (columnOrderBy.ToLower())
		{
		  case "name":
			orderBy = c => c.Name;
			break;
		  case "duration":
			orderBy = c => c.Duration;
			break;
		}
	  }

	  var result = _unitOfWork.CoursesRepo.FindAll(
		  take: pageSize,
		  skip: (pageNumber.Value - 1) * pageSize,
		  criteria: criteria,
		  orderBy: orderBy,
		  orderByDirection: orderByDirection

	  );

	  var mapped = _mapper.Map<List<CourseDisplayViewModel>>(result.Items);
	  foreach (var vm in mapped)
	  {
		var entity = result.Items.First(x => x.Id == vm.Id);
		vm.NumberOfTopics = entity.Tops.Count;
	  }

	  return new PaginatedResult<CourseDisplayViewModel>
	  {
		Items = mapped,
		PageSize = result.PageSize,
		CurrentPage = result.CurrentPage,
		TotalFilteredItems = result.TotalFilteredItems,
		TotalItemsInTable = result.TotalItemsInTable,
		TotalPages = result.TotalPages
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

	  vm.AvailableTopics = _mapper.Map<List<TopicDisplayViewModel>>(
		  _unitOfWork.TopicRepo.FindAll(t => !t.IsDeleted)
	  );

	  vm.SelectedTopicIds = course.Tops.Select(t => t.Id).ToList();

	  return vm;
	}


	public List<TopicDisplayViewModel> GetAvailableTopics()
	{
	  var topics = _unitOfWork.TopicRepo.FindAll(t => !t.IsDeleted).ToList();
	  return _mapper.Map<List<TopicDisplayViewModel>>(topics);
	}

	public object AddCourse(CourseAddEditViewModel model)
	{
	  var course = _mapper.Map<Course>(model);
	  course.IsDeleted = false;

	  
	  var selectedTopics = _unitOfWork.TopicRepo
		  .FindAll(t => model.SelectedTopicIds.Contains(t.Id))
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
		return new JsonResult(new { success = false, message = "Course not found." });

	  _mapper.Map(model, course);

	  course.Tops.Clear();

	  var selectedTopics = _unitOfWork.TopicRepo
		  .FindAll(t => model.SelectedTopicIds.Contains(t.Id))
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

	  var courses = _unitOfWork.CourseRepo.FindAll(c => !c.IsDeleted).ToList();
	  return _mapper.Map<List<CourseDisplayViewModel>>(courses);
	}


  }
}
