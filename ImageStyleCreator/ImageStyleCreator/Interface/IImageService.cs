using ImageStyleCreator.Models;

namespace ImageStyleCreator.Interface
{
	public interface IImageService
	{
		string UploadImage(IFormFile photo);

		string Update_UploadImageBlob(IFormFile image, ImageDetailsModel existingImage);

		
		bool Delete_existImageBlob(ImageDetailsModel existingImage);

	}
}
