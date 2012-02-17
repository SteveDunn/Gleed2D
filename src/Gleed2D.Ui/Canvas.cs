using System;
using System.Collections.Generic;
using System.Drawing ;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Windows.Forms ;
using Gleed2D.Core ;
using Gleed2D.InGame ;
using JetBrains.Annotations ;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using GLEED2D.Properties;
using StructureMap ;
using ButtonState = Microsoft.Xna.Framework.Input.ButtonState ;
using Keys = Microsoft.Xna.Framework.Input.Keys ;
using Rectangle = Microsoft.Xna.Framework.Rectangle ;

namespace GLEED2D
{
	[PublicAPI]
	public class Canvas : ICanvas
	{
		readonly Dictionary<UserActionInEditor, Action> _inputActionLookup ;
		readonly List<Vector2> _positionsBeforeUserInteraction ; //position before user interaction
		readonly List<float> _rotationsBeforeUserInteraction ; //rotation before user interaction
		readonly List<Vector2> _scalesBeforeUserInteraction ; //scale before user interaction

		readonly Cursor _cursorRotate ;
		readonly Cursor _cursorScale ;

		readonly SnapPoint _snapPoint= new SnapPoint(  );

		readonly char[ ] _toTrim = {
			'1', '2', '3', '4', '5', '6', '7', '8', '9', '0', '_'
		} ;

		readonly IMainForm _mainForm ;
		readonly IModel _model ;

		readonly InputHandlerForWhenEditorIdle _inputHandlerForWhenEditorIdle  ;
		readonly InputHandlerForWhenMovingOrCopyingItems _inputHandlerForWhenMovingOrCopyingItems  ;
		readonly InputHandlerWhenRotatingItems _inputHandlerWhenRotatingItems  ;
		readonly InputHandlerWhenScalingItems _inputHandlerWhenScalingItems  ;
		readonly InputHandlerWhenCreatingTheSelectionRectangle _inputHandlerWhenCreatingTheSelectionRectangle;

		readonly EntityCreation _entityCreation ;

		Rectangle _selectionRectangle ;

		Vector2 _grabbedPoint ;
		Vector2 _initialCameraPosition ;

		UserActionInEditor _userActionInEditor ;

		public Canvas( IMainForm mainForm, IModel model )
		{
			_inputHandlerForWhenEditorIdle = new InputHandlerForWhenEditorIdle( this );
			_inputHandlerForWhenMovingOrCopyingItems = new InputHandlerForWhenMovingOrCopyingItems( this );
			_inputHandlerWhenRotatingItems = new InputHandlerWhenRotatingItems( this );
			_inputHandlerWhenScalingItems = new InputHandlerWhenScalingItems( this );
			_inputHandlerWhenCreatingTheSelectionRectangle = new InputHandlerWhenCreatingTheSelectionRectangle( this );
			
			_mainForm = mainForm ;

			_model = model ;

			_model.NewModelLoaded += newModelLoaded ;

			_entityCreation = new EntityCreation( whenEntityReadyToBeAdded: ( ) =>
					{
						addTheEntityCurrentlyBeingCreated( ) ;
						_entityCreation.ClearCurrentEditor( ) ;
						_entityCreation.StartedCreating = false ;
					} ) ;

			_inputActionLookup = new Dictionary<UserActionInEditor, Action>
				{
					{
						UserActionInEditor.Idle, _inputHandlerForWhenEditorIdle.Update
						},
					{
						UserActionInEditor.MovingItems, _inputHandlerForWhenMovingOrCopyingItems.Update
						},
					{
						UserActionInEditor.CopyingItems, _inputHandlerForWhenMovingOrCopyingItems.Update
						},
					{
						UserActionInEditor.RotatingItems, _inputHandlerWhenRotatingItems.Update
						},
					{
						UserActionInEditor.ScalingItems, _inputHandlerWhenScalingItems.Update
						},
					{
						UserActionInEditor.MovingTheCamera, handleUpdateWhenMovingTheCamera
						},
					{
						UserActionInEditor.CreatingSelectionBoxByDragging, _inputHandlerWhenCreatingTheSelectionRectangle.Update
						},
					{
						UserActionInEditor.AddingAnItem, handleInputWhenAddingAnEntity
						},
				} ;

			_userActionInEditor = UserActionInEditor.Idle ;

			_positionsBeforeUserInteraction = new List<Vector2>( ) ;

			_rotationsBeforeUserInteraction = new List<float>( ) ;

			_scalesBeforeUserInteraction = new List<Vector2>( ) ;

			Stream stream = safeGetManifestResourceStream( @"GLEED2D.Resources.cursors.dragcopy.cur" ) ;

			new Cursor( stream ) ;

			stream = safeGetManifestResourceStream( @"GLEED2D.Resources.cursors.rotate.cur" ) ;
			_cursorRotate = new Cursor( stream ) ;

			stream = safeGetManifestResourceStream( @"GLEED2D.Resources.cursors.scale.cur" ) ;
			_cursorScale = new Cursor( stream ) ;

			Constants.TryToLoadOtherwiseSetDefaults( @"settings.xml" ) ;
		}

