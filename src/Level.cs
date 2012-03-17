using System;
using System.Collections.Generic;
using System.ComponentModel ;
using System.IO ;
using System.Linq ;
using System.Windows.Forms ;
using System.Xml.Linq ;
using Gleed2D.Core.Behaviour ;
using Gleed2D.Core.CustomUITypeEditors;
using Gleed2D.InGame ;
using Microsoft.Xna.Framework ;
using StructureMap ;

namespace Gleed2D.Core
{
	public class LevelEditor : ICustomTypeDescriptor, ITreeItem
	{
		static readonly StringComparer _comparer = StringComparer.OrdinalIgnoreCase ;

		readonly LevelProperties _properties ;

		static DateTime _seedForIdGenerator ;
		private string _previousContentRootFolder;

		public LevelEditor()
		{
			Behaviours = new BehaviourCollection( );
			
			const string contentRootFolder = @"c:\";

			_properties = new LevelProperties
				{
					Name = "Root",
					Visible = true,
					CustomProperties = new CustomProperties(),
					ContentRootFolder = contentRootFolder,
					Version = ObjectFactory.GetInstance<IGetAssemblyInformation>( ).Version
				};

			_previousContentRootFolder = contentRootFolder;

			var contentRootChanged = new ContentRootChanged(_previousContentRootFolder, contentRootFolder);
			
			ObjectFactory.GetInstance<IEventHub>().Publish(contentRootChanged);

			Layers = new List<LayerEditor>
				{
					new LayerEditor( this, @"Layer_0" )
				} ;
		}

		public LevelEditor(XElement xml)
		{
			TypeLookup.Rehydrate( xml );
			
			_properties = xml.Element( @"LevelProperties" ).DeserializedAs<LevelProperties>( ) ;

			_previousContentRootFolder = _properties.ContentRootFolder;

			if( !Directory.Exists( _properties.ContentRootFolder ) )
			{
				string message = @"The level file has a content root folder that does not exist.
It says the content root is at ""{0}"". Images specified in this level file are relative to this folder so you should change it in order to load this level file correctly.
Would you like to change it?".FormatWith( _properties.ContentRootFolder ) ;

				if(MessageBox.Show( message, @"Content root folder not found.", MessageBoxButtons.YesNo, MessageBoxIcon.Question )==DialogResult.Yes)
				{
					var folderBrowserDialog = new Ookii.Dialogs.VistaFolderBrowserDialog( ) ;

					DialogResult dialogResult = folderBrowserDialog.ShowDialog( ) ;
					
					if( dialogResult == DialogResult.OK )
					{
						_properties.ContentRootFolder=folderBrowserDialog.SelectedPath ;
					}
				}
			}

			if (_properties.ContentRootFolder != _previousContentRootFolder)
			{
				//ObjectFactory.GetInstance<IEventHub>().Publish(new ContentRootChanged(_previousContentRootFolder, _properties.ContentRootFolder));
			}

			Behaviours = new BehaviourCollection( _properties, xml );

			Layers = new List<LayerEditor>( xml.CertainElement( @"Layers" ).Elements( @"Layer" ).Select( x => LayerEditor.FromXml( this, x ) ) ) ;
			
			ActiveLayer = Layers.FirstOrDefault( ) ;
		}

		public XElement ToXml( )
		{
			var xml = new XElement(
				new XElement(
					@"Level",
					_properties.SerializeToXml( ),
					Behaviours.ToXml( ),
					new XElement(
						@"Layers",
						Layers.Select( l => l.ToXml( ) ) )
					) ) ;

			TypeLookup.Compress( xml ) ;

			return xml ;
		}

		//todo: move into a factory
		public string GetUniqueNameBasedOn(string name)
		{
			int i=0;
			
			string newName = @"Copy of {0}".FormatWith(name);

			while (ContainsAnythingNamed( newName )) 
			{
				newName = @"Copy({0}) of {1}".FormatWith(i++, name);
			}
			
			return newName;
		}

		public IEnumerable<ItemEditor> SelectedEditors
		{
			get
			{
				if( ActiveLayer == null )
				{
					return Enumerable.Empty<ItemEditor>( ) ;
				}
				
				return ActiveLayer.Items.Where( li => li.IsSelected ).OrderBy( li=>li.TimeSelected ) ;
			}
		}

		public LayerEditor ActiveLayer
		{
			get;
			private set ;
		}

		public string Name
		{
			get
			{
				return _properties.Name ;
			}
			set
			{
				_properties.Name = value ;
			}
		}

