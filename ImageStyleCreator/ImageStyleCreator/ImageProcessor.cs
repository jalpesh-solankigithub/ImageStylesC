using SixLabors.ImageSharp;
using SixLabors.ImageSharp.Formats.Jpeg;
using SixLabors.ImageSharp.Processing;


namespace ImageStyleCreator
{
	//public class ImageProcessor
	//{
	//	public static void GenerateImageVersions(string originalImagePath)
	//	{
	//		using (var image = Image.Load(originalImagePath))
	//		{
	//			// Generate thumbnail version (100x100 pixels)
	//			using (var thumbnail = image.Clone(x => x.Resize(100, 100)))
	//			{
	//				SaveImage(thumbnail, "thumbnail.jpg");
	//			}

	//			// Generate web version (800x600 pixels)
	//			using (var web = image.Clone(x => x.Resize(800, 600)))
	//			{
	//				SaveImage(web, "web.jpg");
	//			}

	//			// Generate tablet version (1280x800 pixels)
	//			using (var tablet = image.Clone(x => x.Resize(1280, 800)))
	//			{
	//				SaveImage(tablet, "tablet.jpg");
	//			}

	//			// Generate mobile version (480x320 pixels)
	//			using (var mobile = image.Clone(x => x.Resize(480, 320)))
	//			{
	//				SaveImage(mobile, "mobile.jpg");
	//			}
	//		}
	//	}

	//	private static void SaveImage(Image image, string fileName)
	//	{
	//		// Replace "YourImagesDirectory" with the path to the directory where you want to save the generated images
	//		string outputPath = Path.Combine("YourImagesDirectory", fileName);

	//		// Save the image as JPEG format with 90% quality
	//		image.Save(outputPath, new JpegEncoder { Quality = 90 });
	//	}
	//}
}
