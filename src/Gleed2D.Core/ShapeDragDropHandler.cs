using System;
using System.Collections.Generic;
using System.Windows.Forms;
using Gleed2D.Core.Controls;

namespace Gleed2D.Core
{
	public class ShapeDragDropHandler : IHandleDragDrop
	{
		const DragDropEffects DRAG_DROP_EFFECTS = DragDropEffects.Move ;

		readonly Dictionary<string,object> _properties = new Dictionary<string, object>();
		
		readonly Func<IEditor, ItemEditor> _buildEntityCreationProperties ;

		public ShapeDragDropHandler( Func<IEditor, ItemEditor> buildEntityCreationProperties  )
		{
			_buildEntityCreationProperties = buildEntityCreationProperties ;
		}

		public void WhenDroppedOntoEditor(IEditor editor, DraggingContext context)
		{
			editor.AddNewItemAtMouse(_buildEntityCreationProperties(editor) ) ;
		}

		public void WhenBeingDraggedOverEditor(IEditor editor, DraggingContext draggingContext)
		{
			draggingContext.DragEventArgs.Effect = DRAG_DROP_EFFECTS;
		}

		public void WhenEnteringEditor( IEditor editor, DraggingContext context)
		{
		}

		public void WhenLeavingEditor( IEditor editor, DraggingContext draggingContext )
		{
		}

		public DragDropEffects DragDropEffects
		{
			get
			{
				return DRAG_DROP_EFFECTS;
			}
		}

		public object this[string name]
		{
			get { return _properties[name]; }
			set { _properties[name]=value; }
		}
	}
}