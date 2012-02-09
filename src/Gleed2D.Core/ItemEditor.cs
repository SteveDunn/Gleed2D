using System ;
using System.ComponentModel ;
using System.Diagnostics ;
using System.Windows.Forms ;
using System.Xml.Linq ;
using Gleed2D.Core.Behaviour ;
using Gleed2D.InGame ;
using Microsoft.Xna.Framework ;
using Microsoft.Xna.Framework.Graphics ;
using StructureMap ;
using Color = Microsoft.Xna.Framework.Color ;

namespace Gleed2D.Core
{
	[DebuggerDisplay("Name={Name}, Layer={ParentLayer}, IsSelected={IsSelected}, IsHovering={IsHovering}, Position={ItemProperties.Position}, ItemProperties={ItemProperties}")]
	public abstract class ItemEditor : ITreeItem
	{
		public virtual void RecreateFromXml( LayerEditor parentLayer, XElement xml )
		{
			Behaviours= new BehaviourCollection(ItemProperties, xml);
		}

		public EventHandler<EventArgs> PreviewEndedReadyForCreation ;

		public virtual string NameSeed
		{
			get
			{
				return @"Item" ;
			}
		}

		public BehaviourCollection Behaviours
		{
			get ;
			protected set ;
		}

		protected ItemEditor( )
		{
			Behaviours=new BehaviourCollection( );
		}

		public void AddBehaviour(IBehaviour behaviour)
		{
			Behaviours.Add( behaviour ) ;
		}

		/// <summary>
		/// Called when the user starts creating something, e.g. a Rectangle will have a position but no height or width as this is
		/// set when the user moves the pointer around.
		/// Another example is the Path object.  This initially just has 1 point (that start point) and 
		/// the user add points by clicking around.
		/// </summary>
		/// <param name="parentLayer"></param>
		/// <param name="creationProperties"></param>
		public abstract void CreateInDesignMode(LayerEditor parentLayer, IEntityCreationProperties creationProperties) ;

		public LayerEditor ParentLayer
		{
			get ;
			set ;
		}

		public virtual void PropertiesChanged(PropertyValueChangedEventArgs whatChanged)
		{
			
		}

		public virtual XElement ToXml( )
		{
			return new XElement(
				@"Editor",
				new XAttribute( @"ClrTypeOfEditor", GetType( ).AssemblyQualifiedName ),
				// we need to write the properties type so that in-game component can recreate them
				new XAttribute( @"ClrTypeOfProperties", ItemProperties.GetType( ).AssemblyQualifiedName ),
				new XAttribute( @"Name", Name ),
				ItemProperties.SerializeToXml( ),
				Behaviours.ToXml( )
				) ;
		}

		/// <summary>
		/// Gets the object that represents the properties in the property grid.
		/// This is usually the <see cref="ItemPropertiesWrapper{T}"/> type but you 
		/// can use your own by overriding this.
		/// </summary>
		public virtual ICustomTypeDescriptor ObjectForPropertyGrid
		{
			get
			{
				var wrapper = new ItemPropertiesWrapper<ItemProperties>( ItemProperties ) ;

				return wrapper ;
			}
		}

		bool _isSelected ;

		public bool IsSelected
		{
			get
			{
				return _isSelected ;
			}
			set
			{
				_isSelected = value ;
				TimeSelected = DateTime.UtcNow ;
			}
		}

		public DateTime TimeSelected
		{
			get;
			private set ;
		}

		public virtual void RenameTo( string name )
		{
			ItemProperties.Name = name ;
		}

		public abstract ImageProperties Icon
		{
			get;
		}

		public abstract ItemProperties ItemProperties
		{
			get;
		}

		public abstract ItemEditor Clone( ) ;

		public abstract bool ContainsPoint( Vector2 point ) ;

		protected virtual void WhenUpdatedByUi( )
		{
			
		}

		public virtual void OnMouseOver( Vector2 mouseWorldPos )
		{
			IsHovering = true;
		}

		public virtual void OnMouseOut()
		{
			IsHovering = false;

			var mainForm = ObjectFactory.GetInstance<IMainForm>( ) ;

			mainForm.SetCursorForCanvas(Cursors.Default) ;
		}

		public virtual bool CanRotate()
		{
			return true;
		}

		public virtual float Rotation
		{
			get ;
			set ;
		}

		public virtual bool CanScale
		{
			get;
			set ;
		}
		
		public virtual Vector2 Scale
		{
			get
			{
				return Vector2.One ;
			}
			set
			{
				throw new InvalidOperationException( @"Cannot scale a base item. The derived item should have overridden this property." ) ;
			}
		}

		public virtual void OnMouseButtonDown( Vector2 mouseWorldPos )
		{
		}

		public virtual void OnMouseButtonUp( Vector2 mouseWorldPos )
		{
		}

		public abstract void DrawInEditor( SpriteBatch spriteBatch ) ;

		public abstract void DrawSelectionFrame( SpriteBatch spriteBatch, Color color ) ;

		public virtual void SetPosition( Vector2 position )
		{
			ItemProperties.Position = position ;
			
			WhenUpdatedByUi(  );
		}

		protected bool IsHovering
		{
			get ;
			set ;
		}

		public virtual void Update( GameTime gameTime )
		{
			Behaviours.ForEach( b=>b.Update( gameTime ) );
			//WhenUpdatedByUi(  );
		}

		public abstract string Name
		{
			get ;
		}


		public abstract void UserInteractionDuringCreation( ) ;

		public void ToggleSelection( )
		{
			IsSelected = !IsSelected ;
		}

		public virtual void HandleKeyPressWhenFocused(  )
		{
			
		}

		public virtual void WhenChosenFromToolbox( )
		{
			
		}
	}
}