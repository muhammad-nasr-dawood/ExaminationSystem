namespace ExaminationSystem.MVC.ViewModels.PoolViewModels
{
  public class PaginatedArchivedPoolsVM
  {
	public List<ArchivedPoolsVM> Pools { get; set; } = new List<ArchivedPoolsVM>();
	public int Total { get; set; }
	//page - limit 
  }
}
