using System ;
using System.Windows.Forms ;

namespace Gleed2D.Core.Forms
{
    public partial class AddLayerDialog : Form
    {
    	readonly Predicate<string> _nameChecker ;
    	string _chosenName ;

    	public AddLayerDialog(Predicate<string> nameChecker )
        {
    		_nameChecker = nameChecker ;
        	
			InitializeComponent();
        }

    	public string ChosenName
    	{
    		get
    		{
    			return _chosenName ;
    		}
    	}

		void uiNameTextBoxTextChanged(object sender, EventArgs e)
		{
			string whatTheUserTyped = uiNameTextBox.Text ;

			_chosenName = whatTheUserTyped ;
			
			validateProposedName( ) ;

			uiButtonOk.Enabled=whatTheUserTyped.Length>0 && uiErrorProvider.GetError( uiNameTextBox ).Length==0 ;
		}

    	void validateProposedName( )
    	{
    		string proposedName = uiNameTextBox.Text ;

    		if (!_nameChecker(proposedName))
			{
				uiErrorProvider.SetError(
					uiNameTextBox,
					@"A layer or item with the name ""{0}"" already exists in the level. Please use another name.".FormatWith(
						proposedName));
			}
			else
			{
				uiErrorProvider.SetError( uiNameTextBox, string.Empty );
			}
    	}

		private void uiButtonOk_Click(object sender, EventArgs e)
		{
			DialogResult = DialogResult.OK;
		}

		private void buttonCancel_Click(object sender, EventArgs e)
		{
			DialogResult = DialogResult.Cancel;
		}
    }
}
