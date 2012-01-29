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
		readonly Action<ICanvas, DraggingContext> _whenDraggingOverEditor ;
		readonly Action<DraggingContext> _whenLeavingEditor ;
		readonly Action<ICanvas, DraggingContext> _whenDroppedOntoEditor ;
		readonly DragDropEffects _dragDropEffects = DragDropEffects.Copy;

		public LambdaDrivenDragDropHandler( 
			DragDropEffects dragDropEffects,
			Action<DraggingContext> whenEnteringEditor = null,
			Action<ICanvas, DraggingContext> whenDraggingOverEditor = null,
			Action<DraggingContext> whenLeavingEditor = null,
			Action<ICanvas,DraggingContext> whenDroppedOntoEditor = null )
		{
			_dragDropEffects = dragDropEffects ;
			
			_whenEnteringEditor = whenEnteringEditor ;
			_whenDraggingOverEditor = whenDraggingOverEditor ;
			_whenLeavingEditor = whenLeavingEditor ;
			_whenDroppedOntoEditor = whenDroppedOntoEditor ;
		}

		public void WhenDroppedOntoEditor(ICanvas canvas, DraggingContext context)
		{
			if( _whenDroppedOntoEditor !=null )
			{
				_whenDroppedOntoEditor( canvas, context ) ;
			}
		}

		public void WhenBeingDraggedOverEditor(ICanvas canvas, DraggingContext draggingContext)
		{
			if (_whenDraggingOverEditor != null)
			{
				_whenDraggingOverEditor(canvas, draggingContext);
			}
		}

		public void WhenEnteringEditor( ICanvas canvas, DraggingContext context)
		{
			if (_whenEnteringEditor != null)
			{
				_whenEnteringEditor(context);
			}
		}

		public void WhenLeavingEditor( ICanvas canvas, DraggingContext draggingContext )
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