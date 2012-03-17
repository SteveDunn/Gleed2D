using System ;
using System.ComponentModel ;
using System.Drawing ;
using System.IO ;
using System.Windows.Forms ;
using System.Xml.Linq ;
using Gleed2D.Core ;
using Gleed2D.InGame ;
using Gleed2D.InGame.Krypton ;
using JetBrains.Annotations ;
using Microsoft.Xna.Framework ;
using Microsoft.Xna.Framework.Graphics ;
using StructureMap ;
using Color = Microsoft.Xna.Framework.Color ;
using Keys = Microsoft.Xna.Framework.Input.Keys ;
using Rectangle = Microsoft.Xna.Framework.Rectangle ;

namespace Gleed2D.Plugins.Krypton
{
	public class TexturedHullEditor : ItemEditor
	{
		int _frameIndex ;
		IMainForm _mainForm ;
		TexturedHullProperties _properties ;
		Rectangle _boundingRectangle ;
		Color[ ] _colorDataForTexture ;

		int _column ;
		int _columns ;
		int _frameCount ;
		float _frameTime ;
		Vector2[ ] _polygon ;
		bool _reversed ;
		int _row ;
		int _rows ;

		Texture2D _texture ;
		float _time ;
		Matrix _transform ;

		[UsedImplicitly]
		public TexturedHullEditor( )
		{
			_properties = new TexturedHullProperties( ) ;
		}

		public override string NameSeed
		{
			get
			{
				return @"TexturedHull";
			}
		}

		/// <summary>
		/// Gets the object that represents the properties in the property grid.
		/// This is usually the <see cref="ItemPropertiesWrapper{T}"/> type but you 
		/// can use your own by overriding this.
		/// </summary>
		public override ICustomTypeDescriptor ObjectForPropertyGrid
		{
			get
			{
				var wrapper = new ItemPropertiesWrapper<TexturedHullProperties>( _properties ) ;
				wrapper.Customise( ( ) => _properties.Rotation ).SetDescription( "The item's rotation in radians." ) ;
				wrapper.Customise( ( ) => _properties.Scale ).SetDescription( "The item's scale vector." ) ;
				wrapper.Customise( ( ) => _properties.TintColor ).SetDescription( "The Color to tint the texture with. Use white for no tint." ) ;
				wrapper.Customise( ( ) => _properties.FlipHorizontally ).SetDisplayName( @"Flip horizontally" ).SetDescription( "If true, the texture is flipped horizontally when drawn." ) ;
				wrapper.Customise( ( ) => _properties.FlipVertically ).SetDisplayName( @"Flip vertically" ).SetDescription( "If true, the texture is flipped vertically when drawn." ) ;
				wrapper.Customise( ( ) => _properties.Origin ).SetDescription( "The item's origin in texture space ([0,0] is upper left corner)." ) ;
				wrapper.Customise( ( ) => _properties.IsTemplate ).SetDisplayName( @"Is a template?" ).SetDescription( "Can be used as template for various objects (chains, tracks, spawners), will not be drawn in this exact location" ) ;
				wrapper.Customise( ( ) => _properties.TexturePathRelativeToContentRoot ).Hide( ) ;
				wrapper.Customise( ( ) => _properties.AssetName ).Hide( ) ;

				return wrapper ;
			}
		}


		public override bool CanScale
		{
			get
			{
				return true ;
			}
		}

		public override float Rotation
		{
			get
			{
				return _properties.Rotation ;
			}
			set
			{
				_properties.Rotation = value ;
			}
		}

		public override Vector2 Scale
		{
			get
			{
				return _properties.Scale ;
			}
			set
			{
				_properties.Scale = value ;

				WhenUpdatedByUi( ) ;
			}
		}

		public Color TintColor
		{
			get
			{
				return _properties.TintColor ;
			}
			set
			{
				_properties.TintColor = value ;

				WhenUpdatedByUi( ) ;
			}
		}

