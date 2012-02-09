using System ;
using System.ComponentModel ;
using System.Reflection ;
using System.Windows.Forms ;
using System.Xml.Linq ;
using Gleed2D.Core ;
using Gleed2D.InGame ;
using Gleed2D.InGame.Krypton ;
using JetBrains.Annotations ;
using Krypton.Lights ;
using Microsoft.Xna.Framework ;
using Microsoft.Xna.Framework.Graphics ;
using StructureMap ;

namespace Gleed2D.Plugins.Krypton
{
	public class LightEditor : ItemEditor
	{
		readonly IDrawing _drawing ;

		LightProperties _properties ;
		IMainForm _mainForm ;

		[UsedImplicitly]
		public LightEditor( )
		{
			_properties=new LightProperties(  );
			_drawing = ObjectFactory.GetInstance<IDrawing>( ) ;
		}

		public override void RecreateFromXml( LayerEditor parentLayer, XElement xml )
		{
			base.RecreateFromXml( parentLayer, xml );

			ParentLayer = parentLayer ;
			_properties = xml.CertainElement( @"LightProperties" ).DeserializedAs<LightProperties>( ) ;
		}

		public override void CreateInDesignMode(LayerEditor parentLayer, IEntityCreationProperties creationProperties)
		{
			ParentLayer = parentLayer ;

			_properties= new LightProperties
				{
					Visible=true,
					IsOn = true,
					Range=100f,
					Color=Color.White,
					FieldOfView = MathHelper.TwoPi,
					Intensity = 1,
					ShadowType = convertShadowType( ShadowType.Solid),
					Position = MouseStatus.WorldPosition,
					TextureSize = 128
				} ;
		}

		public override string NameSeed
		{
			get
			{
				return @"Light";
			}
		}

		TypeOfShadow convertShadowType( ShadowType shadowType )
		{
			if( shadowType == ShadowType.Illuminated )
			{
				return TypeOfShadow.Illuminated;
			}

			if( shadowType == ShadowType.Occluded )
			{
				return TypeOfShadow.Occluded;
			}

			if( shadowType == ShadowType.Solid )
			{
				return TypeOfShadow.Solid;
			}

			throw new NotSupportedException( @"Don't know how to handle a shadow type of '{0}'.".FormatWith( shadowType ) ) ;
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
				var wrapper = new ItemPropertiesWrapper<ItemProperties>( ItemProperties ) ;
				wrapper.Customise( ( ) => _properties.Rotation )
					.SetDescription( "The item's rotation in radians." ) ;
				wrapper.Customise( ( ) => _properties.TypeOfLight )
					.SetDescription( "The type of light" ) ;

				wrapper.Customise( ( ) => _properties.TextureSize )
					.SetDescription( "The size of the texture to generate." ) ;

				wrapper.Customise( ( ) => _properties.FieldOfView )
					.SetDescription( @"The item's field of view in radians (or how 'wide' the light is)." ) ;

				wrapper.Customise( ( ) => _properties.Range )
					.SetDescription( @"The item's range." ) ;

				wrapper.Customise( ( ) => _properties.Intensity )
					.SetDescription( @"The item's intensity." ) ;

				wrapper.Customise( ( ) => _properties.ShadowType )
					.SetDescription( "The item's shadow type." ) ;

				wrapper.Customise( ( ) => _properties.Color )
					.SetDescription( "The Color to tint the texture with. Use white for no tint." ) ;

				wrapper.Customise( ( ) => _properties.IsOn )
					.SetDescription( @"Whether the light is on or off" ) ;

				return wrapper ;
			}
		}

		public override ItemEditor Clone( )
		{
			var result = new LightEditor(  );
	
			result.RecreateFromXml( ParentLayer, ToXml() );

			return result ;
		}

		public override bool ContainsPoint(Vector2 point)
		{
			const int outerGrab = 20 ;
			const int innerGrab = 30 ;

			float lengthFromCenter = (point - ItemProperties.Position).Length() ;

			bool outerEdge = lengthFromCenter > _properties.Range-outerGrab && lengthFromCenter < _properties.Range+outerGrab ;

			bool inner = lengthFromCenter > -innerGrab && lengthFromCenter < innerGrab ;

			return outerEdge || inner;
		}

		public override void OnMouseButtonDown(Vector2 mouseWorldPos)
		{
			IsHovering = false;
			
			summonMainForm( ).SetCursorForCanvas( Cursors.SizeAll ) ;
			
			base.OnMouseButtonDown(mouseWorldPos);
		}

		IMainForm summonMainForm( )
		{
			if( _mainForm == null )
			{
				_mainForm = ObjectFactory.GetInstance<IMainForm>();
			}
			
			return _mainForm ;
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
				return new Vector2( _properties.Range, _properties.Range ) ;
			}
			set
			{
				_properties.Range = (float) Math.Round( value.X ) ;
			}
		}

		public override void DrawInEditor(SpriteBatch spriteBatch)
		{
			if (!_properties.Visible)
			{
				return;
			}
			
			Color c = Constants.Instance.ColorPrimitives;
			
			if (IsHovering && Constants.Instance.EnableHighlightOnMouseOver)
			{
				c = Constants.Instance.ColorHighlight;
			}
			
			_drawing.DrawCircle(spriteBatch, _properties.Position, _properties.Range, c, 5);
		}

		public override ImageProperties Icon
		{
			get
			{
				return Images.SummonIcon( Assembly.GetExecutingAssembly(  ), @"Gleed2D.Plugins.Krypton.Resources.icon_light.png" ) ;
			}
		}

		public override ItemProperties ItemProperties
		{
			get
			{
				return _properties ;
			}
		}

		public override void DrawSelectionFrame(SpriteBatch spriteBatch, Color color)
		{
			float range = _properties.Range ;
			Vector2 position = _properties.Position ;

			_drawing.DrawCircle(spriteBatch, position, range, color, 2);

			var extents = new Vector2[4];

			extents[0] = position + Vector2.UnitX * range;
			extents[1] = position + Vector2.UnitY * range;
			extents[2] = position - Vector2.UnitX * range;
			extents[3] = position - Vector2.UnitY * range;

			foreach (Vector2 eachExtent in extents)
			{
				_drawing.DrawCircleFilled(spriteBatch, eachExtent, 4, color);
			}

			_drawing.DrawBoxFilled(spriteBatch, position.X - 5, position.Y - 5, 10, 10, color);
		}

		public override void UserInteractionDuringCreation(  )
		{
			_properties.Range = ( MouseStatus.WorldPosition - _properties.Position ).Length( ) ;

			if( MouseStatus.IsNewLeftMouseButtonClick(  ) )
			{
				PreviewEndedReadyForCreation( this, EventArgs.Empty ) ;
			}
		}

		public override string Name
		{
			get
			{
				return _properties.Name ;
			}
		}
	}
}