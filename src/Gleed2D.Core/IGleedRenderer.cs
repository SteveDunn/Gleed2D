using System ;

namespace Gleed2D.Core
{
	public interface IGleedRenderer
	{
		void Draw( RendererParams rendererParams ) ;
		void QueueForDebugDraw( Action action ) ;
	}
}