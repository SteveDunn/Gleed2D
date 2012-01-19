using System.Collections.Specialized;
using System.Windows.Forms ;
using JetBrains.Annotations;

namespace Gleed2D.Core.Controls
{
	/// <summary>
	/// Represents a type that handle the dragging of assets.
	/// This type is used stored in the drag-drop mechanism, so it's important NOT
	/// to subsclass it as it won't be recognised.
	/// </summary>
	public class HandleDraggingOfAssets
	{
		readonly IHandleDragDrop _handlerForPlugin ;
		readonly StringDictionary _propertyBag;

		public HandleDraggingOfAssets( IHandleDragDrop handlerForPlugin, StringDictionary propertyBag = null )
		{
			_handlerForPlugin = handlerForPlugin ;
			_propertyBag = propertyBag ?? new StringDictionary();
		}

		[NotNull]
		public StringDictionary PropertyBag
		{
			get { return _propertyBag; }
		}

		public DragDropEffects DragDropEffects
		{
			get
			{
				return _handlerForPlugin.DragDropEffects ;
			}
		}

		public void EnteredEditor( IEditor editor, DragEventArgs eventArgs )
		{
			var context = new DraggingContext(eventArgs);
			_handlerForPlugin.WhenEnteringEditor( editor, context );
		}

		public void DraggingOverEditor( IEditor editor, DragEventArgs eventArgs )
		{
			var context = new DraggingContext(eventArgs);

			_handlerForPlugin.WhenBeingDraggedOverEditor( editor, context );
		}

		public void DroppedOnCanvas( IEditor editor, DragEventArgs eventArgs )
		{
			var context = new DraggingContext(eventArgs);

			_handlerForPlugin.WhenDroppedOntoEditor(editor, context ) ;
		}
	}
}