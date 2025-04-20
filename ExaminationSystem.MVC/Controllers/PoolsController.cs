using ExaminationSystem.Core.Models;
using ExaminationSystem.MVC.Services;
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

	public PoolsController(IPoolService poolService)
	{
	  _poolService = poolService;
	}

	[HttpGet]
	public  async Task<IActionResult> TeachAt()
	{
	  //i need to get on the staff id from claim

	  TeachAtViewModel? TAVM =await _poolService.TeachAt(40404040404040);

	  if(TAVM == null)
		return BadRequest("No Teach At Found");

	  return View(TAVM);

	}

	[HttpGet]
	public async Task<IActionResult> Active()
	{
	  //i need to get on the staff id from claim
	  List<GenaricPoolState<ActivePoolsResult>>? GActivePoolList= await _poolService.ActivePools(40404040404040);

	  if (GActivePoolList == null)
		  return BadRequest("No Active Pools Found");

	  return View(GActivePoolList);
	}


	[HttpGet]
	public async Task<IActionResult> Processed()
	{
	  List<GenaricPoolState<ProcessedPoolsResult>>? GProcessedList = await _poolService.ProcessedPools(40404040404040);

	  if (GProcessedList == null)
		return BadRequest("No Active Pools Found");

		return View(GProcessedList);
	}




  }
}
