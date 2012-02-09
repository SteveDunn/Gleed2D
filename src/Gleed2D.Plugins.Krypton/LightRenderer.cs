using System ;
using System.ComponentModel.Composition ;
using System.Linq ;
using Gleed2D.Core ;
using Gleed2D.InGame.Krypton ;
using Krypton ;
using Krypton.Lights ;
using Microsoft.Xna.Framework ;
using Microsoft.Xna.Framework.Content ;
using Microsoft.Xna.Framework.Graphics ;
using Microsoft.Xna.Framework.Input ;
using StructureMap ;

namespace Gleed2D.Plugins.Krypton
{
	[Export( typeof( IRenderer ) )]
	public class LightRenderer : IRenderer
	{
		readonly KryptonEngine _krypton;
		readonly IGame _game ;

		public LightRenderer( )
		{
			_game = ObjectFactory.GetInstance<IGame>( ) ;

			var content = new ResourceContentManager( ( (Game) _game ).Services, Resource1.ResourceManager ) ;
			
			var effect = content.Load<Effect>( @"KryptonEffect" ) ;
			_krypton=new KryptonEngine( (Game) _game, effect );
			
			_krypton.Initialize(  );

			IoC.Model.ItemsAddedOrRemoved += ( s, e ) => rebuild( )  ;
			IoC.Model.ItemChanged += ( s, e ) => rebuild( )  ;
			IoC.Model.NewModelLoaded += ( s, e ) => rebuild( ) ;
		}

		void rebuild( )
		{
			_krypton.Lights.Clear(  );
			_krypton.Hulls.Clear(  );

			LayerEditor activeLayer = IoC.Model.ActiveLayer ;
			if( activeLayer == null )
			{
				return ;
			}
			
			activeLayer.OfType<LightEditor>( ).ForEach(
				light =>
					{
						var props = (LightProperties) light.ItemProperties ;

						Texture2D texture ;
						if (props.TypeOfLight == TypeOfLight.Point)
						{
							 texture = LightTextureBuilder.CreatePointLight(_game.GraphicsDevice, props.TextureSize);
						}
						else
						{
							texture = LightTextureBuilder.CreateConicLight( _game.GraphicsDevice, props.TextureSize, props.FieldOfView ) ;
						}

						if( texture == null )
						{
							throw new InvalidOperationException(@"Cannot create light of type {0}.".FormatWith( props.TypeOfLight ));
						}

						_krypton.Lights.Add( new Light2D
							{
								Angle=props.Rotation,
								Color = props.Color,
								Intensity = props.Intensity,
								Fov = props.FieldOfView,
								Position = props.Position,
								IsOn = props.IsOn,
								Range = props.Range,
								ShadowType = convertShadowType(props.ShadowType),
								Texture = texture
							});
					} ) ;

			activeLayer.OfType<RectangularHullEditor>( ).ForEach(
				light =>
					{
						var props = (RectangularHullProperties) light.ItemProperties ;

						ShadowHull shadowHull = ShadowHull.CreateRectangle( new Vector2(props.Width, props.Height) ) ;
						shadowHull.Angle = props.Rotation ;
						shadowHull.Opacity = props.Opacity ;
						shadowHull.Position = props.Position + new Vector2( props.Width/2, props.Height /2 ) ;
						shadowHull.Scale = props.Scale ;
						shadowHull.Visible = props.Visible ;

						_krypton.Hulls.Add( shadowHull ) ;
					});

			activeLayer.OfType<CircularHullEditor>( ).ForEach(
				light =>
					{
						var props = (CircularHullProperties) light.ItemProperties ;

						ShadowHull shadowHull = ShadowHull.CreateCircle(props.Radius,props.Sides) ;
						shadowHull.Opacity = props.Opacity ;
						shadowHull.Position = props.Position  ;
						shadowHull.Scale = props.Scale ;
						shadowHull.Visible = props.Visible ;

						_krypton.Hulls.Add( shadowHull ) ;
					});

			activeLayer.OfType<ConvexHullEditor>( ).ForEach(
				light =>
					{
						var props = (ConvexHullProperties) light.ItemProperties ;

						Vector2[ ] worldPoints = props.WorldPoints.ToArray( ) ;

						// === DON'T SET THE POSITION!! ===
						ShadowHull shadowHull = ShadowHull.CreateConvex( ref worldPoints ) ;
						shadowHull.Opacity = props.Opacity ;
						shadowHull.Visible = props.Visible ;

						_krypton.Hulls.Add( shadowHull ) ;
					} ) ;

			activeLayer.OfType<PreShapedConvexHullEditor>( ).ForEach(
				light =>
					{
						var props = (ConvexHullProperties) light.ItemProperties ;

						Vector2[ ] worldPoints = props.WorldPoints.ToArray( ) ;

						// === DON'T SET THE POSITION!! ===
						ShadowHull shadowHull = ShadowHull.CreateConvex( ref worldPoints ) ;
						shadowHull.Opacity = props.Opacity ;
						shadowHull.Visible = props.Visible ;

						_krypton.Hulls.Add( shadowHull ) ;
					} ) ;
		}

