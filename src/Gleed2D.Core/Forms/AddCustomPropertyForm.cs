using System ;
using System.Windows.Forms ;
using Gleed2D.InGame ;
using Microsoft.Xna.Framework ;

namespace Gleed2D.Core.Forms
{
    public partial class AddCustomPropertyForm : Form
    {
    	readonly Predicate<string> _nameChecker ;

		readonly CustomProperty _customProperty = new CustomProperty();
    	
        public AddCustomPropertyForm(Predicate<string> nameChecker )
        {
        	_nameChecker = nameChecker ;
        	
			InitializeComponent();
        }

    	public CustomProperty NewCustomProperty
    	{
    		get
    		{
    			return _customProperty ;
    		}
    	}

    	void buttonOkClick(object sender, EventArgs e)
        {
            _customProperty.Name = uiNameTextBox.Text;
            _customProperty.Description = uiDescriptionTextBox.Text;

			if (uiStringRadioButton.Checked)
            {
                _customProperty.Type = typeof(string);
                _customProperty.Value = string.Empty;
            }
            
			if (uiBooleanRadioButton.Checked)
            { 
                _customProperty.Type = typeof(bool);
                _customProperty.Value = false;
            }
            
			if (uiVector2RadioButton.Checked)
            {
                _customProperty.Type = typeof(Vector2);
                _customProperty.Value = new Vector2(1, 1);
            }
            
			if (uiColorRadioButton.Checked)
            {
                _customProperty.Type = typeof(Color);
                _customProperty.Value = Color.White;
            }
            
			if (uiLinkedItemRadioButton.Checked)
            {
                _customProperty.Type = typeof(LinkedItem);
                _customProperty.Value = null;
            }
            
            DialogResult=DialogResult.OK;

			Close();
        }

        private void buttonCancelClick(object sender, EventArgs e)
        {
            Close();
        }

        private void radioButton1_CheckedChanged(object sender, EventArgs e)
        {

        }

        private void AddCustomProperty_Load(object sender, EventArgs e)
        {

        }

		private void textBox1_TextChanged(object sender, EventArgs e)
		{
			uiErrorProvider.SetError( uiNameTextBox, string.Empty );

			string proposedName = uiNameTextBox.Text ;

			if( !_nameChecker(proposedName) )
			{
				uiErrorProvider.SetError( uiNameTextBox, @"An item with the same name exists" );
			}

			buttonOK.Enabled = proposedName.Length > 0 && uiErrorProvider.GetError( uiNameTextBox ).Length == 0 ;
		}
    }
}
