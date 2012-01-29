using System;
using System.Windows.Forms;
using Gleed2D.Core;
using Gleed2D.Core.Controls;

namespace Gleed2D.Plugins
{
	public class TextureDragDropHandler : IHandleDragDrop
	{
		private readonly TextureCreationProperties _creationProperties;
		private EntityCreation _entityCreation;

		const DragDropEffects DRAG_DROP_EFFECTS = DragDropEffects.Move ;

		public TextureDragDropHandler( TextureCreationProperties creationProperties)
		{
			_creationProperties = creationProperties;
		}

		public void WhenDroppedOntoEditor(IEditor editor, DraggingContext context)
		{
			editor.AddNewItemAtMouse(_entityCreation.CurrentEditor);
			editor.SetModeTo(UserActionInEditor.Idle);
		}

		public void WhenBeingDraggedOverEditor(IEditor editor, DraggingContext draggingContext)
		{
			_entityCreation.CurrentEditor.SetPosition(MouseStatus.WorldPosition);

			
			draggingContext.DragEventArgs.Effect = DRAG_DROP_EFFECTS;
		}

		public void WhenEnteringEditor( IEditor editor, DraggingContext context)
		{
			_entityCreation = editor.StartCreatingEntityNow(_creationProperties);
		}

		public void WhenLeavingEditor( IEditor editor, DraggingContext draggingContext )
		{
			editor.CancelCreatingEntity();
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