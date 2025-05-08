namespace ExaminationSystem.MVC.IService
{
  public interface IImageKit
  {

	public Task<List<KeyValuePair<string, string>>> UploadImage(List<IFormFile>files, string folderName);
	public Task<bool[]>  DeleteImage(string[] Id);
	
  }
}
