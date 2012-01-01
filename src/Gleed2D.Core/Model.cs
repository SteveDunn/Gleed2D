using System ;
using System.Collections.Generic ;
using System.Diagnostics ;
using System.Linq ;
using System.Windows.Forms ;
using Gleed2D.Core.Behaviour ;
using Gleed2D.InGame ;
using JetBrains.Annotations ;
using Microsoft.Xna.Framework ;
using StructureMap ;

namespace Gleed2D.Core
{
	[PublicAPI]
	public class Model : IModel
	{
		Level _level ;

		readonly IMemento _memento ;

		public event EventHandler<ModelChangedEventArgs> ItemChanged ;

		public event EventHandler<ModelChangedEventArgs> SelectionChanged ;
		public event EventHandler<EventArgs> ActiveLayerChanged ;

		public event EventHandler<ModelChangedEventArgs> ItemsAddedOrRemoved ;

		public event EventHandler<ModelChangedEventArgs> ItemsMoved ;
		public event EventHandler<EventArgs> NewModelLoaded ;

		public event EventHandler<ModelChangedEventArgs> ItemsRearrangedInLayer ;

		public void AddCustomProperty( ITreeItem treeItem, CustomProperty newCustomProperty )
		{
			treeItem.ItemProperties.CustomProperties.Add( newCustomProperty.Name, newCustomProperty ) ;

			tryFire( ( ) => ItemChanged, treeItem ) ;
		}

		public event EventHandler<ModelUnloadingEventArgs> OnBeforeUnloadingModel ;

		public Model( IMemento memento )
		{
			_memento = memento ;
		}

		public void CreateNewLevel( )
		{
			if( Level != null )
			{
				if( OnBeforeUnloadingModel != null )
				{
					var args = new ModelUnloadingEventArgs( Level ) ;

					OnBeforeUnloadingModel( this, args ) ;

					if( args.Cancelled )
					{
						return ;
					}
				}
			}

			var newLevel = new Level
				{
					Version = ObjectFactory.GetInstance<IGetAssemblyInformation>( ).Version
				} ;

			LoadLevel( newLevel ) ;

			if( NewModelLoaded != null )
			{
				NewModelLoaded( this, EventArgs.Empty ) ;
			}
		}

		public void RemoveCustomPropertyItem( ITreeItem item, DictionaryPropertyDescriptor propertyDescriptor )
		{
			_memento.BeginCommand( "Delete Custom Property" ) ;

			propertyDescriptor.Remove( propertyDescriptor.Name ) ;

			tryFire( ( ) => ItemChanged, item ) ;

			_memento.EndCommand( ) ;
		}

		public void NotifyChanged( IEnumerable<ItemEditor> items )
		{
			tryFire( ( ) => ItemChanged, items ) ;
		}

		public void NotifyChanged( ItemEditor item )
		{
			tryFire( ( ) => ItemChanged, item ) ;
		}

		public void RenameItem( ITreeItem treeItem, string newName )
		{
			_memento.BeginCommand( string.Format( @"Rename Item ('{0}' -> '{1}')", treeItem.Name, newName ) ) ;

			treeItem.RenameTo( newName ) ;

			_memento.EndCommand( ) ;

			// tryFire( ( ) => ItemsRenamed, treeItem ) ;
		}

		public Layer ActiveLayer
		{
			get
			{
				return Level == null ? null : Level.ActiveLayer ;
			}
		}

		public int NextItemNumber
		{
			get
			{
				return _level.GetNextItemNumber( ) ;
			}
		}

		public void DuplicateLayer( Layer layer )
		{
			Layer copiedLayer = layer.Clone( ) ;

			copiedLayer.Name = layer.ParentLevel.GetUniqueNameBasedOn( copiedLayer.Name ) ;

			foreach( ItemEditor eachCopiedItem in copiedLayer.Items )
			{
				eachCopiedItem.ItemProperties.Name = layer.ParentLevel.GetUniqueNameBasedOn( eachCopiedItem.ItemProperties.Name ) ;
			}

			IMainForm mainForm = IoC.MainForm ;

			_memento.BeginCommand( string.Format( @"Duplicate Layer '{0}'", layer.Name ) ) ;

			AddNewLayer( copiedLayer ) ;

			_memento.EndCommand( ) ;

			tryFire( ( ) => ItemsAddedOrRemoved, (ITreeItem) copiedLayer ) ;

			mainForm.LevelExplorer.Rebuild( ) ;
		}

		public Level Level
		{
			get
			{
				return _level ;
			}
			set
			{
				_level = value ;

				if( ItemsAddedOrRemoved != null )
				{
					ItemsAddedOrRemoved( this, new ModelChangedEventArgs( _level ) ) ;
				}
			}
		}

