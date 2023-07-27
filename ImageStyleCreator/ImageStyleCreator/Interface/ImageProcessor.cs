using Azure.Storage.Blobs;
using SixLabors.ImageSharp.Formats.Jpeg;

namespace ImageStyleCreator.Interface
{
	public class ImageProcessor
	{
		private readonly BlobServiceClient _blobServiceClient;
		private readonly string _containerName;

		public ImageProcessor(string connectionString, string containerName)
		{
			_blobServiceClient = new BlobServiceClient(connectionString);
			_containerName = containerName;
		}

		public byte[] FetchImageFromBlob(string blobUrl)
		{			
			var containerClient = _blobServiceClient.GetBlobContainerClient(_containerName);

			// Get the blob name (filename) from the blob URL
			string blobName = new Uri(blobUrl).Segments[^1].Trim('/');

			// Download the image from the Blob Storage
			using (var memoryStream = new MemoryStream())
			{
				var blobClient = containerClient.GetBlobClient(blobName);
				blobClient.DownloadTo(memoryStream);
				memoryStream.Position = 0;
				return memoryStream.ToArray();
			}
	
		}

		public (byte[] originalBytes, byte[] thumbnailBytes, byte[] webBytes, byte[] tabletBytes, byte[] mobileBytes, int id) GenerateImageVersions(byte[] originalImageBytes,int id)
		{
			byte[] _originalBytes;
			byte[] _thumbnailBytes;
			byte[] _webBytes;
			byte[] _tabletBytes;
			byte[] _mobileBytes;
			int _id;

			using (var memoryStream = new MemoryStream(originalImageBytes))
			{
				// Load the original image from the provided byte array
				using (var image = Image.Load(memoryStream))
				{
					// Generate thumbnail version (100x100 pixels)
					using (var thumbnail = image.Clone(x => x.Resize(100, 100)))
					{
						byte[] thumbnailBytes = SaveImage(thumbnail);
						_thumbnailBytes = (byte[]) thumbnailBytes.Clone();
					}
					// Generate web version (800x600 pixels)
					using (var web = image.Clone(x => x.Resize(800, 600)))
					{
						byte[] webBytes = SaveImage(web);
						_webBytes = (byte[]) webBytes.Clone();
					}
					// Generate tablet version (1280x800 pixels)
					using (var tablet = image.Clone(x => x.Resize(1280, 800)))
					{
						byte[] tabletBytes = SaveImage(tablet);
						_tabletBytes = (byte[]) tabletBytes.Clone();
					}
					// Generate mobile version (480x320 pixels)
					using (var mobile = image.Clone(x => x.Resize(480, 320)))
					{
						byte[] mobileBytes = SaveImage(mobile);
						_mobileBytes = (byte[]) mobileBytes.Clone();
					}
					_originalBytes = originalImageBytes;
					_id = id;
					return  (_originalBytes,_thumbnailBytes, _webBytes, _tabletBytes, _mobileBytes,_id);
				}
			}
		}


		private byte[] SaveImage(Image image)
		{
			using (var memoryStream = new MemoryStream())
			{
				// Save the image as JPEG format with 90% quality
				image.SaveAsJpeg(memoryStream, new JpegEncoder { Quality = 90 });
				return memoryStream.ToArray();
			}
		}
	}
}
