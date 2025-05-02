using ExaminationSystem.MVC.ViewModels.Questions;

namespace ExaminationSystem.MVC.IService
{
  public interface IQuestionService
  {
	public Task<PaginatedQuestionsVM> GetByTopic(int topicId, int order, byte type, byte level, int page, int limit);

	public Task<int> AddTFQueston(AddTFQuestionVM _);


	public Task<int> AddMCQQuestion(AddMCQQuestionVM _);


  }
}
