namespace ExaminationSystem.MVC.IService
{
  public interface ITopicService
  {
	public Task<List<GetTopicsByCourseResult>> GetTopicsByCourse(int courseId, int? pageSize, int? pageNumber);


  }
}
