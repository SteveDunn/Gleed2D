using System.ComponentModel.Composition ;
using Gleed2D.Core ;
using Microsoft.Xna.Framework ;

namespace Gleed2D.Plugins
{
	[Export(typeof(IShapeProvider))]
	public class GearShapeProvider : IShapeProvider
	{
		class GearShapeProperties
		{
			internal GearShapeProperties( )
			{
				Radius = 100 ;
				NumberOfTeeth = 5 ;
				TipPercentage = 5 ;
				ToothHeight = 20 ;
			}

			public float Radius
			{
				get;
				set ;
			}

			public float TipPercentage
			{
				get;
				set ;
			}

			public float ToothHeight
			{
				get;
				set ;
			}

			public int NumberOfTeeth
			{
				get;
				set ;
			}
		}

		readonly GearShapeProperties _properties = new GearShapeProperties(  );
		
		public Vector2[ ] Vertices
		{
			get
			{
				Vertices vertices = Core.Vertices.CreateGear(
					_properties.Radius,
					_properties.NumberOfTeeth,
					_properties.TipPercentage,
					_properties.ToothHeight ) ; 
				
				return vertices.ToArray(  ) ;
			}
		}

		public object Variables
		{
			get
			{
				return _properties ;
			}
		}

		public string Name
		{
			get
			{
				return @"Gear" ;
			}
		}
	}
}