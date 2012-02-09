using System ;
using System.IO ;
using System.Linq ;
using System.Xml.Linq ;
using Gleed2D.Core.Legacy ;
using Gleed2D.InGame ;
using Microsoft.Xna.Framework ;
using StructureMap ;
using LegacyLevel = Gleed2D.Core.Legacy.Level;

namespace Gleed2D.Core
{
	public class ConvertOldFileFormats
	{
		readonly string _path ;

		ConvertOldFileFormats( string path )
		{
			LegacyLevel oldLevel=LegacyLevel.FromFile( path ) ;

			XElement d = XElement.Load( path ) ;

			var level = new LevelEditor
				{
					Name = oldLevel.Name,
				} ;

			populateEditorRelatedStuff( level, d ) ;

			convertCustomProperties( oldLevel.CustomProperties, level.CustomProperties ) ;

			level.Layers.AddRange(
				oldLevel.Layers.Select( copyLayer ) ) ;

			_path = Path.GetTempFileName(  ) ;
			
			level.SaveAsXmlToDisk( _path );
		}

		void populateEditorRelatedStuff( LevelEditor level, XElement xml )
		{
			var assemblyInformation = ObjectFactory.GetInstance<IGetAssemblyInformation>( ) ;
			
			XElement ourElement = xml.CertainElement( @"EditorRelated" ) ;

			XElement cameraPositionElement = ourElement.CertainElement( @"CameraPosition" ) ;
			
			var legacyEditorInfo = new LegacyEditorInfo
				{
					NextItemNumber = (int) ourElement.CertainElement( @"NextItemNumber" ),
					ContentRootFolder = (string) ourElement.CertainElement( @"ContentRootFolder" ),
					CameraPosition = new Vector2(
						(float) cameraPositionElement.CertainElement( @"X" ),
						(float) cameraPositionElement.CertainElement( @"Y" ) ),
					Version = assemblyInformation.Version
				} ;

			level.SetLegacyEditorInfo( legacyEditorInfo ) ;
		}

		LayerEditor copyLayer( Legacy.Layer oldLayer )
		{
			var newLayer = new LayerEditor( null, oldLayer.Name )
				{
					Visible = oldLayer.Visible,
					ScrollSpeed = oldLayer.ScrollSpeed
				} ;

			convertCustomProperties( oldLayer.CustomProperties, newLayer.ItemProperties.CustomProperties) ;

			newLayer.Items.AddRange( oldLayer.Items.Select( convertItem ) ) ;

			newLayer.Items.ForEach( i => i.ParentLayer = newLayer ) ;

			return newLayer ;
		}

		ItemEditor convertItem( Item oldItem )
		{
			if( oldItem is TextureItem )
			{
				return convertTextureItem( oldItem as TextureItem ) ;
			}

			if( oldItem is RectangleItem )
			{
				return convertRectangleItem( oldItem as RectangleItem ) ;
			}

			if( oldItem is CircleItem )
			{
				return convertCircleItem( oldItem as CircleItem ) ;
			}

			if( oldItem is PathItem )
			{
				return convertPathItem( oldItem as PathItem ) ;
			}

			throw new NotSupportedException(
				@"Cannot convert a legacy item of type '{0}' as it is not a supported (or recognised) type.".FormatWith(
					oldItem.GetType( ) ) ) ;
		}

		ItemEditor convertTextureItem(TextureItem old)
		{
			var editor =
				(ItemEditor) Activator.CreateInstance( getType( @"Gleed2D.Plugins.TextureItemEditor, Gleed2D.Plugins" ) ) ;
			
			var @new = (TextureItemProperties) editor.ItemProperties ;

			copyPropertiesFromOldToNew( old, @new );

			@new.Origin = old.Origin;
			@new.Rotation = old.Rotation ;
			@new.Scale = old.Scale ;
			@new.FlipHorizontally = old.FlipHorizontally ;
			@new.FlipVertically = old.FlipVertically ;
			@new.TintColor = old.TintColor ;
			@new.IsTemplate = old.isTemplate ;
			@new.TexturePathRelativeToContentRoot = old.texture_filename ;

			return editor ;
		}

		static Type getType( string typeName )
		{
			Type type = Type.GetType( typeName ) ;
	
			if( type == null )
			{
				throw new InvalidOperationException( @"Cannot instantiate a type named '{0}'.".FormatWith( typeName ) ) ;
			}

			return type ;
		}

		ItemEditor convertRectangleItem( RectangleItem old )
		{
			var editor =
				(ItemEditor) Activator.CreateInstance( getType( @"Gleed2D.Plugins.RectangleItemEditor, Gleed2D.Plugins" ) ) ;

			var @new = (RectangleItemProperties) editor.ItemProperties ;
			copyPropertiesFromOldToNew( old, @new );

			@new.Width = old.Width ;
			@new.Height = old.Height ;
			@new.FillColor = old.FillColor ;

			return editor ;
		}

		ItemEditor convertCircleItem( CircleItem old )
		{
			var editor =
				(ItemEditor) Activator.CreateInstance( getType( @"Gleed2D.Plugins.CircleItemEditor, Gleed2D.Plugins" ) ) ;

			var @new = (CircleItemProperties) editor.ItemProperties ;
			copyPropertiesFromOldToNew( old, @new );

			@new.Radius = old.Radius ;
			@new.FillColor = old.FillColor ;

			return editor ;
		}

		ItemEditor convertPathItem( PathItem old )
		{
			var editor =
				(ItemEditor) Activator.CreateInstance( getType( @"Gleed2D.Plugins.PathItemEditor, Gleed2D.Plugins" ) ) ;

			var @new = (PathItemProperties) editor.ItemProperties ;
			copyPropertiesFromOldToNew( old,@new );

			@new.LocalPoints = old.LocalPoints.ToList( ) ;
			@new.WorldPoints = old.WorldPoints.ToList( ) ;
			@new.IsPolygon = old.IsPolygon ;
			@new.LineWidth = old.LineWidth ;
			@new.LineColor = old.LineColor ;

			return editor ;
		}

		void copyPropertiesFromOldToNew(Item old, ItemProperties @new)
		{
			convertCustomProperties( old.CustomProperties, @new.CustomProperties );
			@new.Id = old.Id ;
			@new.Name = old.Name ;
			@new.Position = old.Position ;
			@new.Visible = old.Visible ;
		}

		void convertCustomProperties( SerializableDictionary old, InGame.CustomProperties @new )
		{
			old.ForEach(
				pair =>
					@new.Add(
						pair.Key, new InGame.CustomProperty( pair.Value.name, pair.Value.value, pair.Value.type, pair.Value.description ) ) ) ;
		}

		/// <summary>
		/// Converts an old Gleed file (1.4) to the new Gleed format and returns the 
		/// path to the new file.
		/// </summary>
		/// <param name="path"></param>
		/// <returns></returns>
		public static string Convert( string path )
		{
			return new ConvertOldFileFormats( path )._path ;
		}
	}
}