namespace ExaminationSystem.MVC.MappingProfiles;

public class TeachCourseMappingProfile: Profile
{
	public TeachCourseMappingProfile()
	{
	  CreateMap<StaffBranchIntakeDepartmentCourseTeach, TeachingDisplayViewModel>().AfterMap((src, des) =>
	  {
		des.Branch = src.Branch.ZipCodeNavigation.Governate;
		des.Department = src.Department.Name;
		des.Course = src.Course.Name;
		des.IsInstructorCurrentBranch = src.StaffSsnNavigation.StaffBranchIntakeWorksFors.Any( w => w.BranchId == src.BranchId);
		des.StartingDate = src.StartingDate.ToShortDateString() ;
		des.EndingDate = (src.EndingDate != null ? src.EndingDate?.ToShortDateString() : "Not Finished Yet");
	  });
	}
}
