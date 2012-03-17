using System ;
using System.ComponentModel ;
using System.Drawing.Design ;
using System.Windows.Forms.Design ;
using Gleed2D.Core.Controls ;
using Gleed2D.InGame ;

namespace Gleed2D.Core.CustomUITypeEditors
{
	public class LinkedItemUiTypeEditor : UITypeEditor
	{
		public override UITypeEditorEditStyle GetEditStyle( ITypeDescriptorContext context )
		{
			return UITypeEditorEditStyle.DropDown ;
		}

		public override object EditValue( ITypeDescriptorContext context, IServiceProvider provider, object value )
		{
			var editorService =
				provider.GetService( typeof( IWindowsFormsEditorService ) ) as IWindowsFormsEditorService ;

			if( editorService != null && value != null )
			{
				ITreeItem editor = IoC.Model.Level.GetItemByName( ( (LinkedItem) value ).Name ) ;
				
				var itemSelector = new ItemSelectorControl( editor ) ;
				
				editorService.DropDownControl( itemSelector ) ;

			    ITreeItem itemEditor = itemSelector.ItemEditor;
			    
                value = new LinkedItem
					{
						Name=itemEditor==null?string.Empty:itemEditor.Name
					} ;// itemSelector.ItemEditor;

				return value ;
			}

			return new LinkedItem
				{
					Name = string.Empty
				} ;
		}

		public override bool IsDropDownResizable
		{
			get
			{
				return true ;
			}
		}
	}
}