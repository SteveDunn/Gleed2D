using System ;
using System.Collections.Generic ;
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
using Keys = Microsoft.Xna.Framework.Input.Keys ;

namespace Gleed2D.Plugins
{
	public class RectangleItemEditor : ItemEditor
	{
		static readonly Dictionary<EdgePosition, Cursor> _edgeLookup = new Dictionary<EdgePosition, Cursor>
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
		
		RectangleItemProperties _properties ;

		IMainForm _mainForm ;

		Rectangle _rectangle;
		int _initialWidth ;
		int _initialHeight ;

		EdgePosition _edgeUnderMouse ;
		EdgePosition _edgeGrabbed;

		[UsedImplicitly]
		public RectangleItemEditor( )
		{
			_properties= new RectangleItemProperties(  );

			_properties = fromRectangle(Rectangle.Empty);
			_properties.Position = Vector2.Zero;
			_properties.Visible = true;
			_properties.FillColor = Constants.Instance.ColorPrimitives;
			_properties.Width = _properties.Height = 100 ;
		}

		public override void WhenChosenFromToolbox()
		{
			base.WhenChosenFromToolbox();
			summonMainForm(  ).SetToolStripStatusLabel1( Resource1.Rectangle_Entered);
		}

		public override string NameSeed
		{
			get
			{
				return @"Rectangle" ;
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
				var wrapper = new ItemPropertiesWrapper<RectangleItemProperties>( _properties ) ;
				wrapper.Customise( ( ) => _properties.Width ) ;
				wrapper.Customise( ( ) => _properties.Height ) ;
				wrapper.Customise( ( ) => _properties.FillColor ).SetDisplayName( @"Fill color" ) ;
				wrapper.Customise( ( ) => _properties.Rotation )
					.SetDescription( @"The item's rotation in radians." ) ;

				return wrapper ;
			}
		}


		public override void RecreateFromXml( LayerEditor parentLayer, XElement xml )
		{
			ParentLayer = parentLayer ;

			_properties = xml.Element( @"RectangleItemProperties" ).DeserializedAs<RectangleItemProperties>( ) ;

			//todo: don't like having to call the base - use the Prototype
			base.RecreateFromXml( parentLayer, xml );

			WhenUpdatedByUi(  );
		}

		public override void CreateInDesignMode(LayerEditor parentLayer, IEntityCreationProperties creationProperties)
		{
			ParentLayer = parentLayer ;
			
			_properties = fromRectangle( Rectangle.Empty ) ;
			
			if (creationProperties.TriggeredBy == UiAction.Dragging)
			{
				var size = calculateDefaultSizeOfNewlyCreatedItem();
				_properties.Width = size.X;
				_properties.Height = size.Y;
			}
			
			_properties.Position = MouseStatus.WorldPosition ;

			_properties.Visible = true ;
			_properties.FillColor = Constants.Instance.ColorPrimitives ;
			
			summonMainForm(  ).SetToolStripStatusLabel1(Resource1.Rectangle_Started);

			WhenUpdatedByUi();
		}

		public virtual void CreateReadyForDroppingOntoCanvas(LayerEditor parentLayer, IEntityCreationProperties creationProperties)
		{
			ParentLayer = parentLayer ;
			
			_properties = fromRectangle( Rectangle.Empty ) ;

			_properties.Position = MouseStatus.WorldPosition ;

			Vector2 size = calculateDefaultSizeOfNewlyCreatedItem();
			
			_properties.Width = size.X;
			_properties.Height = size.Y;
			_properties.Visible = true ;
			_properties.FillColor = Constants.Instance.ColorPrimitives ;
			
			summonMainForm(  ).SetToolStripStatusLabel1(Resource1.Rectangle_DragDrop);

			WhenUpdatedByUi();
		}

		Vector2 calculateDefaultSizeOfNewlyCreatedItem()
		{
			return Constants.Instance.GridSpacing;
		}

		static RectangleItemProperties fromRectangle( Rectangle rectangle )
		{
			return new RectangleItemProperties
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
			if (position == _properties.Position)
			{
				return;
			}
			
			Vector2 delta = position - _properties.Position;

			switch (_edgeGrabbed)
			{
				case EdgePosition.Left:
					setX( position.X ) ;
					_properties.Width -= (int)delta.X;
					break;
				case EdgePosition.Right:
					_properties.Width = _initialWidth + (int)delta.X;
					break;
				case EdgePosition.Top:
					setY(position.Y);
					_properties.Height -= (int)delta.Y;
					break;
				case EdgePosition.Bottom:
					_properties.Height = _initialHeight + (int)delta.Y;
					break;
				case EdgePosition.None:
					base.SetPosition(position);
					break;
			}

			WhenUpdatedByUi();
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

			if (KeyboardStatus.IsKeyDown(Keys.LeftControl))
			{
				_properties.Width = _properties.Height ;
			}

			if( MouseStatus.IsNewLeftMouseButtonClick(  ) )
			{
				PreviewEndedReadyForCreation( this, EventArgs.Empty ) ;
			}

			WhenUpdatedByUi(  );
		}

		public override void DrawSelectionFrame(SpriteBatch spriteBatch, Color color)
		{
			var drawing = ObjectFactory.GetInstance<IDrawing>() ;
			
			
			var poly =  _rectangle.RotateAroundPoint( _rectangle.Center.ToVector2(  ), Rotation ) ;
			//Vector2[] poly = r.Transform( matrix );

			drawing.DrawPolygon(spriteBatch, poly, color, 2);

			foreach (Vector2 p in poly)
			{
				drawing.DrawCircleFilled(spriteBatch, p, 4, color);
			}

			drawing.DrawBoxFilled(spriteBatch, poly[0].X - 5, poly[0].Y - 5, 10, 10, color);

			//todo: finish off
			ICanvas e = IoC.Canvas ;
			//e.u


			//            bool rotatingOrScaling = 
			//                rendererParams.UserActionInEditor == UserActionInEditor.RotatingItems || 
			//                rendererParams.UserActionInEditor == UserActionInEditor.ScalingItems ;
			//drawing.DrawLine( spriteBatch, poly[0], MouseStatus.WorldPosition, Constants.Instance.ColorSelectionFirst, 1 ) ;

		}

		public override void DrawInEditor(SpriteBatch spriteBatch)
		{
			if (!_properties.Visible)
			{
				return;
			}

			Color fillColor = _properties.FillColor;
			
			if (IsHovering && Constants.Instance.EnableHighlightOnMouseOver)
			{
				fillColor = Constants.Instance.ColorHighlight;
			}

			var r =  _rectangle.RotateAroundPoint( _rectangle.Center.ToVector2(  ), Rotation ) ;
			
			ObjectFactory.GetInstance<IDrawing>().DrawPolygon(spriteBatch, r, fillColor, 2);
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
			recalculateRectangle( ) ;
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
				return Images.SummonIcon( Assembly.GetExecutingAssembly(  ), @"Gleed2D.Plugins.Resources.icon_rectangle_item.png" ) ;
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
			var result = new RectangleItemEditor(  );
	
			result.RecreateFromXml( ParentLayer, ToXml() );

			return result ;
		}
	}
}
