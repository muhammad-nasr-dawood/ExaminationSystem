using ExaminationSystem.MVC.Views.Questions;

namespace ExaminationSystem.MVC.Services
{
  public interface IQuestionService
  {
	public Task<PaginatedQuestionsViewModel> GetByTopic(int topicId, int order, byte type, byte level, int page, int limit);
  }
}
