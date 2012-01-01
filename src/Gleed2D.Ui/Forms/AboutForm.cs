using System ;
using System.Windows.Forms ;
using Gleed2D.Core ;
using StructureMap ;

namespace GLEED2D.Forms
{
    public partial class AboutForm : Form
    {
        public AboutForm()
        {
            InitializeComponent();
        	var getAssemblyInformation = ObjectFactory.GetInstance<IGetAssemblyInformation>( ) ;
        	textBox1.Text = textBox1.Text.Replace("{v}", getAssemblyInformation.Version);
        }

        private void button1_Click(object sender, EventArgs e)
        {
            Close();
        }

        private void linkLabel1_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            System.Diagnostics.Process.Start("http://gleed2d.codeplex.com");
        }

        private void textBox1_TextChanged(object sender, EventArgs e)
        {

        }

		private void AboutForm_Load(object sender, EventArgs e)
		{
			var extensibility = ObjectFactory.GetInstance<IExtensibility>( ) ;
			extensibility.EditorPlugins.ForEach( buildTab );
		}

    	void buildTab( IEditorPlugin editorPlugin )
    	{
    		var tabPage = new TabPage
    			{
    				Text = editorPlugin.Name
    			} ;

    		Control control = editorPlugin.ControlForAboutBox ;
			
			control.Dock=DockStyle.Fill;

			tabPage.Controls.Add( control );

			uiTabs.TabPages.Add( tabPage );
			
    	}
    }
}
