using System ;
using System.ComponentModel ;

namespace Gleed2D.Core
{
	public class CustomPropertyDescriptor : PropertyDescriptor
	{
		readonly PropertyDescriptor _basePropertyDescriptor;
		readonly PropertyCustomisation _customisation ;

		public CustomPropertyDescriptor(
			PropertyDescriptor propertyDescriptor, 
			PropertyCustomisation customisation) : base(propertyDescriptor)
		{
			_basePropertyDescriptor=propertyDescriptor;
			_customisation = customisation ;
		}

		public override void AddValueChanged(object component, EventHandler handler)
		{
			_basePropertyDescriptor.AddValueChanged (component, handler);
		}

		public override AttributeCollection Attributes
		{
			get
			{
				return _basePropertyDescriptor.Attributes;
			}
		}
	 
		public override bool CanResetValue(object component)
		{
			return _basePropertyDescriptor.CanResetValue(component);
		}
	 
		public override string Category
		{
			get
			{
				if (!string.IsNullOrEmpty(_customisation.Category))
				{
					return _customisation.Category ;
				}
				
				return _basePropertyDescriptor.Category;
			}
		}
	 
		public override Type ComponentType
		{
			get
			{
				return _basePropertyDescriptor.ComponentType;
			}
		}
	 
		public override TypeConverter Converter
		{
			get
			{
				return _basePropertyDescriptor.Converter;
			}
		}

		public override string Description
		{
			get
			{
				if( !string.IsNullOrEmpty( _customisation.Description ) )
				{
					return _customisation.Description ;
				}

				return _basePropertyDescriptor.Description ;
			}
		}

		public override bool DesignTimeOnly
		{
			get
			{
				return _basePropertyDescriptor.DesignTimeOnly;
			}
		}

		//This method is overridden to take notice of the FriendlyNameAttribute
		//if it has been applied to the property 
		public override string DisplayName
		{
			get
			{
				if (!string.IsNullOrEmpty(_customisation.DisplayName))
				{
					return _customisation.DisplayName ;
				}
				
				return _basePropertyDescriptor.DisplayName;
			}
		}
	 
		public override bool Equals(object obj)
		{
			return _basePropertyDescriptor.Equals (obj);
		}
	 
		public override PropertyDescriptorCollection GetChildProperties(object instance, Attribute[] filter)
		{
			return _basePropertyDescriptor.GetChildProperties (instance, filter);
		}
	 
		public override object GetEditor(Type editorBaseType)
		{
			return _basePropertyDescriptor.GetEditor (editorBaseType);
		}
	 
		public override int GetHashCode()
		{
			return _basePropertyDescriptor.GetHashCode ();
		}
	 
		public override object GetValue(object component)
		{
			return _basePropertyDescriptor.GetValue(component);
		}
	 
		public override bool IsBrowsable
		{
			get
			{
				return _customisation.IsBrowsable;
			}
		}
	 
		public override bool IsLocalizable
		{
			get
			{
				return _basePropertyDescriptor.IsLocalizable;
			}
		}
	 
		public override bool IsReadOnly
		{
			get
			{
				return _customisation.IsReadOnly;
			}
		}
	 
		public override string Name
		{
			get
			{
				return _basePropertyDescriptor.Name;
			}
		}
	 
		public override Type PropertyType
		{
			get
			{
				return _basePropertyDescriptor.PropertyType;
			}
		}
	 
		public override void RemoveValueChanged(object component, EventHandler handler)
		{
			_basePropertyDescriptor.RemoveValueChanged (component, handler);
		}
	 
		public override void ResetValue(object component)
		{
			_basePropertyDescriptor.ResetValue(component);      
		}
	 
		public override void SetValue(object component, object value)
		{
			_basePropertyDescriptor.SetValue(component, value);
		}
	 
		public override bool ShouldSerializeValue(object component)
		{
			return _basePropertyDescriptor.ShouldSerializeValue(component);
		}
	 
		public override string ToString()
		{
			return _basePropertyDescriptor.ToString ();
		}
	}
}