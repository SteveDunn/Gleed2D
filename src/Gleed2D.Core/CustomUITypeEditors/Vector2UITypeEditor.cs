using System;
using System.ComponentModel;
using System.Drawing.Design;
using System.Windows.Forms.Design;
using Microsoft.Xna.Framework;

namespace Gleed2D.Core.CustomUITypeEditors
{
	public class Vector2UiTypeEditor : UITypeEditor
	{
		public override UITypeEditorEditStyle GetEditStyle( ITypeDescriptorContext context )
		{
			return UITypeEditorEditStyle.DropDown ;
		}

		public override object EditValue( ITypeDescriptorContext context, IServiceProvider provider, object value )
		{
			var editorService =
				provider.GetService( typeof( IWindowsFormsEditorService ) ) as IWindowsFormsEditorService ;

			if( editorService != null )
			{
				var editorControl = new Vector2EditorControl( (Vector2) value ) ;
				editorService.DropDownControl( editorControl ) ;
				value = editorControl.Value ;
			}
			
			return value ;
		}
	}
}
