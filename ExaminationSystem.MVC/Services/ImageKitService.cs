using Imagekit;
using Imagekit.Models;
using Imagekit.Sdk;
using Microsoft.AspNetCore.Http;
using System.IO;
using System.Threading.Tasks;

namespace ExaminationSystem.MVC.Services;


public class ImageKitService : IImageService
{
    private readonly ImagekitClient _client;

    public ImageKitService()
    {
        _client = new ImagekitClient(
            publicKey: "public_htDZr6lVSyVC4al4m3BxfvhTu4g=",
            privateKey: "private_8QfxhC8wwInvxq7rFu0ZGV5+OwE=",
            urlEndPoint: "https://ik.imagekit.io/nl6nawbsq/"
        );
    }

  public async Task<(string fileId, string url)> UploadImageAsync(IFormFile file, string folder = "/ExaminationImagesFolder")
  {
	if (file == null || file.Length == 0)
	{
	  throw new ArgumentException("No file provided.");
	}

	if (file.Length < 1024)
	{
	  throw new ArgumentException("File is too small to be a valid image.");
	}

	// convert IFormFile to byte array first
	using var memoryStream = new MemoryStream();
	await file.CopyToAsync(memoryStream);
	byte[] fileBytes = memoryStream.ToArray();


	// Upload to ImageKit using byte array
	var uploadFileRequest = new FileCreateRequest
	{
	  file = fileBytes, // Use byte array instead of stream
	  fileName = Path.GetFileName(file.FileName),
	  useUniqueFileName = true,
	  folder = folder,
	};

	var result = await _client.UploadAsync(uploadFileRequest);

	if (result.HttpStatusCode >= 200 && result.HttpStatusCode < 300)
	{
	  return (result.fileId, result.url);
	}

	throw new Exception($"Image upload failed: {result.Raw}");
  }

  public async Task<bool> DeleteImageAsync(string fileId)
	  {
		var result = await _client.DeleteFileAsync(fileId);

		return result.HttpStatusCode >= 200 && result.HttpStatusCode < 300; // success
	  }

}

