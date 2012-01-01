using System ;
using Microsoft.Xna.Framework ;

namespace Gleed2D.InGame.Interpolation
{
	public class Vector2Tweener
	{
		readonly Tweener _tweenerX ;
		readonly Tweener _tweenerY ;

    	public delegate void EndHandler();
    
		public event TweenEndHandler Ended;

		public Vector2Tweener( 
			Vector2 start, 
			Vector2 end, 
			TimeSpan duration, 
			TweeningFunction tweeningFunction )
		{
			_tweenerX = new Tweener( start.X, end.X, duration, tweeningFunction ) ;
			_tweenerY = new Tweener( start.Y, end.Y, duration, tweeningFunction) ;

			_tweenerX.Ended += ( ) =>
			                   	{
			                   		if( _tweenerY.HasEnded )
			                   		{
			                   			if( Ended != null )
			                   			{
			                   				Ended( ) ;
			                   			}
			                   		}
			                   	};

			_tweenerY.Ended += ( ) =>
			                   	{
			                   		if( _tweenerX.HasEnded )
			                   		{
			                   			if( Ended != null )
			                   			{
			                   				Ended( ) ;
			                   			}
			                   		}
			                   	};
		}

		public Vector2 Position
		{
			get
			{
				return new Vector2(_tweenerX.Position, _tweenerY.Position);
			}
		}

		public void Update( GameTime gameTime )
		{
			_tweenerX.Update( gameTime ) ;
			_tweenerY.Update( gameTime ) ;
		}

		public void Reset( )
		{
			_tweenerX.Reset( ) ;
			_tweenerY.Reset( ) ;
		}
	}
}