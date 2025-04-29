using Imagekit;
using Imagekit.Models;
using Imagekit.Models.Response;

using ExaminationSystem.Core.Helpers;
using ExaminationSystem.MVC.IService;

using Imagekit.Helper;

using Imagekit.Sdk;

using Microsoft.Extensions.Options;
using System.Collections.Concurrent;



namespace ExaminationSystem.MVC.Services
{
  public class ImageKit : IImageKit
  {
	private readonly ImageKitSettings _config;

	public ImageKit(IOptions<ImageKitSettings> config)
	{
	  _config = config.Value; // Access the bound configuration
	}

	public async Task<bool[]> DeleteImage(string[] fileIds)
	{

	  if (fileIds == null || fileIds.Length == 0)
	  {
		throw new ArgumentNullException(nameof(fileIds), "File IDs cannot be null or empty");
	  }

	  bool[] deletionResults = new bool[fileIds.Length];

	  // Process deletions in parallel for better performance
	  var deletionTasks = fileIds.Select(async (fileId, index) =>
	  {
		// Validate fileId
		if (string.IsNullOrWhiteSpace(fileId))
		{
		  deletionResults[index] = false;
		  return; // Skip empty or null file IDs and return from it 
		}

		// Create NEW ImagekitClient for each thread
		var threadSafeImageKit = new ImagekitClient(
			publicKey: _config.PublicKey,
			privateKey: _config.PrivateKey,
			urlEndPoint: _config.UrlEndpoint
		);

		try
		{
		  await threadSafeImageKit.DeleteFileAsync(fileId);
		  deletionResults[index] = true;
		  Console.WriteLine($"Successfully deleted file: {fileId}");
		}
		catch (ImagekitException ex)
		{
		  deletionResults[index] = false;
		  Console.WriteLine($"Failed to delete file {fileId}: {ex.Message}");
		}
		catch (Exception ex)
		{
		  deletionResults[index] = false;
		  Console.WriteLine($"Unexpected error deleting file {fileId}: {ex.Message}");
		}
	  });

	  await Task.WhenAll(deletionTasks); // Wait for all deletions to complete
	  //defer execution 
	  return deletionResults;
	}
	 



	public async Task<List<KeyValuePair<string, string>>> UploadImage(List<IFormFile> files, string folderName)
	{

	   
	 

	  // Thread-safe collection for results
	  var uploadedFiles = new ConcurrentBag<KeyValuePair<string, string>>();

	  // Configure parallel uploads
	  var options = new ParallelOptions
	  {
		MaxDegreeOfParallelism = 4 // Optimal for most APIs
	  };

	  await Parallel.ForEachAsync(files, options, async (file, ct) =>
	  {
		try
		{
		  // Create NEW ImagekitClient for each thread
		  var threadSafeImageKit = new ImagekitClient(
			  publicKey: _config.PublicKey,
			  privateKey: _config.PrivateKey,
			  urlEndPoint: _config.UrlEndpoint
		  );

		  using (var memoryStream = new MemoryStream())
		  {
			await file.CopyToAsync(memoryStream, ct);
			memoryStream.Position = 0;

			// Extract complete filename with extension
			var originalFileName = Path.GetFileName(file.FileName); // Gets "image.jpg"
			var fileNameWithoutExt = Path.GetFileNameWithoutExtension(file.FileName); // "image"
			var fileExtension = Path.GetExtension(file.FileName); // ".jpg"

 

			var uploadRequest = new FileCreateRequest
			{
			  file = memoryStream,
			  fileName = $"{fileNameWithoutExt}_{Guid.NewGuid()}{fileExtension}",
			  folder = folderName,
			  useUniqueFileName = true,
			  isPrivateFile = false
			};


			
			var response = await threadSafeImageKit.UploadAsync(uploadRequest);


			uploadedFiles.Add(new KeyValuePair<string, string>(
			   response.fileId,  // Original "image.jpg"
			  response.url       // CDN URL with GUID "image_abc123.jpg"
		   ));



		  }
		}
		catch (ImagekitException ex)
		{
		  Console.WriteLine($"Failed to upload {file.FileName}: {ex.Message}");
		}
		catch (Exception ex)
		{
		  Console.WriteLine($"Unexpected error with {file.FileName}: {ex.Message}");
		}
	  });

	  return new List<KeyValuePair<string, string>>(uploadedFiles);
	}




  }

}
