using AutoMapper;
using ExaminationSystem.Core.Models;
using ExaminationSystem.MVC.ViewModels.DepartmentViewModels;

namespace ExaminationSystem.MVC.MappingProfiles
{
  public class DepartmentMappingProfile : Profile
  {
	public DepartmentMappingProfile()
	{
	  CreateMap<Department, DepartmentViewModel>()
		  .ForMember(dest => dest.TotalCapacity,
			  opt => opt.MapFrom(src => src.StudentIntakeBranchDepartmentStudies.Count))
		  .ForMember(dest => dest.BranchCapacity,
			  opt => opt.Ignore()); 
	  CreateMap<Department, AddEditDeptViewModel>().ReverseMap();
	}

  }
}
