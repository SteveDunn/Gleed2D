using System ;
using System.Collections.Generic ;
using System.Globalization;
using System.IO ;
using System.Drawing ;
using System.Linq ;
using System.Windows.Forms ;
using System.Xml.Linq ;
using GLEED2D.Properties ;
using System.Diagnostics ;
using Gleed2D.Core ;
using Gleed2D.Core.Behaviour ;
using Gleed2D.Core.Controls ;
using Gleed2D.Core.UserActions ;
using StructureMap ;
using Point = System.Drawing.Point ;

namespace GLEED2D.Forms
{
	public partial class MainForm : Form, IMainForm
	{
		readonly IModel _model ;
		readonly IMemento _memento ;

		//todo: encapsulate this somewhere
		string _levelFilename;

		//todo: encapsulate
		bool _dirtyFlag;

		ICanvas _canvas ;
		IGame _game ;
		bool _built ;

		public MainForm( )
		{
			_memento = ObjectFactory.GetInstance<IMemento>() ;

			_model = ObjectFactory.GetInstance<IModel>() ;

			InitializeComponent();

			_model.ActiveLayerChanged += activeLayerChanged ;
			_model.NewModelLoaded += newModelLoaded ;
			_model.OnBeforeUnloadingModel += modelUnloading ;

			_memento.OnCommandEnded += mementoCommandEnded ;
			_memento.OnCommandUndone += mementoCommandUndone;
			_memento.OnCommandRedone += mementoCommandRedone;

			uiAssetsControl.AssetChosenByDoubleClicking += assetChosenByDoubleClicking ;
		}

		void assetChosenByDoubleClicking( object sender, EntityChosenEventArgs e )
		{
			IoC.Canvas.StartCreatingEntityAfterNextClick(e.EntityCreationProperties);
		}

		void modelUnloading( object sender, ModelUnloadingEventArgs e )
		{
			DialogResult r = promptToSaveIfDirty( ) ;
			
			if( r == DialogResult.Cancel )
			{
				e.Cancelled = true ;
			}
		}

		void loadOrCreateNewLevel( )
		{
			if( Environment.GetCommandLineArgs( ).Length > 1 )
			{
				string filename = Environment.GetCommandLineArgs( )[ 1 ] ;
				if (!File.Exists(filename))
				{
					MessageBox.Show(
						@"Could not find file specified in the command line{0}{1}".FormatWith(Environment.NewLine, filename),
						@"File specified in command line not found",
						MessageBoxButtons.OK,
						MessageBoxIcon.Information);
				}
				else
				{
					try
					{
						loadLevel( filename ) ;
						return ;
					}
					catch( IOException ex )
					{
						MessageBox.Show(
							@"Something went wrong loading the file. {0}.{1}{2}".FormatWith(filename, Environment.NewLine, ex.Message),
							@"Could not load the file specified in the command line",
							MessageBoxButtons.OK,
							MessageBoxIcon.Information ) ;
					}
				}
			}
 
			newLevel( ) ;
		}

		void mementoCommandRedone( object sender, CommandEndedArgs e )
		{
			setDirtyFlag( true ) ;
			undoButton.Enabled = undoMenuItem.Enabled = e.UndoCount > 0 ;
			redoButton.Enabled = redoMenuItem.Enabled = e.RedoCount > 0 ;
			undoButton.DropDownItems.Insert( 0, redoButton.DropDownItems[ 0 ] ) ;
		}

		void mementoCommandUndone( object sender, CommandEndedArgs e )
		{
			setDirtyFlag( true ) ;
			
			undoButton.Enabled = undoMenuItem.Enabled = e.UndoCount > 0 ;
			redoButton.Enabled = redoMenuItem.Enabled = e.RedoCount > 0 ;

			redoButton.DropDownItems.Insert( 0, undoButton.DropDownItems[ 0 ] ) ;
		}

		void mementoCommandEnded( object sender, CommandEndedArgs e )
		{
			redoButton.DropDownItems.Clear( ) ;
	
			setDirtyFlag( true ) ;

			undoButton.Enabled = undoMenuItem.Enabled = e.UndoCount > 0 ;
			redoButton.Enabled = redoMenuItem.Enabled = e.RedoCount > 0 ;

			var item = new ToolStripMenuItem( e.Command.Description )
				{
					Tag = e.Command
				} ;

			undoButton.DropDownItems.Insert( 0, item ) ;
		}

