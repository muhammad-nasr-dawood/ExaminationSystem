using AutoMapper;
using ExaminationSystem.Core.Models;
using ExaminationSystem.MVC.ViewModels.StudentViewModels;

namespace ExaminationSystem.MVC.MappingProfiles
{
  public class StudentMappingProfile: Profile
  {
	public StudentMappingProfile() {
	  CreateMap<Student, StudentViewModel>();//.ReverseMap(); // student is the source Object and studentViewModel is the destination object
	  // if you wanna make it both sides use .ReverseMap() watch the video below to understand mapping
	  // https://www.youtube.com/watch?v=GESnbz9l_G4 
	}
  }
}
