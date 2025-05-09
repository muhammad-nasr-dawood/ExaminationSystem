using AutoMapper;
using ExaminationSystem.Core.Models;
using ExaminationSystem.MVC.ViewModels.CourseViewModels;

namespace ExaminationSystem.MVC.MappingProfiles
{
  public class CourseMappingProfile : Profile
  {
	public CourseMappingProfile()
	{
	  CreateMap<Course, CourseDisplayViewModel>()
		  .ForMember(dest => dest.NumberOfTopics,
			  opt => opt.MapFrom(src => src.Tops.Count));

	 
	  CreateMap<Course, CourseAddEditViewModel>()
		  .ForMember(dest => dest.SelectedTopicIds,
			  opt => opt.MapFrom(src => src.Tops.Select(t => t.Id)))
		  .ForMember(dest => dest.AvailableTopics, opt => opt.Ignore());

	 
	  CreateMap<CourseAddEditViewModel, Course>()
		  .ForMember(dest => dest.Tops, opt => opt.Ignore()); 
	}


  }
}
