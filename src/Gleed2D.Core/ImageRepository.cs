using System.Collections.Generic ;
using System.Drawing ;
using System.Windows.Forms ;
using JetBrains.Annotations ;

namespace Gleed2D.Core
{
	[UsedImplicitly]
	public class ImageRepository : IImageRepository
	{
		readonly Dictionary<string, ImageProperties> _images ;
		readonly List<ImageList> _imageLists ;

		public ImageRepository( )
		{
			_images=new Dictionary<string, ImageProperties>();
			_imageLists = new List<ImageList>( ) ;
		}

		public void Set(ImageProperties image)
		{
			_images[ image.Name ] = image ;
			
			_imageLists.ForEach( il => il.Images.Add( image.Name, image.Picture ) ) ;
		}

		public ImageProperties GetByName(string name)
		{
			ImageProperties image ;
			
			_images.TryGetValue( name, out image ) ;

			return image ;
		}

		public bool ContainsImage( string name )
		{
			return _images.ContainsKey( name ) ;
		}

		public ImageList CreateImageList( )
		{
			var imageList = new ImageList
				{
					ImageSize = new Size( 32, 32 )
				} ;

			_imageLists.Add( imageList ) ;
			
			_images.ForEach( kvp => imageList.Images.Add( kvp.Key, kvp.Value.Picture ) ) ;
			
			return imageList ;
		}
	}
}