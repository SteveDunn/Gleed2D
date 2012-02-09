using System ;
using System.Windows.Forms ;

namespace Gleed2D.Core.Controls
{
	public partial class ItemSelectorControl : UserControl
	{
		readonly LevelExplorerControl _levelExplorer ;
		
		public ItemSelectorControl( ITreeItem itemEditor )
		{
			ItemEditor = itemEditor ;

			InitializeComponent( ) ;

			_levelExplorer = new LevelExplorerControl
				{
					Dock = DockStyle.Fill,
					Parent = this,
					CheckBoxes= false,
					Visible = true,
					ShowToolStrip = false,
					HideContextMenus=true,
					InteractsWithModel=false					
				} ;

			_levelExplorer.ItemSelected += ( s, e ) =>
			                               	{
			                               		if( e.Item is ItemEditor )
			                               		{
			                               			ItemEditor = e.Item as ItemEditor ;
			                               		}
			                               	} ;

			Controls.Add( _levelExplorer );

			_levelExplorer.Refresh(  );
		}

		public ITreeItem ItemEditor
		{
			get;
			private set ;
		}

		private void itemSelectorLoad( object sender, EventArgs e )
		{
			if( ItemEditor != null )
			{
				if (IoC.Model.Level.ContainsAnythingNamed(ItemEditor.Name))
				{
					_levelExplorer.SelectItemByName( ItemEditor.Name ) ;
				}
				else
				{
					ItemEditor = null;
				}
			}
		}
	}
}
