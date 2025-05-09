using ExaminationSystem.Core.Consts;
using ExaminationSystem.Core.Helpers;
using ExaminationSystem.MVC.ViewModels.CourseViewModels;
using ExaminationSystem.MVC.ViewModels.TopicViewModels;

namespace ExaminationSystem.MVC.Services
{
  public interface ITopicService
  {
	public PaginatedResult<TopicDisplayViewModel> FindAll(
	int? pageNumber,
	int? pageSize,
	int? courseIdFilter,
	bool? isDeletedFilter,
	string columnOrderBy = null,
	string orderByDirection = OrderBy.Ascending,
	string searchTerm = null);
	public List<CourseDisplayViewModel> GetAllCourses();
	public bool RestoreTopic(int id);
	public Topic Delete(int id);
	public TopicAddEditViewModel GetTopicForEdit(int id);
	public List<CourseDisplayViewModel> GetAvailableCourses();
	public object AddTopic(TopicAddEditViewModel model);
	public object EditTopic(TopicAddEditViewModel model);

  }
}
