using ExaminationSystem.Core.Consts;
using ExaminationSystem.Core.Helpers;
using ExaminationSystem.MVC.ViewModels.CourseViewModels;
using ExaminationSystem.MVC.ViewModels.DepartmentViewModels;
using ExaminationSystem.MVC.ViewModels.GenericViewModels;
using ExaminationSystem.MVC.ViewModels.TopicViewModels;
using Microsoft.AspNetCore.Mvc;

namespace ExaminationSystem.MVC.Services
{
  public interface ICourseService
  {
	Task<PaginatedResult<CourseDisplayViewModel>> FindAll(
		int? pageNumber,
		int? pageSize,
		int? departmentIdFilter,
		bool? isDeletedFilter,
		string columnOrderBy = null,
		string orderByDirection = OrderBy.Ascending,
		string searchTerm = null);
	List<DepartmentViewModel> GetAllDepartments();
	bool RestoreCourse(int id);
	public Course Delete(int id);
	public CourseAddEditViewModel GetCourseForEdit(int id);
	public List<TopicDisplayViewModel> GetAvailableTopics();
	public object AddCourse(CourseAddEditViewModel model);
	public object EditCourse(CourseAddEditViewModel model);
	public List<CourseDisplayViewModel> GetAll();
	public List<Topic> GetTopicsByCourse(int courseId);
	Task<ValidationResultViewModel> CheckCourseNameUniquenessAsync(string name, int? id);

  }
}