		bool FlipHorizontally
		{
			get
			{
				return _properties.FlipHorizontally ;
			}
			set
			{
				_properties.FlipHorizontally = value ;

				WhenUpdatedByUi( ) ;
			}
		}

		bool FlipVertically
		{
			get
			{
				return _properties.FlipVertically ;
			}
			set
			{
				_properties.FlipVertically = value ;

				WhenUpdatedByUi( ) ;
			}
		}

		public bool IsTemplate
		{
			get
			{
				return _properties.IsTemplate ;
			}
			set
			{
				_properties.IsTemplate = value ;

				WhenUpdatedByUi( ) ;
			}
		}

		public override string Name
		{
			get
			{
				return _properties.Name ;
			}
		}

		public override ImageProperties Icon
		{
			get
			{
				var imageRepository = ObjectFactory.GetNamedInstance<IImageRepository>( @"iconImages" ) ;

				string name = @"texture_{0}".FormatWith( _properties.TexturePathRelativeToContentRoot ) ;

				if( !imageRepository.ContainsImage( name ) )
				{
					using( var memoryStream = new MemoryStream( ) )
					{
						_texture.SaveAsPng( memoryStream, 64, 64 ) ;

						memoryStream.Position = 0 ;

						Image image = Image.FromStream( memoryStream ) ;

						imageRepository.Set( new ImageProperties( name, image ) ) ;
					}
				}

				return imageRepository.GetByName( name ) ;
			}
		}

		public override ItemProperties ItemProperties
		{
			get
			{
				return _properties ;
			}
		}

		public override void RecreateFromXml( LayerEditor parentLayer, XElement xml )
		{
			base.RecreateFromXml( parentLayer, xml );

			_polygon = new Vector2[ 4 ] ;

			ParentLayer = parentLayer ;

			_properties = xml.Element( @"TexturedHullProperties" ).DeserializedAs<TexturedHullProperties>( ) ;

			initialiseTexture( _properties.TexturePathRelativeToContentRoot ) ;

			WhenUpdatedByUi( ) ;
		}

		public override void CreateInDesignMode(LayerEditor parentLayer, IEntityCreationProperties creationProperties)
		{
			_polygon = new Vector2[ 4 ] ;

			ParentLayer = parentLayer ;

			string fullPath = null;// creationProperties.Parameters[@"FullPath"];

			initialiseTexture( fullPath ) ;

			_properties = new TexturedHullProperties
				{
					//todo: need filename
					Position = MouseStatus.WorldPosition,
					TexturePathRelativeToContentRoot = makeRelativePath( new PathToFolder( parentLayer.ParentLevel.ContentRootFolder), fullPath ),
					CustomProperties = new CustomProperties( ),
					Visible = true,
					Scale = Vector2.One,
					TintColor = Color.White,
				} ;

			_properties.Origin = getTextureOrigin( ) ;

			WhenUpdatedByUi( ) ;
		}

		/// <summary>
		/// Creates a relative path from one file or folder to another.
		/// </summary>
		/// <param name="fromPath">Contains the directory that defines the start of the relative path.</param>
		/// <param name="toPath">Contains the path that defines the endpoint of the relative path.</param>
		/// <returns>The relative path from the start directory to the end path.</returns>
		/// <exception cref="ArgumentNullException"></exception>
		static string makeRelativePath( PathToFolder fromPath, string toPath )
		{
			var fromUri = new Uri( fromPath.AbsolutePath ) ;
			var toUri = new Uri( toPath ) ;

			Uri relativeUri = fromUri.MakeRelativeUri( toUri ) ;
			string relativePath = Uri.UnescapeDataString( relativeUri.ToString( ) ) ;

			return relativePath.Replace( '/', Path.DirectorySeparatorChar ) ;
		}

		public override bool CanRotate( )
		{
			return true ;
		}