		void newModelLoaded( object sender, EventArgs e )
		{
			Camera = new Camera( _mainForm.CanvasSize.Width, _mainForm.CanvasSize.Height )
				{
					Position = getLevel( ).CameraPosition
				} ;
		}

		static Stream safeGetManifestResourceStream( string resourceName )
		{
			Stream stream = Assembly.GetExecutingAssembly( ).GetManifestResourceStream( resourceName ) ;

			if( stream == null )
			{
				throw new InvalidOperationException( @"Cannot load cursor named '{0}'.  Not found in the manifest resources." ) ;
			}

			return stream ;
		}

		IEnumerable<ItemEditor> selectedEditors( )
		{
			return getLevel( ).SelectedEditors ;
		}

		public Camera Camera
		{
			get ;
			private set ;
		}

		public void StartCreatingEntityAfterNextClick(IEntityCreationProperties properties)
		{
			startCreatingEntity( properties, false );
		}

		public EntityCreation StartCreatingEntityNow(IEntityCreationProperties properties)
		{
			startCreatingEntity( properties, true );
			return _entityCreation;
		}

		void startCreatingEntity( IEntityCreationProperties properties, bool addToCanvasNow )
		{
			if( _model.Level.ActiveLayer == null )
			{
				MessageBox.Show( Resources.No_Layer ) ;

				return ;
			}

			_userActionInEditor = UserActionInEditor.AddingAnItem ;

			_mainForm.SetCursorForCanvas( Cursors.Cross ) ;

			_entityCreation.StartedCreating = addToCanvasNow ;
			_entityCreation.CreationProperties = properties ;

			var extensibility = ObjectFactory.GetInstance<IExtensibility>( ) ;

			var plugin = extensibility.FindPluginInstanceForType( properties.PluginType ) ;

			var newEditor = (ItemEditor) Activator.CreateInstance( plugin.EditorType ) ;

			newEditor.WhenChosenFromToolbox( ) ;
	
			if (addToCanvasNow)
			{
				newEditor.CreateInDesignMode( _model.Level.ActiveLayer, _entityCreation.CreationProperties ) ;
				_entityCreation.CurrentEditor = newEditor;
			}
		}

		ItemEditor buildPrimitiveEditorReadyForDesigning( IEntityCreationProperties creationProperties )
		{
			var extensibility = ObjectFactory.GetInstance<IExtensibility>( ) ;

			var editor = extensibility.GetNewEditor(creationProperties.PluginType);

			editor.CreateInDesignMode( _model.Level.ActiveLayer, creationProperties ) ;

			return editor ;
		}

		public void TrySetCameraZoom( float zoom )
		{
			if( Camera != null )
			{
				Camera.Scale = zoom ;
			}
		}

		public void SetViewportSize( Size size )
		{
			Camera.UpdateViewport( size.Width, size.Height ) ;
		}

