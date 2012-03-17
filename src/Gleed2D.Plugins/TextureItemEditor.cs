using System;
using System.ComponentModel;
using System.Diagnostics;
using System.Drawing;
using System.IO;
using System.Windows.Forms;
using System.Xml.Linq;
using Gleed2D.Core;
using Gleed2D.InGame;
using JetBrains.Annotations;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using StructureMap;
using Color = Microsoft.Xna.Framework.Color;
using Keys = Microsoft.Xna.Framework.Input.Keys;
using Rectangle = Microsoft.Xna.Framework.Rectangle;

namespace Gleed2D.Plugins
{
	[DebuggerDisplay("Name={Name}, Id={Id}, Visible={Visible}, Position={Position}")]
	public class TextureItemEditor : ItemEditor, ISubscriber<ContentRootChanged>
	{
		IMainForm _mainForm;
		TextureItemProperties _properties;
		Rectangle _boundingRectangle;
		Color[] _colorDataForTexture;

		Vector2[] _polygon;

		Texture2D _texture;
		Matrix _transform;

		[UsedImplicitly]
		public TextureItemEditor()
		{
			_properties = new TextureItemProperties();
			
            ObjectFactory.GetInstance<IModelEventHub>().Subscribe<ContentRootChanged>(this);
		}

		public override void RecreateFromXml(LayerEditor parentLayer, XElement xml)
		{
			base.RecreateFromXml(parentLayer, xml);

			_polygon = new Vector2[4];

			ParentLayer = parentLayer;

			_properties = xml.Element(@"TextureItemProperties").DeserializedAs<TextureItemProperties>();

			tryInitialiseTexture(_properties.TexturePathRelativeToContentRoot);

			WhenUpdatedByUi();
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
				var wrapper = new ItemPropertiesWrapper<TextureItemProperties>(_properties);
			
				wrapper.Customise(() => _properties.Rotation).SetDescription(@"The item's rotation in radians.");
				wrapper.Customise(() => _properties.Scale).SetDescription(@"The item's scale vector.");
				wrapper.Customise(() => _properties.TintColor).SetDescription(
					@"The Color to tint the texture with. Use white for no tint.");
				wrapper.Customise(() => _properties.FlipHorizontally).SetDisplayName(@"Flip horizontally").SetDescription(
					@"If true, the texture is flipped horizontally when drawn.");
				wrapper.Customise(() => _properties.FlipVertically).SetDisplayName(@"Flip vertically").SetDescription(
					@"If true, the texture is flipped vertically when drawn.");
				wrapper.Customise(() => _properties.Origin).SetDescription(
					@"The item's origin in texture space ([0,0] is upper left corner).");
				wrapper.Customise(() => _properties.IsTemplate).SetDisplayName(@"Is a template?").SetDescription(
					@"Can be used as template for various objects (chains, tracks, spawners), will not be drawn in this exact location");
				//wrapper.Customise(() => _properties.TexturePathRelativeToContentRoot).Hide();
				wrapper.Customise(() => _properties.AssetName).Hide();

				return wrapper;
			}
		}

		public override string NameSeed
		{
			get
			{
				return @"Texture";
			}
		}

		public override bool CanScale
		{
			get
			{
				return true;
			}
		}

		public override float Rotation
		{
			get
			{
				return _properties.Rotation;
			}
			set
			{
				_properties.Rotation = value;
			}
		}

		public override Vector2 Scale
		{
			get
			{
				return _properties.Scale;
			}
			set
			{
				_properties.Scale = value;

				WhenUpdatedByUi();
			}
		}

		void flipHorizontally(bool value)
		{
			_properties.FlipHorizontally = value;

			WhenUpdatedByUi();
		}

		void flipVertically(bool value)
		{
			_properties.FlipVertically = value;

			WhenUpdatedByUi();
		}

		public override string Name
		{
			get
			{
				return _properties.Name;
			}
		}

