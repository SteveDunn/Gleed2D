using System.Collections.Generic ;
using System.Linq ;
using System.Xml ;
using System.Xml.Linq ;
//using System.Xml.XPath ;

namespace Gleed2D.InGame
{
	/// <summary>
	/// When XML is serialised, instead of repeating the full type name for each editor and properties, a map is built
	/// contaning an index number and the related full type name.  This is put at the start of the XML so that when reloading,
	/// the map is reconstructed first, then the full type name can be resolved from the indexes stored all over the XML.
	/// </summary>
	public static class TypeLookup
	{
		public static void Rehydrate( XElement xml )
		{
			var lookup = new Dictionary<int, string>( ) ;

			xml.CertainElement( @"TypeMap" ).Elements( @"Type" ).ForEach(
				a => lookup[ (int) a.CertainAttribute( @"Index" ) ] = (string) a.CertainAttribute( @"Name" ) ) ;

			xml.Descendants( ).Attributes( ).Where( a => a.Name.LocalName.StartsWith( @"ClrType" ) ).ForEach(
				attribute =>
					{
						var index = (int) attribute ;
						attribute.Value = lookup[ index ] ;
					} ) ;

			xml.Descendants( ).Where( a => a.Name.LocalName.StartsWith( @"ClrType" ) ).ForEach(
				element =>
					{
						var index = (int) element ;
						element.Value = lookup[ index ] ;
					} ) ;
		}

		public static void Rehydrate_old( XElement xml )
		{
			var lookup = new Dictionary<int, string>( ) ;

			xml.CertainElement( @"TypeMap" ).Elements( @"Type" ).ForEach(
				a => lookup[ (int) a.CertainAttribute( @"Index" ) ] = (string) a.CertainAttribute( @"Name" ) ) ;

			xml.Descendants(@"Editor").ForEach( 
				node => new[ ]
					{
						@"ClrTypeOfEditor", @"ClrTypeOfProperties"
					}.ForEach(
						attName =>
							{
								var b = node.CertainAttribute( attName ) ;
								var index = (int) b ;
								b.Value = lookup[ index ] ;
							} ) ) ;
		}

		public static void Compress(XElement xml)
		{
			var typeLookup = new Dictionary<string, int>( ) ;

			int found = 0 ;

			xml.Descendants( ).Attributes( ).Where( a => a.Name.LocalName.StartsWith( @"ClrType" ) ).ForEach(
				attribute =>
					{
						string typeName = attribute.Value ;
						if( !typeLookup.ContainsKey( typeName ) )
						{
							typeLookup[ typeName ] = found++ ;
						}

						attribute.Value = typeLookup[ typeName ].ToString( ) ;
					} ) ;

			xml.Descendants( ).Where( a => a.Name.LocalName.StartsWith( @"ClrType" ) ).ForEach(
				element =>
					{
						string typeName = element.Value ;
						if( !typeLookup.ContainsKey( typeName ) )
						{
							typeLookup[ typeName ] = found++ ;
						}

						element.Value = typeLookup[ typeName ].ToString( ) ;
					} ) ;

			var typeMapElement = new XElement(
				@"TypeMap",
				typeLookup.Select(
					p =>
						new XElement(
							@"Type",
							new XAttribute( "Name", p.Key ),
							new XAttribute( @"Index", p.Value ) ) ) ) ;
			xml.Add( typeMapElement ) ;
		}
	}
}