		public void CreateSelectionRectangle()
		{
			_model.SelectLayer( _model.ActiveLayer ) ;

			_grabbedPoint = MouseStatus.WorldPosition ;
			_selectionRectangle = Rectangle.Empty ;
			_userActionInEditor = UserActionInEditor.CreatingSelectionBoxByDragging ;
			_model.SelectLayer( _model.ActiveLayer ) ;
		}

		public void StartMovingCamera()
		{
			_grabbedPoint = MouseStatus.ScreenPosition ;
			_initialCameraPosition = Camera.Position ;
			_userActionInEditor = UserActionInEditor.MovingTheCamera ;
			_mainForm.SetCursorForCanvas( Cursors.SizeAll ) ;
		}

		public void StartRotatingItems()
		{
			_grabbedPoint = MouseStatus.WorldPosition - selectedEditors( ).First( ).ItemProperties.Position ;

			//save the initial rotation for each item
			_rotationsBeforeUserInteraction.Clear( ) ;

			foreach( ItemEditor eachSelectedItem in selectedEditors( ) )
			{
				if( eachSelectedItem.CanRotate( ) )
				{
					_rotationsBeforeUserInteraction.Add( eachSelectedItem.Rotation ) ;
				}
			}

			_userActionInEditor = UserActionInEditor.RotatingItems ;
			_mainForm.SetCursorForCanvas( _cursorRotate ) ;

			IoC.Memento.BeginCommand( "Rotate Item(s)" ) ;
		}

		public void StartScalingSelectedItems()
		{
			_grabbedPoint = MouseStatus.WorldPosition - selectedEditors( ).First( ).ItemProperties.Position ;

			//save the initial scale for each item
			_scalesBeforeUserInteraction.Clear( ) ;
			foreach( ItemEditor selitem in selectedEditors( ) )
			{
				if( selitem.CanScale )
				{
					_scalesBeforeUserInteraction.Add( selitem.Scale ) ;
				}
			}

			_userActionInEditor = UserActionInEditor.ScalingItems ;

			_mainForm.SetCursorForCanvas( _cursorScale ) ;

			IoC.Memento.BeginCommand( "Scale Item(s)" ) ;
		}

		public void StartCopyingSelectedItems()
		{
			var clonedItems = new List<ItemEditor>( ) ;

			LevelEditor level = getLevel( ) ;

			var editors = selectedEditors( ) ;
			foreach( ItemEditor eachEditor in editors )
			{
				ItemEditor clone = eachEditor.Clone( ) ;

				int nextItemNumber = level.GetNextItemNumber( ) ;

				ItemProperties itemProperties = clone.ItemProperties ;

				itemProperties.Name = @"{0}_{1}".FormatWith(itemProperties.Name.TrimEnd( _toTrim ), nextItemNumber) ;
				itemProperties.Id = level.GenerateId( nextItemNumber ) ;

				clone.IsSelected = true ;
				eachEditor.IsSelected = false ;

				clonedItems.Add( clone ) ;
			}

			IoC.Memento.BeginCommand( "Copy {0} item(s)".FormatWith( clonedItems.Count ) ) ;

			level.AddEditors( clonedItems ) ;

			IoC.Model.SelectEditors( new SelectedEditors( clonedItems ) ) ;

			startMoving( ) ;
		}

		public void StartMovingSelectedItems()
		{
			IoC.Memento.BeginCommand( "Change Item(s)" ) ;
			startMoving( ) ;
		}

		void destroyPrimitiveBrush( )
		{
			_userActionInEditor = UserActionInEditor.Idle ;

			_mainForm.SetCursorForCanvas( Cursors.Default ) ;
		}

		void addTheEntityCurrentlyBeingCreated( )
		{
			ItemEditor currentEditor = _entityCreation.CurrentEditor ;

			int nextNumber = _model.Level.GetNextItemNumber( ) ;
			
			currentEditor.ItemProperties.Id = _model.Level.GenerateId( nextNumber  ) ;
			currentEditor.ItemProperties.Name = @"{0}_{1}".FormatWith( currentEditor.NameSeed, nextNumber ) ;

			var model = ObjectFactory.GetInstance<IModel>( ) ;

			model.AddEditor( currentEditor );
		}

