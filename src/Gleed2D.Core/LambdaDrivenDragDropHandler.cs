using System;
using System.Collections.Generic;
using System.Windows.Forms;
using Gleed2D.Core.Controls;

namespace Gleed2D.Core
{
	public class LambdaDrivenDragDropHandler : IHandleDragDrop
	{
		readonly Dictionary<string,object> _properties = new Dictionary<string, object>();

		readonly Action<DraggingContext> _whenEnteringEditor ;
		readonly Action<IEditor, DraggingContext> _whenDraggingOverEditor ;
		readonly Action<DraggingContext> _whenLeavingEditor ;
		readonly Action<IEditor, DraggingContext> _whenDroppedOntoEditor ;
		readonly DragDropEffects _dragDropEffects = DragDropEffects.Copy;

		public LambdaDrivenDragDropHandler( 
			DragDropEffects dragDropEffects,
			Action<DraggingContext> whenEnteringEditor = null,
			Action<IEditor, DraggingContext> whenDraggingOverEditor = null,
			Action<DraggingContext> whenLeavingEditor = null,
			Action<IEditor,DraggingContext> whenDroppedOntoEditor = null )
		{
			_dragDropEffects = dragDropEffects ;
			
			_whenEnteringEditor = whenEnteringEditor ;
			_whenDraggingOverEditor = whenDraggingOverEditor ;
			_whenLeavingEditor = whenLeavingEditor ;
			_whenDroppedOntoEditor = whenDroppedOntoEditor ;
		}

		public void WhenDroppedOntoEditor(IEditor editor, DraggingContext context)
		{
			if( _whenDroppedOntoEditor !=null )
			{
				_whenDroppedOntoEditor( editor, context ) ;
			}
		}

		public void WhenBeingDraggedOverEditor(IEditor editor, DraggingContext draggingContext)
		{
			if (_whenDraggingOverEditor != null)
			{
				_whenDraggingOverEditor(editor, draggingContext);
			}
		}

		public void WhenEnteringEditor( IEditor editor, DraggingContext context)
		{
			if (_whenEnteringEditor != null)
			{
				_whenEnteringEditor(context);
			}
		}

		public void WhenLeavingEditor( IEditor editor, DraggingContext draggingContext )
		{
			if (_whenLeavingEditor != null)
			{
				_whenLeavingEditor(draggingContext);
			}
		}

		public DragDropEffects DragDropEffects
		{
			get
			{
				return _dragDropEffects ;
			}
		}

		public object this[string name]
		{
			get { return _properties[name]; }
			set { _properties[name]=value; }
		}
	}
}