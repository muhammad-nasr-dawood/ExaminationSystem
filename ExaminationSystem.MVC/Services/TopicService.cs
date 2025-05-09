using ExaminationSystem.Core;
using ExaminationSystem.Core.Consts;
using ExaminationSystem.Core.Helpers;
using ExaminationSystem.MVC.ViewModels.CourseViewModels;
using ExaminationSystem.MVC.ViewModels.DepartmentViewModels;
using ExaminationSystem.MVC.ViewModels.TopicViewModels;
using LinqKit;
using Microsoft.AspNetCore.Mvc;

namespace ExaminationSystem.MVC.Services
{
  public class TopicService : ITopicService
  {

	private readonly IUnitOfWork _unitOfWork;
	private readonly IMapper _mapper;
	private readonly ICourseService _courseService;

	public TopicService(IUnitOfWork unitOfWork, IMapper mapper, ICourseService courseService)
	{
	  _unitOfWork = unitOfWork;
	  _mapper = mapper;
	  _courseService = courseService;
	}

	public PaginatedResult<TopicDisplayViewModel> FindAll(
	int? pageNumber,
	int? pageSize,
	int? courseIdFilter,
	bool? isDeletedFilter,
	string columnOrderBy = null,
	string orderByDirection = OrderBy.Ascending,
	string searchTerm = null)
	{
	  pageNumber ??= 1;
	  pageSize ??= 10;

	  Expression<Func<Topic, bool>> criteria = t => true;

	  if (courseIdFilter.HasValue)
		criteria = criteria.And(t => t.Crs.Any(c => c.Id == courseIdFilter.Value));

	  if (isDeletedFilter.HasValue)
		criteria = criteria.And(t => t.IsDeleted == isDeletedFilter);

	  if (!string.IsNullOrWhiteSpace(searchTerm))
		criteria = criteria.And(t => t.Name.Contains(searchTerm));

	  Expression<Func<Topic, object>> orderBy = t => t.Id;

	  if (!string.IsNullOrEmpty(columnOrderBy))
	  {
		switch (columnOrderBy.ToLower())
		{
		  case "name":
			orderBy = t => t.Name;
			break;
		}
	  }

	  var result = _unitOfWork.TopicRepo.FindAll(
		  take: pageSize,
		  skip: (pageNumber.Value - 1) * pageSize,
		  criteria: criteria,
		  orderBy: orderBy,
		  orderByDirection: orderByDirection
	  );

	  var mapped = _mapper.Map<List<TopicDisplayViewModel>>(result.Items);

	  foreach (var vm in mapped)
	  {
		var entity = result.Items.First(x => x.Id == vm.Id);
		vm.NumberOfCourses = entity.Crs.Count;
	  }

	  return new PaginatedResult<TopicDisplayViewModel>
	  {
		Items = mapped,
		PageSize = result.PageSize,
		CurrentPage = result.CurrentPage,
		TotalFilteredItems = result.TotalFilteredItems,
		TotalItemsInTable = result.TotalItemsInTable,
		TotalPages = result.TotalPages
	  };
	}
	public List<CourseDisplayViewModel> GetAllCourses()
	{

	  return _courseService.GetAll();
	}
	public bool RestoreTopic(int id)
	{
	  var topic = _unitOfWork.TopicRepo.GetById(id);
	  if (topic == null) return false;

	  topic.IsDeleted = false;
	  _unitOfWork.Complete();
	  return true;
	}
	public Topic Delete(int id)
	{
	  var topic = _unitOfWork.TopicRepo.GetById(id);

	  topic.IsDeleted = !topic.IsDeleted;

	  _unitOfWork.Complete();
	  return topic;
	}

	public TopicAddEditViewModel GetTopicForEdit(int id)
	{
	  var topic = _unitOfWork.TopicRepo.GetById(id);

	  var vm = _mapper.Map<TopicAddEditViewModel>(topic);

	  vm.AvailableCourses = _mapper.Map<List<CourseDisplayViewModel>>(
		  _unitOfWork.CoursesRepo.FindAll(c => !c.IsDeleted)
	  );

	  vm.SelectedCourseIds = topic.Crs.Select(c => c.Id).ToList();

	  return vm;
	}
	public List<CourseDisplayViewModel> GetAvailableCourses()
	{
	  var courses = _unitOfWork.CoursesRepo.FindAll(c => !c.IsDeleted).ToList();
	  return _mapper.Map<List<CourseDisplayViewModel>>(courses);
	}

	public object AddTopic(TopicAddEditViewModel model)
	{
	  var topic = _mapper.Map<Topic>(model);
	  topic.IsDeleted = false;


	  var selectedCrs = _unitOfWork.CoursesRepo
		  .FindAll(c => model.SelectedCourseIds.Contains(c.Id))
		  .ToList();


	  foreach (var course in selectedCrs)
	  {
		topic.Crs.Add(course);
	  }

	  _unitOfWork.TopicRepo.Add(topic);
	  _unitOfWork.Complete();

	  return new { success = true, message = "Topic added successfully." };
	}

	public object EditTopic(TopicAddEditViewModel model)
	{
	  var topic = _unitOfWork.TopicRepo.GetById(model.Id.Value);
	  if (topic == null)
		return new JsonResult(new { success = false, message = "Topic not found." });

	  _mapper.Map(model, topic);

	  topic.Crs.Clear();

	  var selectedCrs = _unitOfWork.CoursesRepo
		  .FindAll(c => model.SelectedCourseIds.Contains(c.Id))
		  .ToList();

	  foreach (var course in selectedCrs)
	  {
		topic.Crs.Add(course);
	  }

	  _unitOfWork.Complete();

	  return new { success = true, message = "Topic updated successfully." };
	}


	public async Task<List<Topic>> GetTopicsByCourse(int courseId)
	{
	  return _unitOfWork.TopicRepo
		  .FindAll(t => t.Crs.Any(c => c.Id == courseId) && !t.IsDeleted)
		  .Select(t => new Topic { Id = t.Id, Name = t.Name })
		  .ToList();
	}


  }
}
