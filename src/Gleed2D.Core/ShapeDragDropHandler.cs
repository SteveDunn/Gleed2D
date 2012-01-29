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

		public void WhenDroppedOntoEditor(ICanvas canvas, DraggingContext context)
		{
			canvas.AddNewItemAtMouse(_temporaryEntityOnCanvas.CurrentEditor);
			canvas.SetModeTo(UserActionInEditor.Idle);
		}

		public void WhenBeingDraggedOverEditor(ICanvas canvas, DraggingContext draggingContext)
		{
			_temporaryEntityOnCanvas.CurrentEditor.SetPosition(MouseStatus.WorldPosition);
			draggingContext.DragEventArgs.Effect = DRAG_DROP_EFFECTS;
		}

		public void WhenEnteringEditor( ICanvas canvas, DraggingContext context)
		{
			_temporaryEntityOnCanvas = canvas.StartCreatingEntityNow(_entityCreationProperties);
		}

		public void WhenLeavingEditor( ICanvas canvas, DraggingContext draggingContext )
		{
			canvas.StopCreatingEntity();
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