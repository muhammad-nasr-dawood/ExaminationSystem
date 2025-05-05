namespace ExaminationSystem.MVC.IService
{
  public interface IExamService
  {
	public Task<int> SetExamSession(long staffId, int poolId, DateOnly date, TimeOnly startingTime, TimeOnly endingTime, int duration);

  }

}