		//ItemEditor buildPrimitiveEditorReadyForDesigning( IEntityCreationProperties creationProperties )
		//{
		//    var extensibility = ObjectFactory.GetInstance<IExtensibility>( ) ;

		//    IEditorPlugin plugin = extensibility.FindPluginInstanceForType( creationProperties.PluginType ) ;

		//    var editor = (ItemEditor) Activator.CreateInstance( plugin.EditorType ) ;

		//    editor.CreateInDesignMode( _model.Level.ActiveLayer, creationProperties ) ;

		//    return editor ;
		//}


		//void paintPrimitiveBrush()
		//{
		//    int nextnum = Level.GetNextItemNumber();

		//    IExtensibility extensibility = ObjectFactory.GetInstance<IExtensibility>( ) ;
		//    extensibility.

		//    switch (_currentPrimitive)
		//    {


		//        case PrimitiveType.Rectangle:
		//            ItemEditor rectangleEditor;
		//            if (_customEntity)
		//            {
		//                rectangleEditor = (ItemEditor)Activator.CreateInstance(_customEntityType, Extensions.RectangleFromVectors(_clickedPoints[0], _clickedPoints[1]));
		//            }
		//            else
		//            {
		//                rectangleEditor = new RectangleItemEditor(
		//                    SelectedLayer,
		//                    new CreationProperties
		//                        {
		//                            CurrentMousePosition = _mouseWorldPos,
		//                            ClickPositions = _clickedPoints.ToArray( )
		//                        } ) ;
		//            }

		//            rectangleEditor.ItemProperties.Id = generateID(nextnum);
		//            rectangleEditor.ItemProperties.Name = rectangleEditor.GetNamePrefix() + nextnum;

		//            BeginCommand(string.Format( @"Add Item ""{0}""", rectangleEditor.ItemProperties.Name ));

		//            addItemEditor(rectangleEditor);

		//            EndCommand();

		//            _mainForm.SetToolStripStatusLabel1(Resources.Rectangle_Entered);

		//            break;

		//        case PrimitiveType.Circle:
		//            ItemEditor circleEditor;
		//            if (_customEntity)
		//            {
		//                circleEditor = (ItemEditor)Activator.CreateInstance(_customEntityType, _clickedPoints[0], (_mouseWorldPos - _clickedPoints[0]).Length());
		//            }
		//            else
		//            {
		//                circleEditor = new CircleItemEditor( 
		//                    SelectedLayer, 
		//                    new CreationProperties
		//                        {
		//                            CurrentMousePosition = _mouseWorldPos, 
		//                            ClickPositions = _clickedPoints.ToArray(  ) 
		//                        } );
		//            }
		//            circleEditor.ItemProperties.Id = generateID(nextnum);
		//            circleEditor.ItemProperties.Name = circleEditor.GetNamePrefix() + nextnum;// level.getNextItemNumber(); 

		//            BeginCommand(string.Format( "Add Item \"{0}\"", circleEditor.ItemProperties.Name ));
		//            addItemEditor(circleEditor);
		//            EndCommand();
		//            _mainForm.SetToolStripStatusLabel1(Resources.Circle_Entered);
		//            break;

		//        case PrimitiveType.Path:
		//            PathItemEditor itemEditor;
		//            if (_customEntity)
		//            {
		//                itemEditor = (PathItemEditor)Activator.CreateInstance(_customEntityType, _clickedPoints.ToArray());
		//            }
		//            else
		//            {
		//                itemEditor = new PathItemEditor(
		//                    SelectedLayer,
		//                    new CreationProperties
		//                        {
		//                            ClickPositions = _clickedPoints.ToArray( )
		//                        } ) ;
		//            }

		//            itemEditor.ItemProperties.Id = generateID(nextnum);
		//            itemEditor.ItemProperties.Name = itemEditor.GetNamePrefix()+nextnum;//

