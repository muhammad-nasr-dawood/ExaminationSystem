using ExaminationSystem.Core;
using ExaminationSystem.MVC.IService;

namespace ExaminationSystem.MVC.Services
{
  public class TopicService:ITopicService
  {
	public IUnitOfWork UnitOfWork { get; set; }

	public TopicService(IUnitOfWork unitOfWork)
	{
	  UnitOfWork = unitOfWork;
	}

	public async Task<List<GetTopicsByCourseResult>> GetTopicsByCourse(int courseId, int? pageSize, int? pageNumber)
	{
	  try
	  {

		var res = await UnitOfWork.TopicsRepo.GetTopicsByCourse(courseId, pageSize, pageNumber);

		return res;
	  }
	  catch (Exception ex)
	  {
		// Log the exception (not implemented here)
		throw new(ex.Message);
	  }
	}



  }
}
