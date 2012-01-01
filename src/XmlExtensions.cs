using System;
using System.IO ;
using System.Xml ;
using System.Xml.Linq ;
using System.Xml.Serialization ;

static class XmlExtensions
{
	static readonly XmlSerializerNamespaces _emptyNamespaces = new XmlSerializerNamespaces();

	static XmlExtensions( )
	{
		_emptyNamespaces.Add( string.Empty, string.Empty );
	}

	public static XElement SerializeToXml(this object itemProperties)
	{
		var s = new XmlSerializer( itemProperties.GetType( ), string.Empty ) ;

		using( var memoryStream = new MemoryStream( ) )
		{
			s.Serialize( memoryStream, itemProperties, _emptyNamespaces ) ;
			memoryStream.Position = 0 ;

			var settings = new XmlReaderSettings
				{
					IgnoreWhitespace = true
				} ;
			
			XmlReader xmlReader = XmlReader.Create( memoryStream, settings ) ;
	
			xmlReader.MoveToContent( ) ;

			return (XElement) XElement.ReadFrom( xmlReader ) ;
		}
	}
}
