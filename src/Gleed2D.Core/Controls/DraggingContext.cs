using System;
using System.Windows.Forms;

namespace Gleed2D.Core.Controls
{
	public class DraggingContext
	{
		readonly DragEventArgs _dragEventArgs;

		public DraggingContext(DragEventArgs dragEventArgs)
		{
			_dragEventArgs = dragEventArgs;
		}

		public DragEventArgs DragEventArgs
		{
			get { return _dragEventArgs; }
		}
	}
}