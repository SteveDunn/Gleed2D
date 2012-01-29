using System ;
using Microsoft.Xna.Framework ;
using Microsoft.Xna.Framework.Graphics ;

namespace Gleed2D.Core
{
	public class Drawing : IDrawing
	{
        const int CIRCLE_TEXTURE_RADIUS = 512;

		readonly Texture2D _singlePixelTexture;
		readonly Texture2D _circleTexture;
		readonly BasicEffect _basicEffect ;
		IGame _game ;

		public Drawing(IGame game)
		{
			_game = game ;
			
			_singlePixelTexture = new Texture2D(game.GraphicsDevice, 1, 1);
            _singlePixelTexture.SetData(new[] { Color.White });

			game.CanvasResized += ( s, e ) => resizeBasicEffect( ) ;
            
			_circleTexture = CreateCircleTexture(game.GraphicsDevice, CIRCLE_TEXTURE_RADIUS, 0, 1, 1, Color.White, Color.White);

			GraphicsDevice graphicsDevice = game.GraphicsDevice ;

			_basicEffect = new BasicEffect( graphicsDevice )
				{
					VertexColorEnabled = true
				} ;

			resizeBasicEffect( ) ;

			_basicEffect.World = Matrix.Identity ;
		}

		void resizeBasicEffect( )
		{
			GraphicsDevice graphicsDevice = _game.GraphicsDevice ;

			var viewMatrix = Matrix.CreateLookAt(
				new Vector3( 0, 0, graphicsDevice.Viewport.Width ),
				Vector3.Zero,
				Vector3.Up
				) ;

			var projectionMatrix = Matrix.CreateOrthographicOffCenter(
				0,
			    graphicsDevice.Viewport.Width,
			    graphicsDevice.Viewport.Height,
				0,
			    .1f,
			    1000000f ) ;

			_basicEffect.Projection = projectionMatrix ;
			_basicEffect.View = viewMatrix ;
		}

		public Texture2D CreateCircleTexture(
			GraphicsDevice graphicsDevice, 
			int radius, 
			int borderWidth,
			int borderInnerTransitionWidth, 
			int borderOuterTransitionWidth,
            Color color, 
			Color borderColor)
        {
            int diameter = radius * 2;
            
			var center = new Vector2(radius, radius);
            
            var circle = new Texture2D(graphicsDevice, diameter, diameter, false,  SurfaceFormat.Color);
            
			var colors = new Color[diameter * diameter];
            
			int y = -1;
            
			for (int i = 0; i < colors.Length; i++)
            {
                int x = i % diameter;

                if (x == 0)
                {
                    y += 1;
                }

                Vector2 diff = new Vector2(x, y) - center;
                float length = diff.Length(); // distance.Length();

                if (length > radius)
                {
                    colors[i] = Color.Transparent;
                }
                else if (length >= radius - borderOuterTransitionWidth)
                {
                    float transitionAmount = (length - (radius - borderOuterTransitionWidth)) / borderOuterTransitionWidth;
                    
					transitionAmount = 255 * (1 - transitionAmount);
                    
					colors[i] = new Color(borderColor.R, borderColor.G, borderColor.B, (byte)transitionAmount);
                }
                else if (length > radius - (borderWidth + borderOuterTransitionWidth))
                {
                    colors[i] = borderColor;
                }
                else if (length >= radius - (borderWidth + borderOuterTransitionWidth + borderInnerTransitionWidth))
                {
                    float transitionAmount = (length -
                                              (radius -
                                               (borderWidth + borderOuterTransitionWidth + borderInnerTransitionWidth))) /
                                             (borderInnerTransitionWidth + 1);
            
					colors[i] = new Color((byte)MathHelper.Lerp(color.R, borderColor.R, transitionAmount),
                                          (byte)MathHelper.Lerp(color.G, borderColor.G, transitionAmount),
                                          (byte)MathHelper.Lerp(color.B, borderColor.B, transitionAmount));
                }
                else
                {
                    colors[i] = color;
                }
            }
            
			circle.SetData(colors);
            
			return circle;
        }

        public void DrawPixel(SpriteBatch spriteBatch, int x, int y, Color color)
        {
            spriteBatch.Draw(_singlePixelTexture, new Vector2(x, y), color);
        }

        public void DrawBox(SpriteBatch spriteBatch, Rectangle r, Color c, int lineWidth)
        {
            DrawLine(spriteBatch, r.Left, r.Top, r.Right, r.Top, c, lineWidth);
            DrawLine(spriteBatch, r.Right, r.Y, r.Right, r.Bottom, c, lineWidth);
            DrawLine(spriteBatch, r.Right, r.Bottom, r.Left, r.Bottom, c, lineWidth);
            DrawLine(spriteBatch, r.Left, r.Bottom, r.Left, r.Top, c, lineWidth);
        }

