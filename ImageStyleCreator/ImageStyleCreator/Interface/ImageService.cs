using Azure.Storage.Blobs;
using ImageStyleCreator.Models;
using Microsoft.AspNetCore.Http;

using System;

using System.IO;

using System.Threading.Tasks;


namespace ImageStyleCreator.Interface
{
	public class ImageService : IImageService
	{
		private readonly BlobServiceClient _blobServiceClient;

		public ImageService(string connectionString)
		{
			_blobServiceClient = new BlobServiceClient(connectionString);
		}

		public string UploadImage(IFormFile image)
		{

			var containerName = "jalpeshcontainer";

			var containerClient = _blobServiceClient.GetBlobContainerClient(containerName);

			containerClient.CreateIfNotExistsAsync();

			//var fileName = Guid.NewGuid().ToString() + Path.GetExtension(image.FileName);
			var fileName = image.FileName;

			var blobClient = containerClient.GetBlobClient(fileName);

			using (var stream = image.OpenReadStream())
			{
				blobClient.Upload(stream, true);
			}

			return blobClient.Uri.ToString();

		}


		public string Update_UploadImageBlob(IFormFile newImage, ImageDetailsModel existingImageDetails)
		{
			//var containerName = "jalpeshcontainer";

			//var containerClient = _blobServiceClient.GetBlobContainerClient(containerName);

			// Get the existing blob name (filename) from the existing Blob URL
			string existingBlobName = new Uri(existingImageDetails.BlobUrl).Segments[^1].Trim('/');

			// Delete the existing image from Blob Storage
			DeleteImage(existingBlobName);

			// Upload the new image to Blob Storage
			string newBlobUrl = UploadImage(newImage);


			return newBlobUrl;

		}
		public void DeleteImage(string blobName)
		{
			// Get the BlobContainerClient for the specified container
			var containerClient = _blobServiceClient.GetBlobContainerClient("jalpeshcontainer");

			// Delete the image from Blob Storage
			var blobClient = containerClient.GetBlobClient(blobName);
		 blobClient.DeleteIfExists();
		}

		public bool Delete_existImageBlob(ImageDetailsModel existingImageDetails) 
		{
			string existingBlobName = new Uri(existingImageDetails.BlobUrl).Segments[^1].Trim('/');
			// Delete the existing image from Blob Storage
			var containerClient = _blobServiceClient.GetBlobContainerClient("jalpeshcontainer");

			// Delete the image from Blob Storage
			var blobClient = containerClient.GetBlobClient(existingBlobName);
			var res=	blobClient.DeleteIfExists();
			if (res.Value)
				return true;
			else
				return false;
		}

	}
}
