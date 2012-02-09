using System ;
using System.ComponentModel ;
using System.Reflection ;
using System.Windows.Forms;
using Gleed2D.Core ;
using Gleed2D.Core.Behaviour ;
using Gleed2D.InGame ;
using Gleed2D.InGame.Interpolation ;
using JetBrains.Annotations ;
using Microsoft.Xna.Framework ;

namespace Gleed2D.Plugins
{
	/// <summary>
	/// Represents a type of behaviour for different types of interpolation.
	/// <remarks>This type isn't mean to be used 'in game'.  Let the user create their own interpolator
	/// based on the properties written by this tool.</remarks>
	/// </summary>
	[PublicAPI]
	public sealed class FloatAnimationBehaviour : IBehaviour
	{
		readonly ItemProperties _propertiesThisAppliesTo ;
		readonly FloatAnimationBehaviourProperties _behaviourProperties ;

		Tweener _tweener ;

		PropertyInfo _propertyInfo ;

		object _originalValue ;
	
		bool _started ;

		public  FloatAnimationBehaviour(ItemProperties itemProperties, FloatAnimationBehaviourProperties behaviourProperties)
		{
			_propertiesThisAppliesTo = itemProperties ;
			_behaviourProperties = behaviourProperties ;
		}

		public void Start( )
		{
			if( _started )
			{
				return ;
			}

			_propertyInfo=_propertiesThisAppliesTo.GetType( ).GetProperty( _behaviourProperties.NameOfPropertyToModify ) ;
			
			_originalValue = _propertyInfo.GetValue( _propertiesThisAppliesTo, null ) ;

			_tweener = new Tweener(
				_behaviourProperties.From,
				_behaviourProperties.To,
				TimeSpan.FromSeconds( _behaviourProperties.DurationInSeconds ),
				Tweener.CreateTweeningFunction(
					Type.GetType( _behaviourProperties.ClrTypeOfInterpolator ), _behaviourProperties.Easing ) ) ;

			_started = true ;
		}

		public void Stop( )
		{
			if( !_started )
			{
				return ;
			}
			
			_started = false ;
			
			_propertyInfo.SetValue( _propertiesThisAppliesTo, _originalValue, null );
			
			_tweener.Reset();
		}

		public void Update( GameTime gameTime )
		{
			if (!_started)
			{
				return ;
			}
			
			_tweener.Update( gameTime );
			
			_propertyInfo.SetValue( _propertiesThisAppliesTo, 
				_tweener.Position, null );
		}

		public string Name
		{
			get
			{
				return _behaviourProperties.Name ;
			}
		}

		public ICustomTypeDescriptor ObjectForPropertyGrid
		{
			get
			{
				return new BehaviourPropertiesWrapper<BehaviourProperties>( _behaviourProperties ) ;
			}
		}

		public ItemProperties ItemProperties
		{
			get
			{
				throw new NotImplementedException( ) ;
			}
		}

		public BehaviourCollection Behaviours
		{
			get
			{
				return new BehaviourCollection();
			}
		}

		public void RenameTo( string name )
		{
			_behaviourProperties.Name = name ;
		}

		public void AddBehaviour( IBehaviour behaviour )
		{
			
		}

		public void PropertiesChanged(PropertyValueChangedEventArgs whatChanged)
		{
			
		}

		public BehaviourProperties BehaviourProperties
		{
			get
			{
				return _behaviourProperties ;
			}
		}
	}

	/// <summary>
	/// Represents a type of behaviour for different types of interpolation.
	/// <remarks>This type isn't mean to be used 'in game'.  Let the user create their own interpolator
	/// based on the properties written by this tool.</remarks>
	/// </summary>
	[UsedImplicitly]
	public class ScaleAnimationBehaviour : IBehaviour
	{
		readonly ItemProperties _propertiesThisAppliesTo ;
		readonly FloatAnimationBehaviourProperties _behaviourProperties ;

		Vector2Tweener _tweener ;

		PropertyInfo _propertyInfo ;

		object _originalValue ;
	
		bool _started ;

		public  ScaleAnimationBehaviour(ItemProperties itemProperties, FloatAnimationBehaviourProperties behaviourProperties)
		{
			_propertiesThisAppliesTo = itemProperties ;
			_behaviourProperties = behaviourProperties ;
		}

		public void Start( )
		{
			if( _started )
			{
				return ;
			}

			_propertyInfo=_propertiesThisAppliesTo.GetType( ).GetProperty( _behaviourProperties.NameOfPropertyToModify ) ;
			
			_originalValue = _propertyInfo.GetValue( _propertiesThisAppliesTo, null ) ;

			_tweener = new Vector2Tweener(
				_behaviourProperties.From.AsVector2(  ), 
				_behaviourProperties.To.AsVector2(  ),
				TimeSpan.FromSeconds( _behaviourProperties.DurationInSeconds ),
				Tweener.CreateTweeningFunction(
					Type.GetType( _behaviourProperties.ClrTypeOfInterpolator ), _behaviourProperties.Easing ) ) ;

			_started = true ;
		}

		public void Stop( )
		{
			if( !_started )
			{
				return ;
			}
			
			_started = false ;
			
			_propertyInfo.SetValue( _propertiesThisAppliesTo, _originalValue, null );
			
			_tweener.Reset();
		}

		public void Update( GameTime gameTime )
		{
			if (!_started)
			{
				return ;
			}
			
			_tweener.Update( gameTime );

			_propertyInfo.SetValue(
				_propertiesThisAppliesTo,
				_tweener.Position,
				null ) ;
		}

		public virtual string Name
		{
			get
			{
				return _behaviourProperties.Name ;
			}
		}

		public virtual ICustomTypeDescriptor ObjectForPropertyGrid
		{
			get
			{
				return new BehaviourPropertiesWrapper<BehaviourProperties>( _behaviourProperties ) ;
			}
		}

		public virtual ItemProperties ItemProperties
		{
			get
			{
				throw new NotImplementedException( ) ;
			}
		}

		public virtual BehaviourCollection Behaviours
		{
			get
			{
				return new BehaviourCollection();
			}
		}

		public virtual void RenameTo( string name )
		{
			_behaviourProperties.Name = name ;
		}

		public virtual void AddBehaviour( IBehaviour behaviour )
		{
			
		}

		public void PropertiesChanged(PropertyValueChangedEventArgs whatChanged)
		{
		}

		public virtual BehaviourProperties BehaviourProperties
		{
			get
			{
				return _behaviourProperties ;
			}
		}
	}
}