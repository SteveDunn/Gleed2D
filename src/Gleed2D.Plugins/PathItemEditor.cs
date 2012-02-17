using System ;
using System.Collections.Generic ;
using System.ComponentModel ;
using System.Linq ;
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
	public class PathItemEditor : ItemEditor
	{
		int _pointUnderMouse = -1 ;
		int _pointGrabbed = -1 ;
		Vector2 _initialPos ;
		IMainForm _mainForm ;
		PathItemProperties _properties ;
		ItemEditorMode _editorMode ;

		[UsedImplicitly]
		public PathItemEditor( )
		{
			_properties = new PathItemProperties
				{
					Visible = true,
					Position = Vector2.Zero,
					WorldPoints = new List<Vector2>(),
					LocalPoints = new List<Vector2>(),
					LineWidth = Constants.Instance.DefaultPathItemLineWidth,
					LineColor = Constants.Instance.ColorPrimitives
				};
		}

		public override void WhenChosenFromToolbox()
		{
			base.WhenChosenFromToolbox();
			summonMainForm(  ).SetToolStripStatusLabel1( Resource1.Path_Entered );
		}

		public override void PropertiesChanged(PropertyValueChangedEventArgs whatChanged)
		{
			adjustPointsForNewPosition( ) ;
		}

		public override void RecreateFromXml( LayerEditor parentLayer, XElement xml )
		{
			base.RecreateFromXml( parentLayer, xml );

			_editorMode = ItemEditorMode.Created ;
			ParentLayer = parentLayer ;
			_properties = xml.Element( @"PathItemProperties" ).DeserializedAs<PathItemProperties>( ) ;
			
			WhenUpdatedByUi(  );
		}

		public override string NameSeed
		{
			get
			{
				return @"Path" ;
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
				var wrapper = new ItemPropertiesWrapper<PathItemProperties>( _properties ) ;
				wrapper.Customise( ( ) => _properties.IsPolygon )
					.SetDisplayName(@"Is this a polygon?")
					.SetDescription( @"If true, the Path will be drawn as a polygon (last and first point will be connected)." ) ;
				
				wrapper.Customise( ( ) => _properties.LineWidth )
					.SetDisplayName(@"Line width")
					.SetDescription( @"The line width of this path. Can be used for rendering." ) ;
				wrapper.Customise( ( ) => _properties.LineColor )
					.SetDisplayName( @"Line color")
					.SetDescription( @"The line color of this path. Can be used for rendering." ) ;
				
				wrapper.Customise( ( ) => _properties.LocalPoints ).Hide( ) ;
				
				wrapper.Customise( ( ) => _properties.WorldPoints ).Hide( ) ;

				return wrapper ;
			}
		}

		public override void CreateInDesignMode(LayerEditor parentLayer, IEntityCreationProperties creationProperties)
		{
			_editorMode = ItemEditorMode.Creating ;

			Vector2 mouseWorldPos = MouseStatus.WorldPosition ;

			_properties = new PathItemProperties
				{
					Visible = true,
					Position = mouseWorldPos,
					WorldPoints = new List<Vector2>(),
					LocalPoints = new List<Vector2>(),
					LineWidth = Constants.Instance.DefaultPathItemLineWidth,
					LineColor = Constants.Instance.ColorPrimitives
				} ;

			addWorldPoint( mouseWorldPos );

			summonMainForm(  ).SetToolStripStatusLabel1(Resource1.Path_Started);

			ParentLayer = parentLayer ;

			WhenUpdatedByUi(  );
		}

		public override ImageProperties Icon
		{
			get
			{
				return Images.SummonIcon( Assembly.GetExecutingAssembly(  ), @"Gleed2D.Plugins.Resources.icon_path_item.png" ) ;
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
			var result = new PathItemEditor(  );
	
			result.RecreateFromXml( ParentLayer, ToXml() );

			return result ;
		}

		public override bool ContainsPoint( Vector2 point )
		{
			for( int i = 1; i < _properties.WorldPoints.Count; i++ )
			{
				if( point.DistanceToLineSegment( _properties.WorldPoints[ i ], _properties.WorldPoints[ i - 1 ] )
					<= _properties.LineWidth )
				{
					return true ;
				}
			}
			if( _properties.IsPolygon )
			{
				if( point.DistanceToLineSegment(
					_properties.WorldPoints[ 0 ], _properties.WorldPoints[ _properties.WorldPoints.Count - 1 ] )
						<= _properties.LineWidth )
				{
					return true ;
				}
			}

			return false ;
		}

		/// <summary>
		/// Calculates the WorldPoints based on Position and LocalPoints
		/// </summary>
		protected override void WhenUpdatedByUi( )
		{
			adjustPointsForNewPosition( ) ;
		}

		void adjustPointsForNewPosition( )
		{
			List<Vector2> worldPoints = _properties.WorldPoints ;
			List<Vector2> localPoints = _properties.LocalPoints ;

			for( int i = 0; i < worldPoints.Count; i++ )
			{
				worldPoints[ i ] = localPoints[ i ] + _properties.Position ;
			}
		}

		IMainForm summonMainForm( )
		{
			if( _mainForm == null )
			{
				_mainForm = ObjectFactory.GetInstance<IMainForm>( ) ;
			}

			return _mainForm ;
		}


		public override void OnMouseOver( Vector2 mouseWorldPos )
		{
			_pointUnderMouse = -1 ;

			IMainForm mainForm = summonMainForm( ) ;

			for( int i = 0; i < _properties.WorldPoints.Count; i++ )
			{
				if( mouseWorldPos.DistanceTo( _properties.WorldPoints[ i ] ) <= 5 )
				{
					_pointUnderMouse = i ;
					mainForm.SetCursorForCanvas( Cursors.Hand ) ;
					mainForm.SetToolStripStatusLabel1(
						"{0} (Point {1}: {2})".FormatWith(_properties.Name, i.ToString( ), _properties.WorldPoints[ i ].ToString( )) ) ;
				}
			}

			if( _pointUnderMouse == -1 )
			{
				mainForm.SetCursorForCanvas( Cursors.Default ) ;
			}

			base.OnMouseOver( mouseWorldPos ) ;
		}

		public override void OnMouseButtonDown( Vector2 mouseWorldPos )
		{
			IsHovering = false ;

			if( _pointUnderMouse >= 0 )
			{
				_pointGrabbed = _pointUnderMouse ;
				_initialPos = _properties.WorldPoints[ _pointUnderMouse ] ;
			}
			else
			{
				summonMainForm( ).SetCursorForCanvas( Cursors.SizeAll ) ;

			}
			base.OnMouseButtonDown( mouseWorldPos ) ;
		}

		public override void OnMouseButtonUp( Vector2 mouseWorldPos )
		{
			if( _pointGrabbed == 0 )
			{
				_properties.LocalPoints[ 0 ] = Vector2.Zero ;
				for( int i = 1; i < _properties.LocalPoints.Count; i++ )
				{
					_properties.LocalPoints[ i ] = _properties.WorldPoints[ i ] - _properties.WorldPoints[ 0 ] ;
				}

				_properties.Position = _properties.WorldPoints[ 0 ] ;
				WhenUpdatedByUi( ) ;
			}

			_pointGrabbed = -1 ;
			base.OnMouseButtonUp( mouseWorldPos ) ;
		}

		public override void SetPosition( Vector2 pos )
		{
			if( _pointGrabbed >= 0 )
			{
				_properties.LocalPoints[ _pointGrabbed ] = _initialPos + pos - _properties.Position * 2 ;
				
				WhenUpdatedByUi( ) ;
				
				summonMainForm( ).SetToolStripStatusLabel1(
					"{0} (Point {1}: {2})".FormatWith(_properties.Name, _pointGrabbed.ToString( ), _properties.WorldPoints[ _pointGrabbed ].ToString( )) ) ;
			}
			else
			{
				base.SetPosition( pos ) ;
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
			int lastItemIndex = _properties.WorldPoints.Count - 1 ;

			Vector2 mouseWorldPos = MouseStatus.WorldPosition ;

			//_properties.WorldPoints[ lastItemIndex ] = mouseWorldPos ;
			//_properties.LocalPoints[ lastItemIndex ] = _properties.WorldPoints[0] - mouseWorldPos ;

			//}

			
			if( MouseStatus.IsNewLeftMouseButtonClick(  ) )
			{
				addWorldPoint( mouseWorldPos ) ;

				WhenUpdatedByUi(  );
			}

			if( MouseStatus.IsNewMiddleMouseButtonClick(  ) )
			{
				_editorMode=ItemEditorMode.Created;
				
				PreviewEndedReadyForCreation( this, EventArgs.Empty ) ;
			}

			if( KeyboardStatus.IsNewKeyPress( Keys.Back ) )
			{
				if( _properties.WorldPoints.Count > 1 )
				{
					int lastItem = lastItemIndex ;
					_properties.WorldPoints.RemoveAt( lastItem );
					_properties.LocalPoints.RemoveAt( lastItem );

					WhenUpdatedByUi(  );
				}
			}
		}

		void addWorldPoint( Vector2 mouseWorldPos )
		{
			_properties.WorldPoints.Add( mouseWorldPos ) ;
			_properties.LocalPoints.Add( mouseWorldPos - _properties.WorldPoints[ 0 ] ) ;
		}

		public override bool CanRotate( )
		{
			return true ;
		}

		public override float Rotation
		{
			get
			{
				return (float) Math.Atan2( _properties.LocalPoints[ 1 ].Y, _properties.LocalPoints[ 1 ].X ) ;
			}
			set
			{
				var current = (float) Math.Atan2( _properties.LocalPoints[ 1 ].Y, _properties.LocalPoints[ 1 ].X ) ;
				float delta = value - current ;

				Matrix matrix = Matrix.CreateRotationZ( delta ) ;

				for( int i = 1; i < _properties.LocalPoints.Count; i++ )
				{
					_properties.LocalPoints[ i ] = Vector2.Transform( _properties.LocalPoints[ i ], matrix ) ;
				}

				WhenUpdatedByUi( ) ;
			}
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
				float length = ( _properties.LocalPoints[ 1 ] - _properties.LocalPoints[ 0 ] ).Length( ) ;
				return new Vector2( length, length ) ;
			}
			set
			{

				float factor = value.X / ( _properties.LocalPoints[ 1 ] - _properties.LocalPoints[ 0 ] ).Length( ) ;
				for( int i = 1; i < _properties.LocalPoints.Count; i++ )
				{
					Vector2 olddistance = _properties.LocalPoints[ i ] - _properties.LocalPoints[ 0 ] ;
					_properties.LocalPoints[ i ] = _properties.LocalPoints[ 0 ] + olddistance * factor ;
				}
				WhenUpdatedByUi( ) ;
			}
		}

		public override void DrawInEditor( SpriteBatch spriteBatch )
		{
			if( !_properties.Visible )
			{
				return ;
			}

			Color lineColor = _properties.LineColor ;

			if( IsHovering && Constants.Instance.EnableHighlightOnMouseOver )
			{
				lineColor = Constants.Instance.ColorHighlight ;
			}

			var drawing = ObjectFactory.GetInstance<IDrawing>( ) ;
			if( _properties.IsPolygon )
			{
				drawing.DrawPolygon( spriteBatch, _properties.WorldPoints.ToArray(  ), lineColor, _properties.LineWidth ) ;
			}
			else
			{
				drawing.DrawPath( spriteBatch, _properties.WorldPoints.ToArray(  ), lineColor, _properties.LineWidth ) ;
				if( _editorMode == ItemEditorMode.Creating )
				{
					drawing.DrawLine(
						spriteBatch,
						_properties.WorldPoints.Last( ),
						MouseStatus.WorldPosition,
						Constants.Instance.ColorPrimitives,
						Constants.Instance.DefaultPathItemLineWidth ) ;
				}
			}

		}

		public override void DrawSelectionFrame( SpriteBatch spriteBatch, Color color )
		{
			var worldPoints = _properties.WorldPoints.ToArray( ) ;

			var drawing = ObjectFactory.GetInstance<IDrawing>( ) ;

			if( _properties.IsPolygon )
			{
				drawing.DrawPolygon( spriteBatch, worldPoints, color, 2 ) ;
			}
			else
			{
				drawing.DrawPath( spriteBatch, worldPoints, color, 2 ) ;
			}

			foreach( Vector2 p in worldPoints )
			{
				drawing.DrawCircleFilled( spriteBatch, p, 4, color ) ;
			}

			drawing.DrawBoxFilled( spriteBatch, worldPoints[ 0 ].X - 5, worldPoints[ 0 ].Y - 5, 10, 10, color ) ;
		}
	}
}