		//            BeginCommand(string.Format( "Add Item \"{0}\"", itemEditor.ItemProperties.Name ));

		//            addItemEditor(itemEditor);

		//            EndCommand();

		//            _mainForm.SetToolStripStatusLabel1(Resources.Path_Entered);

		//            break;
		//    }

		//    UpdateTreeView();
		//}


		void startMoving( )
		{
			_grabbedPoint = MouseStatus.WorldPosition ;

			//save the distance to mouse for each item
			_positionsBeforeUserInteraction.Clear( ) ;

			foreach( ItemEditor selitem in selectedEditors( ) )
			{
				_positionsBeforeUserInteraction.Add( selitem.ItemProperties.Position ) ;
			}

			_userActionInEditor = UserActionInEditor.MovingItems ;
		}

		LevelEditor getLevel( )
		{
			return _model.Level ;
		}

		public void SetMousePosition( int screenx, int screeny )
		{
			Vector2 maincameraposition = Camera.Position ;

			if( getLevel( ).ActiveLayer != null )
			{
				Camera.Position *= getLevel( ).ActiveLayer.ScrollSpeed ;
			}

			MouseStatus.WorldPosition = Vector2.Transform( new Vector2( screenx, screeny ), Matrix.Invert( Camera.Matrix ) ) ;

			if( Constants.Instance.SnapToGrid || KeyboardStatus.IsKeyDown( Keys.G ) )
			{
				MouseStatus.WorldPosition = SnapToGrid( MouseStatus.WorldPosition ) ;
			}

			Camera.Position = maincameraposition ;
		}

		[CanBeNull]
		ItemEditor tryGetItemAtPosition( Vector2 mouseworldpos )
		{
			if( getLevel( ).ActiveLayer == null )
			{
				return null ;
			}

			return getLevel( ).ActiveLayer.TryGetItemAtPosition( mouseworldpos ) ;
		}

		public Vector2 SnapToGrid( Vector2 input )
		{
			Vector2 result = input ;
			
			Vector2 gridSpacing = Constants.Instance.GridSpacing;
			
			result.X = gridSpacing.X * (int) Math.Round( result.X / gridSpacing.X ) ;
			result.Y = gridSpacing.Y * (int) Math.Round( result.Y / gridSpacing.Y ) ;

			_snapPoint.Position = result ;
			_snapPoint.Visible = true ;

			return result ;
		}

		public void SetModeToIdle()
		{
			_userActionInEditor = UserActionInEditor.Idle ;

			_mainForm.SetCursorForCanvas( Cursors.Default ) ;
		}

		public void SetModeTo( UserActionInEditor userAction )
		{
			_userActionInEditor = userAction ;
		}

		public void AddNewItemAtMouse( ItemEditor newEditor )
		{
			int nextNumber = _model.Level.GetNextItemNumber();

			newEditor.SetPosition( MouseStatus.WorldPosition );
			newEditor.ItemProperties.Id = _model.Level.GenerateId(nextNumber);
			newEditor.ItemProperties.Name = @"{0}_{1}".FormatWith(newEditor.NameSeed, nextNumber);
			newEditor.ParentLayer = _model.ActiveLayer ;

			_model.AddEditor(newEditor);
		}

		public void StopCreatingEntity()
		{
			_userActionInEditor=UserActionInEditor.Idle;
		}

		public void CancelCreatingEntity()
		{
			_entityCreation.ClearCurrentEditor();
			_userActionInEditor=UserActionInEditor.Idle;
		}

		public ItemEditor ItemUnderMouse
		{
			get
			{
				return tryGetItemAtPosition( MouseStatus.WorldPosition ) ;
			}
		}

		public Vector2 GrabPoint
		{
			get
			{
				return _grabbedPoint ;
			}
		}

		public UserActionInEditor CurrentUserAction
		{
			get
			{
				return _userActionInEditor ;
			}
		}

		public List<Vector2> PositionsBeforeUserInteraction
		{
			get
			{
				return _positionsBeforeUserInteraction ;
			}
		}

