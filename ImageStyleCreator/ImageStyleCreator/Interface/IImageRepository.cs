using ImageStyleCreator.Models;

namespace ImageStyleCreator.Interface
{
	public interface IImageRepository
	{

		int SaveImagetoDB(ImageDetailsModel imageDetails);

		ImageDetailsModel GetImage(int id);

		int UpdateImagetoDB(ImageDetailsModel imageDetails, int id);

		bool DeleteexistingImage(int id);
	}
}