		ShadowType convertShadowType( TypeOfShadow shadowType )
		{
			if( shadowType == TypeOfShadow.Illuminated )
			{
				return ShadowType.Illuminated;
			}

			if( shadowType == TypeOfShadow.Occluded )
			{
				return ShadowType.Occluded;
			}

			if( shadowType == TypeOfShadow.Solid )
			{
				return ShadowType.Solid;
			}

			throw new NotSupportedException( @"Don't know how to handle a shadow type of '{0}'.".FormatWith( shadowType ) ) ;
		}

		public void Render( RendererParams rendererParams, Action<RendererParams> defaultRenderer )
		{
			var lightingState = ObjectFactory.GetInstance<ILightingState>( ) ;
			if( !lightingState.LightingOn )
			{
				defaultRenderer( rendererParams ) ;
				
				return ;
			}
            
			// Create a world view projection matrix to use with krypton
            // Assign the matrix and pre-render the lightmap.
            // Make sure not to change the position of any lights or shadow hulls after this call, as it won't take effect till the next frame!
            _krypton.Matrix = rendererParams.Camera.Matrix;
            
			_krypton.AmbientColor = new Color( 64, 64, 64 ) ;
			_krypton.SpriteBatchCompatablityEnabled = true ;
			_krypton.Bluriness = 23 ;
			_krypton.CullMode = CullMode.None ;

			_krypton.LightMapPrepare();

			_game.GraphicsDevice.Clear( Color.White ) ;

            // ----- DRAW STUFF HERE ----- //
            // By drawing here, you ensure that your scene is properly lit by krypton.
            // Drawing after KryptonEngine.Draw will cause you objects to be drawn on top of the lightmap (can be useful, fyi)
            // ----- DRAW STUFF HERE ----- //
			rendererParams.ItemsToRender = ItemsToRender.Everything ;
			defaultRenderer( rendererParams ) ;

            // Draw hulls
            debugDrawHulls( true);

            // Draw krypton (This can be omited if krypton is in the Component list. It will simply draw krypton when base.Draw is called
            _krypton.Draw(_game.GameTime);


			const ItemsToRender everythingExceptItemsThemselves = ItemsToRender.Everything ^ ItemsToRender.Items ;
			
			rendererParams.ItemsToRender = everythingExceptItemsThemselves ;
			
			defaultRenderer( rendererParams ) ;

            if (Keyboard.GetState().IsKeyDown(Keys.H))
            {
                // Draw hulls
                debugDrawHulls( false);
            }

            if (Keyboard.GetState().IsKeyDown(Keys.L))
            {
                debugDrawLights();
            }
		}

		void debugDrawLights()
        {
            _krypton.RenderHelper.Effect.CurrentTechnique = _krypton.RenderHelper.Effect.Techniques["DebugDraw"];

            _game.GraphicsDevice.RasterizerState = new RasterizerState
            {
                CullMode = CullMode.None,
                FillMode = FillMode.WireFrame,
            };

            // Clear the helpers vertices
            _krypton.RenderHelper.ShadowHullVertices.Clear();
            _krypton.RenderHelper.ShadowHullIndicies.Clear();

            foreach (Light2D light in _krypton.Lights)
            {
                _krypton.RenderHelper.BufferAddBoundOutline(light.Bounds);
            }

            foreach (var effectPass in _krypton.RenderHelper.Effect.CurrentTechnique.Passes)
            {
                effectPass.Apply();
                _krypton.RenderHelper.BufferDraw();
            }
        }

        void debugDrawHulls( bool drawSolid)
        {
            _krypton.RenderHelper.Effect.CurrentTechnique = _krypton.RenderHelper.Effect.Techniques["DebugDraw"];

            _game.GraphicsDevice.RasterizerState = new RasterizerState
            	{
                CullMode = CullMode.None,
                FillMode = drawSolid ? FillMode.Solid : FillMode.WireFrame,
            };

            // Clear the helpers vertices
            _krypton.RenderHelper.ShadowHullVertices.Clear();
            _krypton.RenderHelper.ShadowHullIndicies.Clear();

            foreach (var hull in _krypton.Hulls)
            {
                _krypton.RenderHelper.BufferAddShadowHull(hull);
            }

            foreach (var effectPass in _krypton.RenderHelper.Effect.CurrentTechnique.Passes)
            {
                effectPass.Apply();
                _krypton.RenderHelper.BufferDraw();
            }
        }
	}
}