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

		public void WhenDroppedOntoEditor(ICanvas canvas, DraggingContext context)
		{
			canvas.AddNewItemAtMouse(_entityCreation.CurrentEditor);
			canvas.SetModeTo(UserActionInEditor.Idle);
		}

		public void WhenBeingDraggedOverEditor(ICanvas canvas, DraggingContext draggingContext)
		{
			_entityCreation.CurrentEditor.SetPosition(MouseStatus.WorldPosition);

			
			draggingContext.DragEventArgs.Effect = DRAG_DROP_EFFECTS;
		}

		public void WhenEnteringEditor( ICanvas canvas, DraggingContext context)
		{
			_entityCreation = canvas.StartCreatingEntityNow(_creationProperties);
		}

		public void WhenLeavingEditor( ICanvas canvas, DraggingContext draggingContext )
		{
			canvas.CancelCreatingEntity();
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