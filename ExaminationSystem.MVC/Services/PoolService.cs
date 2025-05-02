using AutoMapper;
using Newtonsoft.Json;
using ExaminationSystem.Core;
using ExaminationSystem.Core.Models;
using ExaminationSystem.MVC.ViewModels.PoolViewModels;
using Microsoft.CodeAnalysis.CSharp.Syntax;
using System.Text;
using ExaminationSystem.MVC.IService;

namespace ExaminationSystem.MVC.Services
{
  public class PoolService : IPoolService
  {
	public IUnitOfWork UnitOfWork { get; set; }
	public IMapper Map { get; set; }

	public PoolService(IUnitOfWork unitOfWork, IMapper map)
	{
	  UnitOfWork = unitOfWork;
	  Map = map;
	}

	public async Task<TeachAtVM?> TeachAt(long staffId)
	{
	  List<TeachAtResult> TAL = await UnitOfWork.PoolRepo.TeachAt(staffId);

	  if (TAL == null)
		return null;

	  return Map.Map<TeachAtVM>(TAL);
	}

	public async Task<List<GenaricPoolState<ActivePoolsResult>>?> ActivePools(long staffId)
	{
	  List<ActivePoolsResult> activePools = await UnitOfWork.PoolRepo.ActivePools(staffId);

	  if (activePools == null)
		return null;

	  return Map.Map<List<GenaricPoolState<ActivePoolsResult>>>(activePools);
	}

	public async Task<List<GenaricPoolState<ProcessedPoolsResult>>?> ProcessedPools(long staffId)
	{
	  List<ProcessedPoolsResult> processedPoolsList = await UnitOfWork.PoolRepo.ProcessedPools(staffId);

	  if (processedPoolsList == null)
		return null;

	  return Map.Map<List<GenaricPoolState<ProcessedPoolsResult>>>(processedPoolsList);
	}


	public async Task<PaginatedArchivedPoolsVM> ArchivedPools(int CourseId, int page, int limit, int order)
	{
	  //validate params

	  try
	  {
		if (!(page > 0 && limit > 0 && (order == -1 || order == 1)))
		  throw new Exception("invalid params");
		// Fetch json file
		List<GetArchivedPoolsResult> jsonList = await UnitOfWork.PoolRepo.ArchivedPools(CourseId, page, limit, order);

		if (jsonList == null || jsonList.Count == 0)
		  throw new Exception("No data found");


		StringBuilder AllPools = new StringBuilder(500);

		foreach (var pool in jsonList)
		{
		  AllPools.Append(pool);
		}
		// Map the result to the desired view model  
		PaginatedArchivedPoolsVM? result =
		  JsonConvert.DeserializeObject<PaginatedArchivedPoolsVM>(AllPools.ToString());

		if (result == null)
		  throw new Exception("Mapping failed");

		return result;
	  }
	  catch (JsonException jsonEx)
	  {
		// Log JSON-specific errors
		throw new Exception($"JSON Error: {jsonEx.Message}");
	  }
	  catch (Exception ex)
	  {
		throw new Exception(ex.Message);
	  }



	}


	public async Task<PaginatedPoolQsVM> PoolQuestions(int PoolId, int Page, int Limit, byte QType, byte OType)
	{
    try
    {


		if (PoolId < 1 || Page < 1 || Limit < 1 || QType < 0 || QType > 1 || OType < 0 || QType > 1)
		  throw new Exception("invalid params");

        // Fetch data from the repository
        List<GetPoolQuestionsResult> poolQuestions = await UnitOfWork.PoolRepo.PoolQuestions(PoolId,Page,Limit,QType,OType);

        if (poolQuestions == null || poolQuestions.Count == 0)
            throw new Exception("Data not found");

		StringBuilder allQuestions = new StringBuilder(500);

		foreach (var poolQuestion in poolQuestions)
		{
			allQuestions.Append( poolQuestion.JSON_F52E2B6118A111d1B10500805F49916B);
		}

        // Deserialize JSON
        PaginatedPoolQsVM? result = JsonConvert.DeserializeObject<PaginatedPoolQsVM>(allQuestions.ToString());

        if (result == null)
            throw new Exception("Mapping failed");

		result.Page = Page;
		result.Limit = Limit;

		return result;
    }
    catch (JsonException jsonEx)
    {
        // Log JSON-specific errors
        throw new Exception($"JSON Error: {jsonEx.Message}");
    }
    catch (Exception ex)
    {
        // Log general errors
        throw new Exception(ex.Message);
    }
}


  }

 
}
