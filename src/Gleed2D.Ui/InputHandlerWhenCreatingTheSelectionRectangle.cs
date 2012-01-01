using System.Linq ;
using Gleed2D.Core ;
using Microsoft.Xna.Framework ;
using ButtonState = Microsoft.Xna.Framework.Input.ButtonState ;

namespace GLEED2D
{
	internal class InputHandlerWhenCreatingTheSelectionRectangle : IHandleEditorInput
	{
		readonly IEditor _editor ;
		readonly IModel _model ;

		public InputHandlerWhenCreatingTheSelectionRectangle( IEditor editor )
		{
			_editor = editor ;
			_model = IoC.Model ;
		}

		public void Update( )
		{
			if( _model.Level.ActiveLayer == null )
			{
				return ;
			}

			Vector2 distance = MouseStatus.WorldPosition - _editor.GrabPoint ;

			if( distance.Length( ) > 0 )
			{
				_editor.SelectionRectangle = Extensions.RectangleFromVectors( _editor.GrabPoint, MouseStatus.WorldPosition ) ;

				var itemsCoveredByDragRectangle = _model.ActiveLayer.Items.Where(
					i =>
						{
							var point = new Point( (int) i.ItemProperties.Position.X, (int) i.ItemProperties.Position.Y ) ;

							return i.ItemProperties.Visible && _editor.SelectionRectangle.Contains( point ) ;
						} ).ToList( ) ;

				itemsCoveredByDragRectangle.Reverse( ) ;
				_model.SelectEditors( new SelectedEditors( itemsCoveredByDragRectangle ) ) ;
			}

			if( MouseStatus.LeftButton == ButtonState.Released )
			{
				_editor.SetModeToIdle( ) ;
			}
		}
	}
}