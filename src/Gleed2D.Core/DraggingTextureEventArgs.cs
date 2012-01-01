using System ;

namespace Gleed2D.Core
{
	public class DraggingTextureEventArgs : EventArgs
	{
		public DragEventType DragEventType
		{
			get ;
			set ;
		}
		
		public string PathToTexture
		{
			get ;
			set ;
		}
	}
}