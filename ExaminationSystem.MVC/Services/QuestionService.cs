using ExaminationSystem.Core;
using ExaminationSystem.Core.Models;
using ExaminationSystem.MVC.IService;
using VM=ExaminationSystem.MVC.ViewModels.Questions;
using Newtonsoft.Json;
using SQLitePCL;
using System.Data;
using ExaminationSystem.MVC.ViewModels.Questions;

namespace ExaminationSystem.MVC.Services
{
  public class QuestionService : IQuestionService
  {
	IUnitOfWork UnitOfWork { get; set; }

	public QuestionService(IUnitOfWork unitOfWork)
	{
	  UnitOfWork = unitOfWork;
	}
	public async Task<VM.PaginatedQuestionsVM> GetByTopic(int topicId, int order, byte type, byte level, int page, int limit)
	{

	  try
	  {
		if (page < 0 || limit < 0 || order < -1 || order > 1 || level < 0 || level > 2 || topicId < 1 || type < 0 || type > 1)
		  throw new ArgumentOutOfRangeException();

		List<GetQuestionsResult> result = await UnitOfWork.QuestionRepo.GetByTopic(topicId, order, type, level, page, limit);

		if (result == null || result.Count == 0 || result[0].JSON_F52E2B6118A111d1B10500805F49916B == null)
		  throw new Exception("No data found");

		// decerialized the result to PaginatedQuestionsViewModel

		PaginatedQuestionsVM? DeserializedResult = JsonConvert.DeserializeObject<PaginatedQuestionsVM>(result[0].JSON_F52E2B6118A111d1B10500805F49916B);

		if (DeserializedResult == null)
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



	private DataTable ConvertImagesToDataTable(List<VM.Image> images)
	{
	  var table = new DataTable();
	  table.Columns.Add("Id", typeof(int));
	  table.Columns.Add("Url", typeof(string));

	  foreach (var img in images)
	  {
		table.Rows.Add(img.Id, img.Url);
	  }

	  return table;
	}

	private DataTable ConvertAnswersToDataTable(List<string> Answers)
	{
	  var table = new DataTable();
	  table.Columns.Add("Id", typeof(int));
	  table.Columns.Add("Url", typeof(string));

	  foreach (var img in images)
	  {
		table.Rows.Add(img.Id, img.Url);
	  }

	  return table;
	}


	public async Task<int> AddTFQueston(AddTFQuestionVM question)
	{

	  try
	  {

		if (question == null)
		  throw new ArgumentNullException("question");

		List<VM.Image>? images = null;

		if (question.Images != null)
		{

		  // upload the images to image kit
		  //handl image kit logic

		}

		DataTable? dataTable = null;

		if (images != null)
		{
		  dataTable = ConvertImagesToDataTable(images);
		}


		int result = await UnitOfWork.QuestionRepo.AddTFQuestion(question.StaffId, question.Level, question.Content,
					 question.TopicId, question.IsTrue, dataTable);

		return result;

	  }
	  catch (Exception ex)
	  {
		throw new Exception(ex.Message);
	  }


	}


	public async Task<int> AddMCQQuestion(AddMCQQuestionVM question)
	{
	  try
	  {
		if (question == null)
		  throw new ArgumentNullException("question");

		List<VM.Image>? images = null;
		if (question.Images != null)
		{
		  // upload the images to image kit
		  //handl image kit logic
		}

		DataTable? iamgesDataTable = null;
		if (images != null)
		{
		  iamgesDataTable = ConvertImagesToDataTable(images);
		}



		int result = await UnitOfWork.QuestionRepo.AddMCQQuestion(question.StaffId, question.Level, question.Content,
					 question.TopicId, question.AnswerIndex, dataTable, question.Answers);

		return result;
	  }
	  catch (Exception ex)
	  {
		throw new Exception(ex.Message);
	  }

	}



  }
}
