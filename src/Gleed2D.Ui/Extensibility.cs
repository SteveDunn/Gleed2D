using System ;
using System.Collections.Generic ;
using System.ComponentModel.Composition.Hosting ;
using System.Linq ;
using Gleed2D.Core ;
using JetBrains.Annotations ;

namespace GLEED2D
{
	[PublicAPI]
	public class Extensibility : IExtensibility
	{
		IEnumerable<IEditorPlugin> _editorPlugins ;
		IEnumerable<IBehaviourPlugin> _behaviourPlugins ;
		IEnumerable<IRenderer> _renderers ;
		IEnumerable<IShapeProvider> _shapeProviders ;
		IEnumerable<IPluginGroup> _pluginGroups ;

		public Extensibility( )
		{
			build( ) ;
		}

		public IEnumerable<IShapeProvider> ShapeProviders
		{
			get
			{
				return _shapeProviders ;
			}
		}

		public IEnumerable<IEditorPlugin> EditorPlugins
		{
			get
			{
				return _editorPlugins ;
			}
		}

		public IEnumerable<IBehaviourPlugin> BehaviourPlugins
		{
			get
			{
				return _behaviourPlugins ;
			}
		}

		public IEnumerable<IPluginGroup> PluginGroup
		{
			get
			{
				return _pluginGroups ;
			}
		}

		void build( )
		{
			var disk = new Disk( ) ;

			string thisDirectory = disk.DirectoryOfExecutingAssembly ;

			using( var catalog = new DirectoryCatalog( thisDirectory, "*.dll" ) )
			{
				var container = new CompositionContainer( catalog ) ;

				_pluginGroups = container.GetExports<IPluginGroup>( ).Select( e => e.Value ) ;
				_pluginGroups.ForEach( e => e.Initialise( ) ) ;

				_editorPlugins = from e in container.GetExports<IEditorPlugin>( ) select e.Value ;
				_behaviourPlugins = from e in container.GetExports<IBehaviourPlugin>( ) select e.Value ;
				_renderers = container.GetExports<IRenderer>( ).Select( e => e.Value ) ;
				_shapeProviders = container.GetExports<IShapeProvider>( ).Select( e => e.Value ) ;
			}
		}

		[NotNull]
		public IEnumerable<IRenderer> Renderers
		{
			get
			{
				return _renderers ;
			}
		}

		[NotNull]
		public IEditorPlugin FindPluginInstanceForType( Type type )
		{
			return _editorPlugins.Single( s => s.GetType(  ) == type ) ;
		}

		public ItemEditor GetNewEditor(Type pluginType)
		{
			IEditorPlugin plugin = FindPluginInstanceForType( pluginType ) ;

			return (ItemEditor) Activator.CreateInstance( plugin.EditorType ) ;
		}
	}
}