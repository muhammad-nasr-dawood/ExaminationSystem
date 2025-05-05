using AutoMapper;
using ExaminationSystem.Core;
using ExaminationSystem.Core.Models;
using ExaminationSystem.EF;
using ExaminationSystem.MVC.ViewModels.PoolViewModels;
using System.Data;

namespace ExaminationSystem.MVC.IService
{
  public interface IPoolService
  {
	public IUnitOfWork UnitOfWork { get; set; }
	public IMapper Map { get; set; }

	public Task<TeachAtVM?> TeachAt(long staffId);

	public Task<List<GenaricPoolState<ActivePoolsResult>>?> ActivePools(long staffId);

	public Task<List<GenaricPoolState<ProcessedPoolsResult>>?> ProcessedPools(long staffId);

	public Task<PaginatedArchivedPoolsVM> ArchivedPools(int c, int p, int l, int o);

	public Task<PaginatedPoolQsVM> PoolQuestions(int PoolId, int Page, int Limit, byte QType, int OType);

	public Task<CreatePoolResult?> CreatePool(long staffId, int courseId, int deptId, int branchId);

	public Task<int> UsePool(long staffId, int srcPoolId, int destPoolId);

	public Task<int> RemoveQuestionFromPool(long staffId, int poolId, int[] _);


	public Task<int> AddQuestionsToPool(long staffId, int poolId, int[] _);

	public Task<int> SetConfigurations(long staffId, int poolId, int noOfDiff, int noOfMed, int noOfEasy, int gradeForDiff, int gradeForMid, int gradeForEasy, int noOfModels, int [] excludedStdIds);


   public Task<int> UpdateConfigurationGrades(long staffId, int poolId, int gradeForDiff, int gradeForMed, int gradeForEasy);


	public Task<int> UpdateConfigurations(long staffId, int poolId, int noOfDiff, int noOfMed, int noOfEasy, int gradeForDiff, int gradeForMed, int gradeForEasy, int noOfModels);

	public Task<int> UpdateConfigurationStudentList(int poolId, long staffId, int[] excludedStds);



  }
}