		public ICustomTypeDescriptor ObjectForPropertyGrid
		{
			get
			{
				var itemPropertiesWrapper = new ItemPropertiesWrapper<ItemProperties>(_properties ) ;
				
				itemPropertiesWrapper.OverrideEditor<string>(typeof(PathToFolderUsingStringUiTypeEditor));

				itemPropertiesWrapper.Customise(() => _properties.ContentRootFolder).SetDescription(
					@"When the level is saved, each texture is saved with a path relative to this folder. You should set this to the ""Content.RootDirectory"" of your game project.")
					.SetDisplayName(@"Content root folder");

				itemPropertiesWrapper.Customise( ( ) => _properties.NextItemNumber ).SetCategory( @"Editor related" ).Hide( ).MakeReadOnly( ) ;

				itemPropertiesWrapper.Customise( ( ) => _properties.CameraPosition ).SetCategory( @"Editor related" ).Hide( ).MakeReadOnly( ) ;

				itemPropertiesWrapper.Customise( ( ) => _properties.Version ).SetCategory( @"Editor related" ).Hide( ).MakeReadOnly( ) ;

				return itemPropertiesWrapper ;
			}
		}

		public ItemProperties ItemProperties
		{
			get
			{
				return _properties ;
			}
		}

		public void RenameTo( string name )
		{
			_properties.Name = name ;
		}

		public void AddBehaviour( IBehaviour behaviour )
		{
			Behaviours.Add( behaviour );
		}

		public void PropertiesChanged(PropertyValueChangedEventArgs whatChanged)
		{
			if (whatChanged.ChangedItem.PropertyDescriptor == null)
			{
				return;
			}
			
			if( whatChanged.ChangedItem.PropertyDescriptor.Name==@"ContentRootFolder")
			{
				var contentRootChanged = new ContentRootChanged(_previousContentRootFolder, _properties.ContentRootFolder);
				ObjectFactory.GetInstance<IEventHub>().Publish(contentRootChanged );
				ObjectFactory.GetInstance<IModelEventHub>().Publish(contentRootChanged );
				
				_previousContentRootFolder = _properties.ContentRootFolder;
			}
		}

		public bool Visible
		{
			get
			{
				return _properties.Visible ;
			}
			set
			{
				_properties.Visible = value ;
			}
		}

		/// <summary>
		/// A Level contains several Layers. Each Layer contains several Items.
		/// </summary>
		public List<LayerEditor> Layers
		{
			get;
			private set ;
		}

		public ItemEditor GetItemByName(string name)
		{
			return Layers.SelectMany( layer => layer.Items ).FirstOrDefault( editor => editor.ItemProperties.Name == name ) ;
		}

		public string ContentRootFolder
		{
			get
			{
				return _properties.ContentRootFolder ;
			}
		}

		public CustomProperties CustomProperties
		{
			get
			{
				return _properties.CustomProperties ;
			}
		}

		public Vector2 CameraPosition
		{
			get
			{
				return _properties.CameraPosition ;
			}
		}

	    public int GetNextItemNumber( )
		{
			return ( ++_properties.NextItemNumber ) ;
		}

		public void SaveAsXmlToDisk( string filename )
		{
			var editor = ObjectFactory.GetInstance<ICanvas>( ) ;

			_properties.CameraPosition = editor.Camera.Position ;

			var document = ToXml( ) ;

			document.Save( filename ) ;
		}

		AttributeCollection ICustomTypeDescriptor.GetAttributes()
		{
			return TypeDescriptor.GetAttributes(this, true);
		}

		string ICustomTypeDescriptor.GetClassName()
		{
			return TypeDescriptor.GetClassName(this, true);
		}

		string ICustomTypeDescriptor.GetComponentName()
		{
			return TypeDescriptor.GetComponentName(this, true);
		}

		TypeConverter ICustomTypeDescriptor.GetConverter()
		{
			return TypeDescriptor.GetConverter(this, true);
		}

		EventDescriptor ICustomTypeDescriptor.GetDefaultEvent()
		{
			return TypeDescriptor.GetDefaultEvent(this, true);
		}

		PropertyDescriptor ICustomTypeDescriptor.GetDefaultProperty()
		{
			return TypeDescriptor.GetDefaultProperty(this, true);
		}

		object ICustomTypeDescriptor.GetEditor(Type editorBaseType)
		{
			return TypeDescriptor.GetEditor(this, editorBaseType, true);
		}

		EventDescriptorCollection ICustomTypeDescriptor.GetEvents(Attribute[] attributes)
		{
			return TypeDescriptor.GetEvents(this, attributes, true);
		}

		EventDescriptorCollection ICustomTypeDescriptor.GetEvents()
		{
			return TypeDescriptor.GetEvents(this, true);
		}

		PropertyDescriptorCollection ICustomTypeDescriptor.GetProperties(Attribute[] attributes)
		{
			var properties = new PropertyDescriptorCollection(new PropertyDescriptor[0]);
			
			foreach (PropertyDescriptor pd in TypeDescriptor.GetProperties(this))
			{
				properties.Add(pd);
			}
			
			foreach (string key in _properties.CustomProperties.Keys)
			{
				properties.Add(new DictionaryPropertyDescriptor(_properties.CustomProperties, key, attributes));
			}
			
			return properties;
		}

		PropertyDescriptorCollection ICustomTypeDescriptor.GetProperties()
		{
			return TypeDescriptor.GetProperties(this, true);
		}

		object ICustomTypeDescriptor.GetPropertyOwner(PropertyDescriptor pd)
		{
			return this;
		}

