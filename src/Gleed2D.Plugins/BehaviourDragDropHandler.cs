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
		readonly Func<ITreeItem, IBehaviour> _buildBehaviour ;

		readonly Dictionary<string,object> _properties = new Dictionary<string, object>();

		public BehaviourDragDropHandler(Func<ITreeItem, IBehaviour> buildBehaviour  )
		{
			_buildBehaviour = buildBehaviour ;
		}

		public void WhenDroppedOntoEditor(ICanvas canvas, DraggingContext context)
		{
			ITreeItem itemEditor = canvas.ItemUnderMouse;

			if (itemEditor == null)
			{
				return;
			}

			IoC.Model.AttachBehaviour(itemEditor, _buildBehaviour(itemEditor));
		}

		public void WhenBeingDraggedOverEditor(ICanvas canvas, DraggingContext draggingContext)
		{
			ITreeItem itemEditor = canvas.ItemUnderMouse;

			if (itemEditor == null)
			{
				draggingContext.DragEventArgs.Effect = DragDropEffects.None;
			}

			draggingContext.DragEventArgs.Effect = DragDropEffects.Link;
		}

		public void WhenEnteringEditor( ICanvas canvas, DraggingContext context)
		{
		}

		public void WhenLeavingEditor( ICanvas canvas, DraggingContext draggingContext )
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