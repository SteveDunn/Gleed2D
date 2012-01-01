using System;
using Microsoft.Xna.Framework ;

namespace Gleed2D.Core
{
	public interface IShapeProvider
	{
		Vector2[ ] Vertices
		{
			get ;
		}

		object Variables
		{
			get ;
		}

		string Name
		{
			get ;
		}
	}
}
