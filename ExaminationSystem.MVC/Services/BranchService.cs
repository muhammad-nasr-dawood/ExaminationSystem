using AutoMapper;
using ExaminationSystem.Core;
using ExaminationSystem.Core.Models;
using ExaminationSystem.MVC.ViewModels.BranchViewModels;
using ExaminationSystem.MVC.ViewModels.StaffViewModels;
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

	
	public BranchEditViewModel GetBranchForEdit(int id)
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
		_unitOfWork.Complete(); 
	  }
	}


	public List<LocationViewModel> GetLocations()
	{
	  
	  var locations = _unitOfWork.LocationRepo.GetAll();
	  return _mapper.Map<List<LocationViewModel>>(locations);
	}
	public void Delete(int id)
	{
	  var branch = _unitOfWork.BranchesRepo.GetById(id);

	  if (branch == null)
	  {
		throw new KeyNotFoundException("Branch not found.");
	  }
	  else
	  {
		branch.IsDeleted = true;
		_unitOfWork.Complete();

	  }

	}


	public async Task<BranchManagerViewModel> GetUnassignedStaffAsync(int branchId)
	{
	 
	  var unassignedStaff = await _unitOfWork.BranchesRepo.GetUnassignedStaffAsync(branchId);

	
	  var assignedStaff = await _unitOfWork.StaffBranchManageRepo
		  .FindAsync(sbm => sbm.BranchId == branchId); 

	  var model = new BranchManagerViewModel
	  {
		UnassignedStaff = _mapper.Map<IEnumerable<StaffGeneralDisplayVM>>(unassignedStaff),
		AssignedStaffSsn = assignedStaff?.StaffSsn 
	  };

	  return model;
	}




	public async Task<bool> AddBranchManager(int branchId, long staffSsn)
	{

	  var rec = await _unitOfWork.StaffBranchManageRepo.AddBranchManager(branchId, staffSsn);

	  _unitOfWork.Complete();

	  return rec != null;

	}
	}
}
