using System.Collections.Generic ;
using System.Linq ;
using System.Xml.Linq ;

namespace Gleed2D.InGame
{
// ReSharper disable UnusedMember.Global
	public class Level 
	{
		readonly LevelProperties _properties ;
		readonly IEnumerable<Layer> _layers ;

		public Level( XElement xml )
		{
			_properties = xml.Element( @"LevelProperties" ).DeserializedAs<LevelProperties>( ) ;

			_layers = (from x in xml.CertainElement( @"Layers" ).Elements(@"Layer")
			           select new Layer( x )).ToList(  );
		}

		public LevelProperties Properties
		{
			get
			{
				return _properties ;
			}
		}

		public IEnumerable<Layer> Layers
		{
			get
			{
				return _layers ;
			}
		}
	}
// ReSharper restore UnusedMember.Global
}