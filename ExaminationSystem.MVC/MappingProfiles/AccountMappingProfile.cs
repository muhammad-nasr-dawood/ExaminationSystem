using ExaminationSystem.MVC.ViewModels.AccountViewModels;

namespace ExaminationSystem.MVC.MappingProfiles;

public class AccountMappingProfile: Profile
{
  public AccountMappingProfile()
  {
	CreateMap<User, AccountEditViewModel>().AfterMap((src, des) => {
	  if (src.Image is not null)
		des.ImageURL = $"{src.Image.ImageUrl}";
	  else des.ImageURL = "/img/defaultImages/defaultImage.png";
	  des.City = src.ZipCodeNavigation.Governate;
	  if (src.Staff is null)
	  {
		des.StudentFaculty = src.Student?.Faculty;
		des.Gpa = src.Student?.Gpa;
		des.GradYear = src.Student?.GradYear;
		des.UserRole = "Student";
	  }
	  else
	  {
		var minId = int.MaxValue;
		var HighestRole = "Instructor";
		foreach (var item in src.Staff.Roles)
		{
		  if(item.Id < minId)
		  {
			minId = item.Id;
			HighestRole = item.Name;
		  }
		}

		des.UserRole = HighestRole.Replace('_', ' ');

	  }
	});


	CreateMap<AccountEditViewModel, User>().ForMember(dest => dest.IsActive, opt => opt.Ignore()).ForAllMembers(opts => opts.Condition((src, dest, srcMember) => srcMember != null)); // This ensures that null values from the DTO won’t overwrite existing values in the entity.

	CreateMap<AccountEditViewModel, Student>();
  }
}
