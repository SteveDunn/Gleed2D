using System ;
using System.Collections.Generic ;
using System.Linq ;
using Gleed2D.Core ;
using Microsoft.Xna.Framework ;
using StructureMap ;
using Keys = Microsoft.Xna.Framework.Input.Keys ;

namespace GLEED2D
{
	internal class InputHandlerWhenRotatingItems : IHandleEditorInput
	{
		readonly ICanvas _canvas ;
		readonly IModel _model ;

		public InputHandlerWhenRotatingItems(ICanvas canvas )
		{
			_canvas = canvas ;
			_model = IoC.Model ;
		}

		public void Update( )
		{
			IEnumerable<ItemEditor> allSelectedEditors = selectedEditors( ).ToList(  ) ;

			Vector2 newpos = MouseStatus.WorldPosition - allSelectedEditors.First().ItemProperties.Position ;
			
			float deltaTheta = (float) Math.Atan2( _canvas.GrabPoint.Y, _canvas.GrabPoint.X ) - (float) Math.Atan2( newpos.Y, newpos.X ) ;

			int i = 0 ;

			foreach( ItemEditor eachSelectedItem in allSelectedEditors )
			{
				if( eachSelectedItem.CanRotate( ) )
				{
					eachSelectedItem.Rotation = _canvas.RotationsBeforeUserInteraction[ i ] - deltaTheta ;

					if( KeyboardStatus.IsKeyDown( Keys.LeftControl ) )
					{
						eachSelectedItem.Rotation = (float) Math.Round( eachSelectedItem.Rotation / MathHelper.PiOver4 )
							* MathHelper.PiOver4 ;
					}

					i++ ;
				}

				IoC.Model.NotifyChanged( allSelectedEditors );
			}


			if( MouseStatus.IsNewMiddleMouseButtonRelease( ) || KeyboardStatus.IsNewKeyRelease( Keys.D2 ) )
			{
				_canvas.SetModeToIdle(  );

				ObjectFactory.GetInstance<IMemento>().EndCommand( ) ;
			}

			return ;
		}

		IEnumerable<ItemEditor> selectedEditors( )
		{
			return getLevel( ).SelectedEditors ;
		}

		LevelEditor getLevel( )
		{
			return _model.Level ;
		}
	}
}