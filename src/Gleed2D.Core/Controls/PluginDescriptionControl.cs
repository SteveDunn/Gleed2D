using System ;
using System.Windows.Forms ;

namespace Gleed2D.Core.Controls
{
	public class PluginDescriptionControl : Control
	{
		public PluginDescriptionControl( IPlugin plugin )
		{
			var textBox = new TextBox
				{
					Text = plugin.Name,
					Dock = DockStyle.Fill
				} ;


			Controls.Add( textBox ) ;
		}
	}
}