using System.ComponentModel ;
using System.Windows.Forms;
using Gleed2D.Core.Behaviour ;
using Gleed2D.InGame ;
using Microsoft.Xna.Framework ;

namespace Gleed2D.Core
{
	public interface ITreeItem
	{
		void Update( GameTime gameTime ) ;

		string Name
		{
			get ;
		}

		ICustomTypeDescriptor ObjectForPropertyGrid
		{
			get ;
		}

		ItemProperties ItemProperties
		{
			get;
		}

		BehaviourCollection Behaviours
		{
			get ;
		}

		void RenameTo( string name ) ;
		void AddBehaviour(IBehaviour behaviour) ;
		void PropertiesChanged(PropertyValueChangedEventArgs whatChanged);
	}
}