using System ;

namespace Gleed2D.Core
{
	public interface IRenderer
	{
		void Render( RendererParams rendererParams, Action<RendererParams> defaultRenderer ) ;
	}
}