		public List<float> RotationsBeforeUserInteraction
		{
			get
			{
				return _rotationsBeforeUserInteraction ;
			}
		}

		public Rectangle SelectionRectangle
		{
			get
			{
				return _selectionRectangle ;
			}
			set
			{
				_selectionRectangle = value ;
			}
		}

		public List<Vector2> ScalesBeforeUserInteraction
		{
			get
			{
				return _scalesBeforeUserInteraction ;
			}
		}

		public void Update( GameTime gameTime )
		{
			if( getLevel( ) == null )
			{
				return ;
			}

			if (_mainForm.CanvasHasFocus && _userActionInEditor!= UserActionInEditor.RunningBehaviour)
			{
				handleInput( gameTime ) ;
			}

			_model.Level.Update( gameTime ) ;
			_model.Level.Layers.ForEach(
				l =>
					{
						l.Update( gameTime ) ;
						l.Items.ForEach( i=>i.Update( gameTime ) ) ;
					} ) ;
		}

		void handleInput( GameTime gameTime )
		{
			KeyboardStatus.Update( Keyboard.GetState( ) ) ;

			MouseStatus.Update( Mouse.GetState( ), Camera ) ;

			handleCameraControls( gameTime ) ;


			if( _inputActionLookup.ContainsKey( _userActionInEditor ) )
			{
				_inputActionLookup[ _userActionInEditor ]( ) ;
			}
		}

		void handleCameraControls( GameTime gameTime )
		{
			int scrollWheelDelta = MouseStatus.ScrollwheelDelta ;

			if( scrollWheelDelta > 0 /* && kstate.IsKeyDown(Keys.LeftControl)*/ )
			{
				float zoom = (float) Math.Round( Camera.Scale * 10 ) * 10.0f + 10.0f ;

				_mainForm.SetZoomComboText( zoom.ToString( ) + "%" ) ;

				Camera.Scale = zoom / 100.0f ;
			}

			if( scrollWheelDelta < 0 /* && kstate.IsKeyDown(Keys.LeftControl)*/ )
			{
				float zoom = (float) Math.Round( Camera.Scale * 10 ) * 10.0f - 10.0f ;
				if( zoom <= 0.0f )
				{
					return ;
				}

				_mainForm.SetZoomComboText( zoom.ToString( ) + "%" ) ;

				Camera.Scale = zoom / 100.0f ;
			}

			//Camera movement
			float delta ;
			if( KeyboardStatus.IsKeyDown( Keys.LeftShift ) )
			{
				delta = Constants.Instance.CameraFastSpeed * (float) gameTime.ElapsedGameTime.TotalSeconds ;
			}
			else
			{
				delta = Constants.Instance.CameraSpeed * (float) gameTime.ElapsedGameTime.TotalSeconds ;
			}

			bool leftControlKeyNotPressed = KeyboardStatus.IsKeyUp( Keys.LeftControl ) ;

			if( KeyboardStatus.IsKeyDown( Keys.W ) && leftControlKeyNotPressed )
			{
				Camera.Position += ( new Vector2( 0, -delta ) ) ;
			}

			if( KeyboardStatus.IsKeyDown( Keys.S ) && leftControlKeyNotPressed )
			{
				Camera.Position += ( new Vector2( 0, +delta ) ) ;
			}

			if( KeyboardStatus.IsKeyDown( Keys.A ) && leftControlKeyNotPressed )
			{
				Camera.Position += ( new Vector2( -delta, 0 ) ) ;
			}

			if( KeyboardStatus.IsKeyDown( Keys.D ) && leftControlKeyNotPressed )
			{
				Camera.Position += ( new Vector2( +delta, 0 ) ) ;
			}

			if( KeyboardStatus.IsKeyDown( Keys.Subtract ) )
			{
				var zoom = (float) ( Camera.Scale * 0.995 ) ;

				_mainForm.SetZoomComboText( textualRepresentationOfZoom( zoom ) ) ;

				Camera.Scale = zoom ;
			}

			if( KeyboardStatus.IsKeyDown( Keys.Add ) )
			{
				var zoom = (float) ( Camera.Scale * 1.005 ) ;
				_mainForm.SetZoomComboText( textualRepresentationOfZoom( zoom ) ) ;
				Camera.Scale = zoom ;
			}

			//get mouse world position considering the ScrollSpeed of the current layer
			Vector2 maincameraposition = Camera.Position ;
			if( getLevel( ).ActiveLayer != null )
			{
				Camera.Position *= getLevel( ).ActiveLayer.ScrollSpeed ;
			}

			MouseStatus.UpdateCamera( Camera ) ;

			MouseStatus.WorldPosition = Vector2.Transform( MouseStatus.ScreenPosition, Matrix.Invert( Camera.Matrix ) ) ;
			MouseStatus.WorldPosition = MouseStatus.WorldPosition.Round( ) ;

			_mainForm.SetToolStripStatusLabel3(
				"Mouse: ({0}, {1})".FormatWith(MouseStatus.WorldPosition.X, MouseStatus.WorldPosition.Y) ) ;

			Camera.Position = maincameraposition ;
		}

