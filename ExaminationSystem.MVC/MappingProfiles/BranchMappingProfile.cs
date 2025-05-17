using AutoMapper;
using ExaminationSystem.Core.Models;
using ExaminationSystem.MVC.ViewModels.BranchViewModels;
using ExaminationSystem.MVC.ViewModels.StudentViewModels;

namespace ExaminationSystem.MVC.MappingProfiles
{
  public class BranchMappingProfile : Profile
  {
	public BranchMappingProfile()
	{
	  CreateMap<Branch, BranchViewModel>()
		  .ForMember(dest => dest.LocationName,
			  opt => opt.MapFrom(src => src.ZipCodeNavigation.Governate))
		  .ForMember(dest => dest.BranchManagerName,
			  opt => opt.MapFrom(src =>
				  src.StaffBranchManage != null &&
				  src.StaffBranchManage.StaffSsnNavigation.SsnNavigation != null
					  ? src.StaffBranchManage.StaffSsnNavigation.SsnNavigation.Fname + " " +
						src.StaffBranchManage.StaffSsnNavigation.SsnNavigation.Lname
					  : "Not Assigned"))
		  .ForMember(dest => dest.NumberOfDepartments,
			  opt => opt.MapFrom(src => src.BranchDepts.Count));

	  CreateMap<Branch, BranchEditViewModel>()
		  .ForMember(dest => dest.ZipCode, opt => opt.MapFrom(src => src.ZipCode))
		  .ForMember(dest => dest.StreetNo, opt => opt.MapFrom(src => src.StreetNo))
		  .ForMember(dest => dest.LocationName, opt => opt.MapFrom(src => src.ZipCodeNavigation.Governate))
		  .ForMember(dest => dest.BranchManagerName, opt => opt.MapFrom(src => src.StaffBranchManage.StaffSsnNavigation.SsnNavigation.Fname + " " +
			   src.StaffBranchManage.StaffSsnNavigation.SsnNavigation.Lname))
		  .ForMember(dest => dest.SelectedDepartmentIds, opt => opt.Ignore())
		  .ForMember(dest => dest.AvailableDepartments, opt => opt.Ignore())
		  .ForMember(dest => dest.NumberOfDepartments, opt => opt.MapFrom(src => src.BranchDepts.Count));



	  CreateMap<BranchEditViewModel, Branch>()
		  .ForMember(dest => dest.ZipCode, opt => opt.MapFrom(src => src.ZipCode))
		  .ForMember(dest => dest.StreetNo, opt => opt.MapFrom(src => src.StreetNo))
		  .ForMember(dest => dest.ZipCodeNavigation, opt => opt.Ignore())
		  .ForMember(dest => dest.StaffBranchManage, opt => opt.Ignore())
		  .ForMember(dest => dest.BranchDepts, opt => opt.Ignore());



	  CreateMap<Location, LocationViewModel>()
		  .ForMember(dest => dest.ZipCode,
			  opt => opt.MapFrom(src => src.ZipCode))
		  .ForMember(dest => dest.LocationName,
			  opt => opt.MapFrom(src => src.Governate));
	}
  }
}
