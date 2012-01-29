using System;
using System.Windows.Forms;
using Gleed2D.Core.Controls;

namespace Gleed2D.Core
{
	public class ShapeDragDropHandler : IHandleDragDrop
	{
		private readonly IEntityCreationProperties _entityCreationProperties;
		const DragDropEffects DRAG_DROP_EFFECTS = DragDropEffects.Move ;

		EntityCreation _temporaryEntityOnCanvas;

		public ShapeDragDropHandler( IEntityCreationProperties entityCreationProperties)
		{
			_entityCreationProperties = entityCreationProperties;
		}

		public void WhenDroppedOntoEditor(IEditor editor, DraggingContext context)
		{
			editor.AddNewItemAtMouse(_temporaryEntityOnCanvas.CurrentEditor);
			editor.SetModeTo(UserActionInEditor.Idle);
		}

		public void WhenBeingDraggedOverEditor(IEditor editor, DraggingContext draggingContext)
		{
			_temporaryEntityOnCanvas.CurrentEditor.SetPosition(MouseStatus.WorldPosition);
			draggingContext.DragEventArgs.Effect = DRAG_DROP_EFFECTS;
		}

		public void WhenEnteringEditor( IEditor editor, DraggingContext context)
		{
			_temporaryEntityOnCanvas = editor.StartCreatingEntityNow(_entityCreationProperties);
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
	}
}