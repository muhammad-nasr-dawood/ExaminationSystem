using AutoMapper;
using ExaminationSystem.Core;
using ExaminationSystem.Core.Models;
using ExaminationSystem.EF;
using ExaminationSystem.MVC.ViewModels.PoolViewModels;

namespace ExaminationSystem.MVC.Services
{
  public interface IPoolService
  {
	public IUnitOfWork UnitOfWork { get; set; }
	public IMapper Map { get; set; }

	public Task<TeachAtViewModel?> TeachAt(long staffId);

	public Task< List< GenaricPoolState<ActivePoolsResult> >?> ActivePools(long staffId);

	public Task<List<GenaricPoolState<ProcessedPoolsResult>>?> ProcessedPools(long staffId);

	public Task<PaginatedArchivedPoolsViewModel> ArchivedPools(int c, int p, int l, int o);

  }
}
