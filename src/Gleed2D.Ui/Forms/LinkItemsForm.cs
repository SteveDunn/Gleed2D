using System ;
using System.Collections.Generic ;
using System.Linq ;
using System.Windows.Forms ;
using Gleed2D.Core ;
using Gleed2D.InGame ;
using StructureMap ;

namespace GLEED2D.Forms
{
    public partial class LinkItemsForm : Form
    {
        public LinkItemsForm()
        {
            InitializeComponent();
        }

        private void uiLinkSecondItemToFirstCheckboxCheckChanged(object sender, EventArgs e)
        {
            label2.Enabled = uiSameAsFirstItemCheckbox.Enabled = uiLinkSecondItemToFirstCheckbox.Checked;
            
            uiSecondItemCustomPropertyNameTextBox.Text = string.Empty;
            
			updateSecondItem();
        }

        private void uiFirstItemCustomPropertyNameTextBoxTextChanged(object sender, EventArgs e)
        {
            updateSecondItem();
        }

        void updateSecondItem()
        {
            uiSecondItemCustomPropertyNameTextBox.Enabled = 
				uiLinkSecondItemToFirstCheckbox.Checked && !uiSameAsFirstItemCheckbox.Checked;
            
			if (!uiLinkSecondItemToFirstCheckbox.Checked) 
				return;
            
			if (uiSameAsFirstItemCheckbox.Checked)
			{
				uiSecondItemCustomPropertyNameTextBox.Text = uiFirstItemCustomPropertyNameTextBox.Text;
			}
        }

        void buttonOkClick(object sender, EventArgs e)
        {
			//todo: move to an action
            if (uiFirstItemCustomPropertyNameTextBox.Text=="")
            {
            	MessageBox.Show(
            		@"Please specify a name for the Custom Property that is to be added to the first Item!",
            		@"Error",
            		MessageBoxButtons.OK,
            		MessageBoxIcon.Error ) ;

				return;
            }
            
			if (uiLinkSecondItemToFirstCheckbox.Checked && uiSecondItemCustomPropertyNameTextBox.Text==string.Empty)
            {
            	MessageBox.Show(
            		@"Please specify a name for the Custom Property that is to be added to the second Item!",
            		@"Error",
            		MessageBoxButtons.OK,
            		MessageBoxIcon.Error ) ;
                
				return;
            }
        	
			var model = ObjectFactory.GetInstance<IModel>() ;

        	IEnumerable<ITreeItem> selectedEditors = model.Level.SelectedEditors.ToList(  ) ;
        	
			ITreeItem firstSelectedItem = selectedEditors.First() ;

        	CustomProperties customPropertiesForFirstItem = firstSelectedItem.ItemProperties.CustomProperties ;

        	if (customPropertiesForFirstItem.ContainsKey(uiFirstItemCustomPropertyNameTextBox.Text))
			{
				MessageBox.Show(
					"The first Item ({0}) already has a Custom Property named \"{1}\". Please use another name.".FormatWith(firstSelectedItem.ItemProperties.Name, uiFirstItemCustomPropertyNameTextBox.Text),
					@"Error",
					MessageBoxButtons.OK,
					MessageBoxIcon.Error ) ;
                
				return;
            }

        	var customProperty = new CustomProperty
        		{
        			Name = uiFirstItemCustomPropertyNameTextBox.Text,
        			Type = typeof( ItemEditor )
        		} ;

        	ITreeItem secondSelectItem = selectedEditors.ElementAt( 1 ) ;
        	
			customProperty.Value = secondSelectItem;
            
			customPropertiesForFirstItem.Add(customProperty.Name, customProperty);

            if (uiLinkSecondItemToFirstCheckbox.Checked)
            {
            	CustomProperties customPropertiesForSecondItem = secondSelectItem.ItemProperties.CustomProperties ;

            	if (customPropertiesForSecondItem.ContainsKey(uiSecondItemCustomPropertyNameTextBox.Text))
            	{
            		MessageBox.Show(
            			"The second Item ({0}) already has a Custom Property named \"{1}\". Please use another name!".FormatWith(secondSelectItem.ItemProperties.Name, uiSecondItemCustomPropertyNameTextBox.Text),
            			"Error",
            			MessageBoxButtons.OK,
            			MessageBoxIcon.Information ) ;
                    
					customPropertiesForFirstItem.Remove(customProperty.Name);
                    
					return;
                }
            	
				customProperty = new CustomProperty
            		{
            			Name = uiSecondItemCustomPropertyNameTextBox.Text,
            			Type = typeof( ItemEditor ),
            			Value = firstSelectedItem
            		} ;
            	
				customPropertiesForSecondItem.Add(customProperty.Name, customProperty);
            }

        	Hide();
        }

        void linkItemsFormVisibleChanged(object sender, EventArgs e)
        {
        	var model = ObjectFactory.GetInstance<IModel>() ;

        	IEnumerable<ITreeItem> selectedEditors = model.Level.SelectedEditors.ToList(  ) ;

        	uiFirstItemGroupBox.Text = uiFirstItemGroupBox.Text.Replace( "$f", selectedEditors.First( ).ItemProperties.Name ) ;
        	uiSecondItemGroupBox.Text = uiSecondItemGroupBox.Text.Replace( "$s", selectedEditors.ElementAt( 1 ).ItemProperties.Name ) ;
        }

        void uiSameAsFirstItemCheckboxCheckChanged(object sender, EventArgs e)
        {
            updateSecondItem();
        }
    }
}
