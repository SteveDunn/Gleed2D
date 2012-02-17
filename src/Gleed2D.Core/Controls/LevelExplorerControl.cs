using System ;
using System.Collections.Generic ;
using System.Drawing ;
using System.Linq ;
using System.Reflection ;
using System.Windows.Forms ;
using Gleed2D.Core.Behaviour ;
using Gleed2D.Core.UserActions ;
using Gleed2D.InGame ;
using JetBrains.Annotations ;
using StructureMap ;

namespace Gleed2D.Core.Controls
{
	public partial class LevelExplorerControl : UserControl
	{
		public event EventHandler<ItemSelectedEventArgs> ItemSelected ;

		const string LAYER_ICON_ACTIVE_NAME = @"Gleed2D.Core.Resources.icon_layer_active.png" ;
		const string LAYER_ICON_NONACTIVE_NAME = @"Gleed2D.Core.Resources.icon_layer_nonactive.png" ;
		const string LEVEL_ICON_NAME = @"Gleed2D.Core.Resources.icon_level.png" ;

		readonly ImageList _imageList ;

		ICanvas _canvas ;
		IGame _game ;

		readonly IModel _model ;
		readonly IMemento _memento ;
		bool _hideContextMenus ;

		public LevelExplorerControl( )
		{
			InteractsWithModel = true ;
			
			InitializeComponent( ) ;

			if( !Helper.IsInDesignMode )
			{
				var imageRepository = ObjectFactory.GetNamedInstance<IImageRepository>( @"iconImages" ) ;

				Assembly thisAssembly = Assembly.GetExecutingAssembly(  ) ;

				Images.SummonIcon( thisAssembly, LAYER_ICON_ACTIVE_NAME ) ;
				Images.SummonIcon( thisAssembly,LAYER_ICON_NONACTIVE_NAME ) ;
				Images.SummonIcon( thisAssembly,LEVEL_ICON_NAME ) ;

				_imageList = imageRepository.CreateImageList( ) ;

				_model = ObjectFactory.GetInstance<IModel>( ) ;
				_memento = ObjectFactory.GetInstance<IMemento>( ) ;

				subscribeToModelEvents( ) ;
			}

			uiTree.AllowDrop = true ;
			uiTree.CheckBoxes = true ;
			uiTree.FullRowSelect = true ;
			uiTree.HideSelection = false ;
			uiTree.HotTracking = true ;
			uiTree.ImageList = _imageList ;
			uiTree.LabelEdit = true ;
			uiTree.Name = "uiEntityTree" ;
			uiTree.ShowNodeToolTips = true ;

			uiTree.KeyDown += entityTreeKeyDown ;
			uiTree.MouseDown += entityTreeMouseDown ;
			uiTree.AfterLabelEdit += entityTreeAfterLabelEdit ;
			uiTree.AfterCheck += entityTreeAfterCheck ;
			uiTree.AfterSelect += entityTreeAfterSelect ;
			uiTree.DragDrop += entityTreeDragDrop ;
			uiTree.DragOver += entityTreeDragOver ;
			uiTree.ItemDrag += entityTreeItemDrag ;
		}

		public bool InteractsWithModel
		{
			get ;
			set ;
		}

		void subscribeToModelEvents( )
		{
			_model.ItemsAddedOrRemoved += ( s, e ) =>
			                     	{
			                     		if( InteractsWithModel )
			                     		{
			                     			Rebuild( ) ;
			                     		}
			                     	} ;
			
			_model.ItemsRearrangedInLayer += ( s, e ) =>
			                                 	{
			                                 		if( InteractsWithModel )
			                                 		{
			                                 			Rebuild( ) ;
			                                 		}
			                                 	} ;

			_model.ActiveLayerChanged += ( s, e ) =>
			                             	{
			                             		if( InteractsWithModel )
			                             		{
			                             			activeLayerChanged( ) ;
			                             		}
			                             	} ;

			_model.ItemsMoved += ( s, e ) =>
			                     	{
			                     		if( InteractsWithModel )
			                     		{
			                     			Rebuild( ) ;
			                     		}
			                     	} ;

			_model.SelectionChanged += (s, e) =>
			{
				if (InteractsWithModel)
				{
					selectionChanged( e.ItemsAffected ) ;
				}
			};

			_model.NewModelLoaded += newModelLoaded ;
		}

		void activeLayerChanged( )
		{
			changeActiveLayerIcons( ) ;
			selectTreeItem( _model.ActiveLayer ) ;
		}

		public bool ShowToolStrip
		{
			get
			{
				return uiTreeToolsStrip.Visible ;
			}
			set
			{
				uiTreeToolsStrip.Visible = value ;
			}
		}

