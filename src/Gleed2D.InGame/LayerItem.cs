using System ;
using System.Xml.Linq ;

namespace Gleed2D.InGame
{
// ReSharper disable UnusedMember.Global
	public class LayerItem
	{
		readonly ItemProperties _properties ;

		public LayerItem( XElement xml )
		{
			string propertiesTypeAsText = xml.CertainAttribute( @"ClrTypeOfProperties" ).Value ;
			Type typeOfProperties = Type.GetType( propertiesTypeAsText ) ;
			string justTheName = typeOfProperties.Name ;

			_properties = (ItemProperties)xml.Element( justTheName ).DeserializedAs( typeOfProperties ) ;
		}

		public ItemProperties Properties
		{
			get
			{
				return _properties ;
			}
		}
	}
// ReSharper restore UnusedMember.Global
}