		void handleUpdateWhenMovingTheCamera( )
		{
			Vector2 distance = ( MouseStatus.ScreenPosition - _grabbedPoint ) / Camera.Scale ;
			if( distance.Length( ) > 0 )
			{
				Camera.Position = _initialCameraPosition - distance ;
			}

			if( MouseStatus.MiddleButton == ButtonState.Released )
			{
				_userActionInEditor = UserActionInEditor.Idle ;
				_mainForm.SetCursorForCanvas( Cursors.Default ) ;
			}
		}

		void handleInputWhenAddingAnEntity( )
		{
			//mef: this might need to go back to the main Update method
			if( Constants.Instance.SnapToGrid || KeyboardStatus.IsKeyDown( Keys.G ) )
			{
				MouseStatus.WorldPosition = SnapToGrid( MouseStatus.WorldPosition ) ;
			}

			if( MouseStatus.IsNewRightMouseButtonClick( ) || KeyboardStatus.IsNewKeyPress( Keys.D3 ) )
			{
				//mef: delegate to current primitive plugin
				if( _entityCreation.StartedCreating )
				{
					_entityCreation.StartedCreating = false ;
					_entityCreation.ClearCurrentEditor( ) ;

					//mef: implement
					//switch (_currentPrimitiveName)
					//{
					//    case PrimitiveType.Rectangle:
					//        //_mainForm.SetToolStripStatusLabel1(Resources.Rectangle_Entered);
					//        break;
					//    case PrimitiveType.Circle:
					//        //_mainForm.SetToolStripStatusLabel1(Resources.Circle_Entered);
					//        break;
					//    case PrimitiveType.Path:
					//        //_mainForm.SetToolStripStatusLabel1(Resources.Path_Entered);
					//        break;
					//}
				}
				else
				{
					destroyPrimitiveBrush( ) ;
					_entityCreation.StartedCreating = false ;
				}
			}

			if( _entityCreation.StartedCreating )
			{
				_entityCreation.CurrentEditor.UserInteractionDuringCreation( ) ;

				return ;
			}

			if( MouseStatus.IsNewLeftMouseButtonClick( ) || KeyboardStatus.IsNewKeyPress( Keys.D3 ) )
			{
				if( _entityCreation.StartedCreating == false )
				{
					_entityCreation.StartedCreating = true ;
					_entityCreation.CurrentEditor = buildPrimitiveEditorReadyForDesigning( _entityCreation.CreationProperties ) ;
				}
			}
		}

		static string textualRepresentationOfZoom( float zoom )
		{
			return ( zoom * 100 ).ToString( "###0.0" ) + "%" ;
		}

		public void Draw( GameTime gameTime, GraphicsDevice graphicsDevice, SpriteBatch spriteBatch )
		{
			ObjectFactory.GetInstance<IGleedRenderer>().Draw( 
				new RendererParams( Camera, _userActionInEditor, _selectionRectangle, _entityCreation, _snapPoint ) ) ;
		}
	}
}
