using ExaminationSystem.Core;
using ExaminationSystem.Core.Models;
using ExaminationSystem.MVC.ViewModels.AccountViewModels;
using Microsoft.DiaSymReader;

namespace ExaminationSystem.MVC.Services;

public class AccountService: IAccountService
{

  public IUnitOfWork UnitOfWork {  get; }
  private readonly IMapper _mapper;
  private readonly IImageService _imageService;
  public AccountService(IUnitOfWork unitOfWork, IMapper mapper, IImageService imageService	)
  {
	UnitOfWork = unitOfWork;
	_mapper = mapper;
	_imageService = imageService;
  }

  public AccountEditViewModel GetAccount(long id)
  {
	var account = UnitOfWork.UserRepo.GetById(id);

	var mappedAccount = _mapper.Map<AccountEditViewModel>(account);

	return mappedAccount;
  }

  public async Task<string> UpdateImage(IFormFile file, long userId)
  {
	var (id, url) = await _imageService.UploadImageAsync(file);
	var user = UnitOfWork.UserRepo.GetById(userId);
	ProfileImage currentImage;
	if(user.ImageId is not null)
	{
	  currentImage = UnitOfWork.ProfileImageRepo.GetById(user.ImageId);
	  var tempImageId = user.ImageId;
	  UnitOfWork.ProfileImageRepo.Delete(currentImage);
	  await _imageService.DeleteImageAsync(tempImageId); 
	}

	  currentImage = new ProfileImage { ImageId = id, ImageUrl = url };
	  UnitOfWork.ProfileImageRepo.Add(currentImage);
	  user.ImageId = id;

	  UnitOfWork.Complete();
	return currentImage.ImageUrl;
  }

  public async Task<string> DeleteImage(long userId)
  {
	var user = UnitOfWork.UserRepo.GetById(userId);
	ProfileImage currentImage;
	if (user.ImageId is not null)
	{
	  currentImage = UnitOfWork.ProfileImageRepo.GetById(user.ImageId);
	  var tempImageId = user.ImageId;
	  UnitOfWork.ProfileImageRepo.Delete(currentImage);
	  await _imageService.DeleteImageAsync(tempImageId);
	  UnitOfWork.Complete();
	}
	return "/img/defaultImages/defaultImage.png";
  }

  public async Task<bool> UpdateAccount(long userId, List<string> roles, AccountEditViewModel model)
  {
	var user = await UnitOfWork.UserRepo.GetByIdAsync(userId);
	bool isStudent = roles.Any(role => role == "Student");
	_mapper.Map(model, user);
	if (isStudent)
	{
	  var student = await UnitOfWork.StudentRepo.GetByIdAsync(userId);
	  _mapper.Map(model, student);
	}

	UnitOfWork.Complete();

	return true;

  }
}
