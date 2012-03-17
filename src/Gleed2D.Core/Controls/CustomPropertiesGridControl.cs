using System;
using System.Collections ;
using System.Collections.Generic ;
using System.ComponentModel ;
using System.Drawing ;
using System.Drawing.Design ;
using System.Linq ;
using System.Windows.Forms;
using StructureMap ;

namespace Gleed2D.Core.Controls
{
	public partial class CustomPropertiesGridControl : UserControl
	{
		readonly Label _label ;
		readonly PropertyGrid _grid ;
		readonly IMemento _memento ;

		readonly IModel _model ;
		ITreeItem _currentItem ;

		static readonly PropertyValueUIItemInvokeHandler _uiItemNullHandler = delegate
		                                                                      	{
		                                                                      	} ;
		static readonly Image _uiItemErrorImage = SystemIcons.Error.ToBitmap( ) ;
		readonly GlyphService _glyphService ;

		public CustomPropertiesGridControl( )
		{
			InitializeComponent( ) ;

			if( !Helper.IsInDesignMode )
			{
				_memento = IoC.Memento ;
				_model = ObjectFactory.GetInstance<IModel>( ) ;
				_model.SelectionChanged += ( sender, e1 ) => refresh( e1.ItemsAffected ) ;
				_model.ItemChanged += ( sender1, e2 ) => refresh( e2.ItemsAffected ) ;
				_model.ActiveLayerChanged += ( s, e ) => refresh(
					new[ ]
						{
							_model.Level.ActiveLayer
						} ) ;
			}

			_grid = new PropertyGrid
				{
					Dock = DockStyle.Fill,
					Visible = false,
					PropertySort = PropertySort.Categorized,
					ToolbarVisible = false
				} ;

			_grid.PropertyValueChanged += gridPropertyValueChanged ;
		    _grid.SelectedObjectsChanged += gridSelectObjectsChanged;
            
			_label = new Label
				{
					Dock = DockStyle.Fill,
					Text = @"Select a single item to view its properties",
					Visible = true,
				} ;

			Controls.Add( _grid ) ;
			Controls.Add( _label ) ;

			var site = new SimpleSite
				{
					Name = @"GridSite",
					Component = _grid,
				} ;

			_glyphService = new GlyphService( ) ;

			_glyphService.QueryPropertyUIValueItems += verifyDataErrorInfo ;

			site.AddService<IPropertyValueUIService>( _glyphService ) ;

			_grid.Site = site ;
		}

	    void gridSelectObjectsChanged(object sender, EventArgs e)
	    {
	        int n = 1;
	    }

	    static void verifyDataErrorInfo(
			ITypeDescriptorContext context, PropertyDescriptor propDesc, ArrayList valueUiItemList )
		{
			IDataErrorInfo errInfo = context == null ? null : context.Instance as IDataErrorInfo ;

			string propName = propDesc == null ? null : propDesc.Name ;

			if( errInfo != null && !string.IsNullOrEmpty( propName ) )
			{
				string errMsg = errInfo[ propName ] ;

				if( !string.IsNullOrEmpty( errMsg ) )
				{
					valueUiItemList.Add( new PropertyValueUIItem( _uiItemErrorImage, _uiItemNullHandler, errMsg ) ) ;
				}
			}
		}

		void refresh( IEnumerable<ITreeItem> itemsAffected )
		{
			var allAffectedItems = itemsAffected.ToList( ) ;

			if( allAffectedItems.Count( ) == 1 )
			{
				showGrid( allAffectedItems.First( ) ) ;
			}
			else
			{
				showMultipleSelection( ) ;
			}
		}

		void showMultipleSelection( )
		{
			if( _label.Visible )
			{
				return ;
			}

			_label.Visible = true ;
			_grid.Visible = false ;
		}

		void showGrid( ITreeItem treeItem )
		{
			_currentItem = treeItem ;
			_grid.Visible = true ;
			_label.Visible = false ;
		    var disposable = _grid.SelectedObject as IDisposable;
		    if (disposable != null)
		    {
                disposable.Dispose();
		    }
		    _grid.SelectedObject = treeItem.ObjectForPropertyGrid ;
		}

		void gridPropertyValueChanged( object s, PropertyValueChangedEventArgs e )
		{
			_memento.EndCommand( ) ;

			_memento.BeginCommand( @"Edit '{0}' property".FormatWith( getNameOfChangedItem( e.ChangedItem ) ) ) ;

			_currentItem.PropertiesChanged(e);

			var itemEditor = _currentItem as ItemEditor ;
			if( itemEditor != null )
			{
				//debt: Should we not notify the model about other non 'ItemEditor' changes, such as the level itself and the layers?
				IoC.Model.NotifyChanged( itemEditor ) ;
			}
		}

		string getNameOfChangedItem( GridItem changedItem )
		{
			if (changedItem.PropertyDescriptor == null)
			{
				throw new InvalidOperationException( @"A property in the grid changed but didn't have a PropertyDescriptor." ) ;
			}

			return changedItem.PropertyDescriptor.Name ;
		}

		void deleteCustomPropertyToolStripMenuItem_Click(object sender, EventArgs e)
		{
			_memento.EndCommand( ) ;

			var propertyDescriptor = (DictionaryPropertyDescriptor) _grid.SelectedGridItem.PropertyDescriptor ;

			_memento.BeginCommand( @"Deleting custom property '{0}'".FormatWith( propertyDescriptor.Name ) );

			_model.RemoveCustomPropertyItem( _currentItem, propertyDescriptor ) ;

			_grid.Refresh( ) ;
		}

		void CustomPropertyContextMenu_Opening(object sender, CancelEventArgs e)
		{
			var propertyDescriptor = _grid.SelectedGridItem.PropertyDescriptor as DictionaryPropertyDescriptor ;
			e.Cancel = propertyDescriptor == null ;
		}
	}
}