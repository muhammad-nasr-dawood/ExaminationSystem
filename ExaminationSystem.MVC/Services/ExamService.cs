using ExaminationSystem.Core;
using ExaminationSystem.MVC.IService;

namespace ExaminationSystem.MVC.Services
{
  public class ExamService : IExamService
  {
	public IUnitOfWork UnitOfWork { get; set; }



	public ExamService(IUnitOfWork unitOfWork, IMapper map)
	{
	  UnitOfWork = unitOfWork;
	}


	public async Task<int> SetExamSession(long staffId, int poolId, DateOnly date, TimeOnly startingTime, TimeOnly endingTime, int duration)
	{
	  try
	  {
	    OutputParameter<int> returnValue = new OutputParameter<int>();

		var res= await UnitOfWork.ExamRepo.SetExamSession(staffId, poolId, date, startingTime, endingTime, duration, returnValue);

		return returnValue.Value;

	  }
	  catch (Exception ex)
	  {
		// Log the exception (not implemented here)
		throw new(ex.Message);
	  }
	}



  }
}
