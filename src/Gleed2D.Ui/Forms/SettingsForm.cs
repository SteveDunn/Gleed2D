using System ;
using System.Windows.Forms ;
using Gleed2D.Core ;

namespace GLEED2D.Forms
{
	public partial class SettingsForm : Form
	{
		public SettingsForm()
		{
			InitializeComponent();
		}

		private void SettingsForm_Load(object sender, EventArgs e)
		{
			Constants.Instance.SaveToDisk(@"settings.xml");
			
			propertyGrid1.SelectedObject = Constants.Instance;

			//Run Level tab
			checkBox1.Checked = Constants.Instance.RunLevelStartApplication;
			textBox1.Text = Constants.Instance.RunLevelApplicationToStart;
			checkBox2.Checked = Constants.Instance.RunLevelAppendLevelFilename;

			//Save Level tab
			checkBox4.Checked = Constants.Instance.SaveLevelStartApplication;
			textBox2.Text = Constants.Instance.SaveLevelApplicationToStart;
			checkBox3.Checked = Constants.Instance.SaveLevelAppendLevelFilename;
		}

		private void buttonOK_Click(object sender, EventArgs e)
		{
			Constants.Instance.RunLevelApplicationToStart = textBox1.Text;
			Constants.Instance.SaveLevelApplicationToStart = textBox2.Text;
			Constants.Instance.SaveToDisk("settings.xml");
			Close();
		}

		private void buttonCancel_Click(object sender, EventArgs e)
		{
			Constants.TryToLoadOtherwiseSetDefaults("settings.xml");
			Close();
		}

		//Run Level tab
		void checkBox1_CheckedChanged(object sender, EventArgs e)
		{
			textBox1.Enabled = buttonBrowse.Enabled = checkBox2.Enabled = checkBox1.Checked;
			Constants.Instance.RunLevelStartApplication = checkBox1.Checked;
		}

		void checkBox2_CheckedChanged(object sender, EventArgs e)
		{
			Constants.Instance.RunLevelAppendLevelFilename = checkBox2.Checked;
		}

		private void buttonBrowse_Click(object sender, EventArgs e)
		{
			using( var dialog = new OpenFileDialog
				{
					FileName = textBox1.Text,
					Filter = @"Executable Files (*.exe, *.bat)|*.exe;*.bat"
				} )
			{
				if( dialog.ShowDialog( ) == DialogResult.OK )
				{
					textBox1.Text = dialog.FileName ;
				}
			}
		}

		//Save Level tab
		private void checkBox4_CheckedChanged(object sender, EventArgs e)
		{
			textBox2.Enabled = button1.Enabled = checkBox3.Enabled = checkBox4.Checked;
			Constants.Instance.SaveLevelStartApplication = checkBox4.Checked;
		}
		private void checkBox3_CheckedChanged(object sender, EventArgs e)
		{
			Constants.Instance.SaveLevelAppendLevelFilename = checkBox3.Checked;
		}
		private void button1_Click(object sender, EventArgs e)
		{
			OpenFileDialog f = new OpenFileDialog();
			f.FileName = textBox2.Text;
			f.Filter = "Executable Files (*.exe, *.bat)|*.exe;*.bat";
			if (f.ShowDialog() == DialogResult.OK)
			{
				textBox2.Text = f.FileName;
			}
		}
	}
}
