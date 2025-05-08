using AutoMapper;
using ExaminationSystem.Core.Models;
using ExaminationSystem.MVC.ViewModels.PoolViewModels;
using Branch = ExaminationSystem.MVC.ViewModels.PoolViewModels.Branch;
using Course = ExaminationSystem.MVC.ViewModels.PoolViewModels.Course;
using Department = ExaminationSystem.MVC.ViewModels.PoolViewModels.Department;


namespace ExaminationSystem.MVC.MappingProfiles
{
  public class TeachAtMappingProfile:Profile
  {
	  public TeachAtMappingProfile() {

	  CreateMap<List<TeachAtResult>, TeachAtVM>().AfterMap((src, des)=>{

		foreach (var item in src)
		{
		  if (! des.Branches.ContainsKey(item.BranchId))
		  {

			des.Branches.Add(item.BranchId, new Branch(Id: item.BranchId, Location: item.LGovernate,zipCode:item.LZipCode));

			des.Branches[item.BranchId].Departments.Add(item.DeptId, new Department(Id: item.DeptId, Name: item.DeptName));

			des.Branches[item.BranchId].Departments[item.DeptId].Courses.Add(item.CourseId,
			  new Course(_Id: item.CourseId, _Name: item.CourseName,_StDate:item.StartingDate , _EndDate:item.EndingDate));


		  }
		  else if (!des.Branches[item.BranchId].Departments.ContainsKey(item.DeptId))
		  {


			des.Branches[item.BranchId].Departments.Add(item.DeptId,
			  new Department(Id: item.DeptId, Name: item.DeptName));

			des.Branches[item.BranchId].Departments[item.DeptId].Courses.Add(item.CourseId,
			  new Course(_Id: item.CourseId, _Name: item.CourseName , _StDate: item.StartingDate, _EndDate: item.EndingDate));


		  }
		  else //if (!des.Branches[item.BranchId].Departments[item.DeptId].Courses.ContainsKey(item.CourseId))
		  {

			des.Branches[item.BranchId].Departments[item.DeptId].Courses.Add(item.CourseId,
			  new Course(_Id: item.CourseId, _Name: item.CourseName ,_StDate:item.StartingDate , _EndDate:item.EndingDate));

		  }

		}//end of for each


	  });


	}
  }
}