		void changeActiveLayerIcons( )
		{
			_model.Level.Layers.ForEach(
				l =>
					{
						tryFindTreeNode( l.Name, true ).ImageKey = _model.ActiveLayer == l
							? LAYER_ICON_ACTIVE_NAME
							: LAYER_ICON_NONACTIVE_NAME ;
					} ) ;
		}

		void newModelLoaded( object sender, EventArgs e )
		{
			Rebuild( ) ;
		}

		void selectionChanged( IEnumerable<ITreeItem> items )
		{
			var allItems = items.ToList( ) ;
			
			changeActiveLayerIcons(  );

			if (allItems.Count() == 1)
			{
				ITreeItem item = allItems.Single( ) ;

				if( SelectedEntity == item )
				{
					return ;
				}

				if( item != null )
				{
					selectTreeItem( item ) ;
				}
			}
		}

		TreeNode selectTreeItem( ITreeItem item )
		{
			TreeNode foundNode = tryFindTreeNode( item.Name, true ) ;
			
			if( foundNode != uiTree.SelectedNode )
			{
				uiTree.SelectedNode = foundNode ;
			}

			if( uiTree.SelectedNode != null )
			{
				uiTree.SelectedNode.EnsureVisible( ) ;
			}

			return foundNode ;
		}

		//TODO: Don't expose.
		public ITreeItem SelectedEntity
		{
			get
			{
				return (ITreeItem) ( uiTree.SelectedNode == null ? null : uiTree.SelectedNode.Tag ) ;
			}
			private set
			{
				TreeNode matchingNode = tryFindTreeNode( value.Name, true ) ;

				if( matchingNode != null )
				{
					uiTree.SelectedNode = matchingNode ;
				}
			}
		}

		public bool CheckBoxes
		{
			get
			{
				return uiTree.CheckBoxes ;
			}
			set
			{
				uiTree.CheckBoxes = value ;
			}
		}

		public bool HideContextMenus
		{
			get
			{
				return _hideContextMenus ;
			}
			set
			{
				_hideContextMenus = value ;
				uiContextMenuForItem.Enabled = !value ;
				uiContextMenuForLayer.Enabled = !value ;
				uiContextMenuForLevel.Enabled = !value ;
			}
		}

		void entityTreeKeyDown( object sender, KeyEventArgs e )
		{
			if( e.KeyCode == Keys.Delete )
			{
				if( SelectedEntity is LayerEditor )
				{
					ObjectFactory.GetInstance<IHandleUserActions>( ).ProcessAction( new DeleteActiveLayerAction( ) ) ;
				}
				if( SelectedEntity is ItemEditor )
				{
					ObjectFactory.GetInstance<IHandleUserActions>( ).ProcessAction( new DeleteSelectedEditorsAction( ) ) ;
				}

				return ;
			}

			var handleKeyboardCommands = ObjectFactory.GetInstance<IHandleKeyboardCommands>( ) ;

			handleKeyboardCommands.HandleKeyDown( e ) ;
		}

		void entityTreeAfterLabelEdit( object sender, NodeLabelEditEventArgs e )
		{
			string newName = e.Label ;

			if( newName == null )
			{
				return ;
			}

			//var editor = ObjectFactory.GetInstance<IEditor>() ;

			TreeNode[ ] nodes = uiTree.Nodes.Find( newName, true ) ;

			if( nodes.Length > 0 )
			{
				MessageBox.Show(
					@"A layer or item with the name ""{0}"" already exists in the level. Please use another name!".FormatWith(newName) ) ;
				e.CancelEdit = true ;
				return ;
			}

			_model.RenameItem( (ITreeItem) e.Node.Tag, newName ) ;
		}

		void entityTreeAfterCheck( object sender, TreeViewEventArgs e )
		{
			if( e.Node.Tag is LevelEditor )
			{
				_model.Level.Visible = e.Node.Checked ;
			}

			if( e.Node.Tag is LayerEditor )
			{
				var layer = (LayerEditor) e.Node.Tag ;
				layer.Visible = e.Node.Checked ;
			}

			if( e.Node.Tag is ItemEditor )
			{
				var item = (ItemEditor) e.Node.Tag ;
				item.ItemProperties.Visible = e.Node.Checked ;
			}
		}

