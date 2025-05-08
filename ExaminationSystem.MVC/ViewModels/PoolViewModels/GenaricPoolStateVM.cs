using Microsoft.CodeAnalysis.CSharp.Syntax;

namespace ExaminationSystem.MVC.ViewModels.PoolViewModels
{
  public class GenaricPoolState<T> where T : class
  {
	public Dictionary<int, Branch<T>> Branches { get; set; } = new Dictionary<int, Branch<T>>();

  }

  public class Branch<T> where T : class
  {
	public Branch(int Id, string Location)
	{
	  this.Id = Id;
	  this.Location = Location;
	   
	}
	public int Id { get; set; }
	public string Location { get; set; }
	public Dictionary<int, Department<T>> Departments { get; set; } = new();

	public List<T> Objects { get; set; } = new();

  }


  public class Department<T> where T : class
  {
	public int Id { get; set; }
	public string Name { get; set; }
	public Dictionary<int, Course<T>> Courses { get; set; } = new();
	public List<T> Objects { get; set; } = new();

	public Department(int Id, string Name)
	{
	  this.Id = Id;
	  this.Name = Name;
	}

	//may be it's pool or [Configuration+Pool]


  }


  public class Course<T> where T : class
  {
	public int Id { get; set; }
	public string Name { get; set; }

	public List<T> Objects { get; set; } = new();

	public Course(int _Id, string _Name)
	{
	  this.Id = _Id;
	  this.Name = _Name;
	}

   
  }


}


 