		Vector2 getTextureOrigin( )
		{
			switch( Constants.Instance.DefaultTextureOrigin )
			{
				case InternalPoint.Middle:
					if( _properties.CustomProperties.ContainsKey( "Animated" ) )
					{
						Vector2 dimensions = (Vector2) _properties.CustomProperties[ "FrameDimensions" ].Value ;

						return new Vector2( dimensions.X / 2, dimensions.Y / 2 ) ;
					}

					return new Vector2( _texture.Width / 2, _texture.Height / 2 ) ;

				case InternalPoint.Centroid:
					var data = new uint[ _texture.Width * _texture.Height ] ;
					_texture.GetData( data ) ;
					Vertices verts = Vertices.CreatePolygon( data, _texture.Width, _texture.Height ) ;
					return verts.GetCentroid( ) ;

				case InternalPoint.TopLeft:
					{
						return new Vector2( 0, 0 ) ;
					}

				case InternalPoint.TopRight:
					{
						return new Vector2( _texture.Width, 0 ) ;
					}

				case InternalPoint.BottomLeft:
					{
						return new Vector2( 0, _texture.Height ) ;
					}

				case InternalPoint.BottomRight:
					{
						return new Vector2( _texture.Width, _texture.Height ) ;
					}
			}

			return Vector2.Zero ;
		}

