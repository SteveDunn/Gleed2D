using System ;
using System.Collections.Generic ;
using System.Diagnostics ;
using System.Linq ;
using System.Windows.Forms ;
using System.Xml.Linq;
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
	    readonly IMemento _memento ;
	    LevelEditor _level ;

	    public Model( IMemento memento )
	    {
	        _memento = memento ;
	    }

	    #region IModel Members

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

	        var newLevel = new LevelEditor();

			LoadLevel( newLevel ) ;

			if( NewModelLoaded != null )
			{
				NewModelLoaded( this, EventArgs.Empty ) ;
			}
		}

		public void RemoveCustomPropertyItem( ITreeItem item, DictionaryPropertyDescriptor propertyDescriptor )
		{
		    _memento.Record(@"Delete custom property", () =>
		                                                   {

		                                                       propertyDescriptor.Remove(propertyDescriptor.Name);

		                                                       tryFire(() => ItemChanged, item);
		                                                   });
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
		    _memento.Record(@"Rename Item ('{0}' -> '{1}')".FormatWith(treeItem.Name, newName),
		                    () => treeItem.RenameTo(newName));
		}

		public LayerEditor ActiveLayer
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

		public void DuplicateLayer( LayerEditor layer )
		{
			LayerEditor copiedLayer = layer.Clone( ) ;

			copiedLayer.Name = layer.ParentLevel.GetUniqueNameBasedOn( copiedLayer.Name ) ;

			foreach( ItemEditor eachCopiedItem in copiedLayer.Items )
			{
				eachCopiedItem.ItemProperties.Name = layer.ParentLevel.GetUniqueNameBasedOn( eachCopiedItem.ItemProperties.Name ) ;
			}

			IMainForm mainForm = IoC.MainForm ;

		    _memento.Record(@"Duplicate Layer '{0}'".FormatWith(layer.Name), () => AddNewLayer( copiedLayer ));

		    tryFire( ( ) => ItemsAddedOrRemoved, (ITreeItem) copiedLayer ) ;

			mainForm.LevelExplorer.Rebuild( ) ;
		}

		public LevelEditor Level
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
		    _memento.Record(@"Add Item '{0}'".FormatWith(editor.ItemProperties.Name), () =>
		        {
		            Level.AddEditor( editor ) ;
		            tryFire( ( ) => ItemsAddedOrRemoved, editor ) ;
		        });
		}

	    public void AttachBehaviour( ITreeItem target, IBehaviour behaviour)
		{
		    _memento.Record(@"Attach behavour to '{0}'".FormatWith(target.Name), () =>
		        {
		            target.Behaviours.Add( behaviour );
		            tryFire( ( ) => ItemsAddedOrRemoved, target ) ;
		        });
		}

	    public void SelectBehaviour( ITreeItem behaviour )
		{
			tryFire( ()=> SelectionChanged, behaviour  );
		}

	    public void DeserialiseLevel(XElement xml)
	    {
	        ObjectFactory.GetInstance<IModelEventHub>().ClearAllSubscribers();

	        Level = new LevelEditor(xml);
	    }

	    public void AddNewLayer( LayerEditor layer )
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

		public void MoveItemToLayer( ItemEditor itemToMove, LayerEditor layer, ItemEditor itemToMoveNewItemUnder )
		{
			int insertPosition = itemToMoveNewItemUnder == null ? 0 : layer.Items.IndexOf( itemToMoveNewItemUnder ) ;

			itemToMove.ParentLayer.Items.Remove( itemToMove ) ;

			layer.Items.Insert( insertPosition, itemToMove ) ;

			itemToMove.ParentLayer = layer ;
		}

		public void DeleteLayer( LayerEditor layer )
		{
		    _memento.Record(@"Delete Layer '{0}'".FormatWith(layer.Name), () => Level.Layers.Remove( layer ));

		    tryFire( ( ) => ItemsAddedOrRemoved, (ITreeItem) layer ) ;

			LayerEditor nextChoiceOfLayerToSelect = Level.Layers.Count > 0 ? Level.Layers.Last( ) : null ;
			if( nextChoiceOfLayerToSelect != null )
			{
				setActiveLayerIfItsDifferent( nextChoiceOfLayerToSelect ) ;
			}
		}

		public void DeleteSelectedItems( )
		{
		    var itemsAffected = new List<ItemEditor>( ) ;
		    
            _memento.Record("Delete Item(s)", () =>
		                                          {
		                                              var selectedEditors = new List<ItemEditor>( Level.SelectedEditors ) ;
		    
                                                      foreach( ItemEditor eachSelectedEditor in selectedEditors )
		                                              {
		                                                  foreach( LayerEditor eachLayer in Level.Layers )
		                                                  {
		                                                      foreach( ItemEditor eachItem in eachLayer.Items )
		                                                      {
		                                                          CustomProperties customProperties = eachItem.ItemProperties.CustomProperties;

		                                                          foreach( CustomProperty eachCustomerProperty in customProperties.Values )
		                                                          {
		                                                              var linkedItem = eachCustomerProperty.Value as LinkedItem ;
		                                                              if( linkedItem != null && ( eachCustomerProperty.Type == typeof( LinkedItem ) && linkedItem.Name == eachSelectedEditor.Name ) )
		                                                              {
		                                                                  eachCustomerProperty.Value = null ;
		                                                                  itemsAffected.Add( eachItem ) ;
		                                                              }
		                                                          }
		                                                      }
		                                                  }

		                                                  eachSelectedEditor.ParentLayer.Items.Remove( eachSelectedEditor ) ;
		                                              }
		                                          });

		    clearSelectedEditors( ) ;

			if( itemsAffected.Count > 0 )
			{
				string message =
					itemsAffected.Aggregate(
						string.Empty,
						( current, item ) => current +
							@"{0} (Layer: {1}){2}".FormatWith(item.ItemProperties.Name, item.ParentLayer.Name, Environment.NewLine) ) ;


				MessageBox.Show(
					@"The following Items have Custom Properties of Type 'LinkedItem' that refered to items that have just been deleted:

{0}
The corresponding Custom Properties have been set to NULL, since the Item referred to doesn't exist anymore.".FormatWith(message) ) ;
			}

			tryFire( ( ) => ItemsAddedOrRemoved, itemsAffected ) ;
		}

		public void MoveLayerUp( LayerEditor layer )
		{
		    _memento.Record(@"Move Down Layer '{0}'".FormatWith(layer.Name), () =>
		        {
		            int index = Level.Layers.IndexOf( layer ) ;
		            Level.Layers[ index ] = Level.Layers[ index - 1 ] ;
		            Level.Layers[ index - 1 ] = layer ;
		            tryFire( ( ) => ItemsRearrangedInLayer, (ITreeItem) layer ) ;
		            SelectLayer( layer ) ;
		        });
		}

	    public void MoveLayerDown( LayerEditor layer )
	    {
	        _memento.Record(@"Move Up Layer '{0}'".FormatWith(layer.Name), () =>
	            {
	                int index = Level.Layers.IndexOf( layer ) ;
	                Level.Layers[ index ] = Level.Layers[ index + 1 ] ;
	                Level.Layers[ index + 1 ] = layer ;
	                tryFire( ( ) => ItemsRearrangedInLayer, (ITreeItem) layer ) ;
	                SelectLayer( layer ) ;
	            });
	    }

	    public void SelectLayer( LayerEditor layer )
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
		    _memento.Record(@"Move editor up '{0}'".FormatWith(i.Name), () =>
		        {
		            int index = i.ParentLayer.Items.IndexOf( i ) ;
		            i.ParentLayer.Items[ index ] = i.ParentLayer.Items[ index - 1 ] ;
		            i.ParentLayer.Items[ index - 1 ] = i ;
		        });

		    SelectEditor( i ) ;

			tryFire( ( ) => ItemsRearrangedInLayer, i ) ;
		}

		public void MoveEditorDown( ItemEditor editor )
		{
		    _memento.Record(@"Move editor down'{0}'".FormatWith(editor.Name), () =>
		        {
		            int index = editor.ParentLayer.Items.IndexOf( editor ) ;
		            editor.ParentLayer.Items[ index ] = editor.ParentLayer.Items[ index + 1 ] ;
		            editor.ParentLayer.Items[ index + 1 ] = editor ;
		        });

		    SelectEditor( editor ) ;

			tryFire( ( ) => ItemsRearrangedInLayer, editor ) ;
		}

		public void LoadLevel( LevelEditor level )
		{
			if( !ObjectFactory.GetInstance<IDisk>().FolderExists(level.ContentRootFolder) )
			{
				MessageBox.Show(
					@"The directory '{0}' doesn't exist! Please adjust the XML file before trying again.".FormatWith(level.ContentRootFolder) ) ;

				return ;
			}

			Level = level ;

			if( NewModelLoaded != null )
			{
				NewModelLoaded( this, EventArgs.Empty ) ;
			}

			if( Level.Layers.Count > 0 )
			{
				setActiveLayerIfItsDifferent( Level.Layers[ 0 ] ) ;
			}

			_memento.Clear( ) ;
		}

		public void SaveLevel( string filename )
		{
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

		public void MoveSelectedItemsToLayer( LayerEditor chosenLayer )
		{
			if( chosenLayer == Level.ActiveLayer )
			{
				return ;
			}

			_memento.BeginCommand( @"Move Item(s) To Layer '{0}'".FormatWith(chosenLayer.Name) ) ;

			Level.MoveSelectedIditorsToLayer( chosenLayer ) ;

			tryFire( ( ) => ItemsMoved, Level.SelectedEditors ) ;
		}

		public void CopySelectedItemsToLayer( LayerEditor layer )
		{
			if( layer == ActiveLayer )
			{
				return ;
			}

		    List<ItemEditor> clonedEditors = null;
		   
		    _memento.Record(@"Copy Item(s) To Layer '{0}'".FormatWith(layer.Name), () => { clonedEditors = Level.CopySelectedEditorsToLayer( layer ).ToList( ) ; });

		    Level.SelectEditors( new SelectedEditors( clonedEditors ) ) ;

			tryFire( ( ) => ItemsAddedOrRemoved, clonedEditors ) ;
		}

	    public void AlignHorizontally( )
	    {
	        _memento.Record("Align Horizontally", () =>
	            {
	                IEnumerable<ItemEditor> selectedEditors = Level.SelectedEditors.ToList( ) ;
	                foreach( ItemEditor eachEditor in selectedEditors )
	                {
	                    eachEditor.ItemProperties.Position = new Vector2(
	                        eachEditor.ItemProperties.Position.X, selectedEditors.First( ).ItemProperties.Position.Y ) ;
	                }
	                tryFire( ( ) => SelectionChanged, selectedEditors ) ;
	            });
	    }

	    public void AlignVertically( )
	    {
	        _memento.Record("Align Vertically", () =>
	            {
	                IEnumerable<ItemEditor> selectedEditors = Level.SelectedEditors.ToList( ) ;
	                foreach( ItemEditor eachEditor in selectedEditors )
	                {
	                    eachEditor.ItemProperties.Position = new Vector2(
	                        selectedEditors.First( ).ItemProperties.Position.X, eachEditor.ItemProperties.Position.Y ) ;
	                }
	            });
	    }

	    public void AlignRotation( )
	    {
	        _memento.Record(@"Align Rotation", () =>
	            {
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
	            });
	    }

	    public void AlignScale( )
	    {
	        _memento.Record(@"Align scale", () =>
	            {
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
	            });
	    }

	    #endregion

	    void clearSelectedEditors( )
	    {
	        Level.ClearSelectedEditors( ) ;

	        tryFire( ( ) => SelectionChanged, Enumerable.Empty<ItemEditor>( ) ) ;
	    }

	    void setActiveLayerIfItsDifferent( LayerEditor layer )
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
	}
}