		public override ImageProperties Icon
		{
			get
			{
				var imageRepository = ObjectFactory.GetNamedInstance<IImageRepository>(@"iconImages");

				string name = @"texture_{0}".FormatWith(_properties.TexturePathRelativeToContentRoot);

				if (!imageRepository.ContainsImage(name))
				{
					using (var memoryStream = new MemoryStream())
					{
						_texture.SaveAsPng(memoryStream, 64, 64);

						memoryStream.Position = 0;

						Image image = Image.FromStream(memoryStream);

						imageRepository.Set(new ImageProperties(name, image));
					}
				}

				return imageRepository.GetByName(name);
			}
		}

		public override ItemProperties ItemProperties
		{
			get
			{
				return _properties;
			}
		}

		public override void CreateInDesignMode(
			LayerEditor parentLayer,
			IEntityCreationProperties creationProperties)
		{
			_polygon = new Vector2[4];

			ParentLayer = parentLayer;

			var tCreationProperties = (TextureCreationProperties)creationProperties;

			string fullPath = tCreationProperties.PathToTexture;

			tryInitialiseTexture(fullPath);

			// ReSharper disable UseObjectOrCollectionInitializer
			_properties = new TextureItemProperties
				// ReSharper restore UseObjectOrCollectionInitializer
				{
					Position = MouseStatus.WorldPosition,
					TexturePathRelativeToContentRoot = ObjectFactory.GetInstance<IDisk>().MakeRelativePath(parentLayer.ParentLevel.ContentRootFolder, fullPath),
					CustomProperties = new CustomProperties(),
					Visible = true,
					Scale = Vector2.One,
					TintColor = Color.White,
				};

			_properties.Origin = getTextureOrigin();

			WhenUpdatedByUi();
		}

		//public virtual void CreateReadyForDroppingOntoCanvas(
		//    LayerEditor parentLayer,
		//    IEntityCreationProperties creationProperties)
		//{
		//    _polygon = new Vector2[4];

		//    ParentLayer = parentLayer;

		//    var tCreationProperties = (TextureCreationProperties)creationProperties;

		//    string fullPath = tCreationProperties.PathToTexture;

		//    initialiseTexture(fullPath);

		//    // ReSharper disable UseObjectOrCollectionInitializer
		//    _properties = new TextureItemProperties
		//        // ReSharper restore UseObjectOrCollectionInitializer
		//        {
		//            Position = MouseStatus.WorldPosition,
		//            TexturePathRelativeToContentRoot = ObjectFactory.GetInstance<IDisk>().MakeRelativePath(parentLayer.ParentLevel.ContentRootFolder, fullPath),
		//            CustomProperties = new CustomProperties(),
		//            Visible = true,
		//            Scale = Vector2.One,
		//            TintColor = Color.White,
		//        };

		//    _properties.Origin = getTextureOrigin();

		//    WhenUpdatedByUi();
		//}

		public override bool CanRotate()
		{
			return true;
		}

		Vector2 getTextureOrigin()
		{
			switch (Constants.Instance.DefaultTextureOrigin)
			{
				case InternalPoint.Middle:
					return new Vector2(_texture.Width, _texture.Height) / 2;

				case InternalPoint.Centroid:
					var data = new uint[_texture.Width * _texture.Height];
					_texture.GetData(data);
					Vertices verts = Vertices.CreatePolygon(data, _texture.Width, _texture.Height);
					Vector2 textureOrigin = verts.GetCentroid();
					return new Vector2(textureOrigin.X, textureOrigin.Y);

				case InternalPoint.TopLeft:
					{
						return new Vector2(0, 0);
					}

				case InternalPoint.TopRight:
					{
						return new Vector2(_texture.Width, 0);
					}

				case InternalPoint.BottomLeft:
					{
						return new Vector2(0, _texture.Height);
					}

				case InternalPoint.BottomRight:
					{
						return new Vector2(_texture.Width, _texture.Height);
					}
			}

			return Vector2.Zero;
		}

