using System ;
using System.Windows.Forms ;

namespace Gleed2D.Core
{
	public interface IBehaviourPlugin : IPlugin
	{
		Control ControlForAboutBox
		{
			get ;
		}

		void InitialiseInUi( IMainForm mainForm ) ;
	}
}