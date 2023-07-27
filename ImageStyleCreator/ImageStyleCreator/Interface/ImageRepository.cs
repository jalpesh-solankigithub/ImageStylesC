using ImageStyleCreator.Models;
using ImageStyleCreator.Interface;

namespace ImageStyleCreator.Interface
{
	public class ImageRepository : IImageRepository
	{
		private readonly ImageStyleCreatorContext _context;

		public ImageRepository(ImageStyleCreatorContext context)
		{
			_context = context;
		}

		public int SaveImagetoDB(ImageDetailsModel imageDetails)
		{
			ImageDetailsModel imageDetailsModel = new ImageDetailsModel
			{
				Name = imageDetails.Name,
				Size = imageDetails.Size,
				BlobUrl = imageDetails.BlobUrl,
				Type = imageDetails.Type,
				Id = imageDetails.Id
			};

			if (imageDetailsModel != null)
			{
				try
				{
					var res=_context.ImageDetailsModel.Add(imageDetailsModel);
					 _context.SaveChanges();
					return imageDetailsModel.Id;
				}
				catch (Exception ex)
				{
				}
			}
			return 0;
		}

		public ImageDetailsModel GetImage(int id) 
		{
			var res=	_context.ImageDetailsModel.Where(x => x.Id == id).FirstOrDefault();

			if (res != null)
			{			
				return res;
			}
			return null;
		}

		public int UpdateImagetoDB(ImageDetailsModel newimageDetails, int id)
		{
			//ImageDetailsModel imageDetailsModel = new ImageDetailsModel
			//{
			//	Name = newimageDetails.Name,
			//	Size = newimageDetails.Size,
			//	BlobUrl = newimageDetails.BlobUrl,
			//	Type = newimageDetails.Type,
			//	Id = id
			//};

			if (newimageDetails != null)
			{
				try
				{
					var exist = _context.ImageDetailsModel.Single(x => x.Id == id);

					exist.Name = newimageDetails.Name;
					exist.Size = newimageDetails.Size;
					exist.BlobUrl = newimageDetails.BlobUrl;
					exist.Type = newimageDetails.Type;

					_context.SaveChanges();
					
					return exist.Id;
				}
				catch (Exception ex)
				{
				}
			}
			return 0;
		}

		public bool DeleteexistingImage(int id)
		{
			try
			{
				var exist = _context.ImageDetailsModel.Where(x => x.Id == id).FirstOrDefault();

				if (exist != null)
				{
					_context.ImageDetailsModel.Remove(exist);
					_context.SaveChanges();
					return true;
				}
				return false;
			}
			catch (Exception ex)
			{
				return false;
			}
		}
	}
}
