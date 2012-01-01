using System.Collections.Generic ;
using System.Linq ;
using System.Xml.Linq ;

namespace Gleed2D.InGame
{
// ReSharper disable UnusedMember.Global
	public class Layer
	{
		readonly LayerProperties _properties ;
		readonly IEnumerable<LayerItem> _items ;

		public Layer( XElement xml )
		{
			_properties = xml.Element( @"LayerProperties" ).DeserializedAs<LayerProperties>(  ) ;

			_items = (from x in xml.CertainElement( @"Editors" ).Elements(@"Editor" )
			             select new LayerItem( x )).ToList(  ) ;
		}

		public LayerProperties Properties
		{
			get
			{
				return _properties ;
			}
		}

		public IEnumerable<LayerItem> Items
		{
			get
			{
				return _items ;
			}
		}
	}
// ReSharper restore UnusedMember.Global
}