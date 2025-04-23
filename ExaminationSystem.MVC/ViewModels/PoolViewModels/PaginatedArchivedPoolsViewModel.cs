namespace ExaminationSystem.MVC.ViewModels.PoolViewModels
{
  public class PaginatedArchivedPoolsViewModel
  {
	public List<ArchivedPoolsViewModel> Pools { get; set; } = new List<ArchivedPoolsViewModel>();
	public int Total { get; set; }

  }
}
