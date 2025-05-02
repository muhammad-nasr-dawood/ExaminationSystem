using AutoMapper;
using ExaminationSystem.Core.Models;
using ExaminationSystem.MVC.ViewModels.StudentViewModels;

namespace ExaminationSystem.MVC.MappingProfiles
{
  public class StudentMappingProfile: Profile
  {
	public StudentMappingProfile() {
	  //CreateMap<Student, StudentViewModel>();//.ReverseMap(); // student is the source Object and studentViewModel is the destination object
			//								 // if you wanna make it both sides use .ReverseMap() watch the video below to understand mapping
			//								 // https://www.youtube.com/watch?v=GESnbz9l_G4



	  // mapping for Student VM
	  CreateMap<Student, StudentVM>()
		.ForMember(dest => dest.FullName, opt => opt.MapFrom(src => $"{src.SsnNavigation.Fname} {src.SsnNavigation.Lname}"))
		.ForMember(dest => dest.ImageUrl, opt => opt.MapFrom(src => src.SsnNavigation.Image == null ? "/img/defaultImages/defaultImage.png" : src.SsnNavigation.Image.ImageUrl))
		.ForMember(dest => dest.Email, opt => opt.MapFrom(src => src.SsnNavigation.Email))
		.ForMember(dest => dest.Gender, opt => opt.MapFrom(src => src.SsnNavigation.Gender == "F" ? "Female" : "Male"))
		.ForMember(dest => dest.IsActive, opt => opt.MapFrom(src => src.SsnNavigation.IsActive)).ReverseMap();

	  // mapping for Student Details VM

	  CreateMap<Student, StudentDetailsVM>()
	   .ForMember(dest => dest.Fname, opt => opt.MapFrom(src => src.SsnNavigation.Fname))
	   .ForMember(dest => dest.Ssn, opt => opt.MapFrom(src => src.SsnNavigation.Ssn))
	   .ForMember(dest => dest.StreetNo, opt => opt.MapFrom(src => src.SsnNavigation.StreetNo))
	   .ForMember(dest => dest.Location, opt => opt.MapFrom(src => src.SsnNavigation.ZipCodeNavigation.Governate))
	   .ForMember(dest => dest.ZipCode, opt => opt.MapFrom(src => src.SsnNavigation.ZipCode))
	   .ForMember(dest => dest.Lname, opt => opt.MapFrom(src => src.SsnNavigation.Lname))
	   .ForMember(dest => dest.Email, opt => opt.MapFrom(src => src.SsnNavigation.Email))
	   .ForMember(dest => dest.Bd, opt => opt.MapFrom(src => src.SsnNavigation.Bd))
	   .ForMember(dest => dest.Gender, opt => opt.MapFrom(src => src.SsnNavigation.Gender))
	   .ForMember(dest => dest.PhoneNumber, opt => opt.MapFrom(src => src.SsnNavigation.PhoneNumber))
	   .ForMember(dest => dest.ImageURL, opt => opt.MapFrom(src => src.SsnNavigation.Image == null ? "/img/defaultImages/defaultImage.png" : src.SsnNavigation.Image.ImageUrl))
	   .ForMember(dest => dest.IsActive, opt => opt.MapFrom(src => src.SsnNavigation.IsActive))
	   .ForMember(dest => dest.StudentDepartment, opt => opt.MapFrom(src =>
		src.StudentIntakeBranchDepartmentStudies
	  	 .Select(x => $"{x.Department.Name} | {x.Branch.ZipCodeNavigation.Governate}")
	  	 .ToList())).ReverseMap();




	  CreateMap<StudentAddVM, User>().AfterMap((src, dest) =>
	  {
		dest.IsActive = true;
		dest.UserType = "Student";
	  });


	  CreateMap<StudentAddVM, Student>().ReverseMap(); 



	}



  }
}
