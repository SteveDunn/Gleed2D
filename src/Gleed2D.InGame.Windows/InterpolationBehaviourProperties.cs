using System ;
using Gleed2D.InGame.Interpolation ;

namespace Gleed2D.InGame
{
	public class FloatAnimationBehaviourProperties : BehaviourProperties
	{
		public string NameOfPropertyToModify
		{
			get;
			set ;
		}

		public float From
		{
			get ;
			set ;
		}

		public float To
		{
			get ;
			set ;
		}

		public string ClrTypeOfInterpolator
		{
			get ;
			set ;
		}

		public Easing Easing
		{
			get ;
			set ;
		}

		public float DurationInSeconds
		{
			get ;
			set ;
		}

		
	}
}