		public void AddEditor( ItemEditor editor )
		{
			_memento.BeginCommand( string.Format( @"Add Item '{0}'", editor.ItemProperties.Name ) ) ;

			Level.AddEditor( editor ) ;

			tryFire( ( ) => ItemsAddedOrRemoved, editor ) ;

			_memento.EndCommand( ) ;
		}

		public void AttachBehaviour( ITreeItem target, IBehaviour behaviour)
		{
			_memento.BeginCommand( string.Format( @"Attach behavour to '{0}'", target.Name ) ) ;

			target.Behaviours.Add( behaviour );

			tryFire( ( ) => ItemsAddedOrRemoved, target ) ;

			_memento.EndCommand( ) ;
		}

		public void SelectBehaviour( ITreeItem behaviour )
		{
			tryFire( ()=> SelectionChanged, behaviour  );
		}

		void setActiveLayerIfItsDifferent( Layer layer )
		{
			if( ActiveLayer == layer )
			{
				return ;
			}

			Level.SelectLayer( layer ) ;

			var eventHandler = ActiveLayerChanged ;

			if( eventHandler != null )
			{
				eventHandler( this, EventArgs.Empty ) ;
			}
		}

		public void AddNewLayer( Layer layer )
		{
			layer.ParentLevel = Level ;

			Level.Layers.Add( layer ) ;

			tryFire(
				( ) => ItemsAddedOrRemoved,
				new[ ]
					{
						layer
					} ) ;

			SelectLayer( layer ) ;
		}

		public void SelectEditors( SelectedEditors itemEditors )
		{
			Debug.WriteLine( @"SelectEditors with {0} item(s)", itemEditors.Count ) ;

			if( itemEditors.Count != 0 )
			{
				setActiveLayerIfItsDifferent( itemEditors.Items.First( ).ParentLayer ) ;
			}

			Level.SelectEditors( itemEditors ) ;

			tryFire( ( ) => SelectionChanged, itemEditors.Items ) ;
		}

		public void AddEditorToSelection( ItemEditor editor )
		{
			Level.SelectEditor( editor ) ;

			tryFire( ( ) => SelectionChanged, Level.SelectedEditors ) ;
		}

		void clearSelectedEditors( )
		{
			Level.ClearSelectedEditors( ) ;

			tryFire( ( ) => SelectionChanged, Enumerable.Empty<ItemEditor>( ) ) ;
		}

		public void ToggleSelectionOnItem( ItemEditor item )
		{
			item.ToggleSelection( ) ;

			tryFire( ( ) => SelectionChanged, Level.SelectedEditors ) ;
		}

		public void SelectEditor( ItemEditor editor )
		{
			Level.ClearSelectedEditors( ) ;

			setActiveLayerIfItsDifferent( editor.ParentLayer ) ;

			Level.SelectEditor( editor ) ;

			tryFire( ( ) => SelectionChanged, editor ) ;
		}

		public void MoveItemToLayer( ItemEditor itemToMove, Layer layer, ItemEditor itemToMoveNewItemUnder )
		{
			int insertPosition = itemToMoveNewItemUnder == null ? 0 : layer.Items.IndexOf( itemToMoveNewItemUnder ) ;

			itemToMove.ParentLayer.Items.Remove( itemToMove ) ;

			layer.Items.Insert( insertPosition, itemToMove ) ;

			itemToMove.ParentLayer = layer ;
		}

		public void DeleteLayer( Layer layer )
		{
			_memento.BeginCommand( string.Format( @"Delete Layer '{0}'", layer.Name ) ) ;
			Level.Layers.Remove( layer ) ;
			_memento.EndCommand( ) ;

			tryFire( ( ) => ItemsAddedOrRemoved, (ITreeItem) layer ) ;

			Layer nextChoiceOfLayerToSelect = Level.Layers.Count > 0 ? Level.Layers.Last( ) : null ;
			if( nextChoiceOfLayerToSelect != null )
			{
				setActiveLayerIfItsDifferent( nextChoiceOfLayerToSelect ) ;
			}
		}