		void tryInitialiseTexture(string textureFilename)
		{
			var textureStore = ObjectFactory.GetInstance<ITextureStore>();
			var game = ObjectFactory.GetInstance<IGame>();

			string absolutePath =
				Path.Combine(@"{0}\".FormatWith(ParentLayer.ParentLevel.ContentRootFolder), textureFilename);

			if (!File.Exists(absolutePath))
			{
//                DialogResult result =
//                    MessageBox.Show(
//                        @"The file ""{0}"" doesn't exist!
//The texture path is a combination of the Level's ContentRootFolder and the TextureItem's relative path.
//Please adjust the XML file before trying to load this level again.
//For now, a dummy texture will be used. Continue loading the level?"
//                            .FormatWith(
//                                absolutePath),
//                        @"Error loading texture file",
//                        MessageBoxButtons.YesNo,
//                        MessageBoxIcon.Question);

//                if (result == DialogResult.No)
//                {
//                    return;
//                }

				_texture = textureStore.DummyTexture;
			}
			else
			{
				_texture = textureStore.FromFile(game.GraphicsDevice, absolutePath);
			}

			//for per-pixel-collision
			_colorDataForTexture = new Color[_texture.Width * _texture.Height];

			_texture.GetData(_colorDataForTexture);
		}

		IMainForm summonMainForm()
		{
			if (_mainForm == null)
			{
				_mainForm = ObjectFactory.GetInstance<IMainForm>();
			}

			return _mainForm;
		}

		public override bool ContainsPoint(Vector2 point)
		{
			if (_boundingRectangle.Contains((int)point.X, (int)point.Y))
			{
				return intersectPixels(point);
			}

			return false;
		}

		bool intersectPixels(Vector2 worldpos)
		{
			Vector2 positionInB = Vector2.Transform(worldpos, Matrix.Invert(_transform));

			var xB = (int)Math.Round(positionInB.X);
			var yB = (int)Math.Round(positionInB.Y);

			if (_properties.FlipHorizontally)
			{
				xB = _texture.Width - xB;
			}

			if (_properties.FlipVertically)
			{
				yB = _texture.Height - yB;
			}

			// If the pixel lies within the bounds of B
			if (0 <= xB && xB < _texture.Width && 0 <= yB && yB < _texture.Height)
			{
				Color colorB = _colorDataForTexture[xB + yB * _texture.Width];
				if (colorB.A != 0)
				{
					return true;
				}
			}
			return false;
		}

		public override void OnMouseButtonUp(Vector2 mouseWorldPos)
		{
		}

		public override void SetPosition(Vector2 position)
		{
			ItemProperties.Position = position;

			WhenUpdatedByUi();
		}

		public override void UserInteractionDuringCreation()
		{
			SetPosition(MouseStatus.WorldPosition);

			if (MouseStatus.IsNewLeftMouseButtonClick())
			{
				PreviewEndedReadyForCreation(this, EventArgs.Empty);
			}

			WhenUpdatedByUi();
		}

		public override void DrawSelectionFrame(SpriteBatch spriteBatch, Color color)
		{
			Matrix matrix = Matrix.Identity;

			var drawing = ObjectFactory.GetInstance<IDrawing>();

			var poly = new Vector2[4];
			Vector2.Transform(_polygon, ref matrix, poly);

			drawing.DrawPolygon(spriteBatch, poly, color, 2);

			foreach (Vector2 eachVector in poly)
			{
				drawing.DrawCircleFilled(spriteBatch, eachVector, 4, color);
			}

			Vector2 origin = Vector2.Transform(_properties.Position, matrix);

			drawing.DrawBoxFilled(spriteBatch, origin.X - 5, origin.Y - 5, 10, 10, color);
		}

		public override void DrawInEditor(SpriteBatch spriteBatch)
		{
			if (!_properties.Visible)
			{
				return;
			}

			SpriteEffects effects = SpriteEffects.None;
			if (_properties.FlipHorizontally)
			{
				effects |= SpriteEffects.FlipHorizontally;
			}

			if (_properties.FlipVertically)
			{
				effects |= SpriteEffects.FlipVertically;
			}

			Color tintColor = _properties.TintColor;

			if (IsHovering && Constants.Instance.EnableHighlightOnMouseOver)
			{
				tintColor = Constants.Instance.ColorHighlight;
			}

			spriteBatch.Draw(_texture, _properties.Position, null, tintColor, Rotation, _properties.Origin, Scale, effects, 0);
		}

		public override void OnMouseButtonDown(Vector2 mouseWorldPos)
		{
			IsHovering = false;

			summonMainForm().SetCursorForCanvas(Cursors.SizeAll);
		}

		public override void OnMouseOver(Vector2 mouseWorldPos)
		{
			IsHovering = true;
		}

		protected override void WhenUpdatedByUi()
		{
			_transform =
				Matrix.CreateTranslation(new Vector3(-_properties.Origin.X, -_properties.Origin.Y, 0.0f)) *
				Matrix.CreateScale(Scale.X, Scale.Y, 1) *
				Matrix.CreateRotationZ(Rotation) *
				Matrix.CreateTranslation(new Vector3(_properties.Position, 0.0f));

			Vector2 leftTop = Vector2.Zero;

			var leftBottom = new Vector2(0, _texture.Height);
			var rightTop = new Vector2(_texture.Width, 0);
			var rightBottom = new Vector2(_texture.Width, _texture.Height);

			// Transform all four corners into work space
			Vector2.Transform(ref leftTop, ref _transform, out leftTop);
			Vector2.Transform(ref rightTop, ref _transform, out rightTop);
			Vector2.Transform(ref leftBottom, ref _transform, out leftBottom);
			Vector2.Transform(ref rightBottom, ref _transform, out rightBottom);

			_polygon[0] = leftTop;
			_polygon[1] = rightTop;
			_polygon[3] = leftBottom;
			_polygon[2] = rightBottom;

			// Find the minimum and maximum extents of the rectangle in world space
			Vector2 min = Vector2.Min(
				Vector2.Min(leftTop, rightTop),
				Vector2.Min(leftBottom, rightBottom));

			Vector2 max = Vector2.Max(
				Vector2.Max(leftTop, rightTop),
				Vector2.Max(leftBottom, rightBottom));

			// Return as a rectangle
			_boundingRectangle = new Rectangle(
				(int)min.X,
				(int)min.Y,
				(int)(max.X - min.X),
				(int)(max.Y - min.Y));
		}

		public override ItemEditor Clone()
		{
			var result = new TextureItemEditor();

			result.RecreateFromXml(ParentLayer, ToXml());

			return result;
		}

		public override void HandleKeyPressWhenFocused()
		{
			if (KeyboardStatus.IsNewKeyPress(Keys.H))
			{
				IMemento memento = IoC.Memento;
			    memento.Record(@"Flip Item(s) Horizontally", () =>
			        {
			            flipHorizontally(!_properties.FlipHorizontally);
			            IoC.Model.NotifyChanged(this);
			        });
			}

			if (KeyboardStatus.IsNewKeyPress(Keys.V))
			{
				IMemento memento = IoC.Memento;
			    
                memento.Record(@"Flip Item(s) Vertically", () =>
			        {
			            flipVertically(!_properties.FlipVertically);
			            IoC.Model.NotifyChanged(this);
			        });
			}
		}

		public void Receive(ContentRootChanged whatChanged)
		{
			var disk = ObjectFactory.GetInstance<IDisk>();

			string oldAbsolutePath =
				Path.Combine(whatChanged.OldContentRootFolder, _properties.TexturePathRelativeToContentRoot);

			string relativePath = disk.MakeRelativePath(whatChanged.NewContentRootFolder, whatChanged.OldContentRootFolder);

			int n = _properties.TexturePathRelativeToContentRoot.LastIndexOf(@"\", StringComparison.Ordinal);
			string s = _properties.TexturePathRelativeToContentRoot.Substring(n+1);

			string texturePathRelativeToContentRoot = Path.Combine(relativePath,s);
			_properties.TexturePathRelativeToContentRoot = texturePathRelativeToContentRoot;

			tryInitialiseTexture(_properties.TexturePathRelativeToContentRoot);

			WhenUpdatedByUi();
		}
	}
}