        public void DrawBoxFilled(SpriteBatch sb, float x, float y, float w, float h, Color c)
        {
            sb.Draw(_singlePixelTexture, new Rectangle((int)x, (int)y, (int)w, (int)h), c);
        }

        public void DrawBoxFilled(SpriteBatch sb, Vector2 upperLeft, Vector2 lowerRight, Color c)
        {
            Rectangle r = Extensions.RectangleFromVectors(upperLeft, lowerRight);
            sb.Draw(_singlePixelTexture, r, c);
        }

        public void DrawBoxFilled(SpriteBatch sb, Rectangle r, Color c)
        {
            sb.Draw(_singlePixelTexture, r, c);
        }

        public void DrawCircle(SpriteBatch sb, Vector2 position, float radius, Color c, int linewidth)
        {
            DrawPolygon(sb, makeCircle(position, radius, 32), c, linewidth);
        }

        public void DrawCircleFilled(SpriteBatch spriteBatch, Vector2 position, float radius, Color color)
        {
        	spriteBatch.Draw(
        		_circleTexture,
        		position,
        		null,
        		color,
        		0,
        		new Vector2( CIRCLE_TEXTURE_RADIUS, CIRCLE_TEXTURE_RADIUS ),
        		radius / CIRCLE_TEXTURE_RADIUS,
        		SpriteEffects.None,
				0);
        }

        public void DrawLine(SpriteBatch spriteBatch, 
			float x1, 
			float y1, 
			float x2, 
			float y2, 
			Color color, 
			int lineWidth)
        {
            var v = new Vector2(x2 - x1, y2 - y1);
            var rotation = (float)Math.Atan2(y2 - y1, x2 - x1);
            
			spriteBatch.Draw(
				_singlePixelTexture, 
				new Vector2(x1, y1), 
				new Rectangle(1, 1, 1, lineWidth), 
				color, 
				rotation,
                new Vector2(0, lineWidth / 2), 
				new Vector2(v.Length(), 1), 
				SpriteEffects.None, 
				0);
        }
        
		public void DrawLine(SpriteBatch spriteBatch, Vector2 startpos, Vector2 endpos, Color c, int linewidth)
        {
            DrawLine(
				spriteBatch, 
				startpos.X, 
				startpos.Y, 
				endpos.X, 
				endpos.Y, 
				c, 
				linewidth);
        }

        public void DrawPath(SpriteBatch spriteBatch, Vector2[] points, Color color, int lineWidth)
        {
            for (int i = 0; i < points.Length - 1; i++)
            {
                DrawLine(spriteBatch, points[i], points[i + 1], color, lineWidth);
            }
        }

        public void DrawPolygon(SpriteBatch spriteBatch, Vector2[] points, Color color, int lineWidth)
        {
            DrawPath(spriteBatch, points, color, lineWidth);
            
			DrawLine(spriteBatch, points[points.Length-1], points[0], color, lineWidth);
        }

		public void DrawPolygonFilled( SpriteBatch spriteBatch, Vector2[ ] worldPoints, Color color )
		{
			Camera camera = IoC.Canvas.Camera ;

			_basicEffect.World = camera.Matrix ;

			var vertices = new VertexPositionColor[ worldPoints.Length ] ;

			for (int i = 0; i < worldPoints.Length; i++)
			{
				vertices[ i ] = new VertexPositionColor( new Vector3( worldPoints[ i ], 0 ), color ) ;
			}

			Vector2[ ] outputVertices ;
			short[ ] outputIndicies ;
			Vertices.Triangulate( worldPoints, Vertices.WindingOrder.Clockwise, out outputVertices, out outputIndicies ) ;

			if (outputIndicies.Length > 0)
			{
				foreach( EffectPass pass in _basicEffect.CurrentTechnique.Passes )
				{
					pass.Apply( ) ;

					spriteBatch.GraphicsDevice.DrawUserIndexedPrimitives(
						Microsoft.Xna.Framework.Graphics.PrimitiveType.TriangleList,
						vertices,
						0,
						vertices.Length,
						outputIndicies,
						0,
						outputIndicies.Length /3 ) ;
				}
			}
		}

		Vector2[] makeCircle(Vector2 position, float radius, int numpoints)
        {
            var points = new Vector2[numpoints];
        
			float angle = 0;
            
			for (int i = 0; i < numpoints; i++)
            {
                float x = (float)Math.Cos(angle) * radius;
                float y = (float)Math.Sin(angle) * radius;
                points[i] = position + new Vector2(x, y);
                angle += MathHelper.TwoPi / numpoints;
            }
            
			return points;
        }



    }
}
