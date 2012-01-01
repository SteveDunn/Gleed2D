using System.Windows.Forms ;

namespace Gleed2D.Core.Controls
{
	public class HandleDraggingOfAssets
	{
		readonly IHandleDragDrop _pluginDragDropHandler ;

		public HandleDraggingOfAssets( IPlugin pluginBeingDragged )
		{
			_pluginDragDropHandler = pluginBeingDragged.DragDropHandler ;
		}

		public DragDropEffects DragDropEffects
		{
			get
			{
				return _pluginDragDropHandler.DragDropEffects ;
			}
		}

		public void EnteredEditor( IEditor editor, DragEventArgs e )
		{
			_pluginDragDropHandler.WhenEnteringEditor( e);
		}

		public void DraggingOverEditor( IEditor editor, DragEventArgs e )
		{
			_pluginDragDropHandler.WhenBeingDraggedOverEditor( editor, e );
		}

		public void DroppedOnCanvas( IEditor editor, DragEventArgs e )
		{
			_pluginDragDropHandler.WhenDroppedOntoEditor(editor ) ;
		}
	}
}