		public void DeleteSelectedItems( )
		{
			_memento.BeginCommand( "Delete Item(s)" ) ;

			var selectedEditors = new List<ItemEditor>( Level.SelectedEditors ) ;

			var itemsAffected = new List<ItemEditor>( ) ;

			foreach( ItemEditor eachSelectedEditor in selectedEditors )
			{
				foreach( Layer eachLayer in Level.Layers )
				{
					foreach( ItemEditor eachItem in eachLayer.Items )
					{
						foreach( CustomProperty cp in eachItem.ItemProperties.CustomProperties.Values )
						{
							var linkedItem = (LinkedItem) cp.Value ;
							if( linkedItem != null && ( cp.Type == typeof( LinkedItem ) && linkedItem.Name == eachSelectedEditor.Name ) )
							{
								cp.Value = null ;
								itemsAffected.Add( eachItem ) ;
							}
						}
					}
				}

				eachSelectedEditor.ParentLayer.Items.Remove( eachSelectedEditor ) ;
			}

			_memento.EndCommand( ) ;

			clearSelectedEditors( ) ;

			if( itemsAffected.Count > 0 )
			{
				string message =
					itemsAffected.Aggregate(
						string.Empty,
						( current, item ) => current +
							string.Format( "{0} (Layer: {1}){2}", item.ItemProperties.Name, item.ParentLayer.Name, Environment.NewLine ) ) ;


				MessageBox.Show(
					string.Format(
						@"The following Items have Custom Properties of Type 'LinkedItem' that refered to items that have just been deleted:

{0}
The corresponding Custom Properties have been set to NULL, since the Item referred to doesn't exist anymore.",
						message ) ) ;
			}

			tryFire( ( ) => ItemsAddedOrRemoved, itemsAffected ) ;
		}

		public void MoveLayerUp( Layer layer )
		{
			_memento.BeginCommand( string.Format( @"Move Down Layer '{0}'", layer.Name ) ) ;

			int index = Level.Layers.IndexOf( layer ) ;
			Level.Layers[ index ] = Level.Layers[ index - 1 ] ;
			Level.Layers[ index - 1 ] = layer ;

			tryFire( ( ) => ItemsRearrangedInLayer, (ITreeItem) layer ) ;

			SelectLayer( layer ) ;
		}

		public void MoveLayerDown( Layer layer )
		{
			_memento.BeginCommand( string.Format( @"Move Up Layer '{0}'", layer.Name ) ) ;

			int index = Level.Layers.IndexOf( layer ) ;
			Level.Layers[ index ] = Level.Layers[ index + 1 ] ;
			Level.Layers[ index + 1 ] = layer ;

			tryFire( ( ) => ItemsRearrangedInLayer, (ITreeItem) layer ) ;

			SelectLayer( layer ) ;

			_memento.EndCommand( ) ;
		}

		public void SelectLayer( Layer layer )
		{
			if( Level.SelectedEditors.Any( ) )
			{
				Level.ClearSelectedEditors( ) ;
			}

			setActiveLayerIfItsDifferent( layer ) ;

			tryFire( ( ) => SelectionChanged, (ITreeItem) layer ) ;
		}

		public void SelectLevel( )
		{
			tryFire( ( ) => SelectionChanged, Level ) ;
		}

		public void MoveEditorUp( ItemEditor i )
		{
			_memento.BeginCommand( string.Format( @"Move editor up '{0}'", i.Name ) ) ;

			int index = i.ParentLayer.Items.IndexOf( i ) ;

			i.ParentLayer.Items[ index ] = i.ParentLayer.Items[ index - 1 ] ;
			i.ParentLayer.Items[ index - 1 ] = i ;

			_memento.EndCommand( ) ;

			SelectEditor( i ) ;

			tryFire( ( ) => ItemsRearrangedInLayer, i ) ;
		}

		public void MoveEditorDown( ItemEditor editor )
		{
			_memento.BeginCommand( string.Format( @"Move editor down'{0}'", editor.Name ) ) ;

			int index = editor.ParentLayer.Items.IndexOf( editor ) ;
			editor.ParentLayer.Items[ index ] = editor.ParentLayer.Items[ index + 1 ] ;
			editor.ParentLayer.Items[ index + 1 ] = editor ;

			_memento.EndCommand( ) ;

			SelectEditor( editor ) ;

			tryFire( ( ) => ItemsRearrangedInLayer, editor ) ;
		}

		public void LoadLevel( Level level )
		{
			//todo: move to when the level is actually loaded

			if( !level.ContentRootFolder.Exists )
			{
				MessageBox.Show(
					string.Format(
						@"The directory '{0}' doesn't exist! Please adjust the XML file before trying again.", level.ContentRootFolder ) ) ;

				return ;
			}

			//foreach( Layer layer in level.Layers )
			//{
			//    layer.ParentLevel = level ;

			//    foreach( ItemEditor item in layer.Items )
			//    {
			//        item.ParentLayer = layer ;
			//        if( !item.LoadIntoEditor( ) )
			//        {
			//            return ;
			//        }
			//    }
			//}

			Level = level ;

			if( NewModelLoaded != null )
			{
				NewModelLoaded( this, EventArgs.Empty ) ;
			}

			//if( Level.Name == null )
			//{
			//    Level.Name = "Level_01" ;
			//}

			if( Level.Layers.Count > 0 )
			{
				setActiveLayerIfItsDifferent( Level.Layers[ 0 ] ) ;
			}

			_memento.Clear( ) ;

		}

