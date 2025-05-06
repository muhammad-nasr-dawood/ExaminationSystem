using ExaminationSystem.Core;
 using ExaminationSystem.MVC.IService;
using VM=ExaminationSystem.MVC.ViewModels.Questions;
using Newtonsoft.Json;
 using System.Data;
using ExaminationSystem.MVC.ViewModels.Questions;
using System.Text;
using ExaminationSystem.Core.Models;

namespace ExaminationSystem.MVC.Services
{
  public class QuestionService : IQuestionService
  {
	IUnitOfWork UnitOfWork { get; set; }
	IImageKit _imageKit { get; set; }

	public QuestionService(IUnitOfWork unitOfWork,IImageKit imageKit)
	{
	  UnitOfWork = unitOfWork;
	  _imageKit = imageKit;
	}
	public async Task<VM.PaginatedQuestionsVM> GetByTopic(int topicId, int order, byte type, byte level, int page, int limit)
	{
	  try
	  {
		if (page < 0 || limit < 0 || order < -1 || order > 1 || level < 0 || level > 2 || topicId < 1 || type < 0 || type > 1)
		  throw new ArgumentOutOfRangeException();

		List<GetQuestionsResult> result = await UnitOfWork.QuestionRepo.GetByTopic(topicId, order, type, level, page, limit);


		if (result == null || result.Count == 0 || result[0].JSON_F52E2B6118A111d1B10500805F49916B == null)
		{
		  return new PaginatedQuestionsVM() { Total = 0, Questions = null, page = page, limit = limit };
		}


		StringBuilder allQuestions = new StringBuilder(500);

		foreach (var poolQuestion in result)
		{
		  allQuestions.Append(poolQuestion.JSON_F52E2B6118A111d1B10500805F49916B);
		}

		// decerialized the result to PaginatedQuestionsViewModel

		PaginatedQuestionsVM? DeserializedResult = JsonConvert.DeserializeObject<PaginatedQuestionsVM>(allQuestions.ToString());


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
	  table.Columns.Add("Value", typeof(int));

	  foreach (var ans in Answers)
	  {
		table.Rows.Add(ans);
	  }

	  return table;
	}


	public async Task<int> AddTFQueston(AddTFQuestionVM question)
	{

	  try
	  {

		if (question == null)
		  throw new ArgumentNullException("question");

		List<VM.Image>? imagesInfo = null;

		if (question.Images != null)
		{
		  string  folderName= "TFQuestions";
		  List<KeyValuePair<string,string>> KitInfo= await _imageKit.UploadImage(question.Images, folderName);

		  

		  if (imagesInfo != null && imagesInfo.Count != 0)
		  {
			imagesInfo = new();
			foreach (var info in KitInfo)
			{
			  imagesInfo.Add(new VM.Image() { Id=info.Key,  Url=info.Value});
			}
		  }


		  // upload the images to image kit
		  //handl image kit logic

		}

		DataTable? dataTable = null;

		if (imagesInfo != null)
		{
		  dataTable = ConvertImagesToDataTable(imagesInfo);
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

		List<VM.Image>? imagesInfo = null;

		if (question.Images != null)
		{
		  string folderName = "MCQuestions";
		  List<KeyValuePair<string, string>> KitInfo = await _imageKit.UploadImage(question.Images, folderName);



		  if (imagesInfo != null && imagesInfo.Count != 0)
		  {
			imagesInfo = new();
			foreach (var info in KitInfo)
			{
			  imagesInfo.Add(new VM.Image() { Id = info.Key, Url = info.Value });
			}
		  }


		  // upload the images to image kit
		  //handl image kit logic

		}

		DataTable? iamgesDataTable = null;
		if (imagesInfo != null)
		{
		  iamgesDataTable = ConvertImagesToDataTable(imagesInfo);
		}

		DataTable answersDataTable = ConvertAnswersToDataTable(question.Answers);


		int result = await UnitOfWork.QuestionRepo.AddMCQuestion(question.StaffId, question.Level, question.Content,
					 question.TopicId, question.AnswerIndex, iamgesDataTable, answersDataTable);

		return result;
	  }
	  catch (Exception ex)
	  {
		throw new Exception(ex.Message);
	  }

	}


 


  }
}
