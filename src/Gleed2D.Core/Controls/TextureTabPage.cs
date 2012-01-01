using System;
using System.Windows.Forms ;
using Gleed2D.InGame ;
using StructureMap ;

namespace Gleed2D.Core.Controls
{
	public class TextureTabPage  : TabPage, ICategoryTabPage
	{
		readonly TexturePickerControl _picker ;

		public TextureTabPage()
		{
			_picker = new TexturePickerControl
				{
					Dock = DockStyle.Fill
				} ;

			_picker.PathToFolderChanged += pathToFolderChanging ;
			_picker.DraggingTextureEvent += handlePickerDragEvent ;
			_picker.TextureChosen += textureChosen ;

			IModel model = IoC.Model ;

			if( model.Level != null )
			{
				model.Level.ContentRootFolder.PathChanging += ( s, e ) => _picker.SetFolder( new PathToFolder( e.ChosenFolder ) ) ;
			}

			model.NewModelLoaded += ( s, e ) =>
			                        	{
			                        		model.Level.ContentRootFolder.PathChanging +=
			                        			( ss, ee ) => _picker.SetFolder( new PathToFolder( ee.ChosenFolder ) ) ;

			                        		_picker.SetFolder( model.Level.ContentRootFolder ) ;
			                        	} ;

			Controls.Add( _picker ) ;
		}

		//todo: move somewhere else
		void textureChosen(object sender, EntityChosenEventArgs e)
		{
			IoC.Editor.StartCreatingEntityNow( e.EntityCreationProperties ) ;
		}

		void handlePickerDragEvent( object sender, DraggingTextureEventArgs e )
		{
			int n = 1 ;
		}

		void pathToFolderChanging( object sender, PathToFolderChangedEventArgs e )
		{
			var disk = ObjectFactory.GetInstance<IDisk>( ) ;

			string currentContentRoot = IoC.Model.Level.ContentRootFolder.AbsolutePath ;

			if (disk.IsSubfolder(currentContentRoot, e.ChosenFolder))
			{
				return ;
			}

			string message =
				@"You've chosen to show textures from '{0}'.  This isn't a subfolder of the 'content root' ({1}).".FormatWith( e.ChosenFolder, currentContentRoot ) ;
			
			MessageBox.Show( message, @"Content root mismatch", MessageBoxButtons.OK, MessageBoxIcon.Information ) ;
			
			e.Cancel = true ;
		}

		public string CategoryName
		{
			get
			{
				return Name ;
			}
			set
			{
				Text = Name = value ;
			}
		}

		public void AddPlugin( IEditorPlugin editorPlugin )
		{
			_picker.CreationPropertiesForEachItem = new EntityCreationProperties
				{
					Name = editorPlugin.Name,
					PluginType = editorPlugin.GetType( )
				} ;
		}

		public void SetRootFolder( PathToFolder folder )
		{
			_picker.SetFolder( folder );
		}
	}
}
