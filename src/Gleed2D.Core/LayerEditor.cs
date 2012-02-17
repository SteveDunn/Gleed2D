using System;
using System.Collections ;
using System.Collections.Generic;
using System.ComponentModel;
using System.Diagnostics;
using System.Linq ;
using System.Windows.Forms;
using System.Xml.Linq ;
using Gleed2D.Core.Behaviour ;
using Gleed2D.InGame ;
using JetBrains.Annotations ;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace Gleed2D.Core
{
	[DebuggerDisplay("Name={Name}, ItemProperties={ItemProperties}, Layer={ParentLayer}, IsSelected={IsSelected}, IsHovering={IsHovering}, Position={ItemProperties.Position}")]
	public class LayerEditor : IEnumerable<ItemEditor>, ITreeItem
	{
		static readonly StringComparer _comparer = StringComparer.OrdinalIgnoreCase ;

		LayerProperties _properties ;

		BehaviourCollection _behaviours ;

		public void Update( GameTime gameTime )
		{
			_behaviours.ForEach( b => b.Update( gameTime ) ) ;
		}

		public LevelEditor Level
		{
			get { return ParentLevel; }
		}

		public LayerEditor(LevelEditor parentLevel, string name)
		{
			_properties = new LayerProperties
				{
					Name = name,
					CustomProperties = new CustomProperties( ),
					Visible = true,
					ScrollSpeed = Vector2.One
				} ;

			ParentLevel = parentLevel ;
	
			Items = new List<ItemEditor>();

			_behaviours = new BehaviourCollection( ) ;
		}

		public XElement ToXml( )
		{
			return new XElement(
				@"Layer",
				new XAttribute( @"Name", Name ),
				_properties.SerializeToXml( ),
				Behaviours.ToXml(  ),
				new XElement( @"Editors", Items.Select( i => i.ToXml( ) ) ) ) ;
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
				var wrapper = new ItemPropertiesWrapper<LayerProperties>( _properties ) ;

				wrapper.Customise( ( ) => _properties.ScrollSpeed ).SetDisplayName( @"Scroll speed" )
					.SetDescription( @"The scroll speed relative to the main camera. The X and Y components are interpreted as factors, so Vector2.One means same scrolling speed as the main camera. To be used for parallax scrolling." ) ;

				return wrapper ;
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
		/// The list of the items in this layer.
		/// </summary>
		public List<ItemEditor> Items
		{
			get;
			private set;
		}

		public void RenameTo( string name )
		{
			ItemProperties.Name = name ;
		}

		public void AddBehaviour( IBehaviour behaviour )
		{
			Behaviours.Add( behaviour );
		}

		public void PropertiesChanged(PropertyValueChangedEventArgs whatChanged)
		{
		}

		public Vector2 ScrollSpeed
		{
			get
			{
				return _properties.ScrollSpeed ;
			}
			set
			{
				_properties.ScrollSpeed = value ;
			}
		}

		[Browsable(false)]
		public LevelEditor ParentLevel
		{
			get ;
			set ;
		}

		public ItemProperties ItemProperties
		{
			get
			{
				return _properties ;
			}
		}

		public LayerEditor Clone()
		{
			var clone = FromXml( ParentLevel, ToXml( ) ) ;
			
			clone.Items = new List<ItemEditor>(Items);

			List<ItemEditor> itemEditors = clone.Items ;

			for (int i = 0; i < itemEditors.Count; i++)
			{
				itemEditors[i] = itemEditors[i].Clone();
				itemEditors[i].ParentLayer= clone ;
			}
			
			return clone;
		}

		[CanBeNull]
		public ItemEditor TryGetItemAtPosition(Vector2 worldPosition)
		{
			for (int i = Items.Count - 1; i >= 0; i--)
			{
				ItemEditor editor = Items[i] ;

				if (editor.ContainsPoint(worldPosition) && editor.ItemProperties.Visible)
				{
					return editor;
				}
			}
			
			return null;
		}

		public void DrawInEditor(SpriteBatch spriteBatch)
		{
			if (!Visible)
			{
				return;
			}
			
			foreach (ItemEditor item in Items)
			{
				item.DrawInEditor( spriteBatch );
			}
		}

		public void AddItem( ItemProperties itemProperties )
		{
			instantiateEditorForProperties( itemProperties ) ;
		}

		void instantiateEditorForProperties( ItemProperties itemProperties )
		{
			string editorName = @"{0}Editor".FormatWith( itemProperties.GetType( ).Name ) ;

			Type t = Type.GetType( editorName ) ;

			if( t == null )
			{
				throw new InvalidOperationException(
					@"Cannot instantiate an editor named '{0}' for the item properties of type '{1}'.  No such editor exists.".
						FormatWith(
							editorName, itemProperties.GetType( ) ) ) ;
			}

			Activator.CreateInstance( t, this, itemProperties ) ;
		}

		public IEnumerator<ItemEditor> GetEnumerator( )
		{
			return Items.GetEnumerator( ) ;
		}

		IEnumerator IEnumerable.GetEnumerator( )
		{
			return GetEnumerator( ) ;
		}

		public bool ContainsAnythingNamed( string name )
		{
			return Items.Any( i => _comparer.Compare(i.Name,name)==0  ) ;
		}

		public void RemoveEditor( ItemEditor editor )
		{
			Items.Remove( editor ) ;
		}

		public void AddEditor( ItemEditor editor )
		{
			Items.Add( editor );
		}

		public static LayerEditor FromXml(LevelEditor level, XElement xml)
		{
			var layerProperties = xml.CertainElement( @"LayerProperties" ).DeserializedAs<LayerProperties>( ) ;
			var layer = new LayerEditor( level, xml.CertainAttribute( @"Name" ).Value )
				{
					_properties = layerProperties,
					_behaviours = new BehaviourCollection(layerProperties,xml)
				} ;

			layer.Items =
				new List<ItemEditor>(
					xml.CertainElement( @"Editors" ).Elements( @"Editor" ).Select( x => createEditor( layer, x ) ) ) ;

			return layer ;
		}

		static ItemEditor createEditor( LayerEditor layer, XElement xml )
		{
			string typeName = xml.CertainAttribute( @"ClrTypeOfEditor" ).Value ;

			Type type = Type.GetType( typeName ) ;
			
			if( type == null )
			{
				throw new InvalidOperationException(
					@"Cannot construct an editor from the XML as the type '{0}' cannot be created.".FormatWith( typeName ) ) ;
			}

			var editor = (ItemEditor) Activator.CreateInstance( type ) ;
			
			editor.RecreateFromXml( layer, xml ) ;
			
			return editor ;
		}

		public BehaviourCollection Behaviours
		{
			get
			{
				return _behaviours ;
			}
		}
	}
}