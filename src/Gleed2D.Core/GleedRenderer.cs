using System ;
using System.Collections.Generic ;
using System.Linq ;
using Microsoft.Xna.Framework ;
using Microsoft.Xna.Framework.Graphics ;
using Microsoft.Xna.Framework.Input ;
using StructureMap ;

namespace Gleed2D.Core
{
	public class GleedRenderer : IGleedRenderer
	{
		readonly IDrawing _drawing ;
		readonly IGame _game ;
		readonly List<Action> _debugDrawActions ;

		public GleedRenderer( IDrawing drawing, IGame game )
		{
			_drawing = drawing ;
			_game = game ;
			_debugDrawActions = new List<Action>();
		}

		public void QueueForDebugDraw( Action action )
		{
			_debugDrawActions.Add( action ) ;
		}
	
		public void Draw( RendererParams rendererParams )
		{
			if( rendererParams.Camera == null )
			{
				return ;
			}

			var level = IoC.Model.Level ;

			if( !level.Visible )
			{
				return ;
			}

			var game = ObjectFactory.GetInstance<IGame>( ) ;

			game.GraphicsDevice.Clear( Constants.Instance.ColorBackground ) ;

			var extensibility = ObjectFactory.GetInstance<IExtensibility>( ) ;

			IEnumerable<IRenderer> otherRenderers = extensibility.Renderers.ToList(  ) ;

			if( !otherRenderers.Any( ) )
			{
				render( rendererParams ) ;
				return ;
			}
			
			otherRenderers.ForEach( r => r.Render( rendererParams, render ) ) ;
		}

		void render(RendererParams rendererParams)
		{
			if( rendererParams.ItemsToRender.Has( ItemsToRender.SelectionRectangle ) )
			{
				drawSelectionBoxIfNeeded( rendererParams );
			}

			if( Constants.Instance.ShowGrid )
			{
				if(rendererParams.ItemsToRender.Has( ItemsToRender.Grid))
				{
					drawGrid( rendererParams.Camera ) ;
				}
			}

			if( rendererParams.ItemsToRender.Has( ItemsToRender.Items ) )
			{
				drawAllItems( rendererParams ) ;
			}

			if(rendererParams.ItemsToRender.Has( ItemsToRender.HooksOnSelectedItems))
			{
				drawFramesAroundSelectedEditors( rendererParams ) ;
			}

			if( rendererParams.SnapPoint.Visible && rendererParams.ItemsToRender.Has( ItemsToRender.SnapPoint ) )
			{
				_game.SpriteBatch.Begin( ) ;
				
				var snappedPoint = Vector2.Transform( rendererParams.SnapPoint.Position, rendererParams.Camera.Matrix ) ;
				
				_drawing.DrawBoxFilled(
					_game.SpriteBatch, snappedPoint.X - 5, snappedPoint.Y - 5, 10, 10, Constants.Instance.ColorSelectionFirst ) ;
				
				_game.SpriteBatch.End( ) ;
			}

			if( Constants.Instance.ShowWorldOrigin )
			{
				if(rendererParams.ItemsToRender.Has( ItemsToRender.WorldOrigin))
				{
					drawWorldOrigin( rendererParams.Camera ) ;
				}
			}

			if( _debugDrawActions.Any() )
			{
				_game.SpriteBatch.Begin( ) ;

				_debugDrawActions.ForEach( a=>a() );
				
				_game.SpriteBatch.End( ) ;

				_debugDrawActions.Clear(  );
			}
		}

		void drawCurrentItemBeingCreatedIfNeeded( UserActionInEditor userActionInEditor, EntityCreation entityCreation )
		{
			if( userActionInEditor == UserActionInEditor.AddingAnItem && entityCreation.StartedCreating )
			{
				entityCreation.CurrentEditor.DrawInEditor( _game.SpriteBatch ) ;
			}
		}

		void drawSelectionBoxIfNeeded(RendererParams rendererParams)
		{
			if( rendererParams.UserActionInEditor != UserActionInEditor.CreatingSelectionBoxByDragging )
			{
				return ;
			}

			Camera camera = rendererParams.Camera ;

			Vector2 mainCameraPosition = camera.Position ;

			camera.Position *= IoC.Model.ActiveLayer.ScrollSpeed ;

			_game.SpriteBatch.Begin(
				SpriteSortMode.Deferred,
				BlendState.AlphaBlend,
				null,
				null,
				null,
				null,
				camera.Matrix ) ;

			_drawing.DrawBoxFilled( _game.SpriteBatch, rendererParams.SelectionRectangle, Constants.Instance.ColorSelectionBox) ;

			_game.SpriteBatch.End(  );

			camera.Position =mainCameraPosition ;
		}

		void drawAllItems( RendererParams rendererParams )
		{
			var level = IoC.Model.Level ;

			foreach( LayerEditor eachLayer in level.Layers )
			{
				Vector2 mainCameraPosition = rendererParams.Camera.Position ;

				rendererParams.Camera.Position *= eachLayer.ScrollSpeed ;

				_game.SpriteBatch.Begin(
					SpriteSortMode.Deferred,
					BlendState.AlphaBlend,
					null,
					null,
					null,
					null,
					rendererParams.Camera.Matrix ) ;

				eachLayer.DrawInEditor( _game.SpriteBatch ) ;

				if( eachLayer == level.ActiveLayer )
				{
					//drawSelectionBoxIfNeeded( rendererParams.UserActionInEditor, rendererParams.SelectionRectangle ) ;

					drawCurrentItemBeingCreatedIfNeeded( rendererParams.UserActionInEditor, rendererParams.EntityCreation ) ;
				}

				_game.SpriteBatch.End( ) ;

				//restore main camera position
				rendererParams.Camera.Position = mainCameraPosition ;
			}
		}

