using Gleed2D.InGame ;

namespace Gleed2D.Core.Behaviour
{
	public interface IBehaviour : ITreeItem
	{
		void Start( ) ;
		void Stop( ) ;

		BehaviourProperties BehaviourProperties 
		{
			get ;
		}
	}
}