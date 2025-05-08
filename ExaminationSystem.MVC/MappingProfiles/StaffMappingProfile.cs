using AutoMapper;
using ExaminationSystem.Core.Models;
using ExaminationSystem.MVC.Services;
using ExaminationSystem.MVC.ViewModels.BranchViewModels;
using ExaminationSystem.MVC.ViewModels.StaffViewModels;
using NuGet.Versioning;

namespace ExaminationSystem.MVC.MappingProfiles;

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

	  /* for staff details model*/
	  CreateMap<Staff, StaffDisplayDetailViewModel>().AfterMap((src, des) =>
	  {
		des.Fname = src.SsnNavigation.Fname;
		des.Lname = src.SsnNavigation.Lname;
		des.Location = src.SsnNavigation.ZipCodeNavigation.Governate;
		des.StreetNo = src.SsnNavigation.StreetNo;
		des.Email = src.SsnNavigation.Email;
		des.PhoneNumber = src.SsnNavigation.PhoneNumber;
		des.Bd = src.SsnNavigation.Bd;
		des.Gender = src.SsnNavigation.Gender;
		des.IsActive = src.SsnNavigation.IsActive;
		des.ZipCode = src.SsnNavigation.ZipCode;
		var tempWorksFor = src.StaffBranchIntakeWorksFors.FirstOrDefault( w => w.StaffSsn == src.Ssn);
		if (tempWorksFor is not null)
		{
		  des.BranchId = tempWorksFor.BranchId;
		}
		if (src.SsnNavigation.Image != null)
		  des.ImageURL = $"{src.SsnNavigation.Image.ImageUrl}";
		else des.ImageURL = "/img/defaultImages/defaultImage.png";
		if (src.Roles != null)
		{
		  foreach (var role in src.Roles)
		  {
			des.Roles.Add(role.Name);
		  }

		}
	  });
	  // Prevent overwrite;  for IsActive
	  CreateMap<StaffDisplayDetailViewModel, User>().ForMember(dest => dest.IsActive, opt => opt.Ignore()).ForAllMembers(opts => opts.Condition((src, dest, srcMember) => srcMember != null)); 

	  CreateMap<StaffDisplayDetailViewModel, Staff>().ForAllMembers(opts => opts.Condition((src, dest, srcMember) => srcMember != null)); // This ensures that null values from the DTO won’t overwrite existing values in the entity.

	  CreateMap<Branch, BranchDisplayViewModel>().AfterMap((src, des) =>
	  {
		if(src.ZipCodeNavigation is not null)
		  des.Location = src.ZipCodeNavigation.Governate;
	  });
	}

}
