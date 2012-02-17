using System;
using System.Windows.Forms;
using Gleed2D.Core;
using Gleed2D.Core.Controls;
using Gleed2D.InGame;
using StructureMap;

namespace Gleed2D.Plugins
{
	public class TextureTabPage  : TabPage, ICategoryTabPage, ISubscriber<ContentRootChanged>, ISubscriber<NewModelLoaded>
	{
		readonly TexturePickerControl _picker ;

		public TextureTabPage()
		{
			_picker = new TexturePickerControl
				{
					Dock = DockStyle.Fill
				} ;

			//_picker.PathToFolderChanged += pathToFolderChanging ;
			_picker.TextureChosen += textureChosen ;

			IModel model = IoC.Model ;

			var eventHub = ObjectFactory.GetInstance<IEventHub>();
			eventHub.Subscribe<ContentRootChanged>(this);
			eventHub.Subscribe<NewModelLoaded>(this);

			if( model.Level != null )
			{
				//model.Level.ContentRootFolder.PathChanging += ( s, e ) => _picker.SetFolder( new PathToFolder( e.ChosenFolder ) ) ;
			}

			//model.NewModelLoaded += ( s, e ) =>
			//                            {
			//                                model.Level.ContentRootFolder.PathChanging +=
			//                                    ( ss, ee ) => _picker.SetFolder( new PathToFolder( ee.ChosenFolder ) ) ;

			//                                _picker.SetFolder( model.Level.ContentRootFolder ) ;
			//                            } ;

			Controls.Add( _picker ) ;
		}

		//todo: move somewhere else
		void textureChosen(object sender, EntityChosenEventArgs e)
		{
			IoC.Canvas.StartCreatingEntityNow( e.EntityCreationProperties ) ;
		}

		void pathToFolderChanging( object sender, PathToFolderChangedEventArgs e )
		{
			var disk = ObjectFactory.GetInstance<IDisk>( ) ;

			string currentContentRoot = IoC.Model.Level.ContentRootFolder ;

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
		}

		public void SetRootFolder( string folder )
		{
			_picker.SetFolder( folder );
		}

		public void Receive(ContentRootChanged subject)
		{
			_picker.SetFolder( subject.NewContentRootFolder) ;
		}

		public void Receive(NewModelLoaded subject)
		{
			int n = 1;
		}
	}
}
