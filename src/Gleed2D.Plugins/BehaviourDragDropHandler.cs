using System;
using System.Collections.Generic;
using System.Windows.Forms;
using Gleed2D.Core;
using Gleed2D.Core.Behaviour;
using Gleed2D.Core.Controls;

namespace Gleed2D.Plugins
{
	public class BehaviourDragDropHandler : IHandleDragDrop
	{
		readonly Func<ItemEditor, IBehaviour> _buildBehaviour ;

		readonly Dictionary<string,object> _properties = new Dictionary<string, object>();

		public BehaviourDragDropHandler(Func<ItemEditor, IBehaviour> buildBehaviour  )
		{
			_buildBehaviour = buildBehaviour ;
		}

		public void WhenDroppedOntoEditor(IEditor editor, DraggingContext context)
		{
			ItemEditor itemEditor = editor.ItemUnderMouse;

			if (itemEditor == null)
			{
				return;
			}

			IoC.Model.AttachBehaviour(itemEditor, _buildBehaviour(itemEditor));
		}

		public void WhenBeingDraggedOverEditor(IEditor editor, DraggingContext draggingContext)
		{
			ItemEditor itemEditor = editor.ItemUnderMouse;

			if (itemEditor == null)
			{
				draggingContext.DragEventArgs.Effect = DragDropEffects.None;
			}

			draggingContext.DragEventArgs.Effect = DragDropEffects.Link;
		}

		public void WhenEnteringEditor( IEditor editor, DraggingContext context)
		{
		}

		public void WhenLeavingEditor( IEditor editor, DraggingContext draggingContext )
		{
		}

		public DragDropEffects DragDropEffects
		{
			get { return DragDropEffects.Link;}
		}

		public object this[string name]
		{
			get { return _properties[name]; }
			set { _properties[name]=value; }
		}
	}
}