		void newModelLoaded( object sender, EventArgs e )
		{
			initialiseForNewLevel( ) ;
		}

		void initialiseForNewLevel( )
		{
			SetZoomComboText( "100%" ) ;

			resetUndoRedoControls( ) ;
		}

		void activeLayerChanged( object sender, EventArgs e )
		{
			LayerEditor layer = _model.Level.ActiveLayer ;
			
			if( layer == null )
			{
				SetToolStripStatusLabel1( "Layer: (none)" ) ;
			}
			else
			{
				SetToolStripStatusLabel2( "Layer: " + layer.Name ) ;
			}
		}

		ICanvas summonEditor( )
		{
			if( _canvas == null )
			{
				_canvas = ObjectFactory.GetInstance<ICanvas>( ) ;
			}
			
			return _canvas ;
		}

		IGame summonGame()
		{
			if( _game == null )
			{
				_game = ObjectFactory.GetInstance<IGame>( ) ;
			}
		
			return _game ;
		}

		void setDirtyFlag( bool value )
		{
			_dirtyFlag = value ;
			updateTitleBar( ) ;
		}

		public IntPtr GetHandle()
		{
			return uiCanvas.Handle;
		}

		public ICategoryTabPage TryGetTabForCategory( string categoryName )
		{
			TabPage tryGetTabForCategory = uiCategoriesTab.TabPages.ContainsKey( categoryName ) ? uiCategoriesTab.TabPages[ categoryName ] : null ;
			return tryGetTabForCategory as ICategoryTabPage ;
		}

		public void AddCategoryTab( ICategoryTabPage categoryTabPage )
		{
			uiCategoriesTab.TabPages.Add( (TabPage)categoryTabPage );
		}

		void updateTitleBar()
		{
			Text = @"Gleed 2D - {0}{1}".FormatWith(_levelFilename, ( _dirtyFlag ? @"*" : string.Empty )) ;
		}

		void mainFormLoad(object sender, EventArgs e)
		{
			loadPlugins( ) ;

			fillZoomComboBox( ) ;

			uiZoomCombo.SelectedText = "100%";

			uiCanvas.AllowDrop = true;

			loadOrCreateNewLevel( ) ;

			_built = true ;
		}

		void loadPlugins( )
		{
			var extensibility = ObjectFactory.GetInstance<IExtensibility>( ) ;
			
			extensibility.EditorPlugins.ForEach( e => e.InitialiseInUi( this ) ) ;
		}

		void fillZoomComboBox( )
		{
			for( int i = 25; i <= 200; i += 25 )
			{
				uiZoomCombo.Items.Add( @"{0}%".FormatWith(i)) ;
			}
		}

		void mainFormFormClosed(object sender, FormClosedEventArgs e)
		{
			Constants.Instance.SaveToDisk("settings.xml");
			
			ObjectFactory.GetInstance<IGame>().Exit();
		}

		void mainFormFormClosing(object sender, FormClosingEventArgs e)
		{
			if (promptToSaveIfDirty() == DialogResult.Cancel)
			{
				e.Cancel = true;
			}
		}

		void canvasPreviewKeyDown(object sender, PreviewKeyDownEventArgs e)
		{
			if( e.KeyCode == Keys.Delete )
			{
				actionDelete( this, EventArgs.Empty ) ;
				
				return ;
			}

			var handleKeyboardCommands = ObjectFactory.GetInstance<IHandleKeyboardCommands>( ) ;

			handleKeyboardCommands.HandleKeyDown( new KeyEventArgs( e.KeyData ) );


			//var args = new KeyEventArgs(e.KeyData);
//			entityTreeKeyDown(sender, args);
		}
		
		void canvasResize(object sender, EventArgs e)
		{
			if (!_built)
			{
				return ;
			}
			
			var game = summonGame( ) ;

			game.ResizeBackBuffer(uiCanvas.Width, uiCanvas.Height);

			summonEditor( ).SetViewportSize( uiCanvas.Size ) ;
		}
		
