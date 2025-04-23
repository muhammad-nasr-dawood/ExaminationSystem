using AutoMapper;
using Newtonsoft.Json;
using ExaminationSystem.Core;
using ExaminationSystem.Core.Models;
using ExaminationSystem.MVC.ViewModels.PoolViewModels;
 
namespace ExaminationSystem.MVC.Services
{
  public class PoolService : IPoolService
  {
	public IUnitOfWork UnitOfWork { get; set; }
	public IMapper Map { get  ; set ; }

	public PoolService(IUnitOfWork unitOfWork, IMapper map)
	{
	  UnitOfWork = unitOfWork;
	  Map = map;
	}

	public async Task<TeachAtViewModel?> TeachAt(long staffId)
	{
		List<TeachAtResult> TAL=await UnitOfWork.PoolRepo.TeachAt(staffId);

	  if (TAL == null)
		  return null;

	  return Map.Map<TeachAtViewModel>(TAL);
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

	  if(processedPoolsList == null)
		return null;

	  return Map.Map<List<GenaricPoolState<ProcessedPoolsResult>>>(processedPoolsList);
	}



	public async Task<PaginatedArchivedPoolsViewModel> ArchivedPools(int CourseId , int page , int limit,int order)
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

		// Map the result to the desired view model  
		PaginatedArchivedPoolsViewModel? result =
		  JsonConvert.DeserializeObject<PaginatedArchivedPoolsViewModel>(jsonList[0].JSON_F52E2B6118A111d1B10500805F49916B);

		if(result == null)
		  throw new Exception("Mapping failed");

		return result;
	  }
	  catch (Exception ex) {
		throw new Exception(ex.Message);
	  }
	   
	   

	}

  }
  
}
