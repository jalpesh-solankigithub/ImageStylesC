using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using System.IO;
using System.Threading.Tasks;
using ImageStyleCreator.Interface;
using ImageStyleCreator.Models;

namespace ImageStyleCreator.Controllers
{
	[Route("api/[controller]")]
	[ApiController]
	public class ImageController : ControllerBase
	{

		private readonly IImageService _imageservice;
		private readonly IImageRepository _imagerepository;
		private readonly ImageProcessor _imageProcessor;


		public ImageController(IImageRepository imagerepository, IImageService imageservice, ImageProcessor imageProcessor)
		{
			_imagerepository = imagerepository;
			_imageservice = imageservice;
			_imageProcessor = imageProcessor;
		}


		[HttpPost("upload")]
		public IActionResult UploadPhoto(IFormFile photo)
		{
			// Perform validation for size and resolution here
			if (photo == null || photo.Length == 0)

				return BadRequest("No photo uploaded.");

			// Validate photo size (not more than 2MB)
			if (photo.Length > 2 * 1024 * 1024)
				return BadRequest("Photo size exceeds the maximum allowed limit (2MB).");

			// Perform validation for resolution here
			var isValidResolution = IsResolutionValid(photo);

			if (!isValidResolution)
				return BadRequest("Photo resolution does not meet the required criteria.");

			// Upload the photo to Azure Blob storage
			var blobUrl = _imageservice.UploadImage(photo);


			// Save image details to the database
			var imageDetails = new ImageDetailsModel
			{
				Name = photo.FileName,
				Size = photo.Length,
				Type = photo.ContentType,
				BlobUrl = blobUrl
			};
			// Call database service to save imageDetails to the database
			int id = _imagerepository.SaveImagetoDB(imageDetails);

			// photo should be render with 4 different sizes: thumbnail, web, tablets, mobile
			var imageDetail = _imagerepository.GetImage(id);

			// Fetch the image bytes from the Blob Storage using the provided URL
			//byte[] imageBytes = _imageProcessor.FetchImageFromBlob(blobUrl);
			byte[] imageBytes = _imageProcessor.FetchImageFromBlob(imageDetail.BlobUrl);

			// Call the ImageProcessor to generate image versions
			var (originalBytes, thumbnailBytes, webBytes, tabletBytes, mobileBytes, rowid) = _imageProcessor.GenerateImageVersions(imageBytes, id);

			// Return the image bytes in the API response
			return Ok(new
			{
				OriginalBytes = originalBytes,
				ThumbnailBytes = thumbnailBytes,
				WebBytes = webBytes,
				TabletBytes = tabletBytes,
				MobileBytes = mobileBytes,
				Rowid = rowid
			});

		}

		private bool IsResolutionValid(IFormFile photo)
		{
			// Load the photo into an ImageSharp Image object
			using (var stream = new MemoryStream())
			{
				photo.CopyTo(stream);
				stream.Seek(0, SeekOrigin.Begin);

				using (var image = Image.Load(stream))
				{
					// Calculate image dimensions
					int width = image.Width;
					int height = image.Height;

					// Define criteria for poor quality resolution
					const int minWidth = 800;
					const int minHeight = 600;

					// Perform the resolution check
					return width > minWidth && height > minHeight;
				}
			}
		}



		//public IActionResult UpdateOldImage(IFormFile newImage, int oldId)
		[HttpPost("UpdateOldImage/{oldId}")]
		public IActionResult UpdateOldImage(int oldId, IFormFile photo)
		{
			//int oldId = 1;
			var existingImage = _imagerepository.GetImage(oldId);

			if (existingImage == null)
			{
				return NotFound();
			}

			//var imageDetail = _imagerepository.GetImage(id);

			// Update the image in Azure Blob Storage
			string updatedImageBlobUrl = _imageservice.Update_UploadImageBlob(photo, existingImage);

			// Update the image details in the database
			var newimageDetails = new ImageDetailsModel
			{
				BlobUrl = updatedImageBlobUrl,
				Name = photo.FileName,
				Size = photo.Length,
				Type = photo.ContentType
			};

			int id = _imagerepository.UpdateImagetoDB(newimageDetails, existingImage.Id);


			// photo should be render with 4 different sizes: thumbnail, web, tablets, mobile

			// Fetch the image bytes from the Blob Storage using the provided URL
			byte[] imageBytes = _imageProcessor.FetchImageFromBlob(newimageDetails.BlobUrl);

			// Call the ImageProcessor to generate image versions
			var (originalBytes, thumbnailBytes, webBytes, tabletBytes, mobileBytes, rowid) = _imageProcessor.GenerateImageVersions(imageBytes, id);

			// Return the image bytes in the API response
			return Ok(new
			{
				OriginalBytes = originalBytes,
				ThumbnailBytes = thumbnailBytes,
				WebBytes = webBytes,
				TabletBytes = tabletBytes,
				MobileBytes = mobileBytes,
				Rowid = rowid
			});

		}

		[HttpPost("DeleteImage/{oldId}")]
		public IActionResult DeleteImage(int oldId)
		{
			var existingImage = _imagerepository.GetImage(oldId);

			if (existingImage == null)
			{
				return NotFound();
			}
			var updatedImageBlobUrl = _imageservice.Delete_existImageBlob(existingImage);

			// Delete the image details in the database			
			var res = _imagerepository.DeleteexistingImage(existingImage.Id);
			return Ok(res);

		}
	}

}
