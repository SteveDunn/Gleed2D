using System ;
using System.Collections.Generic ;
using System.Xml.Linq;
using Gleed2D.Core.Behaviour ;
using Gleed2D.InGame ;

namespace Gleed2D.Core
{
	public interface IModel
	{
		event EventHandler<ModelChangedEventArgs> ItemChanged ;
		
		event EventHandler<ModelChangedEventArgs> ItemsAddedOrRemoved ;

		event EventHandler<ModelChangedEventArgs> ItemsMoved ;
	
		event EventHandler<EventArgs> NewModelLoaded ;
		
		event EventHandler<ModelChangedEventArgs> SelectionChanged ;
		
		event EventHandler<ModelUnloadingEventArgs> OnBeforeUnloadingModel ;

        LevelEditor Level
        {
            get;
        }

	    LayerEditor ActiveLayer
		{
			get ;
		}

		int NextItemNumber
		{
			get ;
		}

		void AddEditor( ItemEditor editor ) ;

		void AddNewLayer( LayerEditor layer ) ;

		void SelectEditor( ItemEditor editor ) ;
	
		void MoveItemToLayer( ItemEditor i1, LayerEditor layer, ItemEditor itemEditor ) ;

		void DeleteLayer( LayerEditor layer ) ;
	
		void DeleteSelectedItems( ) ;
		
		void MoveLayerUp( LayerEditor layer ) ;
		void MoveEditorUp( ItemEditor item ) ;
		
		void MoveLayerDown( LayerEditor layer ) ;
		void MoveEditorDown( ItemEditor editor ) ;
		
		void SelectEverythingInSelectedLayer( ) ;

		void MoveSelectedItemsToLayer( LayerEditor chosenLayer ) ;
		void CopySelectedItemsToLayer( LayerEditor chosenlayer ) ;
	
		void AlignHorizontally( ) ;
		void AlignVertically( ) ;
		void AlignRotation( ) ;
		void AlignScale( ) ;

	
		void LoadLevel( LevelEditor newlevel ) ;
		void SaveLevel( string filename ) ;
		void CreateNewLevel( ) ;
		void RemoveCustomPropertyItem( ITreeItem item, DictionaryPropertyDescriptor propertyDescriptor ) ;
		void RenameItem( ITreeItem treeItem, string newName ) ;
		void DuplicateLayer( LayerEditor layer ) ;
		void SelectEditors( SelectedEditors itemEditors ) ;
		void SelectLayer( LayerEditor layer ) ;
		void AddEditorToSelection(ItemEditor editor) ;
		void ToggleSelectionOnItem( ItemEditor item ) ;
		event EventHandler<EventArgs> ActiveLayerChanged ;
		event EventHandler<ModelChangedEventArgs> ItemsRearrangedInLayer ;
		void AddCustomProperty( ITreeItem treeItem, CustomProperty newCustomProperty ) ;
		
		void NotifyChanged( IEnumerable<ItemEditor> items ) ;
		void NotifyChanged( ItemEditor item ) ;
		
		void SelectLevel( ) ;
		void AttachBehaviour( ITreeItem target, IBehaviour behaviour) ;
		void SelectBehaviour( ITreeItem behaviour ) ;
	    void DeserialiseLevel(XElement xml);
	}
}