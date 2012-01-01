using System.Collections.Generic ;
using System.ComponentModel ;
using System.ComponentModel.Composition ;
using System.Linq ;
using FarseerPhysics.Common ;
using FarseerPhysics.Common.Decomposition ;
using FarseerPhysics.Common.PolygonManipulation ;
using Gleed2D.Core ;
using Gleed2D.InGame ;
using Microsoft.Xna.Framework ;
using Microsoft.Xna.Framework.Graphics ;
using StructureMap ;
using Vertices = Gleed2D.Core.Vertices ;

namespace Gleed2D.Plugins
{
	[Export(typeof(IShapeProvider))]
	public class ShapeFromTextureProvider : IShapeProvider
	{
		class ShapeFromTextureProperties
		{
			internal ShapeFromTextureProperties( )
			{
				PathToFile = new PathToFile
					{
						AbsolutePath = @"c:\"
					} ;

				AlphaTolerance = 20 ;
				HullTolerance = 1.5f ;
			}

			public PathToFile PathToFile
			{
				get;
				set ;
			}

			[DisplayName("Hull Tolerance"), Category(" General"), Description("This argument controls the amount of details found in the detection")]
			public float HullTolerance
			{
				get;
				set ;
			}

			[DisplayName("Alpha Tolerance"), Category(" General"), Description("The alpha tolerance")]
			public byte AlphaTolerance
			{
				get;
				set ;
			}

			//todo: confirm what both of these do.
			[DisplayName("Multipart detection"), Category(" General"), Description("Detect multiple parts...Probly...")]
			public bool MultiPartDetection
			{
				get;
				set ;
			}

			[DisplayName("Hole detection"), Category(" General"), Description("Detect holes...Probly...")]
			public bool HoleDetection
			{
				get;
				set ;
			}
		}

		readonly ShapeFromTextureProperties _properties = new ShapeFromTextureProperties(  );
		
		public Vector2[ ] Vertices2
		{
			get
			{
				if (!_properties.PathToFile.Exists)
				{
					return new Vector2[ ]
						{
							new Vector2(0,0),  new Vector2(100,0),  new Vector2(100,100), new Vector2(0,100), 
						} ;
				}
				
				var textureStore = ObjectFactory.GetInstance<ITextureStore>( ) ;
				var game = ObjectFactory.GetInstance<IGame>( ) ;
				var texture = textureStore.FromFile( game.GraphicsDevice, _properties.PathToFile.AbsolutePath ) ;
			var colors1D = new uint[texture.Width * texture.Height];
			texture.GetData(colors1D);

				List<Vertices> vertices = Core.Vertices.CreatePolygon(
					colors1D, 
					texture.Width, 
					texture.Height,
					_properties.HullTolerance,
					_properties.AlphaTolerance,
					_properties.MultiPartDetection,
					_properties.HoleDetection) ;

				Vector2[] all = vertices.SelectMany(v=>v.ToArray(  )).ToArray(  ) ;
				
				return all ;
			}
		}
		
		public Vector2[ ] Vertices
		{
			get
			{
				if (!_properties.PathToFile.Exists)
				{
					return new Vector2[ ]
						{
							new Vector2(0,0),  new Vector2(100,0),  new Vector2(100,100), new Vector2(0,100), 
						} ;
				}
				
				var textureStore = ObjectFactory.GetInstance<ITextureStore>( ) ;
				var game = ObjectFactory.GetInstance<IGame>( ) ;
				var texture = textureStore.FromFile( game.GraphicsDevice, _properties.PathToFile.AbsolutePath ) ;
			var data = new uint[texture.Width * texture.Height];
			texture.GetData(data);

				List<FarseerPhysics.Common.Vertices> textureVertices = PolygonTools.CreatePolygon( data, texture.Width, 
				_properties.HullTolerance, _properties.AlphaTolerance, _properties.MultiPartDetection, _properties.HoleDetection);


            //We simplify the vertices found in the texture.
				//textureVertices.ForEach( tv=>tv=SimplifyTools.ReduceByDistance( tv,4 ) );
            //textureVertices = SimplifyTools.ReduceByDistance(textureVertices, 4f);

            //Since it is a concave polygon, we need to partition it into several smaller convex polygons
				//List<FarseerPhysics.Common.Vertices> list =
				//    textureVertices.SelectMany( a => BayazitDecomposer.ConvexPartition( a ) ).ToList(  ) ;

				
				//BayazitDecomposer.ConvexPartition(textureVertices);


				Vector2[] all = textureVertices.SelectMany(v=>v.ToArray(  )).ToArray(  ) ;
				
				return all ;
			}
		}
		public Vector2[ ] Vertices3
		{
			get
			{
				if (!_properties.PathToFile.Exists)
				{
					return new Vector2[ ]
						{
							new Vector2(0,0),  new Vector2(100,0),  new Vector2(100,100), new Vector2(0,100), 
						} ;
				}
				
				var textureStore = ObjectFactory.GetInstance<ITextureStore>( ) ;
				var game = ObjectFactory.GetInstance<IGame>( ) ;
				var texture = textureStore.FromFile( game.GraphicsDevice, _properties.PathToFile.AbsolutePath ) ;
			var data = new uint[texture.Width * texture.Height];
			texture.GetData(data);

				FarseerPhysics.Common.Vertices textureVertices = PolygonTools.CreatePolygon( data, texture.Width, true ) ;
				//_properties.HullTolerance, _properties.AlphaTolerance, _properties.MultiPartDetection, _properties.HoleDetection);

            //1. To translate the vertices so the polygon is centered around the centroid.
            Vector2 centroid = -textureVertices.GetCentroid();
            textureVertices.Translate(ref centroid);

            //We simplify the vertices found in the texture.
            textureVertices = SimplifyTools.ReduceByDistance(textureVertices, 4f);

            //Since it is a concave polygon, we need to partition it into several smaller convex polygons
            List<FarseerPhysics.Common.Vertices> list = BayazitDecomposer.ConvexPartition(textureVertices);


				Vector2[] all = list.SelectMany(v=>v.ToArray(  )).ToArray(  ) ;
				
				return all ;
			}
		}

		static Color[,] textureTo2DArray(Texture2D texture)
		{
			var colors1D = new Color[texture.Width * texture.Height];
			texture.GetData(colors1D);

			var colors2D = new Color[texture.Width, texture.Height];
			for (int x = 0; x < texture.Width; x++)
			{
				for (int y = 0; y < texture.Height; y++)
				{
					colors2D[x, y] = colors1D[x + y * texture.Width];
				}
				
			}
			
			return colors2D;
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
				return @"From texture" ;
			}
		}
	}
}