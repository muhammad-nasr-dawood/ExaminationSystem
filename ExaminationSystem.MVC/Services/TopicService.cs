using AutoMapper.QueryableExtensions;
using ExaminationSystem.Core;
using ExaminationSystem.Core.Consts;
using ExaminationSystem.Core.Helpers;
using ExaminationSystem.MVC.ViewModels.CourseViewModels;
using ExaminationSystem.MVC.ViewModels.DepartmentViewModels;
using ExaminationSystem.MVC.ViewModels.TopicViewModels;
using LinqKit;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace ExaminationSystem.MVC.Services
{
  public class TopicService:ITopicService
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

	public async Task<PaginatedResult<TopicDisplayViewModel>> FindAll(
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

	  var query = _unitOfWork.TopicRepo
		  .FindAllQueryable(criteria)
		  .AsNoTracking();

	  var totalFilteredItems = await query.CountAsync();

	  switch (columnOrderBy?.ToLower())
	  {
		case "name":
		  query = orderByDirection == OrderBy.Ascending
			  ? query.OrderBy(t => t.Name)
			  : query.OrderByDescending(t => t.Name);
		  break;

		default:
		  query = orderByDirection == OrderBy.Ascending
			  ? query.OrderBy(t => t.Id)
			  : query.OrderByDescending(t => t.Id);
		  break;
	  }

	  var projected = await query
		  .Select(t => new TopicDisplayViewModel
		  {
			Id = t.Id,
			Name = t.Name,
			IsDeleted = t.IsDeleted,
			NumberOfCourses = t.Crs.Count
		  })
		  .Skip((pageNumber.Value - 1) * pageSize.Value)
		  .Take(pageSize.Value)
		  .ToListAsync();

	  return new PaginatedResult<TopicDisplayViewModel>
	  {
		Items = projected,
		PageSize = pageSize.Value,
		CurrentPage = pageNumber.Value,
		TotalFilteredItems = totalFilteredItems,
		TotalItemsInTable = totalFilteredItems,
		TotalPages = (int)Math.Ceiling((double)totalFilteredItems / pageSize.Value)
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
	  var topic = _unitOfWork.TopicRepo
		  .FindAllQueryable(t => t.Id == id)
		  .AsNoTracking()
		  .FirstOrDefault();

	  if (topic == null)
		return null;

	  var vm = _mapper.Map<TopicAddEditViewModel>(topic);

	  var courses = _unitOfWork.CoursesRepo
		  .FindAllQueryable(c => !c.IsDeleted)
		  .AsNoTracking()
		  .ToList();

	  vm.AvailableCourses = _unitOfWork.CoursesRepo
	.FindAllQueryable(c => !c.IsDeleted)
	.AsNoTracking()
	.ProjectTo<CourseDisplayViewModel>(_mapper.ConfigurationProvider)
	.ToList();


	  vm.SelectedCourseIds = topic.Crs.Select(c => c.Id).ToList();

	  return vm;
	}

	public List<CourseDisplayViewModel> GetAvailableCourses()
	{
	  return _unitOfWork.CoursesRepo
		  .FindAllQueryable(c => !c.IsDeleted)
		  .AsNoTracking()
		  .ProjectTo<CourseDisplayViewModel>(_mapper.ConfigurationProvider)
		  .ToList();
	}



	public object AddTopic(TopicAddEditViewModel model)
	{
	  var topic = _mapper.Map<Topic>(model);
	  topic.IsDeleted = false;

	  var selectedCrs = _unitOfWork.CoursesRepo
		  .FindAllQueryable(c => model.SelectedCourseIds.Contains(c.Id))
		  .AsNoTracking()
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
		return new { success = false, message = "Topic not found." };

	  _mapper.Map(model, topic);

	  topic.Crs.Clear();

	  var selectedCrs = _unitOfWork.CoursesRepo
		  .FindAllQueryable(c => model.SelectedCourseIds.Contains(c.Id))
		  .AsNoTracking()
		  .ToList();

	  foreach (var course in selectedCrs)
	  {
		topic.Crs.Add(course);
	  }

	  _unitOfWork.Complete();

	  return new { success = true, message = "Topic updated successfully." };
	}

	public async Task<string?> CheckTopicNameStatusAsync(string name, int? id)
	{
	  var existing = await _unitOfWork.TopicRepo
		  .FindAllQueryable(t => t.Name == name && (!id.HasValue || t.Id != id.Value))
		  .Select(t => new { t.IsDeleted })
		  .FirstOrDefaultAsync();

	  if (existing == null)
		return null; 

	  if (!existing.IsDeleted)
		return "Topic name already exists.";

	  return "Topic name exists but is inactive. Please restore it instead.";
	}



  }
}
