using System.Collections.Generic ;
using System.Linq ;
using Gleed2D.Core ;
using Microsoft.Xna.Framework ;
using StructureMap ;
using Keys = Microsoft.Xna.Framework.Input.Keys ;

namespace GLEED2D
{
	internal class InputHandlerForWhenMovingOrCopyingItems : IHandleEditorInput
	{
		Vector2 _newPosition ;

		readonly ICanvas _canvas ;
		readonly IModel _model ;

		public InputHandlerForWhenMovingOrCopyingItems(ICanvas canvas )
		{
			_canvas = canvas ;
			_model = IoC.Model ;
		}

		public void Update( )
		{
			int i = 0 ;

			IEnumerable<ItemEditor> allSelectedEditors = selectedEditors( ).ToList(  ) ;
			
			foreach( ItemEditor eachSelectedEditor in allSelectedEditors )
			{
				_newPosition = _canvas.PositionsBeforeUserInteraction[ i ] + MouseStatus.WorldPosition - _canvas.GrabPoint ;

				if( Constants.Instance.SnapToGrid || KeyboardStatus.IsKeyDown( Keys.G ) )
				{
					_newPosition = _canvas.SnapToGrid( _newPosition ) ;
				}

				//_editor.SnapPoint.Visible = false ;

				eachSelectedEditor.SetPosition( _newPosition ) ;

				i++ ;
			}

			IoC.Model.NotifyChanged( allSelectedEditors );

			if( MouseStatus.IsNewLeftMouseButtonReleased( ) || KeyboardStatus.IsNewKeyRelease( Keys.D1 ) )
			{
				foreach( ItemEditor eachSelectedEditor in allSelectedEditors )
				{
					eachSelectedEditor.OnMouseButtonUp( MouseStatus.WorldPosition ) ;
				}

				bool isMoving = _canvas.CurrentUserAction == UserActionInEditor.MovingItems ;
				_canvas.SetModeToIdle( ) ;

				var samePoint = MouseStatus.WorldPosition == _canvas.GrabPoint;
				
				var memento = ObjectFactory.GetInstance<IMemento>() ;
				
				if( samePoint && isMoving  )
				{
					memento.AbortCommand( ) ;
				}
				else
				{
					memento.EndCommand( ) ;
				}
			}
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