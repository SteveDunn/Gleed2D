using System ;
using System.Collections.Generic ;
using System.ComponentModel ;
using JetBrains.Annotations ;

namespace Gleed2D.Core.Controls
{
	public sealed class SimpleSite : ISite
	{
		private Dictionary<Type, object> _services ;
		private readonly IContainer container = new Container( ) ;

		public IComponent Component
		{
			get ;
			set ;
		}

		public IContainer Container
		{
			get
			{
				return container ;
			}
		}

		public bool DesignMode
		{
			get ;
			set ;
		}

		public string Name
		{
			get ;
			set ;
		}

		public void AddService< T >( T service ) where T : class
		{
			if( _services == null )
			{
				_services = new Dictionary<Type, object>( ) ;
			}
		
			_services[ typeof( T ) ] = service ;
		}

		public void RemoveService< T >( ) where T : class
		{
			if( _services != null )
			{
				_services.Remove( typeof( T ) ) ;
			}
		}

		[CanBeNull]
		public object GetService( Type serviceType )
		{
			object service ;
		
			if( _services != null && _services.TryGetValue( serviceType, out service ) )
			{
				return service ;
			}
		
			return null ;
		}
	}
}