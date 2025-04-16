using ExaminationSystem.MVC.ViewModels.BranchViewModels;
using ExaminationSystem.MVC.ViewModels.StudentViewModels;

namespace ExaminationSystem.MVC.Services
{
  public interface IBranchService
  {
	List<BranchViewModel> GetAll();
	List<LocationViewModel> GetLocations();
	BranchEditViewModel GetById(int id);
	void Update(BranchEditViewModel viewModel);
	void Delete(int id);
  }

  } 
