using System.Windows.Forms ;

namespace Gleed2D.Core
{
	public interface IImageRepository
	{
		void Set(ImageProperties imageProperties) ;
		
		ImageProperties GetByName(string name) ;
		
		bool ContainsImage( string name ) ;
		
		ImageList CreateImageList( ) ;
	}
}