		void drawFramesAroundSelectedEditors(RendererParams rendererParams )
		{
			LevelEditor level = IoC.Model.Level ;

			IEnumerable<ItemEditor> selectedEditors = level.SelectedEditors.ToList(  ) ;

			if( selectedEditors.Any( ) )
			{
				Vector2 maincameraposition = rendererParams.Camera.Position ;

				rendererParams.Camera.Position *= selectedEditors.First( ).ParentLayer.ScrollSpeed ;

			_game.SpriteBatch.Begin(
				SpriteSortMode.Deferred,
				BlendState.AlphaBlend,
				null,
				null,
				null,
				null,
				rendererParams.Camera.Matrix ) ;

				bool first = true ;

				foreach( ItemEditor item in selectedEditors )
				{
					if( item.ItemProperties.Visible && item.ParentLayer.Visible && KeyboardStatus.IsKeyUp( Keys.Space ) )
					{
						Color color = first ? Constants.Instance.ColorSelectionFirst : Constants.Instance.ColorSelectionRest ;
						
						item.DrawSelectionFrame( _game.SpriteBatch, color ) ;

						bool rotatingOrScaling = 
							rendererParams.UserActionInEditor == UserActionInEditor.RotatingItems || 
							rendererParams.UserActionInEditor == UserActionInEditor.ScalingItems ;

						if( first  && rotatingOrScaling )
						{
							//Vector2 center = Vector2.Transform( item.ItemProperties.Position, rendererParams.Camera.Matrix ) ;
							//Vector2 mouse = Vector2.Transform( MouseStatus.WorldPosition, rendererParams.Camera.Matrix ) ;

							//todo move the line drawing into each shape and remove the matrix parameter when drawing the frame
							//_drawing.DrawLine( _game.SpriteBatch, item.ItemProperties.Position, MouseStatus.WorldPosition, Constants.Instance.ColorSelectionFirst, 1 ) ;
						}
					}

					first = false ;
				}
				
				_game.SpriteBatch.End( ) ;
				
				//restore main camera position
				rendererParams.Camera.Position = maincameraposition ;
			}
		}

		void drawGrid( Camera camera )
		{
			_game.SpriteBatch.Begin( ) ;

			int max = Constants.Instance.GridNumberOfGridLines / 2 ;

			Color gridColor = Constants.Instance.GridColor ;

			int thickness = Constants.Instance.GridLineThickness ;

			for( int x = 0; x <= max; x++ )
			{
				float gridSpacingX = Constants.Instance.GridSpacing.X ;
				Vector2 start = Vector2.Transform( new Vector2( x, -max ) * gridSpacingX, camera.Matrix ) ;
				Vector2 end = Vector2.Transform( new Vector2( x, max ) * gridSpacingX, camera.Matrix ) ;
				_drawing.DrawLine( _game.SpriteBatch, start, end, gridColor, thickness ) ;
				start = Vector2.Transform( new Vector2( -x, -max ) * gridSpacingX, camera.Matrix ) ;
				end = Vector2.Transform( new Vector2( -x, max ) * gridSpacingX, camera.Matrix ) ;
				_drawing.DrawLine( _game.SpriteBatch, start, end, gridColor, thickness ) ;
			}

			for( int y = 0; y <= max; y++ )
			{
				float gridSpacingY = Constants.Instance.GridSpacing.Y ;

				Vector2 start = Vector2.Transform( new Vector2( -max, y ) * gridSpacingY, camera.Matrix ) ;
				Vector2 end = Vector2.Transform( new Vector2( max, y ) * gridSpacingY, camera.Matrix ) ;
				_drawing.DrawLine( _game.SpriteBatch, start, end, gridColor, thickness ) ;
				start = Vector2.Transform( new Vector2( -max, -y ) * gridSpacingY, camera.Matrix ) ;
				end = Vector2.Transform( new Vector2( max, -y ) * gridSpacingY, camera.Matrix ) ;
				_drawing.DrawLine( _game.SpriteBatch, start, end, gridColor, thickness ) ;
			}

			_game.SpriteBatch.End( ) ;
		}

		void drawWorldOrigin( Camera camera )
		{
			_game.SpriteBatch.Begin( ) ;

			Vector2 worldOrigin = Vector2.Transform( Vector2.Zero, camera.Matrix ) ;

			_drawing.DrawLine(
				_game.SpriteBatch,
				worldOrigin + new Vector2( -20, 0 ),
				worldOrigin + new Vector2( +20, 0 ),
				Constants.Instance.WorldOriginColor,
				Constants.Instance.WorldOriginLineThickness ) ;

			_drawing.DrawLine(
				_game.SpriteBatch,
				worldOrigin + new Vector2( 0, -20 ),
				worldOrigin + new Vector2( 0, 20 ),
				Constants.Instance.WorldOriginColor,
				Constants.Instance.WorldOriginLineThickness ) ;

			_game.SpriteBatch.End( ) ;
		}
	}
}