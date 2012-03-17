using System ;
using System.Collections.Generic ;
using System.ComponentModel ;
using System.Drawing.Design;
using System.Linq ;
using System.Linq.Expressions ;
using Gleed2D.InGame ;
using JetBrains.Annotations ;

namespace Gleed2D.Core
{
	public class ItemPropertiesWrapper< T > : IDisposable, ICustomTypeDescriptor, IDataErrorInfo where T : ItemProperties
	{
		readonly T _itemProperties ;
		readonly Dictionary<string, Func<T, ValidationError>> _validationFuncs ;
		readonly Dictionary<string, PropertyCustomisation> _customisations ;

	    readonly Dictionary<Type, Type> _editorOverrides = new Dictionary<Type, Type>();
	    TypeDescriptionProvider _typeDescriptionProvider;


	    public ItemPropertiesWrapper( T itemProperties )
		{
			_itemProperties = itemProperties ;
			_validationFuncs = new Dictionary<string, Func<T, ValidationError>>( ) ;
			_customisations = new Dictionary<string, PropertyCustomisation>( ) ;

			Customise( ( ) => itemProperties.Name ).Hide( ) ;
			Customise( ( ) => itemProperties.Id ).Hide( ) ;
			Customise( ( ) => itemProperties.Visible ).Hide( ) ;
			Customise( ( ) => itemProperties.CustomProperties ).Hide( ) ;

			Customise( ( ) => itemProperties.Position )
				.SetDisplayName( @"Position" ).
				SetDescription( @"The item's position in world space." ) ;
		}

        public void OverrideEditor<TType>(Type editorType)
        {
            _editorOverrides.Add(typeof (TType), editorType);
            _typeDescriptionProvider = TypeDescriptor.AddAttributes(typeof (TType), new EditorAttribute(editorType, typeof (UITypeEditor)));
        }

	    AttributeCollection ICustomTypeDescriptor.GetAttributes( )
		{
			return TypeDescriptor.GetAttributes( this, true ) ;
		}

		string ICustomTypeDescriptor.GetClassName( )
		{
			return TypeDescriptor.GetClassName( this, true ) ;
		}

		string ICustomTypeDescriptor.GetComponentName( )
		{
			return TypeDescriptor.GetComponentName( this, true ) ;
		}

		TypeConverter ICustomTypeDescriptor.GetConverter( )
		{
			return TypeDescriptor.GetConverter( this, true ) ;
		}

		EventDescriptor ICustomTypeDescriptor.GetDefaultEvent( )
		{
			return TypeDescriptor.GetDefaultEvent( this, true ) ;
		}

		PropertyDescriptor ICustomTypeDescriptor.GetDefaultProperty( )
		{
			return TypeDescriptor.GetDefaultProperty( this, true ) ;
		}

		object ICustomTypeDescriptor.GetEditor( Type editorBaseType )
		{
			return TypeDescriptor.GetEditor( this, editorBaseType, true ) ;
		}

		EventDescriptorCollection ICustomTypeDescriptor.GetEvents( Attribute[ ] attributes )
		{
			return TypeDescriptor.GetEvents( _itemProperties, attributes, true ) ;
		}

		EventDescriptorCollection ICustomTypeDescriptor.GetEvents( )
		{
			return TypeDescriptor.GetEvents( _itemProperties, true ) ;
		}

		PropertyDescriptorCollection ICustomTypeDescriptor.GetProperties( Attribute[ ] attributes )
		{
			var descriptors = new PropertyDescriptorCollection( null ) ;

			PropertyDescriptorCollection properties = TypeDescriptor.GetProperties( _itemProperties ) ;

			foreach (PropertyDescriptor eachProperty in properties)
			{
				descriptors.Add(
					_customisations.ContainsKey( eachProperty.Name )
						? new CustomPropertyDescriptor( eachProperty, _customisations[ eachProperty.Name ] )
						: eachProperty ) ;
			}

			var nonBrowsables = _customisations.Where( c => !c.Value.IsBrowsable ) ;
			nonBrowsables.ForEach( a => descriptors.Remove( descriptors[ a.Key ] ) ) ;

			//put Position property on top
			PropertyDescriptor positionDescriptor = descriptors[ "Position" ] ;

			descriptors.Remove( positionDescriptor ) ;

			descriptors.Insert( 0, positionDescriptor ) ;

			foreach( string key in _itemProperties.CustomProperties.Keys )
			{
				descriptors.Add( new DictionaryPropertyDescriptor( _itemProperties.CustomProperties, key, attributes ) ) ;
			}

			return descriptors ;
		}

		PropertyDescriptorCollection ICustomTypeDescriptor.GetProperties( )
		{
			return TypeDescriptor.GetProperties( _itemProperties, true ) ;
		}

		object ICustomTypeDescriptor.GetPropertyOwner( PropertyDescriptor pd )
		{
			return _itemProperties ;
		}

		[CanBeNull]
		string IDataErrorInfo.Error
		{
			get
			{
				return null ;
			}
		}

		[CanBeNull]
		string IDataErrorInfo.this[ string columnName ]
		{
			get
			{
				Func<T, ValidationError> ret ;
				if( _validationFuncs.TryGetValue( columnName, out ret ) )
				{
					var error = ret( _itemProperties ) ;

					return error != null ? error.Message : null ;
				}

				return null ;
			}
		}

		public void AddValidation< TB >( Expression<Func<TB>> expression, Func<T, ValidationError> validationMethod )
		{
			var body = (MemberExpression) expression.Body ;

			_validationFuncs.Add( body.Member.Name, validationMethod ) ;
		}

		public PropertyCustomisation Customise< TB >( Expression<Func<TB>> expression )
		{
			var body = (MemberExpression) expression.Body ;

			return Customise( body.Member.Name ) ;
		}

		public PropertyCustomisation Customise( string fieldName )
		{
			return summonCustomisation( fieldName ) ;
		}

		PropertyCustomisation summonCustomisation( string fieldName )
		{
			if( !_customisations.ContainsKey( fieldName ) )
			{
				_customisations.Add( fieldName, new PropertyCustomisation( ) ) ;
			}
			
			return _customisations[ fieldName ] ;
		}

	    public void Dispose()
	    {
	        if (_typeDescriptionProvider != null)
	        {
	            _editorOverrides.ForEach(x => TypeDescriptor.RemoveProvider(_typeDescriptionProvider, x.Key));
	        }
	    }
	}
}
