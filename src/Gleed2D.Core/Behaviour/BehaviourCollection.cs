using System ;
using System.Collections ;
using System.Collections.Generic ;
using System.Linq ;
using System.Xml.Linq ;
using Gleed2D.InGame ;
using JetBrains.Annotations ;

namespace Gleed2D.Core.Behaviour
{
	public class BehaviourCollection : IEnumerable<IBehaviour>
	{
		readonly List<IBehaviour> _behaviours ;

		public BehaviourCollection( )
		{
			_behaviours = new List<IBehaviour>( ) ;
		}

		public BehaviourCollection(ItemProperties parent,XElement xml)
		{
			_behaviours = new List<IBehaviour>( ) ;
			
			var element = xml.Element( @"Behaviours" ) ;
			
			if( element == null )
			{
				return ;
			}
			
			element.Elements( ).ForEach(
				e =>
					{
						var typeNameOfRunner = (string) e.CertainAttribute( @"ClrTypeOfRunner" ) ;
						var typeNameOfProperties = (string) e.CertainAttribute( @"ClrTypeOfProperties" ) ;
						
						Type runnerType = Type.GetType( typeNameOfRunner ) ;
						Type propertiesType = Type.GetType( typeNameOfProperties) ;

						string justTheNameOfTheType = propertiesType.Name ;
						var propertiesInstance = e.CertainElement( justTheNameOfTheType ).DeserializedAs( propertiesType ) ;
						var instance = (IBehaviour) Activator.CreateInstance( runnerType, parent, propertiesInstance ) ;

						_behaviours.Add( instance ) ;
					} ) ;
		}

		public IBehaviour this[ string name ]
		{
			get
			{
				return _behaviours.Single( b => b.BehaviourProperties.Name == name ) ;
			}
		}

		public IEnumerator<IBehaviour> GetEnumerator( )
		{
			return _behaviours.GetEnumerator( ) ;
		}

		IEnumerator IEnumerable.GetEnumerator( )
		{
			return GetEnumerator( ) ;
		}

		public void Add( IBehaviour behaviour )
		{
			_behaviours.Add( behaviour ) ;
		}

		[CanBeNull]
		public XElement ToXml( )
		{
			if( !_behaviours.Any( ) )
			{
				return null ;
			}
			
			return new XElement(
				@"Behaviours",
				_behaviours.Select(
					b =>
						new XElement(
							@"Behaviour",
							new XAttribute( @"ClrTypeOfRunner", b.GetType( ).AssemblyQualifiedName ),
							new XAttribute( @"ClrTypeOfProperties", b.BehaviourProperties.GetType( ).AssemblyQualifiedName ),
							b.BehaviourProperties.SerializeToXml( ) ) ) ) ;
		}
	}
}