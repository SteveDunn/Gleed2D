using System ;
using System.ComponentModel ;
using Gleed2D.Core.CustomUITypeEditors ;
using Gleed2D.InGame ;
using JetBrains.Annotations ;
using Microsoft.Xna.Framework ;

namespace Gleed2D.Core
{
	public class DictionaryPropertyDescriptor : PropertyDescriptor
	{
		readonly string _key;
		readonly CustomProperties _customProperties;

		public DictionaryPropertyDescriptor(CustomProperties customProperties, string key, Attribute[] attrs)
			: base(key, attrs)
		{
			_key = key;
			_customProperties = customProperties;
		}

		public override bool CanResetValue(object component)
		{
			return false;
		}

		[CanBeNull]
		public override Type ComponentType
		{
			get { return null; }
		}

		public override object GetValue(object component)
		{
			return _customProperties[_key].Value;
		}

		public override string Description
		{
			get { return _customProperties[_key].Description; }
		}

		public override string Category
		{
			get { return "Custom Properties"; }
		}

		public override string DisplayName
		{
			get { return _key; }
		}

		public override bool IsReadOnly
		{
			get { return false; }
		}

		public override void ResetValue(object component)
		{
			//todo: Have to implement
		}

		public override bool ShouldSerializeValue(object component)
		{
			return false;
		}

		public override void SetValue(object component, object value)
		{
			_customProperties[_key].Value = value;
		}

		public override Type PropertyType
		{
			get { return _customProperties[_key].Type; }
		}

		public override object GetEditor(Type editorBaseType)
		{
			CustomProperty customProperty = _customProperties[_key] ;

			if (customProperty.Type == typeof(Vector2))
			{
				return new Vector2UiTypeEditor();
			}
			
			if (customProperty.Type == typeof(Color))
			{
				return  new XnaColorUiTypeEditor();
			}
			
			if (customProperty.Type == typeof(LinkedItem))
			{
				return new LinkedItemUiTypeEditor();
			}
			
			return base.GetEditor(editorBaseType);
		}

		public void Remove( string name )
		{
			_customProperties.Remove( name ) ;
		}
	}
}