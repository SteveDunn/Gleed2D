using Microsoft.Xna.Framework.Input ;

namespace Gleed2D.Core
{
	public static class KeyboardStatus
	{
		static KeyboardState _oldState ;
		static KeyboardState _currentState ;

		public static void Update(KeyboardState keyboardState)
		{
			_oldState = _currentState ;
			_currentState = keyboardState ;
		}

		public static bool IsNewKeyPress( Keys key )
		{
			return _currentState.IsKeyDown(key) && _oldState.IsKeyUp(key) ;
		}

		public static bool IsNewKeyRelease( Keys key )
		{
			return _currentState.IsKeyUp( key ) && _oldState.IsKeyDown( key ) ;
		}

		public static bool IsKeyDown( Keys keys )
		{
			return _currentState.IsKeyDown( keys ) ;
		}

		public static bool IsKeyUp( Keys keys )
		{
			return _currentState.IsKeyUp( keys ) ;
		}
	}
}