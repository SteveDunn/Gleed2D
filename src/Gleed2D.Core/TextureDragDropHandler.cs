using System;
using System.Windows.Forms;
using Gleed2D.Core.Controls;

namespace Gleed2D.Core
{
	public class TextureDragDropHandler : IHandleDragDrop
	{
		private readonly ItemEditor _itemEditor;
		private readonly TextureCreationProperties _creationProperties;
		
		const DragDropEffects DRAG_DROP_EFFECTS = DragDropEffects.Move ;

		public TextureDragDropHandler(ItemEditor itemEditor, TextureCreationProperties creationProperties)
		{
			_itemEditor = itemEditor;
			_creationProperties = creationProperties;
		}

		public void WhenDroppedOntoEditor(IEditor editor, DraggingContext context)
		{
			editor.AddNewItemAtMouse(_itemEditor);
			editor.SetModeTo(UserActionInEditor.Idle);
		}

		public void WhenBeingDraggedOverEditor(IEditor editor, DraggingContext draggingContext)
		{
			_itemEditor.SetPosition(MouseStatus.WorldPosition);

			
			draggingContext.DragEventArgs.Effect = DRAG_DROP_EFFECTS;
		}

		public void WhenEnteringEditor( IEditor editor, DraggingContext context)
		{
			editor.AddNewItemAtMouse(_itemEditor);
		}

		public void WhenLeavingEditor( IEditor editor, DraggingContext draggingContext )
		{
			editor.RemoveItem(_itemEditor);
		}

		public DragDropEffects DragDropEffects
		{
			get
			{
				return DRAG_DROP_EFFECTS;
			}
		}
	}
}