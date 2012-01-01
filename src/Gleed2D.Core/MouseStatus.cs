using Microsoft.Xna.Framework ;
using Microsoft.Xna.Framework.Input ;

namespace Gleed2D.Core
{
	public static class MouseStatus
	{
		static MouseState _oldState ;
		static MouseState _currentState ;
		static Vector2 _mouseWorldPos ;

		public static int ScrollwheelDelta
		{
			get
			{
				return _currentState.ScrollWheelValue - _oldState.ScrollWheelValue;
			}
		}

		public static Vector2 ScreenPosition
		{
			get
			{
				return new Vector2( _currentState.X, _currentState.Y ) ;
			}
		}

		public static ButtonState LeftButton
		{
			get
			{
				return _currentState.LeftButton ;
			}
		}

		public static Vector2 WorldPosition
		{
			get
			{
				return _mouseWorldPos ;
			}
			set
			{
				_mouseWorldPos = value ;
			}
		}

		public static ButtonState MiddleButton
		{
			get
			{
				return _currentState.MiddleButton ;
			}
		}

		public static void Update( MouseState mouseState, Camera camera )
		{
			_oldState = _currentState ;
			_currentState = mouseState ;

			adjustWorldPosition( camera ) ;
		}

		static void adjustWorldPosition( Camera camera )
		{
			_mouseWorldPos = Vector2.Transform( ScreenPosition, Matrix.Invert( camera.Matrix ) ) ;
			_mouseWorldPos = _mouseWorldPos.Round( ) ;
		}

		public static bool IsNewRightMouseButtonClick( )
		{
			return _currentState.RightButton == ButtonState.Pressed && _oldState.RightButton == ButtonState.Released ;
		}

		public static bool IsNewRightMouseButtonRelease( )
		{
			return _currentState.RightButton == ButtonState.Released && _oldState.RightButton == ButtonState.Pressed ;
		}

		public static bool IsNewLeftMouseButtonClick( )
		{
			return _currentState.LeftButton == ButtonState.Pressed && _oldState.LeftButton == ButtonState.Released ;
		}

		public static bool IsNewMiddleMouseButtonClick( )
		{
			return _currentState.MiddleButton == ButtonState.Pressed && _oldState.MiddleButton == ButtonState.Released ;
		}

		public static bool IsNewMiddleMouseButtonRelease( )
		{
			return _currentState.MiddleButton == ButtonState.Released && _oldState.MiddleButton == ButtonState.Pressed ;
		}

		public static void UpdateCamera( Camera camera )
		{
			adjustWorldPosition( camera );			
		}

		public static bool IsNewLeftMouseButtonReleased( )
		{
			return _currentState.LeftButton == ButtonState.Released && _oldState.LeftButton == ButtonState.Pressed ;
		}
	}
}