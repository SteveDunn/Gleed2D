using System ;
using System.ComponentModel ;
using System.Drawing.Design ;
using System.Windows.Forms ;
using Gleed2D.InGame ;

namespace Gleed2D.Core.CustomUITypeEditors
{
	/// <summary>
    /// A FolderEditor that always starts at the currently selected folder. For use on a property of type: string.
    /// </summary>
    public class PathToFolderUiTypeEditor : UITypeEditor
    {
        public override UITypeEditorEditStyle GetEditStyle(ITypeDescriptorContext context)
        {
            return UITypeEditorEditStyle.Modal;
        }

        public override object EditValue(ITypeDescriptorContext context, IServiceProvider provider, object value)
        {
            string path = Convert.ToString(value);
            
			using (var dialog = new FolderBrowserDialog())
            {
                dialog.SelectedPath = path;
                if (dialog.ShowDialog() == DialogResult.OK)
                {
                    path = dialog.SelectedPath;
                }
            }
        	return new PathToFolder
        		{
        			AbsolutePath = path
        		} ;
        }
    }
}