		void canvasMouseEnter(object sender, EventArgs e)
		{
			uiCanvas.Select();
		}

		void canvasMouseLeave(object sender, EventArgs e)
		{
			uiMenuStrip.Select();

		}

		void canvasDragEnter(object sender, DragEventArgs e)
		{
			e.Effect = DragDropEffects.Move ;

			//todo: encapsulate
			var dragHandler = (HandleDraggingOfAssets) e.Data.GetData(typeof( HandleDraggingOfAssets )) ;

			if( dragHandler != null )
			{
				dragHandler.EnteredEditor( _canvas, e ) ;
				return ;
			}

			var listViewItem = (ListViewItem) e.Data.GetData( typeof( ListViewItem ) ) ;

			summonEditor( ).StartCreatingEntityAfterNextClick( listViewItem.Tag as IEntityCreationProperties ) ;
		}

		void canvasDragOver(object sender, DragEventArgs e)
		{
			e.Effect = DragDropEffects.Move;
	
			Point p = uiCanvas.PointToClient(new Point(e.X, e.Y));

			ICanvas canvas = summonEditor( ) ;

			canvas.SetMousePosition(p.X, p.Y);

			IGame game = summonGame( ) ;
			canvas.Draw(game.GameTime,game.GraphicsDevice,game.SpriteBatch);

			var dragHandler = (HandleDraggingOfAssets) e.Data.GetData( typeof( HandleDraggingOfAssets ) ) ;
			if( dragHandler != null )
			{
				dragHandler.DraggingOverEditor( _canvas, e );
				//return ;
			}

			
			game.GraphicsDevice.Present();
		}
		
		void canvasDragLeave(object sender, EventArgs e)
		{
			IGame game = summonGame( ) ;

			ICanvas canvas = summonEditor( ) ;

			canvas.SetModeToIdle();
			
			game.GraphicsDevice.Present();
		}
		
		void canvasDragDrop(object sender, DragEventArgs e)
		{
			//uiTexturesListView.Cursor = Cursors.Default;
			uiCanvas.Cursor = Cursors.Default;

			var dragHandler = (HandleDraggingOfAssets) e.Data.GetData( typeof( HandleDraggingOfAssets ) ) ;
			
			if( dragHandler != null )
			{
				dragHandler.DroppedOnCanvas( _canvas, e );
			}
		}

		void newLevel()
		{
			Application.DoEvents();

			_model.CreateNewLevel( ) ;
			
			_levelFilename = "untitled";
			
			setDirtyFlag( false );
		}

		void saveLevel(string filename)
		{
			_model.SaveLevel(filename);
			
			_levelFilename = filename;
			
			setDirtyFlag( false );

			if (Constants.Instance.SaveLevelStartApplication)
			{
				if (!File.Exists(Constants.Instance.SaveLevelApplicationToStart))
				{
					MessageBox.Show(
						@"The file ""{0}"" doesn't exist!
Please provide a valid application executable in Tools -> Settings -> Save Level!
Level was saved."
							.FormatWith(Constants.Instance.SaveLevelApplicationToStart),
						@"Warning", MessageBoxButtons.OK, MessageBoxIcon.Warning);
					
					return;
				}

				if (Constants.Instance.SaveLevelAppendLevelFilename)
				{
					Process.Start(Constants.Instance.SaveLevelApplicationToStart, @"""{0}""".FormatWith(_levelFilename));
				}
				else
				{
					Process.Start(Constants.Instance.SaveLevelApplicationToStart);
				}
			}
		}

		void loadLevel(string filename)
		{
			var level = new LevelEditor( XElement.Load( filename  )  ); 

			_model.LoadLevel(level);

			_levelFilename = filename;
			
			setDirtyFlag( false );
		}

