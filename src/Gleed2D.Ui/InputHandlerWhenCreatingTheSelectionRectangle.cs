using System.Linq ;
using Gleed2D.Core ;
using Microsoft.Xna.Framework ;
using ButtonState = Microsoft.Xna.Framework.Input.ButtonState ;

namespace GLEED2D
{
	internal class InputHandlerWhenCreatingTheSelectionRectangle : IHandleEditorInput
	{
		readonly ICanvas _canvas ;
		readonly IModel _model ;

		public InputHandlerWhenCreatingTheSelectionRectangle( ICanvas canvas )
		{
			_canvas = canvas ;
			_model = IoC.Model ;
		}

		public void Update( )
		{
			if( _model.Level.ActiveLayer == null )
			{
				return ;
			}

			Vector2 distance = MouseStatus.WorldPosition - _canvas.GrabPoint ;

			if( distance.Length( ) > 0 )
			{
				_canvas.SelectionRectangle = Extensions.RectangleFromVectors( _canvas.GrabPoint, MouseStatus.WorldPosition ) ;

				var itemsCoveredByDragRectangle = _model.ActiveLayer.Items.Where(
					i =>
						{
							var point = new Point( (int) i.ItemProperties.Position.X, (int) i.ItemProperties.Position.Y ) ;

							return i.ItemProperties.Visible && _canvas.SelectionRectangle.Contains( point ) ;
						} ).ToList( ) ;

				itemsCoveredByDragRectangle.Reverse( ) ;
				_model.SelectEditors( new SelectedEditors( itemsCoveredByDragRectangle ) ) ;
			}

			if( MouseStatus.LeftButton == ButtonState.Released )
			{
				_canvas.SetModeToIdle( ) ;
			}
		}
	}
}