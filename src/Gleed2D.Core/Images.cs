using System.Reflection ;
using StructureMap ;

namespace Gleed2D.Core
{
	public static class Images
	{
		static ImageProperties summonImage(Assembly assembly, string name, IImageRepository imageRepository )
		{
			if( !imageRepository.ContainsImage( name ) )
			{
				var imageLoader = ObjectFactory.GetInstance<ILoadImages>( ) ;

				imageRepository.Set(new ImageProperties(
					name,
					imageLoader.LoadFromResource( assembly, name ) ) ) ;
			}

			return imageRepository.GetByName( name ) ;
		}

		public static ImageProperties SummonIcon(Assembly assembly, string name )
		{
			var imageRepository = ObjectFactory.GetNamedInstance<IImageRepository>( @"iconImages") ;

			return summonImage( assembly, name, imageRepository ) ;
		}

		public static ImageProperties SummonImage(Assembly assembly, string name)
		{
			var imageRepository = ObjectFactory.GetInstance<IImageRepository>( ) ;

			return summonImage(assembly, name, imageRepository ) ;
		}
	}
}