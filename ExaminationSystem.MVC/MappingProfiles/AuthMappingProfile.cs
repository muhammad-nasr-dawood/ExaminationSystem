using ExaminationSystem.MVC.ViewModels.AuthViewModels;
using ExaminationSystem.Core.Models;
using AutoMapper;

namespace ExaminationSystem.MVC.MappingProfiles;

public class AuthMappingProfile: Profile
{
	public AuthMappingProfile()
	{
	  CreateMap<User, UserLoginViewModel>().AfterMap((src, des) =>
	  {
		des.ImageURL = src.Image?.ImageUrl;
		des.Location = src.ZipCodeNavigation.Governate;
		if(src.Staff is null)
		{
		  des.StudentFaculty = src.Student.Faculty;
		}
		else
		{
		  foreach(var role in src.Staff.Roles)
		  {
			des.StaffRoles.Add(role.Name);
		  }
		}
		// to be continued
	  });
	}
}