		void entityTreeAfterSelect( object sender, TreeViewEventArgs e )
		{
			if( ItemSelected != null )
			{
				ItemSelected(
					this,
					new ItemSelectedEventArgs
						{
							Item = (ITreeItem)e.Node.Tag
						} ) ;
			}

			if( !InteractsWithModel )
			{
				return ;
			}

			if( e.Node.Tag is LevelEditor )
			{
				ObjectFactory.GetInstance<IHandleUserActions>( ).ProcessAction(
					new SelectLevelAction( ) ) ;
			}

			if( e.Node.Tag is LayerEditor )
			{
				ObjectFactory.GetInstance<IHandleUserActions>( ).ProcessAction(
					new SelectLayerAction( e.Node.Tag as LayerEditor ) ) ;
			}

			if( e.Node.Tag is ItemEditor )
			{
				ObjectFactory.GetInstance<IHandleUserActions>( ).ProcessAction(
					new SelectEditorAction( e.Node.Tag as ItemEditor ) ) ;
			}

			if( e.Node.Tag is IBehaviour )
			{
				IoC.Model.SelectBehaviour( e.Node.Tag as IBehaviour ) ;
			}
		}

		void entityTreeMouseDown( object sender, MouseEventArgs e )
		{
			if( e.Button == MouseButtons.Right )
			{
				uiTree.SelectedNode = uiTree.GetNodeAt( e.X, e.Y ) ;
			}
		}

		void entityTreeItemDrag( object sender, ItemDragEventArgs e )
		{
			if( ( (TreeNode) e.Item ).Tag is LayerEditor )
			{
				return ;
			}

			if( ( (TreeNode) e.Item ).Tag is LevelEditor )
			{
				return ;
			}

			_memento.BeginCommand( "Drag Item" ) ;

			DoDragDrop( e.Item, DragDropEffects.Move ) ;
		}

		void entityTreeDragOver( object sender, DragEventArgs e )
		{
			//get source node
			var sourceNode = (TreeNode) e.Data.GetData( typeof( TreeNode ) ) ;

			if( sourceNode == null )
			{
				e.Effect = DragDropEffects.None ;
				return ;
			}

			e.Effect = DragDropEffects.Move ;

			//get destination node and select it
			Point p = uiTree.PointToClient( new Point( e.X, e.Y ) ) ;

			TreeNode destNode = uiTree.GetNodeAt( p ) ;

			if( destNode.Tag is LevelEditor )
			{
				return ;
			}

			uiTree.SelectedNode = destNode ;

			if( destNode == sourceNode )
			{
				return ;
			}

			var sourceItem = (ItemEditor) sourceNode.Tag ;

			if( destNode.Tag is ItemEditor )
			{
				var destItem = (ItemEditor) destNode.Tag ;
				_model.MoveItemToLayer( sourceItem, destItem.ParentLayer, destItem ) ;

				int delta = 0 ;

				if( destNode.Index > sourceNode.Index && sourceItem.ParentLayer == destItem.ParentLayer )
				{
					delta = 1 ;
				}

				sourceNode.Remove( ) ;

				destNode.Parent.Nodes.Insert( destNode.Index + delta, sourceNode ) ;
			}

			if( destNode.Tag is LayerEditor )
			{
				var destItem = (LayerEditor) destNode.Tag ;

				_model.MoveItemToLayer( sourceItem, destItem, null ) ;

				sourceNode.Remove( ) ;

				destNode.Nodes.Insert( 0, sourceNode ) ;
			}

			_model.SelectEditor( sourceItem ) ;

			var game = summonGame( ) ;

			//editor.Draw(gameTime, graphicsDevice, game.SpriteBatch ) ;

			game.GraphicsDevice.Present( ) ;

			Application.DoEvents( ) ;
		}

		void entityTreeDragDrop( object sender, DragEventArgs e )
		{
			_memento.EndCommand( ) ;
		}

		ICanvas summonEditor( )
		{
			if( _canvas == null )
			{
				_canvas = ObjectFactory.GetInstance<ICanvas>( ) ;
			}

			return _canvas ;
		}

		IGame summonGame( )
		{
			if( _game == null )
			{
				_game = ObjectFactory.GetInstance<IGame>( ) ;
			}

			return _game ;
		}

		public void StartRename( )
		{
			TreeNode selectedNode = uiTree.SelectedNode ;

			if( selectedNode != null )
			{
				selectedNode.BeginEdit( ) ;
			}
		}

		[CanBeNull]
		TreeNode tryFindTreeNode( string name, bool searchAllChildren )
		{
			TreeNode[ ] findTreeNode = uiTree.Nodes.Find( name, searchAllChildren ) ;

			if( findTreeNode.Length == 0 )
			{
				return null ;
			}

			if( findTreeNode.Length > 1 )
			{
				throw new InvalidOperationException(
					@"The tree is in an unknown state.  There are 2 or more items named '{0}'.".FormatWith( name ) ) ;
			}

			return findTreeNode[ 0 ] ;
		}

		public override void Refresh()
		{
			Rebuild(  );
			base.Refresh();
		}

