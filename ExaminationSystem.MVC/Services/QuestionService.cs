using ExaminationSystem.Core;
using ExaminationSystem.Core.Models;
using ExaminationSystem.MVC.Views.Questions;
using Newtonsoft.Json;

namespace ExaminationSystem.MVC.Services
{
  public class QuestionService: IQuestionService
  {
	IUnitOfWork UnitOfWork { get; set; }

	public QuestionService(IUnitOfWork unitOfWork)
	{
	  UnitOfWork = unitOfWork;
	}
	public async Task<PaginatedQuestionsViewModel> GetByTopic(int topicId, int order, byte type, byte level, int page, int limit)
	{

	  try
	  {
		  if(page<0 || limit < 0 || order<-1 || order>1  || level <0 || level >2 ||  topicId < 1 || type < 0 || type >1)
			throw new ArgumentOutOfRangeException();

		  List<GetQuestionsResult> result = await UnitOfWork.QuestionRepo.GetByTopic(topicId, order, type, level, page, limit);

		  if (result == null || result.Count == 0  || result[0].JSON_F52E2B6118A111d1B10500805F49916B == null)
			throw new Exception("No data found");

		  // decerialized the result to PaginatedQuestionsViewModel

		 PaginatedQuestionsViewModel?  DeserializedResult = JsonConvert.DeserializeObject<PaginatedQuestionsViewModel>(result[0].JSON_F52E2B6118A111d1B10500805F49916B);

		if(DeserializedResult  == null)
			throw new Exception("some thing went very wrong during DeSerialization Process");

		DeserializedResult.page = page;
		DeserializedResult.limit = limit;

		return DeserializedResult;

	  }
	  catch (Exception ex)
	  {
		throw new Exception(ex.Message);
	  }

	  

	}

  }
}
