using AutoMapper;
using ExaminationSystem.Core.Models;
using ExaminationSystem.MVC.Services;
using ExaminationSystem.MVC.ViewModels.StaffViewModels;

namespace ExaminationSystem.MVC.MappingProfiles
{
  public class StaffMappingProfile: Profile
  {
	private readonly IPasswordService _passwordService;
	public StaffMappingProfile()
	{
	  CreateMap<Staff, StaffGeneralDisplayVM>().AfterMap((src, des) =>
	  {
		des.Ssn = src.Ssn;
		des.FullName = $"{src.SsnNavigation.Fname} {src.SsnNavigation.Lname}";
		if (src.SsnNavigation.Image != null)
		  des.ImageURL = $"{src.SsnNavigation.Image.ImageUrl}";
		else des.ImageURL = "/img/defaultImages/defaultImage.png";
		  des.Salary = src.Salary;
		des.IsActive = src.SsnNavigation.IsActive;
	  });

	  CreateMap<StaffAddViewModel, User>().AfterMap((src, des) =>
	  {
		des.IsActive = true;
		des.UserType = "Staff";
		
	  });

	  CreateMap<StaffAddViewModel, Staff>().AfterMap((src, des) =>
	  {
		des.Salary = 60000;
	  });
	}
  }
}
