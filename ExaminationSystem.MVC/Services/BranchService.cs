using AutoMapper;
using ExaminationSystem.Core;
using ExaminationSystem.MVC.ViewModels.BranchViewModels;
using System;
using System.Collections.Generic;
using System.Linq;

namespace ExaminationSystem.MVC.Services
{
  public class BranchService : IBranchService
  {
	private readonly IUnitOfWork _unitOfWork;
	private readonly IMapper _mapper;

	public BranchService(IUnitOfWork unitOfWork, IMapper mapper)
	{
	  _unitOfWork = unitOfWork;
	  _mapper = mapper;
	}

	public List<BranchViewModel> GetAll()
	{
	  var branches = _unitOfWork.BranchesRepo.GetAll().ToList();
	  return _mapper.Map<List<BranchViewModel>>(branches);
	}

	
	public BranchEditViewModel GetById(int id)
	{
	  var branch = _unitOfWork.BranchesRepo.GetById(id); 
	  if (branch == null)
	  {
		return null; 
	  }

	  return _mapper.Map<BranchEditViewModel>(branch); 
	}

	public void Update(BranchEditViewModel viewModel)
	{
	  var branch = _unitOfWork.BranchesRepo.GetById(viewModel.Id); 
	  if (branch != null)
	  {
		
		_mapper.Map(viewModel, branch);
		Console.WriteLine(branch.ZipCodeNavigation.Governate);



		Console.WriteLine();




		_unitOfWork.Complete(); 
	  }
	}


	public List<LocationViewModel> GetLocations()
	{
	  
	  var locations = _unitOfWork.LocationRepo.GetAll();
	  return _mapper.Map<List<LocationViewModel>>(locations);
	}

  }
}