		DialogResult promptToSaveIfDirty()
		{
			if (_dirtyFlag)
			{
				DialogResult dr = MessageBox.Show(
					@"The current level has not been saved. Do you want to save now?",
					@"Save?",
					MessageBoxButtons.YesNoCancel,
					MessageBoxIcon.Question ) ;
				
				if (dr == DialogResult.Yes)
				{
					if (_levelFilename == @"untitled")
					{
						var saveFileDialog = new SaveFileDialog
							{
								Filter = Resources.fileDialogFilenameFilter
							} ;
						
						if (saveFileDialog.ShowDialog() == DialogResult.OK)
						{
							saveLevel(saveFileDialog.FileName);
						}
						else
						{
							return DialogResult.Cancel;
						}
					}
					else
					{
						saveLevel(_levelFilename);
					}
				}
				if (dr == DialogResult.Cancel)
				{
					return DialogResult.Cancel;
				}
			}
			
			return DialogResult.OK;
		}

		void fileNewClicked(object sender, EventArgs e)
		{
			newLevel();
		}
		
		void fileOpenClicked(object sender, EventArgs e)
		{
			if (promptToSaveIfDirty() == DialogResult.Cancel)
			{
				return;
			}
			
			var opendialog = new OpenFileDialog
								{
									Filter = Resources.fileDialogFilenameFilter
								} ;
			
			if (opendialog.ShowDialog() == DialogResult.OK)
			{
				loadLevel(opendialog.FileName);
			}
		}
		
		void fileSave(object sender, EventArgs e)
		{
			if (_levelFilename == "untitled")
			{
				onSaveAsClicked(sender,e);
			}
			else
			{
				saveLevel(_levelFilename);
			}
		}
		
		void onSaveAsClicked( object sender, EventArgs eventArgs )
		{
			var saveFileDialog = new SaveFileDialog
				{
					Filter = Resources.fileDialogFilenameFilter
				} ;
			
			if (saveFileDialog.ShowDialog() == DialogResult.OK)
			{
				saveLevel(saveFileDialog.FileName);
			}
		}
		
		void onCloseClicked( object sender, EventArgs eventArgs )
		{
			if( OnShuttingDown != null )
			{
				OnShuttingDown( sender, eventArgs ) ;
			}

			Close();
		}

		void editUndo(object sender, EventArgs e)
		{
			_memento.Undo();
		}

		void editRedo(object sender, EventArgs e)
		{
			_memento.Redo();
		}

		void editSelectAll(object sender, EventArgs e)
		{
			_model.SelectEverythingInSelectedLayer();
		}

		void zoomComboTextChanged(object sender, EventArgs e)
		{
			toolStripStatusLabel5.Text = uiZoomCombo.SelectedText;
			if (uiZoomCombo.Text.Length > 0 )
			{
				float zoom = float.Parse(uiZoomCombo.Text.Substring(0, uiZoomCombo.Text.Length - 1));

				summonEditor( ).TrySetCameraZoom( zoom / 100 ) ;
			}
		}

		void helpQuickGuide(object sender, EventArgs e)
		{
			new QuickGuide().Show();
		}

		void helpAbout(object sender, EventArgs e)
		{
			new AboutForm().ShowDialog();
		}

		void toolsMenuMouseEnter(object sender, EventArgs e)
		{
			IEnumerable<ItemEditor> selectedEditors = _model.Level.SelectedEditors.ToList(  ) ;
			
			uiMoveSelectedItemsToLayerToolMenu.Enabled =
				copySelectedItemsToLayerToolStripMenuItem.Enabled = selectedEditors.Any();
			
			var selectedEditorCount = selectedEditors.Count() ;
			
			alignHorizontallyToolStripMenuItem.Enabled =
			alignVerticallyToolStripMenuItem.Enabled =
			alignRotationToolStripMenuItem.Enabled =
			alignScaleToolStripMenuItem.Enabled = selectedEditorCount > 1;

			linkItemsByACustomPropertyToolStripMenuItem.Enabled = selectedEditorCount == 2;

		}
		void toolsMenuClick(object sender, EventArgs e)
		{
		}

		void toolsAlignHorizontally(object sender, EventArgs e)
		{
			_model.AlignHorizontally();
		}
		
		void toolsAlignVertically(object sender, EventArgs e)
		{
			_model.AlignVertically();
		}
		
		void toolsAlignRotation(object sender, EventArgs e)
		{
			_model.AlignRotation();
		}
		
