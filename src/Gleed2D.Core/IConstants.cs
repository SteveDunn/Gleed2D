using System.ComponentModel ;
using Gleed2D.Core.CustomUITypeEditors ;
using Gleed2D.InGame ;
using Microsoft.Xna.Framework ;

namespace Gleed2D.Core
{
	public interface IConstants
	{
		[Editor( typeof( XnaColorUiTypeEditor ), typeof( System.Drawing.Design.UITypeEditor ) )]
		[Category( "General" ), Description( "The color used for the frame around the first selected item." )]
		Color ColorSelectionFirst
		{
			get ;
			set ;
		}

		[Editor( typeof( XnaColorUiTypeEditor ), typeof( System.Drawing.Design.UITypeEditor ) )]
		[Category( "General" ), Description( "The color used for the frame around all selected items but the first." )]
		Color ColorSelectionRest
		{
			get ;
			set ;
		}

		[Category( "General" ), Description( "If enabled, items are tinted in 'ColorHighlight' when the mouse is over them." )
		]
		bool EnableHighlightOnMouseOver
		{
			get ;
			set ;
		}

		[Editor( typeof( XnaColorUiTypeEditor ), typeof( System.Drawing.Design.UITypeEditor ) )]
		[Category( "General" ),
		 Description(
		 	"The color used when hovering over an item. Only relevant if 'EnableHighlightOnMouseOver' is set to true." )]
		Color ColorHighlight
		{
			get ;
			set ;
		}

		[Editor( typeof( XnaColorUiTypeEditor ), typeof( System.Drawing.Design.UITypeEditor ) )]
		[Category( "General" ), Description( "The background color for the editor." )]
		Color ColorBackground
		{
			get ;
			set ;
		}

		[Editor( typeof( XnaColorUiTypeEditor ), typeof( System.Drawing.Design.UITypeEditor ) )]
		[Category( "General" ),
		 Description( "The color used when selecting multiple items by holding down the left mouse button." )]
		Color ColorSelectionBox
		{
			get ;
			set ;
		}

		[Editor( typeof( XnaColorUiTypeEditor ), typeof( System.Drawing.Design.UITypeEditor ) )]
		[Category( "General" ), Description( "The default color each new Primitive gets when added to the level." )]
		Color ColorPrimitives
		{
			get ;
			set ;
		}

		[Editor( typeof( XnaColorUiTypeEditor ), typeof( System.Drawing.Design.UITypeEditor ) )]
		[Category( "General" ),
		 Description( "The color for colorkeying. When a texture is loaded, this color is made fully transparent." )]
		Color ColorTextureTransparent
		{
			get ;
			set ;
		}

		[Category( "General" ), Description( "The default LineWidth of each new PathItem added to the level." )]
		int DefaultPathItemLineWidth
		{
			get ;
			set ;
		}

		[EditorAttribute( typeof( PathToFolderUiTypeEditor ), typeof( System.Drawing.Design.UITypeEditor ) )]
		[Category( "General" ),
		 Description( "When a new level is created, its ContentRootFolder will be initially set to this." )]
		PathToFolder DefaultContentRootFolder
		{
			get ;
			set ;
		}

		[Category( "General" ), Description( "The speed of the camera when moved with the W,A,S,D-Keys in WorldUnits/sec." )]
		float CameraSpeed
		{
			get ;
			set ;
		}

		[Category( "General" ),
		 Description( "The speed of the camera when moved via holding down Shift + W,A,S,D-Keys in WorldUnits/sec." )]
		float CameraFastSpeed
		{
			get ;
			set ;
		}

		[Category( "General" ),
		 Description(
		 	"The method for calculating the origin of each new texture.\n"
		 		+ "\"TextureCenter\" means the origin is calculated as Vector2(texture.Width/2, texture.Height/2).\n"
		 			+ "\"Centroid\" is Farseer's way of calculating the Centroid of a polygon created from the texture.\n"
		 				+ "\"TopLeft\",\"TopRight\",\"BottomLeft\" and \"BottomRight\" are the corresponding corners of the texture." )]
		InternalPoint DefaultTextureOrigin
		{
			get ;
			set ;
		}

		[Editor( typeof( XnaColorUiTypeEditor ), typeof( System.Drawing.Design.UITypeEditor ) )]
		[Category( "World Origin" ), Description( "The color of the world origin cross." )]
		Color WorldOriginColor
		{
			get ;
			set ;
		}

		[Category( "World Origin" ), Description( "The thickness of the world origin cross in pixels." )]
		int WorldOriginLineThickness
		{
			get ;
			set ;
		}

		[Editor( typeof( XnaColorUiTypeEditor ), typeof( System.Drawing.Design.UITypeEditor ) )]
		[Category( "Grid" ), Description( "The color of the grid lines." )]
		Color GridColor
		{
			get ;
			set ;
		}

		[Category( "Grid" ), Description( "The thickness of the grid lines in pixels." )]
		int GridLineThickness
		{
			get ;
			set ;
		}

		[Category( "Grid" ), Description( "How many grid lines should be drawn in either X or Y direction?" )]
		int GridNumberOfGridLines
		{
			get ;
			set ;
		}

		[Category( "Grid" ), Description( "The grid spacing in X and Y direction in WorldUnits." )]
		Microsoft.Xna.Framework.Vector2 GridSpacing
		{
			get ;
			set ;
		}
	}
}