using System ;
using System.Collections.Generic ;
using System.ComponentModel ;
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
	public class RectangularHullEditor : ItemEditor
	{
		static Dictionary<EdgePosition, Cursor> _edgeLookup ;

		RectangularHullProperties _properties ;

		IMainForm _mainForm ;

		Rectangle _rectangle;
		int _initialWidth ;
		int _initialHeight ;

		EdgePosition _edgeUnderMouse ;
		EdgePosition _edgeGrabbed;
		
		public override string NameSeed
		{
			get
			{
				return @"RectangularHull";
			}
		}

		public override void WhenChosenFromToolbox()
		{
			base.WhenChosenFromToolbox();
			summonMainForm(  ).SetToolStripStatusLabel1( Resource1.Path_Entered );
		}


		[UsedImplicitly]
		public RectangularHullEditor( )
		{
			_properties= new RectangularHullProperties(  );

			_edgeLookup = new Dictionary<EdgePosition, Cursor>
				{
					{
						EdgePosition.None, Cursors.Default
						},
					{
						EdgePosition.Left, Cursors.SizeWE
						},
					{
						EdgePosition.Right, Cursors.SizeWE
						},
					{
						EdgePosition.Top, Cursors.SizeNS
						},
					{
						EdgePosition.Bottom, Cursors.SizeNS
						},
				} ;
		}

		public override void PropertiesChanged(PropertyValueChangedEventArgs whatChanged)
		{
			recalculateRectangle( ) ;
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
				var wrapper = new ItemPropertiesWrapper<RectangularHullProperties>( _properties ) ;
				wrapper.Customise( ( ) => _properties.Opacity ) ;
				wrapper.Customise( ( ) => _properties.Scale ).SetDescription( @"The item's scale vector." ) ;
				wrapper.Customise( ( ) => _properties.Width ) ;
				wrapper.Customise( ( ) => _properties.Height ) ;
				wrapper.Customise( ( ) => _properties.Rotation )
					.SetDescription( @"The item's rotation in radians." ) ;

				return wrapper ;
			}
		}


		public override void RecreateFromXml( LayerEditor parentLayer, XElement xml )
		{
			base.RecreateFromXml( parentLayer, xml );
			ParentLayer = parentLayer ;
			_properties = xml.Element( @"RectangularHullProperties" ).DeserializedAs<RectangularHullProperties>( ) ;

			WhenUpdatedByUi(  );
		}

		public override void CreateInDesignMode(LayerEditor parentLayer, IEntityCreationProperties creationProperties)
		{
			ParentLayer = parentLayer ;
			
			_properties = fromRectangle( Rectangle.Empty ) ;
			_properties.Position = MouseStatus.WorldPosition ;
			_properties.Visible = true ;
			_properties.Opacity = 1f ;
			_properties.Scale = Vector2.One ;
			_properties.Rotation = 0f ;
			
			summonMainForm(  ).SetToolStripStatusLabel1(Resource1.Rectangle_Entered);

			WhenUpdatedByUi();
		}

		static RectangularHullProperties fromRectangle( Rectangle rectangle )
		{
			return new RectangularHullProperties
				{
					Position =  rectangle.Location.ToVector2(  ),
					Width = rectangle.Width,
					Height = rectangle.Height,
				} ;
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

				WhenUpdatedByUi( ) ;
			}
		}

		public override Vector2 Scale
		{
			get
			{
				return new Vector2( _properties.Width, _properties.Height ) ;
			}
			set
			{
				float factor = value.X / _properties.Width ;

				_properties.Width = (float) Math.Round( value.X ) ;

				_properties.Height = (float) Math.Round( _properties.Height * factor ) ;

				WhenUpdatedByUi( ) ;
			}
		}

		IMainForm summonMainForm( )
		{
			if( _mainForm == null )
			{
				_mainForm = ObjectFactory.GetInstance<IMainForm>();
			}
			
			return _mainForm ;
		}

		void setY( float y )
		{
			_properties.Position = new Vector2(_properties.Position.X, y);
		}

		void setX( float x )
		{
			_properties.Position = new Vector2(x, _properties.Position.Y);
		}

		public override bool ContainsPoint(Vector2 point)
		{
			return _rectangle.RotateAroundCenter( Rotation ).ContainsPoint( point ) ;
		}

		public override void OnMouseButtonUp(Vector2 mouseWorldPos)
		{
			_edgeGrabbed = EdgePosition.None;
		}

		public override void SetPosition(Vector2 position)
		{
			Vector2 delta = position - _properties.Position;
			
			if (position == _properties.Position)
			{
				return;
			}
			
			switch (_edgeGrabbed)
			{
				case EdgePosition.Left:
					setX( position.X ) ;
					_properties.Width -= (int)delta.X;
					WhenUpdatedByUi();
					break;
				case EdgePosition.Right:
					_properties.Width = _initialWidth + (int)delta.X;
					WhenUpdatedByUi();
					break;
				case EdgePosition.Top:
					setY(position.Y);
					_properties.Height -= (int)delta.Y;
					WhenUpdatedByUi();
					break;
				case EdgePosition.Bottom:
					_properties.Height = _initialHeight + (int)delta.Y;
					WhenUpdatedByUi();
					break;
				case EdgePosition.None:
					base.SetPosition(position);
					break;
			}
		}

		public override string Name
		{
			get
			{
				return _properties.Name ;
			}
		}

		public override void UserInteractionDuringCreation( )
		{
			_properties.Width = MouseStatus.WorldPosition.X - _properties.Position.X ;
			_properties.Height = MouseStatus.WorldPosition.Y - _properties.Position.Y ;

			if( MouseStatus.IsNewLeftMouseButtonClick(  ) )
			{
				PreviewEndedReadyForCreation( this, EventArgs.Empty ) ;
			}

			WhenUpdatedByUi(  );
		}

		public override void DrawSelectionFrame(SpriteBatch spriteBatch, Color color)
		{
			//todo: remove
			Matrix matrix = Matrix.Identity ;

			var drawing = ObjectFactory.GetInstance<IDrawing>() ;

			Vector2[ ] points = rotateRectangleAroundCenter( _rectangle, Rotation ) ;

			var transformedPoints = new Vector2[ points.Length ] ;
			
			Vector2.Transform( points, ref matrix, transformedPoints ) ;

			drawing.DrawPolygon( spriteBatch, transformedPoints, color, 2 );
			
			foreach (Vector2 p in transformedPoints)
			{
				drawing.DrawCircleFilled(spriteBatch, p, 4, color);
			}

			drawing.DrawBoxFilled(spriteBatch, transformedPoints[0].X - 5, transformedPoints[0].Y - 5, 10, 10, color);
		}

		public override void DrawInEditor(SpriteBatch spriteBatch)
		{
			if (!_properties.Visible)
			{
				return;
			}

			Color fillColor = Constants.Instance.ColorPrimitives;
			
			if (IsHovering && Constants.Instance.EnableHighlightOnMouseOver)
			{
				fillColor = Constants.Instance.ColorHighlight;
			}

			Vector2[ ] points = rotateRectangleAroundCenter( _rectangle, Rotation ) ;

			var drawing = ObjectFactory.GetInstance<IDrawing>() ;

			var camera = IoC.Canvas.Camera ;

			drawing.DrawPolygon( spriteBatch, points, fillColor, 5  );
			//drawing.DrawPolygonFilled( camera, spriteBatch, points, fillColor  );
		}

		static Vector2[ ] rotateRectangleAroundCenter( Rectangle rectangle, float angleInRadians )
		{
			Vector2[ ] points = rectangle.ToPolygon( ) ;

			Vector2 center = rectangle.Center.ToVector2( ) ;

			for( int i = 0; i < points.Length; i++ )
			{
				points[ i ] = points[ i ].RotateAroundPoint(center, angleInRadians ) ;
			}
			
			return points ;
		}


		public override void OnMouseButtonDown(Vector2 mouseWorldPos)
		{
			IsHovering = false;
			
			if (_edgeUnderMouse != EdgePosition.None)
			{
				_edgeGrabbed = _edgeUnderMouse;
				_initialWidth = _rectangle.Width;
				_initialHeight = _rectangle.Height;
			}
			else
			{
				summonMainForm().SetCursorForCanvas(Cursors.SizeAll);
			}
			
			base.OnMouseButtonDown(mouseWorldPos);
		}

		public override void OnMouseOver(Vector2 mouseWorldPos)
		{
			const int edgeWidth = 10 ;

			var edge=Helper.WhichEdge( _rectangle, Rotation, mouseWorldPos, edgeWidth ) ;

			_edgeUnderMouse = edge ;

			summonMainForm( ).SetCursorForCanvas( _edgeLookup[ edge ] ) ;
		}

		protected override void WhenUpdatedByUi()
		{
			recalculateRectangle(  );
		}

		void recalculateRectangle( )
		{
			_rectangle.Location = _properties.Position.ToPoint( ) ;
			_rectangle.Width = (int) _properties.Width ;
			_rectangle.Height = (int) _properties.Height ;
		}

		public override ImageProperties Icon
		{
			get
			{
				return Images.SummonIcon( Assembly.GetExecutingAssembly(  ), @"Gleed2D.Plugins.Krypton.Resources.icon_rectangular_hull.png" ) ;
			}
		}

		public override ItemProperties ItemProperties
		{
			get
			{
				return _properties ;
			}
		}

		public override ItemEditor Clone( )
		{
			var result = new RectangularHullEditor(  );
	
			result.RecreateFromXml( ParentLayer, ToXml() );

			return result ;
		}

		public override bool CanRotate( )
		{
			return true ;
		}
	}
}
