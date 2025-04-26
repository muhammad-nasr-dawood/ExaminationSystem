namespace ExaminationSystem.MVC.Services
{
  public interface IImageService
  {
	Task<(string fileId, string url)> UploadImageAsync(IFormFile file, string folder= "/ExaminationImagesFolder");

	Task<bool> DeleteImageAsync(string fileId);
  }
}
