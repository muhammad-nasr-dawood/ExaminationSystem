using ExaminationSystem.Core.Models;
using ExaminationSystem.MVC.IService;
using ExaminationSystem.MVC.ViewModels.PoolViewModels;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Razor.Language.CodeGeneration;
using Microsoft.CSharp.RuntimeBinder;
using Microsoft.EntityFrameworkCore.Metadata.Internal;

namespace ExaminationSystem.MVC.Controllers
{
  public class PoolsController : Controller
  {

	IPoolService _poolService;
	ITopicService TopicService;

	public PoolsController(IPoolService poolService, ITopicService topicService)
	{
	  _poolService = poolService;
	  TopicService = topicService;
	}

	[HttpGet]
	public async Task<IActionResult> TeachAt()
	{
	  //i need to get on the staff id from claim

	  TeachAtVM? TAVM = await _poolService.TeachAt(40404040404040);

	  if (TAVM == null)
		return BadRequest("No Teach At Found");

	  return View(TAVM);

	}

	[HttpGet]
	public async Task<IActionResult> Active()
	{
	  //i need to get on the staff id from claim
	   GenaricPoolState<ActivePoolsResult>? GActivePoolList = await _poolService.ActivePools(40404040404040);

	  if (GActivePoolList == null)
		return BadRequest("No Active Pools Found");

	  return View(GActivePoolList);
	}

	[HttpGet]
	public async Task<IActionResult> Processed()
	{
	  //claim 
	  List<GenaricPoolState<ProcessedPoolsResult>>? GProcessedList = await _poolService.ProcessedPools(40404040404040);

	  if (GProcessedList == null)
		return BadRequest("No Active Pools Found");

	  return View(GProcessedList);
	}


	[HttpGet]
	public async Task<IActionResult> Archived(int CourseId, int page, int limit, int order)
	{
	  try
	  {
		PaginatedArchivedPoolsVM paginatedArchivedPoolsViewModel = await _poolService.ArchivedPools(CourseId, page, limit, order);
		if (paginatedArchivedPoolsViewModel == null)
		  return BadRequest("No Archived Pools Found");
		return View(paginatedArchivedPoolsViewModel);
	  }
	  catch (Exception ex)
	  {
		return BadRequest(ex.Message);
	  }

	}

	[HttpGet]
	public async Task<IActionResult> PoolQuestions(int PoolId, int CourseId, int Page, int Limit, byte QType, int OType)
	{
	  try
	  {
		PaginatedPoolQsVM poolQuestions = await _poolService.PoolQuestions(PoolId, Page, Limit, QType, OType);

		ViewBag.PoolId = PoolId;
		ViewBag.QType = QType;
		ViewBag.OType = OType;

		TempData["CurrentPoolId"] = PoolId;
		TempData["CurrentCourseId"] = CourseId;

		return View(poolQuestions);
	  }
	  catch (Exception ex)
	  {
		return BadRequest(ex.Message);
	  }
	}

	[HttpPut]
	public async Task<IActionResult> CreatePool(long staffId, int courseId, int deptId, int branchId)
	{
	  try
	  {
		CreatePoolResult? result = await _poolService.CreatePool(staffId, courseId, deptId, branchId);

		if (result == null)
		  return BadRequest("No Pool Created");

		return View(result);
	  }
	  catch (Exception ex)
	  {
		return BadRequest(ex.Message);
	  }


	}//end of calss

	[HttpPost]
	public async Task<IActionResult> UsePool(long staffId, int srcPoolId, int destPoolId)
	{
	  try
	  {
		int result = await _poolService.UsePool(staffId, srcPoolId, destPoolId);

		if (result == 0)
		  return View(result);

		if (result == -1)
		  return BadRequest("System/unknown error occurred");

		else if (result == 1)
		  return BadRequest("invalid src pool Id");

		else if (result == 2)
		  return BadRequest("you cannot modify this pool");
		else // 3 
		  return BadRequest("invalid des pool Id");


	  }
	  catch (Exception ex)
	  {
		return BadRequest(ex.Message);
	  }

	}

	[HttpPost]
	public async Task<IActionResult> RemoveQuestionFromPool(long staffId, int poolId, int[] QIDS)
	{
	  try
	  {
		if (QIDS == null || QIDS.Length == 0)
		  return BadRequest("No Questions Found");

		int result = await _poolService.RemoveQuestionFromPool(staffId, poolId, QIDS);

		if (result == 0)
		  return View(result);
		if (result == -1)
		  return BadRequest("System/unknown error occurred");
		else if (result == 1)
		  return BadRequest("Invalid params");
		else if (result == 2)
		  return BadRequest("One or more questions do not belong to your pool");
		else // 3 
		  return BadRequest("Cannot modify questions in this pool - used in active exams");
	  }
	  catch (Exception ex)
	  {
		return BadRequest(ex.Message);
	  }
	}

	[HttpPost]
	public async Task<IActionResult> AddQuestionsToPool(long staffId, int poolId, int[] QIDS)
	{
	  try
	  {
		if (QIDS == null || QIDS.Length == 0)
		  return BadRequest("No Questions Found");

		int result = await _poolService.AddQuestionsToPool(staffId, poolId, QIDS);

		if (result == 0)
		  return View(result);

		if (result == -1)
		  return BadRequest("System/unknown error occurred");
		else //if (result == 1)
		  return BadRequest("You cannot modify questions in this pool because it is used in active exams. ");

	  }
	  catch (Exception ex)
	  {
		return BadRequest(ex.Message);
	  }

	}

