using System ;
using System.ComponentModel ;
using System.Reflection ;
using System.Windows.Forms ;
using System.Xml.Linq ;
using Gleed2D.Core ;
using Gleed2D.InGame ;
using JetBrains.Annotations ;
using Microsoft.Xna.Framework ;
using Microsoft.Xna.Framework.Graphics ;
using StructureMap ;

namespace Gleed2D.Plugins
{
	public class CircleItemEditor : ItemEditor
	{
		CircleItemProperties _properties ;
		IMainForm _mainForm ;

		[UsedImplicitly]
		public CircleItemEditor( )
		{
			_properties=new CircleItemProperties(  );

			_properties = new CircleItemProperties
				{
					Visible = true,
					Position = MouseStatus.WorldPosition,
					Radius = 50f,
					FillColor = Constants.Instance.ColorPrimitives
				};
		}

		public override string NameSeed
		{
			get
			{
				return @"Circle" ;
			}
		}

		public override void WhenChosenFromToolbox()
		{
			base.WhenChosenFromToolbox();
			summonMainForm(  ).SetToolStripStatusLabel1( Resource1.Circle_Entered );
		}


		public override void RecreateFromXml( LayerEditor parentLayer, XElement xml )
		{
			base.RecreateFromXml( parentLayer, xml );
			ParentLayer = parentLayer ;
			_properties = xml.Element( @"CircleItemProperties" ).DeserializedAs<CircleItemProperties>( ) ;
		}

		public override void CreateInDesignMode(LayerEditor parentLayer, IEntityCreationProperties creationProperties)
		{
			ParentLayer = parentLayer ;

			_properties= new CircleItemProperties
				{
					Visible=true,
					Position = MouseStatus.WorldPosition,
					Radius = 0f,
					FillColor = Constants.Instance.ColorPrimitives 
				} ;

			if (creationProperties.TriggeredBy == UiAction.Dragging)
			{
				_properties.Radius = Constants.Instance.GridSpacing.X;
			}

			summonMainForm(  ).SetToolStripStatusLabel1(Resource1.Circle_Started);
		}

		public override ItemEditor Clone( )
		{
			var result = new CircleItemEditor(  );
	
			result.RecreateFromXml( ParentLayer, ToXml() );

			return result ;
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
				wrapper.Customise( ( ) => _properties.Radius )
					.SetDescription( @"The radius" ) ;

				return wrapper ;
			}
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

			Color c = _properties.FillColor;
			
			if (IsHovering && Constants.Instance.EnableHighlightOnMouseOver)
			{
				c = Constants.Instance.ColorHighlight;
			}
			
			ObjectFactory.GetInstance<IDrawing>().DrawCircleFilled(spriteBatch, _properties.Position, _properties.Radius, c);
		}

		public override ImageProperties Icon
		{
			get
			{
				return Images.SummonIcon( Assembly.GetExecutingAssembly(  ), @"Gleed2D.Plugins.Resources.icon_circle_item.png" ) ;
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
			//todo: remove
			Matrix matrix = Matrix.Identity ;

			Vector2 transformedPosition = Vector2.Transform(_properties.Position, matrix);
			Vector2 transformedRadius = Vector2.TransformNormal(Vector2.UnitX * _properties.Radius, matrix);
			
			var drawing = ObjectFactory.GetInstance<IDrawing>() ;

			float transformedRadiusLength = transformedRadius.Length() ;

			drawing.DrawCircle(spriteBatch, transformedPosition, transformedRadiusLength, color, 2);

			var extents = new Vector2[4];

			extents[0] = transformedPosition + Vector2.UnitX * transformedRadiusLength;
			extents[1] = transformedPosition + Vector2.UnitY * transformedRadiusLength;
			extents[2] = transformedPosition - Vector2.UnitX * transformedRadiusLength;
			extents[3] = transformedPosition - Vector2.UnitY * transformedRadiusLength;

			foreach (Vector2 eachExtent in extents)
			{
				drawing.DrawCircleFilled(spriteBatch, eachExtent, 4, color);
			}

			Vector2 origin = Vector2.Transform(_properties.Position, matrix);

			drawing.DrawBoxFilled(spriteBatch, origin.X - 5, origin.Y - 5, 10, 10, color);
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
	}
}