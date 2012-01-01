using System ;
using System.Collections.Generic ;
using System.Globalization ;
using System.Xml ;
using System.Xml.Serialization ;
using Microsoft.Xna.Framework ;

namespace Gleed2D.InGame
{
	public class CustomProperties : Dictionary<string, CustomProperty>, IXmlSerializable
	{
		public CustomProperties()	: base(StringComparer.InvariantCultureIgnoreCase)
		{
		}

		public CustomProperties(CustomProperties copy)
			: base(copy, StringComparer.InvariantCultureIgnoreCase)
		{
			var keyscopy = new string[Keys.Count];
            
			Keys.CopyTo(keyscopy, 0);
            
			foreach (string key in keyscopy)
			{
				this[key] = this[key].Clone();
			}
		}

		public System.Xml.Schema.XmlSchema GetSchema()
		{
			return null;
		}

		public void ReadXml(XmlReader reader)
		{
			bool wasEmpty = reader.IsEmptyElement;
            
			reader.Read();

			if (wasEmpty)
			{
				return;
			}

			while (reader.NodeType != XmlNodeType.EndElement)
			{
				var property = new CustomProperty
					{
						Name = reader.GetAttribute( "Name" ),
						Description = reader.GetAttribute( "Description" )
					} ;

				string type = reader.GetAttribute("Type");
               
				if (type == "string")
				{
					property.Type = typeof(string);
				}
				
				if (type == "bool")
				{
					property.Type = typeof(bool);
				}
				
				if (type == "Vector2")
				{
					property.Type = typeof(Vector2);
				}
			
				if (type == "Color")
				{
					property.Type = typeof(Color);
				}
				
				if (type == "LinkedItem")
				{
					property.Type = typeof(LinkedItem);
				}

				if (property.Type == typeof(LinkedItem))
				{
					property.Value = reader.ReadInnerXml();
					Add(property.Name, property);
				}
				else
				{
					reader.ReadStartElement("Property");
					
					var valueSerializer = new XmlSerializer(property.Type);
					object obj = valueSerializer.Deserialize(reader);
					property.Value = Convert.ChangeType(obj, property.Type, CultureInfo.InvariantCulture);
					Add(property.Name, property);
					
					reader.ReadEndElement();
				}

				reader.MoveToContent();
			}
			
			reader.ReadEndElement();
		}

		public void WriteXml(XmlWriter writer)
		{
			foreach (string key in Keys)
			{
				writer.WriteStartElement("Property");
				writer.WriteAttributeString("Name", this[key].Name);
				
				if (this[key].Type == typeof(string))
				{
					writer.WriteAttributeString("Type", "string");
				}
				if (this[key].Type == typeof(bool))
				{
					writer.WriteAttributeString("Type", "bool");
				}
				if (this[key].Type == typeof(Vector2))
				{
					writer.WriteAttributeString("Type", "Vector2");
				}
				if (this[key].Type == typeof(Color))
				{
					writer.WriteAttributeString("Type", "Color");
				}
				if (this[key].Type == typeof(LinkedItem))
				{
					writer.WriteAttributeString("Type", "LinkedItem");
				}
             
				writer.WriteAttributeString("Description", this[key].Description);

				if (this[key].Type == typeof(LinkedItem))
				{
					var item = this[key].Value as LinkedItem;
					
					if (item != null)
					{
						writer.WriteString(item.Name);
					}
					else
					{
						writer.WriteString("$null$");
					}
				}
				else
				{
					var valueSerializer = new XmlSerializer(this[key].Type);
					valueSerializer.Serialize(writer, this[key].Value);
				}
				
				writer.WriteEndElement();
			}
		}

		/// <summary>
		/// Must be called after all Items have been deserialized. 
		/// Restores the Item references in CustomProperties of type Item.
		/// </summary>
		//public void RestoreItemAssociations(Level level)
		//{
		//    foreach (CustomProperty eachCustomProperty in Values)
		//    {
		//        if (eachCustomProperty.Type == typeof(ItemEditor))
		//        {
		//            eachCustomProperty.Value = level.GetItemByName((string)eachCustomProperty.Value);
		//        }
		//    }
		//}
	}
}