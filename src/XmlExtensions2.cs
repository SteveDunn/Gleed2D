using System;
using System.Globalization ;
using System.Xml ;
using System.Xml.Linq ;
using System.Xml.Serialization ;

static class XmlExtensions2
{
	public static T DeserializedAs<T>(this XElement xml)
	{
		return (T)DeserializedAs( xml, typeof( T ) ) ;
	}

	public static object DeserializedAs(this XElement xml, Type T) 
	{
		var serializer = new XmlSerializer( T ) ;

		XmlReader xmlReader = xml.CreateReader( ) ;

		return serializer.Deserialize( xmlReader ) ;
	}

	public static XAttribute CertainAttribute( this XElement element, string name )
	{
		var attribute = element.Attribute( name ) ;

		if( attribute == null )
		{
			throw new InvalidOperationException(
				string.Format(
					CultureInfo.InvariantCulture,
					@"Cannot get the attribute named '{0}' from element '{1}' as it does not exist.",
					name,
					element.Name ) ) ;
		}

		return attribute ;
	}

	public static XElement CertainElement( this XElement element, string name )
	{
		var childElement = element.Element( name ) ;

		if( childElement == null )
		{
			throw new InvalidOperationException(
				string.Format(
					CultureInfo.InvariantCulture,
					@"Cannot get the element named '{0}' from element '{1}' as it does not exist.",
					name,
					element.Name ) ) ;
		}

		return childElement ;
	}
}
