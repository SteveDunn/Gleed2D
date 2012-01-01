using System;
using Microsoft.Xna.Framework;

static class Extensions
{
	public static Vector2 Round( this Vector2 v )
	{
		return new Vector2( (float) Math.Round( v.X ), (float) Math.Round( v.Y ) ) ;
	}

	public static Point ToPoint( this Vector2 v )
	{
		return new Point( (int) Math.Round( v.X ), (int) Math.Round( v.Y ) ) ;
	}

	public static Vector2 ToVector2( this Point p )
	{
		return new Vector2( p.X, p.Y ) ;
	}

	public static float DistanceTo( this Vector2 v0, Vector2 v )
	{
		return ( v - v0 ).Length( ) ;
	}

	public static float DistanceToLineSegment( this Vector2 v, Vector2 a, Vector2 b )
	{
		Vector2 x = b - a ;
		x.Normalize( ) ;
	
		float t = Vector2.Dot( x, v - a ) ;
		
		if( t < 0 )
		{
			return ( a - v ).Length( ) ;
		}
		float d = ( b - a ).Length( ) ;
		
		if( t > d )
		{
			return ( b - v ).Length( ) ;
		}
		
		return ( a + x * t - v ).Length( ) ;

	}

	/// <summary>
	/// Convert the Rectangle to an array of Vector2 containing its 4 corners.
	/// </summary>
	/// <param name="rectangle"></param>
	/// <returns>An array of Vector2 representing the rectangle's corners starting from top/left and going clockwise.</returns>
	public static Vector2[ ] ToPolygon( this Rectangle rectangle )
	{
		var poly = new Vector2[ 4 ] ;
		poly[ 0 ] = new Vector2( rectangle.Left, rectangle.Top ) ;
		poly[ 1 ] = new Vector2( rectangle.Right, rectangle.Top ) ;
		poly[ 2 ] = new Vector2( rectangle.Right, rectangle.Bottom ) ;
		poly[ 3 ] = new Vector2( rectangle.Left, rectangle.Bottom ) ;
		
		return poly ;
	}

	public static Rectangle RectangleFromVectors( Vector2 v1, Vector2 v2 )
	{
		Vector2 distance = v2 - v1 ;
		var result = new Rectangle
			{
				X = (int) Math.Min( v1.X, v2.X ),
				Y = (int) Math.Min( v1.Y, v2.Y ),
				Width = (int) Math.Abs( distance.X ),
				Height = (int) Math.Abs( distance.Y )
			} ;
		
		return result ;
	}
}