		void toolsAlignScale(object sender, EventArgs e)
		{
			_model.AlignScale();
		}
		
		void showToolsSettingsForm(object sender, EventArgs e)
		{
			var settingsForm = new SettingsForm();
			
			settingsForm.ShowDialog();
		}

		void mainFormResize(object sender, EventArgs e)
		{
			IGame game = summonGame( ) ;

			//summonEditor( ).Draw(game.SpriteBatch);
			
			game.GraphicsDevice.Present();
			
			Application.DoEvents();
		}

		void undoManyCommands(object sender, ToolStripItemClickedEventArgs e)
		{
			var command = (Command)e.ClickedItem.Tag;
			_memento.UndoMany(command);
		}

		void redoManyCommands(object sender, ToolStripItemClickedEventArgs e)
		{
			var command = (Command)e.ClickedItem.Tag;
			_memento.RedoMany(command);
		}

		public void SetZoomComboText( string text )
		{
			uiZoomCombo.Text = text ;
		}

		void runLevel(object sender, EventArgs e)
		{
			if( !Constants.Instance.RunLevelStartApplication )
			{
				return ;
			}
			
			if (!File.Exists(Constants.Instance.RunLevelApplicationToStart))
			{
				MessageBox.Show(
					"The file \"{0}\" doesn't exist!\nPlease provide a valid application executable in Tools -> Settings -> Run Level!".FormatWith(Constants.Instance.RunLevelApplicationToStart),
					@"Error",
					MessageBoxButtons.OK,
					MessageBoxIcon.Error ) ;
					
				return;
			}

			fileSave(sender, e);
				
			if (Constants.Instance.RunLevelAppendLevelFilename)
			{
				Process.Start(Constants.Instance.RunLevelApplicationToStart, "\"" + _levelFilename + "\"");
			}
			else
			{
				Process.Start(Constants.Instance.RunLevelApplicationToStart);
			}
		}

		void viewGridCheckedChanged(object sender, EventArgs e)
		{
			Constants.Instance.ShowGrid = ShowGridButton.Checked = ViewGrid.Checked;
		}
		
		void viewWorldOriginCheckedChanged(object sender, EventArgs e)
		{
			Constants.Instance.ShowWorldOrigin = ShowWorldOriginButton.Checked = ViewWorldOrigin.Checked;
		}
		
		void showGridButtonCheckedChanged(object sender, EventArgs e)
		{
			Constants.Instance.ShowGrid = ViewGrid.Checked = ShowGridButton.Checked;
		}

		void showWorldOriginButtonCheckedChanged(object sender, EventArgs e)
		{
			Constants.Instance.ShowWorldOrigin = ViewWorldOrigin.Checked = ShowWorldOriginButton.Checked;
		}

		void snapToGridButtonCheckedChanged(object sender, EventArgs e)
		{
			Constants.Instance.SnapToGrid =  ViewSnapToGrid.Checked = SnapToGridButton.Checked;
		}

		void viewSnapToGridCheckedChanged(object sender, EventArgs e)
		{
			Constants.Instance.SnapToGrid = SnapToGridButton.Checked = ViewSnapToGrid.Checked;
		}

		//todo: remove public access
		public event EventHandler<EventArgs> OnShuttingDown ;

		public LevelExplorerControl LevelExplorer
		{
			get
			{
				return _uiLevelExplorer ;
			}
		}

		public Size CanvasSize
		{
			get
			{
				return uiCanvas.Size ;
			}
		}

		public bool CanvasHasFocus
		{
			get
			{
				return uiCanvas.ContainsFocus ;
			}
		}

		public MenuStrip MenuStrip
		{
			get
			{
				return uiMenuStrip ;
			}
		}

		public void SetCursorForCanvas( Cursor cursor )
		{
			uiCanvas.Cursor = cursor;
		}

		public void SetToolStripStatusLabel1( string text )
		{
			toolStripStatusLabel5.Text = text ;
		}

		public void SetFpsDiagText( string text )
		{
			toolStripStatusLabel8.Text = text ;
		}

		public void SetToolStripStatusLabel2( string text )
		{
			toolStripStatusLabel6.Text = text ;
		}

