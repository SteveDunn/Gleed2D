using System ;
using Microsoft.Xna.Framework ;
using Microsoft.Xna.Framework.Content ;
using Microsoft.Xna.Framework.Graphics ;

namespace Gleed2D.Core
{
	public interface IGame
	{
		event EventHandler<EventArgs> CanvasResized ;

		GraphicsDevice GraphicsDevice
		{
			get ;
		}

		GameTime GameTime
		{
			get ;
		}

		SpriteBatch SpriteBatch
		{
			get ;
		}

		ContentManager Content
		{
			get ;
		}

		bool HasExited
		{
			get ;
		}

		BasicEffect BasicEffect
		{
			get ;
		}

		void Exit( ) ;
		void ResizeBackBuffer( int width, int height ) ;
		void Run( ) ;
	}
}