	[HttpPost]
	public async Task<IActionResult> SetConfigurations(long staffId, int poolId, int noOfDiff, int noOfMed, int noOfEasy, int gradeForDiff, int gradeForMid, int gradeForEasy, int noOfModels, int[] excludedStdIds)
	{
	  try
	  {
		if (excludedStdIds == null || excludedStdIds.Length == 0)
		  return BadRequest("No Questions Found");
		if (noOfDiff < 0 || noOfMed < 0 || noOfEasy < 0 || gradeForDiff < 0 || gradeForMid < 0 || gradeForEasy < 0 || noOfModels < 0)
		  return BadRequest("invalid params");

		int result = await _poolService.SetConfigurations(staffId, poolId, noOfDiff, noOfMed, noOfEasy, gradeForDiff, gradeForMid, gradeForEasy, noOfModels, excludedStdIds);

		if (result == 0)
		  return View(result);
		if (result == -1)
		  return BadRequest("System/unknown error occurred");
		else if (result == 1)
		  return BadRequest("students do not belong to that branch and dept");
		else if (result == 2)
		  return BadRequest("this pool has been configured before this time.");
		else //3 
		  return BadRequest("this pool dose not exist");


	  }
	  catch (Exception ex)
	  {
		return BadRequest(ex.Message);
	  }

	}


	[HttpPost]
	public async Task<IActionResult> UpdateConfigurationGrades(long staffId, int poolId, int gradeForDiff, int gradeForMed, int gradeForEasy)
	{
	  try
	  {
		if (gradeForDiff < 0 || gradeForMed < 0 || gradeForEasy < 0)
		  return BadRequest("invalid params");

		int result = await _poolService.UpdateConfigurationGrades(staffId, poolId, gradeForDiff, gradeForMed, gradeForEasy);

		if (result == 0)
		  return View(result);
		if (result == -1)
		  return BadRequest("System/unknown error occurred");
		else if (result == 1)
		  return BadRequest("this pool dose not exist");
		else //2 
		  return BadRequest("this pool has not been configured before this time.");
	  }
	  catch (Exception ex)
	  {
		return BadRequest(ex.Message);

	  }
	}


	[HttpPost]
	public async Task<IActionResult> UpdateConfigurations(long staffId, int poolId, int noOfDiff, int noOfMed, int noOfEasy, int gradeForDiff, int gradeForMed, int gradeForEasy, int noOfModels)
	{
	  try
	  {
		if (noOfDiff < 0 || noOfMed < 0 || noOfEasy < 0 || gradeForDiff < 0 || gradeForMed < 0 || gradeForEasy < 0 || noOfModels < 0)
		  return BadRequest("invalid params");
		int result = await _poolService.UpdateConfigurations(staffId, poolId, noOfDiff, noOfMed, noOfEasy, gradeForDiff, gradeForMed, gradeForEasy, noOfModels);
		if (result == 0)
		  return View(result);
		if (result == -1)
		  return BadRequest("System/unknown error occurred");
		else if (result == 1)
		  return BadRequest("this pool dose not exist");
		else if (result == 2)
		  return BadRequest("this pool has not been configured before this time.");
		else //3 
		  return BadRequest("you can not modify in this pool configuration.");
	  }
	  catch (Exception ex)
	  {
		return BadRequest(ex.Message);
	  }
	}


	[HttpPost]
	public async Task<IActionResult> UpdateConfigurationStudentList(int poolId, long staffId, int[] excludedStdIds)
	{
	  try
	  {
		if (excludedStdIds == null || excludedStdIds.Length == 0)
		  return BadRequest("No Questions Found");
		int result = await _poolService.UpdateConfigurationStudentList(poolId, staffId, excludedStdIds);
		if (result == 0)
		  return View(result);
		if (result == -1)
		  return BadRequest("System/unknown error occurred");
		else if (result == 1)
		  return BadRequest("this pool dose not exist");
		else if (result == 2)
		  return BadRequest("this pool has not been configured before this time.");
		else if (result == 3)
		  return BadRequest("you can not modify in this pool configuration.");
		else //4
		  return BadRequest("invalid student ids");

	  }
	  catch (Exception ex)
	  {
		return BadRequest(ex.Message);
	  }
	}

	[HttpGet]
	public async Task<IActionResult> Bank(int? CourseId)
	{
	    var poolId = TempData.Peek("CurrentPoolId");
	    
	    // Use passed CourseId or fallback to TempData
	    var courseId = CourseId ?? TempData.Peek("CurrentCourseId") as int?;
	
	    if (poolId == null || courseId == null)
	    {
	        return RedirectToAction("Active");
	    }
	
	    // Store CourseId in TempData for next use
	    TempData["CurrentCourseId"] = courseId;
	
	    // Get topics for the course
	    var topics = await TopicService.GetTopicsByCourse((int)courseId, null, null);
	
	    ViewBag.PoolId = poolId;
	    ViewBag.CourseId = courseId;
	
	    return View(topics);
	}
  }
}//end of namespace
