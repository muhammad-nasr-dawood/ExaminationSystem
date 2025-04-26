using ExaminationSystem.Core.Models;

namespace ExaminationSystem.MVC.ViewModels.PoolViewModels
{
  public class TeachAtVM
  {
	public Dictionary<int, Branch> Branches { get; set; } = new();

  }
  public class Branch
  {
	public Branch(int Id, string Location, string zipCode)
	{
	  this.Id = Id;
	  this.Location = Location;
	  Departments = new();
	  ZipCode = zipCode;
	}
	public int Id { get; set; }
	public string Location{ get;set; }
	public string ZipCode { get; set; }
	public Dictionary<int, Department> Departments { get; set; }

  }

  public class Department 
  {
	public int Id { get; set; }
	public string Name { get; set; }
	public Dictionary<int, Course> Courses { get; set; }

	public Department(int Id, string Name)
	{
	  this.Id = Id;
	  this.Name = Name;
	  Courses = new();
	}
  }

  public class Course 
  {
	public int Id { get; set; }
	public string Name { get; set; }

	public DateTime StartDate { get; set; }
	public DateTime? EndDate { get; set; }

	public Course(int _Id, string _Name,DateTime _StDate , DateTime? _EndDate)
	{
	  this.Id = _Id;
	  this.Name = _Name;
	  StartDate = _StDate;
	  EndDate = _EndDate;
	}



  }
}