		public void SaveLevel( string filename )
		{
			//todo: might need to put this back
			//Level.CameraPosition = Camera.Position ;
			//Level.EditorRelated.Version = Version ;

			Level.SaveAsXmlToDisk( filename ) ;
		}

		public void SelectEverythingInSelectedLayer( )
		{
			if( Level.ActiveLayer == null )
			{
				return ;
			}

			Level.SelectEverythingInSelectedLayer( ) ;

			tryFire( ( ) => SelectionChanged, Level.SelectedEditors ) ;
		}

		public void MoveSelectedItemsToLayer( Layer chosenLayer )
		{
			if( chosenLayer == Level.ActiveLayer )
			{
				return ;
			}

			_memento.BeginCommand( string.Format( @"Move Item(s) To Layer '{0}'", chosenLayer.Name ) ) ;

			Level.MoveSelectedIditorsToLayer( chosenLayer ) ;

			tryFire( ( ) => ItemsMoved, Level.SelectedEditors ) ;
		}

		public void CopySelectedItemsToLayer( Layer layer )
		{
			if( layer == ActiveLayer )
			{
				return ;
			}

			_memento.BeginCommand( string.Format( @"Copy Item(s) To Layer '{0}'", layer.Name ) ) ;

			var clonedEditors = Level.CopySelectedEditorsToLayer( layer ).ToList( ) ;

			_memento.EndCommand( ) ;

			Level.SelectEditors( new SelectedEditors( clonedEditors ) ) ;

			tryFire( ( ) => ItemsAddedOrRemoved, clonedEditors ) ;
		}

		void tryFire( Func<EventHandler<ModelChangedEventArgs>> func, ITreeItem item )
		{
			tryFire(
				func,
				new[ ]
					{
						item
					} ) ;
		}

		void tryFire( Func<EventHandler<ModelChangedEventArgs>> func, IEnumerable<ITreeItem> items )
		{
			var f = func( ) ;

			if( f != null )
			{
				f( this, new ModelChangedEventArgs( items ) ) ;
			}
		}

		public void AlignHorizontally( )
		{
			_memento.BeginCommand( "Align Horizontally" ) ;

			IEnumerable<ItemEditor> selectedEditors = Level.SelectedEditors.ToList( ) ;

			foreach( ItemEditor eachEditor in selectedEditors )
			{
				eachEditor.ItemProperties.Position = new Vector2(
					eachEditor.ItemProperties.Position.X, selectedEditors.First( ).ItemProperties.Position.Y ) ;
			}

			tryFire( ( ) => SelectionChanged, selectedEditors ) ;

			_memento.EndCommand( ) ;
		}

		public void AlignVertically( )
		{
			_memento.BeginCommand( "Align Vertically" ) ;
			IEnumerable<ItemEditor> selectedEditors = Level.SelectedEditors.ToList( ) ;

			foreach( ItemEditor eachEditor in selectedEditors )
			{
				eachEditor.ItemProperties.Position = new Vector2(
					selectedEditors.First( ).ItemProperties.Position.X, eachEditor.ItemProperties.Position.Y ) ;
			}

			_memento.EndCommand( ) ;
		}

		public void AlignRotation( )
		{
			_memento.BeginCommand( @"Align Rotation" ) ;

			var rotatables = ( from s in _level.SelectedEditors
			                   where s.ItemProperties is IRotatable
			                   select s.ItemProperties as IRotatable ).ToList( ) ;


			if( rotatables.Count <= 1 )
			{
				return ;
			}

			for( int i = 1; i < rotatables.Count; i++ )
			{
				rotatables[ i ].Rotation = rotatables[ 0 ].Rotation ;
			}

			_memento.EndCommand( ) ;
		}

		public void AlignScale( )
		{
			_memento.BeginCommand( @"Align scale" ) ;

			var scalables = ( from s in _level.SelectedEditors
			                  where s.ItemProperties is IScalable
			                  select s.ItemProperties as IScalable ).ToList( ) ;


			if( scalables.Count <= 1 )
			{
				return ;
			}

			for( int i = 1; i < scalables.Count; i++ )
			{
				scalables[ i ].Scale = scalables[ 0 ].Scale ;
			}

			_memento.EndCommand( ) ;
		}
	}
}