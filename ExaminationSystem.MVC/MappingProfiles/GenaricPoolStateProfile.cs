using AutoMapper;
using ExaminationSystem.Core.Models;
using ExaminationSystem.MVC.ViewModels.PoolViewModels;

namespace ExaminationSystem.MVC.MappingProfiles
{
  public class GenericPoolStateProfile<T> : Profile where T :  class
  {
	public GenericPoolStateProfile()
	{
	  CreateMap< List<T>, GenaricPoolState<T> >().AfterMap((src, dest) =>
	  {

		//dynamic dynamicSrc = src;


		foreach(dynamic dynamicSrc in src)
		{
		  // Get values
		  int branchId = dynamicSrc.PBranchId;
		  int deptId = dynamicSrc.PDeptId;
		  int courseId = dynamicSrc.PCourseId;
		  string location = dynamicSrc.LGovernate;
		  string deptName = dynamicSrc.DName;
		  string courseName = dynamicSrc.CName;

		  if (!dest.Branches.ContainsKey(branchId))
		  {
			// Create new branch
			var branch = new Branch<T>(branchId, location);
			branch.Objects.Add(dynamicSrc);

			// Create new department
			var department = new Department<T>(deptId, deptName);
			department.Objects.Add(dynamicSrc);

			// Create new course
			var course = new Course<T>(courseId, courseName);
			course.Objects.Add(dynamicSrc);

			// Build hierarchy
			department.Courses.Add(courseId, course);
			branch.Departments.Add(deptId, department);
			dest.Branches.Add(branchId, branch);
		  }
		  else if (!dest.Branches[branchId].Departments.ContainsKey(deptId))
		  {
			// Branch exists but department doesn't
			var department = new Department<T>(deptId, deptName);
			department.Objects.Add(dynamicSrc);

			var course = new Course<T>(courseId, courseName);
			course.Objects.Add(dynamicSrc);

			department.Courses.Add(courseId, course);
			dest.Branches[branchId].Departments.Add(deptId, department);
			dest.Branches[branchId].Objects.Add(dynamicSrc);
		  }
		  else
		  {
			// Both branch and department exist
			var department = dest.Branches[branchId].Departments[deptId];


			var course = new Course<T>(courseId, courseName);
			course.Objects.Add(dynamicSrc);

			department.Courses.Add(courseId, course);
			department.Objects.Add(dynamicSrc);

			dest.Branches[branchId].Objects.Add(dynamicSrc);

		  }
		 
		 
		} 

	  });
	}
  }

 
}
