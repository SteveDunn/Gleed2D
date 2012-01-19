using System;
using System.Collections.Generic;
using System.Windows.Forms;
using Gleed2D.Core.Controls;
using Microsoft.Xna.Framework;

namespace Gleed2D.Core
{
	public class TextureDragDropHandler : IHandleDragDrop
	{
		const DragDropEffects DRAG_DROP_EFFECTS = DragDropEffects.Move ;
		readonly Dictionary<string,object> _properties = new Dictionary<string, object>();
		
		readonly Func<IEditor, ItemEditor> _funcToBuildItemEditor ;
		private EntityCreation _entityCreation;

		public TextureDragDropHandler( Func<IEditor, ItemEditor> funcToBuildItemEditor  )
		{
			_funcToBuildItemEditor = funcToBuildItemEditor ;
		}

		public void WhenDroppedOntoEditor(IEditor editor, DraggingContext context)
		{
			editor.AddNewItemAtMouse(_entityCreation.CurrentEditor);
			editor.SetModeTo(UserActionInEditor.Idle);
		}

		public void WhenBeingDraggedOverEditor(IEditor editor, DraggingContext draggingContext)
		{
			if (_entityCreation != null)
			{
				_entityCreation.CurrentEditor.SetPosition(MouseStatus.WorldPosition);
			}
			
			draggingContext.DragEventArgs.Effect = DRAG_DROP_EFFECTS;
		}

		public void WhenEnteringEditor( IEditor editor, DraggingContext context)
		{
			var entityCreationProperties = (EntityCreationProperties) _properties[@"CreationProperties"];

			_entityCreation = IoC.Editor.StartCreatingEntityNow(entityCreationProperties);
		}

		public void WhenLeavingEditor( IEditor editor, DraggingContext draggingContext )
		{
			editor.StopCreatingEntity();
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