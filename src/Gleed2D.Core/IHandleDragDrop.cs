using System;
using System.Windows.Forms;
using Gleed2D.Core.Controls;

namespace Gleed2D.Core
{
	public interface IHandleDragDrop
	{
		void WhenDroppedOntoEditor(IEditor editor, DraggingContext context);
		void WhenBeingDraggedOverEditor(IEditor editor, DraggingContext draggingContext);
		void WhenEnteringEditor( IEditor editor, DraggingContext context) ;
		void WhenLeavingEditor( IEditor editor, DraggingContext draggingContext ) ;

		DragDropEffects DragDropEffects
		{
			get ;
		}
	}
}