using Microsoft.Xna.Framework ;
using Microsoft.Xna.Framework.Graphics ;

namespace Gleed2D.Core
{
	public interface IDrawing
	{
		Texture2D CreateCircleTexture(GraphicsDevice graphicsDevice, int radius, int borderWidth,
			int borderInnerTransitionWidth, int borderOuterTransitionWidth,
			Color color, Color borderColor) ;

		void DrawPixel(SpriteBatch spriteBatch, int x, int y, Color color) ;
		void DrawBox(SpriteBatch spriteBatch, Rectangle r, Color c, int lineWidth) ;
		void DrawBoxFilled(SpriteBatch sb, float x, float y, float w, float h, Color c) ;
		void DrawBoxFilled(SpriteBatch sb, Vector2 upperLeft, Vector2 lowerRight, Color c) ;
		void DrawBoxFilled(SpriteBatch sb, Rectangle r, Color c) ;
		void DrawCircle(SpriteBatch sb, Vector2 position, float radius, Color c, int linewidth) ;
		void DrawCircleFilled(SpriteBatch spriteBatch, Vector2 position, float radius, Color color) ;

		void DrawLine(SpriteBatch spriteBatch, 
			float x1, 
			float y1, 
			float x2, 
			float y2, 
			Color color, 
			int lineWidth) ;

		void DrawLine(SpriteBatch spriteBatch, Vector2 startpos, Vector2 endpos, Color c, int linewidth) ;
		void DrawPath(SpriteBatch spriteBatch, Vector2[] points, Color color, int lineWidth) ;
		void DrawPolygon(SpriteBatch spriteBatch, Vector2[] points, Color color, int lineWidth) ;
		void DrawPolygonFilled( SpriteBatch spriteBatch, Vector2[ ] worldPoints, Color color ) ;
	}
}