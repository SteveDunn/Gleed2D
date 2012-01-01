using System ;
using System.Windows.Forms ;

namespace Gleed2D.Core
{
	public interface IHandleDragDrop
	{
		void WhenDroppedOntoEditor(IEditor editor);
		void WhenBeingDraggedOverEditor(IEditor editor, DragEventArgs dragEventArgs);
		void WhenEnteringEditor( DragEventArgs dragEventArgs ) ;
		void WhenLeavingEditor( DragEventArgs dragEventArgs ) ;

		DragDropEffects DragDropEffects
		{
			get ;
		}
	}

	public class DefaultDragDropHandler : IHandleDragDrop
	{
		readonly Action<DragEventArgs> _whenEnteringEditor ;
		readonly Action<IEditor, DragEventArgs> _whenDraggingOverEditor ;
		readonly Action<DragEventArgs> _whenLeavingEditor ;
		readonly Action<IEditor> _whenDroppedOntoEditor ;
		DragDropEffects _dragDropEffects = DragDropEffects.Copy;

		public DefaultDragDropHandler( 
			DragDropEffects dragDropEffects,
			Action<DragEventArgs> whenEnteringEditor = null,
			Action<IEditor, DragEventArgs> whenDraggingOverEditor = null,
			Action<DragEventArgs> whenLeavingEditor = null,
			Action<IEditor> whenDroppedOntoEditor = null )
		{
			_dragDropEffects = dragDropEffects ;
			_whenEnteringEditor = whenEnteringEditor ;
			_whenDraggingOverEditor = whenDraggingOverEditor ;
			_whenLeavingEditor = whenLeavingEditor ;
			_whenDroppedOntoEditor = whenDroppedOntoEditor ;
		}

		public void WhenDroppedOntoEditor( IEditor editor )
		{
			if( _whenDroppedOntoEditor !=null )
			{
				_whenDroppedOntoEditor( editor ) ;
			}
		}

		public void WhenBeingDraggedOverEditor( IEditor editor, DragEventArgs dragEventArgs )
		{
			if (_whenDraggingOverEditor != null)
			{
				_whenDraggingOverEditor(editor, dragEventArgs);
			}
		}

		public void WhenEnteringEditor( DragEventArgs dragEventArgs )
		{
			if (_whenEnteringEditor != null)
			{
				_whenEnteringEditor(dragEventArgs);
			}
			
		}

		public void WhenLeavingEditor( DragEventArgs dragEventArgs )
		{
			if (_whenLeavingEditor != null)
			{
				_whenLeavingEditor(dragEventArgs);
			}
		}

		public DragDropEffects DragDropEffects
		{
			get
			{
				return _dragDropEffects ;
			}
		}
	}

	public interface IPlugin
	{
		string Name
		{
			get ;
		}

		string CategoryName
		{
			get ;
		}

		ImageProperties Icon
		{
			get;
		}

		IHandleDragDrop DragDropHandler
		{
			get ;
		}
	}
}