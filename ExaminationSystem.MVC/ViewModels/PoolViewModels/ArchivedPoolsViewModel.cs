namespace ExaminationSystem.MVC.ViewModels.PoolViewModels
{
  public class ArchivedPoolsViewModel
  {
	public int Id{ get; set; }
	public int IsActive { get; set; }
	public long StaffId { get; set; }
	public int DeptId { get; set; }
	public int BranchId { get; set; }
	public int CourseId { get; set; }
	public int NoOfMedium { get; set; }
	public int NoOfEasy { get; set; }
	public int NoOfDifficult { get; set; }
	public int ZipCode { get; set; }
	public string? Governate { get; set; }
	public string?  Title { get; set; }
	public string? DName { get; set; }
	public string? FName { get; set; }
	public string? Lname { get; set; }
	public string? Email { get; set; }

  }

}
