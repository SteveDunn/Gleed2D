using System ;
using System.ComponentModel ;
using System.IO ;
using System.Windows.Forms;
using System.Xml.Serialization ;
using Gleed2D.Core.CustomUITypeEditors ;
using Gleed2D.InGame ;
using JetBrains.Annotations ;
using Microsoft.Xna.Framework ;

namespace Gleed2D.Core
{
	public class Constants : IConstants
	{
		static Constants _instance ;

		[NotNull]
		public static Constants Instance
		{
			get
			{
				if( _instance != null )
				{
					return _instance ;
				}

				throw new InvalidOperationException(@"Cannot get the instance that represents the constants.");
			}
		}

		Constants( )
		{
			//initialize all settings with default values
			ColorSelectionFirst = new Color( 255, 255, 0, 255 ) ;
			ColorSelectionRest = new Color( 255, 128, 0, 255 ) ;
			EnableHighlightOnMouseOver = true ;
			ColorHighlight = new Color( 255, 0, 0, 228 ) ;
			ColorBackground = new Color( 100, 149, 237, 255 ) ;
			ColorSelectionBox = new Color( 255, 255, 255 ) * .25f ;
			ColorPrimitives = new Color( 192, 0, 192, 145 ) ;
			ColorTextureTransparent = new Color( 255, 0, 255, 255 ) ;
			DefaultPathItemLineWidth = 4 ;
			DefaultContentRootFolder = new PathToFolder{AbsolutePath = @"C:\" };
			CameraSpeed = 500 ;
			CameraFastSpeed = 2000 ;
			DefaultTextureOrigin = InternalPoint.Middle ;
			WorldOriginColor = new Color( 255, 255, 255, 255 ) ;
			WorldOriginLineThickness = 2 ;
			GridColor = new Color( 192, 192, 192, 120 ) ;
			GridLineThickness = 1 ;
			GridNumberOfGridLines = 500 ;
			GridSpacing = new Vector2( 64, 64 ) ;
		}

		[Editor( typeof( XnaColorUiTypeEditor ), typeof( System.Drawing.Design.UITypeEditor ) )]
		[Category( "General" ), Description( "The color used for the frame around the first selected item." )]
		public Color ColorSelectionFirst
		{
			get ;
			set ;
		}

		[Editor( typeof( XnaColorUiTypeEditor ), typeof( System.Drawing.Design.UITypeEditor ) )]
		[Category( "General" ), Description( "The color used for the frame around all selected items but the first." )]
		public Color ColorSelectionRest
		{
			get ;
			set ;
		}

		[Category( "General" ), Description( "If enabled, items are tinted in 'ColorHighlight' when the mouse is over them." )
		]
		public bool EnableHighlightOnMouseOver
		{
			get ;
			set ;
		}

		[Editor( typeof( XnaColorUiTypeEditor ), typeof( System.Drawing.Design.UITypeEditor ) )]
		[Category( "General" ),
		 Description(
		 	"The color used when hovering over an item. Only relevant if 'EnableHighlightOnMouseOver' is set to true." )]
		public Color ColorHighlight
		{
			get ;
			set ;
		}

		[Editor( typeof( XnaColorUiTypeEditor ), typeof( System.Drawing.Design.UITypeEditor ) )]
		[Category( "General" ), Description( "The background color for the editor." )]
		public Color ColorBackground
		{
			get ;
			set ;
		}

		[Editor( typeof( XnaColorUiTypeEditor ), typeof( System.Drawing.Design.UITypeEditor ) )]
		[Category( "General" ),
		 Description( "The color used when selecting multiple items by holding down the left mouse button." )]
		public Color ColorSelectionBox
		{
			get ;
			set ;
		}

		[Editor( typeof( XnaColorUiTypeEditor ), typeof( System.Drawing.Design.UITypeEditor ) )]
		[Category( "General" ), Description( "The default color each new Primitive gets when added to the level." )]
		public Color ColorPrimitives
		{
			get ;
			set ;
		}

		[Editor( typeof( XnaColorUiTypeEditor ), typeof( System.Drawing.Design.UITypeEditor ) )]
		[Category( "General" ),
		 Description( "The color for colorkeying. When a texture is loaded, this color is made fully transparent." )]
		public Color ColorTextureTransparent
		{
			get ;
			set ;
		}

		[Category( "General" ), Description( "The default LineWidth of each new PathItem added to the level." )]
		public int DefaultPathItemLineWidth
		{
			get ;
			set ;
		}


		[EditorAttribute( typeof( PathToFolderUiTypeEditor ), typeof( System.Drawing.Design.UITypeEditor ) )]
		[Category( "General" ),
		 Description( "When a new level is created, its ContentRootFolder will be initially set to this." )]
		public PathToFolder DefaultContentRootFolder
		{
			get ;
			set ;
		}

		[Category( "General" ), Description( "The speed of the camera when moved with the W,A,S,D-Keys in WorldUnits/sec." )]
		public float CameraSpeed
		{
			get ;
			set ;
		}

		[Category( "General" ),
		 Description( "The speed of the camera when moved via holding down Shift + W,A,S,D-Keys in WorldUnits/sec." )]
		public float CameraFastSpeed
		{
			get ;
			set ;
		}

		[Category( "General" ), Description( "The method for calculating the origin of each new texture.\n" +
			"\"TextureCenter\" means the origin is calculated as Vector2(texture.Width/2, texture.Height/2).\n" +
				"\"Centroid\" is Farseer's way of calculating the Centroid of a polygon created from the texture.\n" +
					"\"TopLeft\",\"TopRight\",\"BottomLeft\" and \"BottomRight\" are the corresponding corners of the texture." )]
		public InternalPoint DefaultTextureOrigin
		{
			get ;
			set ;
		}

		[Editor( typeof( XnaColorUiTypeEditor ), typeof( System.Drawing.Design.UITypeEditor ) )]
		[Category( "World Origin" ), Description( "The color of the world origin cross." )]
		public Color WorldOriginColor
		{
			get ;
			set ;
		}

		[Category( "World Origin" ), Description( "The thickness of the world origin cross in pixels." )]
		public int WorldOriginLineThickness
		{
			get ;
			set ;
		}

		[Editor( typeof( XnaColorUiTypeEditor ), typeof( System.Drawing.Design.UITypeEditor ) )]
		[Category( "Grid" ), Description( "The color of the grid lines." )]
		public Color GridColor
		{
			get ;
			set ;
		}

		[Category( "Grid" ), Description( "The thickness of the grid lines in pixels." )]
		public int GridLineThickness
		{
			get ;
			set ;
		}

		[Category( "Grid" ), Description( "How many grid lines should be drawn in either X or Y direction?" )]
		public int GridNumberOfGridLines
		{
			get ;
			set ;
		}

		[Category( "Grid" ), Description( "The grid spacing in X and Y direction in WorldUnits." )]
		public Vector2 GridSpacing
		{
			get ;
			set ;
		}

		public bool RunLevelStartApplication ;
		public string RunLevelApplicationToStart ;
		public bool RunLevelAppendLevelFilename ;

		public bool SaveLevelStartApplication ;
		public string SaveLevelApplicationToStart ;
		public bool SaveLevelAppendLevelFilename ;

		public bool ShowGrid ;
		public bool SnapToGrid ;
		public bool ShowWorldOrigin ;
		
		//XML import and export
		public void SaveToDisk( string filename )
		{
			string path = System.Windows.Forms.Application.StartupPath + "\\" + filename ;
			FileStream stream = File.Open( path, FileMode.Create ) ;
			
			var serializer = new XmlSerializer( typeof( Constants ) ) ;
			
			serializer.Serialize( stream, this ) ;
			
			stream.Close( ) ;
		}

		public static void TryToLoadOtherwiseSetDefaults( string filename )
		{
			string path = @"{0}\{1}".FormatWith(Application.StartupPath, filename) ;

			if( !File.Exists( path ) )
			{
				initWithDefaults(  );
				return ;
			}

			using( FileStream stream = File.Open( path, FileMode.Open ) )
			{
				var serializer = new XmlSerializer( typeof( Constants ) ) ;

				_instance = (Constants) serializer.Deserialize( stream ) ;

				stream.Close( ) ;
			}
		}

		static void initWithDefaults( )
		{
			_instance=new Constants(  );
		}
	}
}