		public void SetToolStripStatusLabel3( string text )
		{
			toolStripStatusLabel7.Text = text ;
		}

		void resetUndoRedoControls()
		{
			undoButton.DropDownItems.Clear( ) ;
			redoButton.DropDownItems.Clear( ) ;
		
			undoButton.Enabled = undoMenuItem.Enabled = false ;
			redoButton.Enabled = redoMenuItem.Enabled = false ;
		}

		ToolStripMenuItem[ ] buildMenuItemsForLayers( Action<LayerEditor> whenSelected )
		{
			ToolStripMenuItem[ ] items = (
				from layer in _model.Level.Layers
				select new ToolStripMenuItem( layer.Name, null, ( sender, e ) => whenSelected(((ToolStripMenuItem)sender).Tag as LayerEditor) )
					{
						Text = layer.Name,
						Name = layer.Name,
						Enabled = layer != _model.Level.ActiveLayer,
						Tag = layer,
						ToolTipText = @"Move to layer '{0}'".FormatWith( layer.Name ),
						DisplayStyle = ToolStripItemDisplayStyle.Text,
					} ).ToArray( ) ;
			return items ;
		}

		void actionDelete( object sender, EventArgs e )
		{
			ObjectFactory.GetInstance<IHandleUserActions>().ProcessAction( new DeleteSelectedEditorsAction(  ) );
		}

		void whenImportOfOldGleedLevelIsClicked(object sender, EventArgs e)
		{
			var opendialog = new OpenFileDialog
			                    {
			                        Filter = Resources.fileDialogFilenameFilter,
			                        Title = @"Open older versions of a Gleed 2D file",
			                    } ;

			if (opendialog.ShowDialog() == DialogResult.OK)
			{
				string filename = ConvertOldFileFormats.Convert( opendialog.FileName ) ;

				loadLevel( filename ) ;
			}
		}

		void copySelectedItemsToLayerMenuOpening(object sender, EventArgs e)
		{
			ToolStripMenuItem[ ] items = buildMenuItemsForLayers(
				chosenLayer => ObjectFactory.GetInstance<IHandleUserActions>( ).ProcessAction(
					new CopySelectedEditorsToLayerAction( chosenLayer ) ) ) ;

			copySelectedItemsToLayerToolStripMenuItem.DropDownItems.Clear(  );
			
			copySelectedItemsToLayerToolStripMenuItem.DropDownItems.AddRange( items  );
		}

		void moveSelectedItemsToLayerMenuOpening(object sender, EventArgs e)
		{
			var items = buildMenuItemsForLayers(
				chosenLayer => ObjectFactory.GetInstance<IHandleUserActions>( ).ProcessAction(
					new MoveSelectedEditorsToLayerAction( chosenLayer ) ) ) ;

			uiMoveSelectedItemsToLayerToolMenu.DropDownItems.Clear(  );
			
			uiMoveSelectedItemsToLayerToolMenu.DropDownItems.AddRange( items  );
		}

		private void uiStartBehaviourMenuItem_Click(object sender, EventArgs e)
		{
			everythingWithBehaviour( ).ForEach( b => b.Start( ) ) ;
			
			_canvas.SetModeTo( UserActionInEditor.RunningBehaviour ) ;
		}

		private void uiStopBehaviourMenuItem_Click(object sender, EventArgs e)
		{
			everythingWithBehaviour( ).ForEach( b => b.Stop( ) ) ;
			_canvas.SetModeToIdle();
		}

		IEnumerable<IBehaviour> everythingWithBehaviour( )
		{
			var behaviours = new List<IBehaviour>(); 
			behaviours.AddRange(_model.Level.Behaviours);

			_model.Level.Layers.ForEach(
				layer =>
					{
						behaviours.AddRange( layer.Behaviours ) ;
						layer.ForEach( item => behaviours.AddRange( item.Behaviours ) ) ;
					} ) ;
			
			return behaviours ;
		}

		private void fileToolStripMenuItem_DropDownOpening(object sender, EventArgs e)
		{
//			int n = 1 ;
			//return _editor.CurrentUserAction != UserActionInEditor.RunningBehaviour ;
		}
	}
}
