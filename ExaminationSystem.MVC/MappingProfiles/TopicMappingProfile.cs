using ExaminationSystem.MVC.ViewModels.TopicViewModels;

namespace ExaminationSystem.MVC.MappingProfiles
{
  public class TopicMappingProfile:Profile
  {
	public TopicMappingProfile()
	{
	  
	  CreateMap<Topic, TopicDisplayViewModel>()
		  .ForMember(dest => dest.NumberOfCourses,
					 opt => opt.MapFrom(src => src.Crs.Count))
		  .ForMember(dest => dest.IsDeleted,
					 opt => opt.MapFrom(src => src.IsDeleted));

	  
	  CreateMap<Topic, TopicAddEditViewModel>()
		  .ForMember(dest => dest.SelectedCourseIds,
					 opt => opt.MapFrom(src => src.Crs.Select(c => c.Id)));

	 
	  CreateMap<TopicAddEditViewModel, Topic>()
		  .ForMember(dest => dest.Crs, opt => opt.Ignore()); 
	}
  }
}