		void initialiseTexture( string textureFilename )
		{
			var textureStore = ObjectFactory.GetInstance<ITextureStore>( ) ;
			var game = ObjectFactory.GetInstance<IGame>( ) ;

			string absolutePath =
				Path.Combine( @"{0}\".FormatWith(ParentLayer.ParentLevel.ContentRootFolder), textureFilename ) ;

			if( !File.Exists( absolutePath ) )
			{
				DialogResult dr =
					MessageBox.Show(
						@"The file ""{0}"" doesn't exist!
The texture path is a combination of the Level's ContentRootFolder and the TextureItem's relative path.
Please adjust the XML file before trying to load this level again.
For now, a dummy texture will be used. Continue loading the level?".FormatWith(absolutePath),
						@"Error loading texture file",
						MessageBoxButtons.YesNo,
						MessageBoxIcon.Question ) ;

				if( dr == DialogResult.No )
				{
					return ;
				}

				_texture = textureStore.DummyTexture ;
			}
			else
			{
				_texture = textureStore.FromFile( game.GraphicsDevice, absolutePath ) ;
			}

			//for per-pixel-collision
			_colorDataForTexture = new Color[ _texture.Width * _texture.Height ] ;
			_texture.GetData( _colorDataForTexture ) ;
		}

		IMainForm summonMainForm( )
		{
			if( _mainForm == null )
			{
				_mainForm = ObjectFactory.GetInstance<IMainForm>( ) ;
			}

			return _mainForm ;
		}

		public override bool ContainsPoint( Vector2 point )
		{
			if( _boundingRectangle.Contains( (int) point.X, (int) point.Y ) )
			{
				return intersectPixels( point ) ;
			}

			return false ;
		}

		bool intersectPixels( Vector2 worldpos )
		{
			Vector2 positionInB = Vector2.Transform( worldpos, Matrix.Invert( _transform ) ) ;

			var xB = (int) Math.Round( positionInB.X ) ;
			var yB = (int) Math.Round( positionInB.Y ) ;

			if( _properties.FlipHorizontally )
			{
				xB = _texture.Width - xB ;
			}

			if( _properties.FlipVertically )
			{
				yB = _texture.Height - yB ;
			}

			// If the pixel lies within the bounds of B
			if( 0 <= xB && xB < _texture.Width && 0 <= yB && yB < _texture.Height )
			{
				Color colorB = _colorDataForTexture[ xB + yB * _texture.Width ] ;
				if( colorB.A != 0 )
				{
					return true ;
				}
			}
			return false ;
		}

		public override void OnMouseButtonUp( Vector2 mouseWorldPos )
		{
		}

		public override void SetPosition( Vector2 position )
		{
			ItemProperties.Position = position ;

			WhenUpdatedByUi( ) ;
		}

		public override void UserInteractionDuringCreation( )
		{
			SetPosition( MouseStatus.WorldPosition ) ;

			if( MouseStatus.IsNewLeftMouseButtonClick( ) )
			{
				PreviewEndedReadyForCreation( this, EventArgs.Empty ) ;
			}

			WhenUpdatedByUi( ) ;
		}

		public override void DrawSelectionFrame( SpriteBatch spriteBatch, Color color )
		{
			//todo: remove
			Matrix matrix = Matrix.Identity ;

			var drawing = ObjectFactory.GetInstance<IDrawing>( ) ;

			var poly = new Vector2[ 4 ] ;
			Vector2.Transform( _polygon, ref matrix, poly ) ;

			drawing.DrawPolygon( spriteBatch, poly, color, 2 ) ;

			foreach( Vector2 eachVector in poly )
			{
				drawing.DrawCircleFilled( spriteBatch, eachVector, 4, color ) ;
			}

			Vector2 origin = Vector2.Transform( _properties.Position, matrix ) ;

			drawing.DrawBoxFilled( spriteBatch, origin.X - 5, origin.Y - 5, 10, 10, color ) ;
		}

		public override void DrawInEditor( SpriteBatch spriteBatch )
		{
			var game = ObjectFactory.GetInstance<IGame>( ) ;

			if( !_properties.Visible )
			{
				return ;
			}

			SpriteEffects se = SpriteEffects.None ;
			if( _properties.FlipHorizontally )
				se |= SpriteEffects.FlipHorizontally ;
			if( _properties.FlipVertically )
				se |= SpriteEffects.FlipVertically ;
			Color c = _properties.TintColor ;
			if( IsHovering && Constants.Instance.EnableHighlightOnMouseOver )
				c = Constants.Instance.ColorHighlight ;
			if( _properties.CustomProperties.ContainsKey( "Animated" ) )
			{
				var dimensions = (Vector2) _properties.CustomProperties[ "FrameDimensions" ].Value ;

				_properties.Origin = new Vector2( dimensions.X / 2, dimensions.Y / 2 ) ;
				_frameCount = int.Parse( (string) _properties.CustomProperties[ "FrameCount" ].Value ) ;
				_frameTime = 1 / float.Parse( (string) _properties.CustomProperties[ "FrameRate" ].Value ) ;
				_columns = (int) ( _texture.Width / dimensions.X ) ;
				_rows = (int) ( _texture.Height / dimensions.Y ) ;
				_reversed = (bool) _properties.CustomProperties[ "Reversed" ].Value ;

				//  Rectangle temprectangle = new Rectangle(0, 0, (int)dimensions.X, (int)dimensions.Y);
				if( _column < 0 )
				{
					_column = 0 ;
				}
				if( _row < 0 )
				{
					_row = 0 ;
				}

				_time += (float) game.GameTime.ElapsedGameTime.TotalSeconds ;

				if( _time > _frameTime )
				{
					if( !_reversed )
					{
						computeFrameIndex( ) ;
					}
					else
					{
						reversedFrameIndex( ) ;
					}
				}

				var source = new Rectangle(
					(int) dimensions.X * _column, (int) dimensions.Y * _row, (int) dimensions.X, (int) dimensions.Y ) ;

				spriteBatch.Draw( _texture, _properties.Position, source, c, Rotation, _properties.Origin, Scale, se, 0 ) ;
			}
			else
			{
				spriteBatch.Draw( _texture, _properties.Position, null, c, Rotation, _properties.Origin, Scale, se, 0 ) ;
			}
		}

		public override void OnMouseButtonDown( Vector2 mouseWorldPos )
		{
			IsHovering = false ;

			summonMainForm( ).SetCursorForCanvas( Cursors.SizeAll ) ;
		}

		public override void OnMouseOver( Vector2 mouseWorldPos )
		{
			IsHovering = true ;
		}

		protected override void WhenUpdatedByUi( )
		{
			_transform =
				Matrix.CreateTranslation( new Vector3( -_properties.Origin.X, -_properties.Origin.Y, 0.0f ) ) *
					Matrix.CreateScale( Scale.X, Scale.Y, 1 ) *
						Matrix.CreateRotationZ( Rotation ) *
							Matrix.CreateTranslation( new Vector3( _properties.Position, 0.0f ) ) ;

			Vector2 leftTop = Vector2.Zero ;
			Vector2 leftBottom ;
			Vector2 rightTop ;
			Vector2 rightBottom ;

			if( _properties.CustomProperties.ContainsKey( "Animated" ) )
			{
				var dimensions = (Vector2) _properties.CustomProperties[ "FrameDimensions" ].Value ;
				leftBottom = new Vector2( 0, dimensions.X ) ;

				rightTop = new Vector2( dimensions.X, 0 ) ;

				rightBottom = new Vector2( dimensions.X, dimensions.Y ) ;

				_column = 0 ;
				_row = 0 ;
				_frameIndex = 0 ;
			}
			else
			{
				leftBottom = new Vector2( 0, _texture.Height ) ;
				rightTop = new Vector2( _texture.Width, 0 ) ;
				rightBottom = new Vector2( _texture.Width, _texture.Height ) ;
			}

			// Transform all four corners into work space
			Vector2.Transform( ref leftTop, ref _transform, out leftTop ) ;
			Vector2.Transform( ref rightTop, ref _transform, out rightTop ) ;
			Vector2.Transform( ref leftBottom, ref _transform, out leftBottom ) ;
			Vector2.Transform( ref rightBottom, ref _transform, out rightBottom ) ;

			_polygon[ 0 ] = leftTop ;
			_polygon[ 1 ] = rightTop ;
			_polygon[ 3 ] = leftBottom ;
			_polygon[ 2 ] = rightBottom ;

			// Find the minimum and maximum extents of the rectangle in world space
			Vector2 min = Vector2.Min(
				Vector2.Min( leftTop, rightTop ),
				Vector2.Min( leftBottom, rightBottom ) ) ;
			Vector2 max = Vector2.Max(
				Vector2.Max( leftTop, rightTop ),
				Vector2.Max( leftBottom, rightBottom ) ) ;

			// Return as a rectangle
			_boundingRectangle = new Rectangle(
				(int) min.X,
				(int) min.Y,
				(int) ( max.X - min.X ),
				(int) ( max.Y - min.Y ) ) ;
		}

		public override ItemEditor Clone( )
		{
			var result = new TexturedHullEditor( ) ;

			result.RecreateFromXml( ParentLayer, ToXml( ) ) ;

			return result ;
		}

		//Compute frame index in forward animations
		private void computeFrameIndex( )
		{
			_time -= _frameTime ;

			_frameIndex++ ;

			_column++ ;

			if( _column >= _columns )
			{
				_column = 0 ;
				_row++ ;
			}

			if( _frameIndex >= _frameCount )
			{
				_frameIndex = 0 ;
				_row = 0 ;
				_column = 0 ;
			}
		}

		void reversedFrameIndex( )
		{
			_time -= _frameTime ;

			_frameIndex-- ;

			_column-- ;

			if( _column <= 0 )
			{
				_column = _columns - 1 ;

				_row-- ;

				if( _row < 0 )
				{
					_row = 0 ;
				}
			}

			if( _frameIndex <= 0 )
			{
				_frameIndex = _frameCount ;
				_column = _columns - 1 ;
				_row = _rows - 1 ;
			}
		}

		public override void HandleKeyPressWhenFocused(  )
		{
			if( KeyboardStatus.IsNewKeyPress( Keys.H ) )
			{
				IMemento memento = IoC.Memento ;
			    memento.Record("Flip Item(s) Horizontally", () =>
			        {
			            FlipHorizontally = !FlipHorizontally ;
			            IoC.Model.NotifyChanged( this ) ;
			        });
			}
			
			if( KeyboardStatus.IsNewKeyPress( Keys.V ) )
			{
				IMemento memento = IoC.Memento ;
			    memento.Record("Flip Item(s) Vertically", () =>
			        {
			            FlipVertically = !FlipVertically ;
			            IoC.Model.NotifyChanged( this ) ;
			        });
			}
		}
	}
}