		public bool ContainsAnythingNamed( string name )
		{
			if(Layers.Any( l=> _comparer.Compare( l.Name,name) == 0) )
			{
				return true ;
			}

			return Layers.Any( l => l.ContainsAnythingNamed( name ) ) ;
		}

		public void SelectEditor( ItemEditor editor )
		{
			LayerEditor parentLayer = editor.ParentLayer ;

			editor.IsSelected = true ;

			ActiveLayer = parentLayer ;
		}

		public void SelectLayer( LayerEditor value )
		{
			clearAllSelections( ) ;
			
			ActiveLayer = value ;
		}

		void clearAllSelections( )
		{
			allEditors( ).ForEach( editor => editor.IsSelected = false ) ;
		}

		IEnumerable<ItemEditor> allEditors( )
		{
			return Layers.SelectMany( layer => layer.Items ) ;
		}

		public void SelectEverythingInSelectedLayer( )
		{
			ClearSelectedEditors(  );

			ActiveLayer.Items.ForEach( i=>i.IsSelected=true );
		}

		public void MoveSelectedIditorsToLayer( LayerEditor chosenLayer )
		{
			var selected = SelectedEditors.ToList(  ) ;

			foreach( var eachSelectedEditor in selected )
			{
				ActiveLayer.RemoveEditor( eachSelectedEditor ) ;
				
				eachSelectedEditor.ParentLayer = chosenLayer ;
				
				chosenLayer.AddEditor( eachSelectedEditor );
			}
		}

		public IEnumerable<ItemEditor> CopySelectedEditorsToLayer( LayerEditor destinationLayer )
		{
			var selectedEditors = SelectedEditors.ToList(  )  ;

			foreach( ItemEditor eachEditor in selectedEditors )
			{
				ItemEditor clonedEditor = eachEditor.Clone( ) ;
				
				clonedEditor.ParentLayer = destinationLayer ;
				clonedEditor.ItemProperties.Name = destinationLayer.ParentLevel.GetUniqueNameBasedOn( clonedEditor.ItemProperties.Name ) ;

				destinationLayer.AddEditor( clonedEditor );

				clonedEditor.ItemProperties.Id = generateId( ) ;

				yield return clonedEditor ;
			}
		}

		public int GenerateId(int number)
		{
			_seedForIdGenerator = new DateTime( 1970, 1, 1, 0, 0, 0 ) ;
			
			TimeSpan timeSpan = ( DateTime.UtcNow - _seedForIdGenerator ) ;
			
			var unixTime = (int) timeSpan.TotalSeconds ;
			
			return unixTime + number ; 
		}

		int generateId()
		{
			return GenerateId( GetNextItemNumber( ) ) ;
		}

		public void SelectEditors( SelectedEditors editors )
		{
			clearAllSelections(  );

			editors.Items.ForEach( ce => ce.IsSelected = true ) ;
		}

		public void AddEditor( ItemEditor editor )
		{
			var possibleDuplicate = allEditors( ).FirstOrDefault( e => e.Name == editor.Name ) ;

			if (possibleDuplicate!=null)
			{
				throw new InvalidOperationException(
					@"Cannot add editor '{0}' into layer '{1}' as an editor of the same name exists in layer '{2}'.".FormatWith(
						editor.Name, editor.ParentLayer.Name, possibleDuplicate.ParentLayer.Name ) ) ;
			}

			if (allEditors().Any(e => e == editor ) )
			{
				throw new InvalidOperationException(
					@"Cannot add editor '{0}' into layer '{1}' as the same editor has already been added.".FormatWith(
						editor.Name, editor.ParentLayer.Name ) ) ;
			}

			editor.ParentLayer.AddEditor( editor );
			
			ActiveLayer = editor.ParentLayer ;
		}

		public void ClearSelectedEditors( )
		{
			allEditors(  ).ForEach( e=>e.IsSelected=false );
		}

		public void AddEditors( IEnumerable<ItemEditor> itemEditors )
		{
			itemEditors.ForEach( AddEditor ) ;
		}

		/// <summary>
		/// Sets the editor info for this level from properties obtained
		/// from legacy version of Gleed2D.
		/// </summary>
		/// <param name="legacyEditorInfo"></param>
		public void SetLegacyEditorInfo( LegacyEditorInfo legacyEditorInfo )
		{
			var contentRootFolder = legacyEditorInfo.ContentRootFolder;

			_properties.ContentRootFolder = contentRootFolder ;

			ObjectFactory.GetInstance<IModelEventHub>().Publish(new ContentRootChanged(contentRootFolder, contentRootFolder));
			
			_properties.NextItemNumber = legacyEditorInfo.NextItemNumber ;
			_properties.Position = legacyEditorInfo.CameraPosition ;
			_properties.Version = legacyEditorInfo.Version ;
		}

		public void Update( GameTime gameTime )
		{
			Behaviours.ForEach( b => b.Update( gameTime ) ) ;
		}

		public BehaviourCollection Behaviours
		{
			get ;
			private set ;
		}
	}
}