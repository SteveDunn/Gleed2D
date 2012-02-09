using System ;
using System.Reflection ;
using System.Windows.Forms ;
using System.Xml.Linq ;
using Gleed2D.Core ;
using Gleed2D.InGame ;
using Gleed2D.InGame.Krypton ;
using JetBrains.Annotations ;
using Microsoft.Xna.Framework ;
using Microsoft.Xna.Framework.Graphics ;
using StructureMap ;

namespace Gleed2D.Plugins.Krypton
{
	public class CircularHullEditor : ItemEditor
	{
		CircularHullProperties _properties ;
		IMainForm _mainForm ;

		[PublicAPI]
		public CircularHullEditor( )
		{
			_properties=new CircularHullProperties(  );
		}

		public override string NameSeed
		{
			get
			{
				return @"CircularHull";
			}
		}

		public override void WhenChosenFromToolbox()
		{
			base.WhenChosenFromToolbox();
			summonMainForm(  ).SetToolStripStatusLabel1( Resource1.Circle_Entered );
		}


		public override void RecreateFromXml( LayerEditor parentLayer, XElement xml )
		{
			ParentLayer = parentLayer ;
			base.RecreateFromXml( parentLayer, xml );
			_properties = xml.CertainElement( @"CircularHullProperties" ).DeserializedAs<CircularHullProperties>( ) ;
		}

		public override void CreateInDesignMode(LayerEditor parentLayer, IEntityCreationProperties creationProperties)
		{
			ParentLayer = parentLayer ;

			_properties= new CircularHullProperties
				{
					Visible=true,
					Position = MouseStatus.WorldPosition,
					Radius = 10f,
					Scale = Vector2.One,
					Opacity = 1f,
					Sides = 10,
					FillColor = Color.Black 
				} ;

			summonMainForm(  ).SetToolStripStatusLabel1(Resource1.Circle_Started);
		}

		public override ItemEditor Clone( )
		{
			var result = new CircularHullEditor(  );
	
			result.RecreateFromXml( ParentLayer, ToXml() );

			return result ;
		}

		public override bool ContainsPoint(Vector2 point)
		{
			return (point - ItemProperties.Position).Length() <= _properties.Radius;
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

		public override Vector2 Scale
		{
			get
			{
				return new Vector2( _properties.Radius, _properties.Radius ) ;
			}
			set
			{
				_properties.Radius = (float) Math.Round( value.X ) ;
			}
		}

		public override void DrawInEditor(SpriteBatch spriteBatch)
		{
			if (!_properties.Visible)
			{
				return;
			}
			
			Color drawColor = _properties.FillColor;

			var lightingState = ObjectFactory.GetInstance<ILightingState>( ) ;
			bool fill = !lightingState.LightingOn ;
			
			if (IsHovering && Constants.Instance.EnableHighlightOnMouseOver)
			{
				drawColor = Constants.Instance.ColorHighlight;
			}

			var drawing = ObjectFactory.GetInstance<IDrawing>() ;
			
			if (fill)
			{
				drawing.DrawCircleFilled(spriteBatch, _properties.Position, _properties.Radius, drawColor *.5f);
			}
			else
			{
				drawing.DrawCircle(
					spriteBatch, _properties.Position, _properties.Radius, drawColor * .5f, 5 ) ;
			}
		}

		public override ImageProperties Icon
		{
			get
			{
				return Images.SummonIcon( Assembly.GetExecutingAssembly(  ), @"Gleed2D.Plugins.Krypton.Resources.icon_circular_hull.png" ) ;
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
			var drawing = ObjectFactory.GetInstance<IDrawing>() ;

			float radius = _properties.Radius ;
			Vector2 position = _properties.Position ;
			
			drawing.DrawCircle(spriteBatch, position, radius, color, 2);

			var extents = new Vector2[4];

			extents[0] = position + Vector2.UnitX * radius;
			extents[1] = position + Vector2.UnitY * radius;
			extents[2] = position - Vector2.UnitX * radius;
			extents[3] = position - Vector2.UnitY * radius;

			foreach (Vector2 eachExtent in extents)
			{
				drawing.DrawCircleFilled(spriteBatch, eachExtent, 4, color);
			}

			drawing.DrawBoxFilled(spriteBatch, position.X - 5, position.Y - 5, 10, 10, color);
		}

		public override void UserInteractionDuringCreation(  )
		{
			_properties.Radius = ( MouseStatus.WorldPosition - _properties.Position ).Length( ) ;

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

		public override System.ComponentModel.ICustomTypeDescriptor ObjectForPropertyGrid
		{
			get
			{
				var wrapper = new ItemPropertiesWrapper<CircularHullProperties>( _properties ) ;

				wrapper.Customise( ( ) => _properties.Radius )
					.SetDescription( @"The radius" ) ;

				wrapper.Customise( ( ) => _properties.Sides )
					.SetDescription( @"Amount of sides" ) ;

				wrapper.Customise( ( ) => _properties.Scale )
					.SetDescription( @"The item's scale vector." ) ;

				wrapper.AddValidation(
					( ) => _properties.Sides, f => f.Sides < 3 ? new ValidationError( @"Must have 3 or more sides" ) : null ) ;
				
				return wrapper ;
			}
		}
	}
}