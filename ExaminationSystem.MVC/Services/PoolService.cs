using AutoMapper;
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


  }
  
}