		//TODO: Don't expose this
		public void Rebuild( )
		{
			ITreeItem selectedEntity = SelectedEntity ;

			uiTree.Nodes.Clear( ) ;

			LevelEditor level = _model.Level ;

			var levelTreeNode = uiTree.Nodes.Add( level.Name ) ;

			levelTreeNode.Tag = level ;

			levelTreeNode.ImageKey = levelTreeNode.SelectedImageKey = LEVEL_ICON_NAME ;

			levelTreeNode.Checked = level.Visible ;

			levelTreeNode.ContextMenuStrip = _hideContextMenus?null:uiContextMenuForLevel ;

			foreach( LayerEditor eachLayer in level.Layers )
			{
				TreeNode layerTreeNode = levelTreeNode.Nodes.Add( eachLayer.Name, eachLayer.Name ) ;

				layerTreeNode.Tag = eachLayer ;
				layerTreeNode.Checked = eachLayer.Visible ;
				layerTreeNode.ContextMenuStrip = _hideContextMenus?null:uiContextMenuForLayer ;

				string iconName = level.ActiveLayer==eachLayer ? LAYER_ICON_ACTIVE_NAME : LAYER_ICON_NONACTIVE_NAME ;

				layerTreeNode.ImageKey = layerTreeNode.ImageKey = iconName ;

				foreach( ItemEditor eachEditor in eachLayer.Items )
				{
					addNodeForEditor( layerTreeNode, eachEditor ) ;
				}

				layerTreeNode.Expand( ) ;
			}

			levelTreeNode.Expand( ) ;

			if( selectedEntity != null )
			{
				SelectedEntity = selectedEntity ;
			}
		}

		void addNodeForEditor( TreeNode parentNode, ItemEditor editor )
		{
			ItemProperties itemProperties = editor.ItemProperties ;

			TreeNode itemNode = parentNode.Nodes.Add( itemProperties.Name, itemProperties.Name ) ;

			itemNode.ImageKey = itemNode.SelectedImageKey = editor.Icon.Name ;
			itemNode.Tag = editor ;
			itemNode.Checked = true ;

			itemNode.ContextMenuStrip = _hideContextMenus ? null : uiContextMenuForItem ;

			editor.Behaviours.ForEach(
				b =>
					{
						BehaviourProperties behaviourProperties = b.BehaviourProperties ;
						TreeNode node = itemNode.Nodes.Add( behaviourProperties.Name, behaviourProperties.Name ) ;

						//node.ImageKey = node.SelectedImageKey = b.Icon.Name ;

						node.Tag = b ;

						node.Checked = true ;

						//node.ContextMenuStrip = _hideContextMenus ? null : uiContextMenuForItem ;
					} ) ;
			
			itemNode.Expand(  );
		}

		private void uiAddCustomPropertyClicked( object sender, EventArgs e )
		{
			ObjectFactory.GetInstance<IHandleUserActions>( ).ProcessAction(
				new AddCustomPropertyAction( SelectedEntity ) ) ;
		}

		void actionMoveItemUp( object sender, EventArgs e )
		{
			ObjectFactory.GetInstance<IHandleUserActions>( ).ProcessAction( new MoveItemUpAction( SelectedEntity ) ) ;
		}

		void actionMoveItemDown( object sender, EventArgs e )
		{
			ObjectFactory.GetInstance<IHandleUserActions>( ).ProcessAction( new MoveItemDownAction( SelectedEntity ) ) ;
		}

		void actionNewLayer( object sender, EventArgs e )
		{
			ObjectFactory.GetInstance<IHandleUserActions>( ).ProcessAction( new AddNewLayerAction( ) ) ;
		}

		void actionDelete( object sender, EventArgs eventArgs )
		{
			if (SelectedEntity is LayerEditor)
			{
				ObjectFactory.GetInstance<IHandleUserActions>( ).ProcessAction( new DeleteActiveLayerAction( ) ) ;
			}
			if( SelectedEntity is ItemEditor )
			{
				ObjectFactory.GetInstance<IHandleUserActions>( ).ProcessAction( new DeleteSelectedEditorsAction( ) ) ;
			}
		}

		private void actionRename(object sender, EventArgs e)
		{
			ObjectFactory.GetInstance<IHandleUserActions>( ).ProcessAction( new RenameInPlaceAction( ) ) ;
		}

		private void actionCenterView(object sender, EventArgs e)
		{

		}

		public void SelectItemByName( string name )
		{
			TreeNode treeNode = tryFindTreeNode( name, true ) ;
			
			uiTree.SelectedNode = treeNode  ;
		}

		private void duplicateToolStripMenuItem_Click(object sender, EventArgs e)
		{
			ObjectFactory.GetInstance<IHandleUserActions>( ).ProcessAction( 
				new DuplicateLayerAction( SelectedEntity as LayerEditor ) ) ;
		}
	}

	public class ItemSelectedEventArgs : EventArgs
	{
		public ITreeItem Item ;
	}
}
