using System;
using System.Collections.Generic;
using System.Globalization;
using Microsoft.Xna.Framework;

internal static class ExtensionMethods
{
		/// <summary>
		/// Determines if a given point is located inside of a shape.
		/// </summary>
		/// <param name="point">The point to test.</param>
		/// <param name="verts">The vertices of the shape.</param>
		/// <returns>True if the point is in the shape; false otherwise.</returns>
	public static bool ContainsPoint(this Vector2[] verts, Vector2 point)
	{
		/* http://local.wasp.uwa.edu.au/~pbourke/geometry/insidepoly/ */

		bool oddNodes = false ;

		int j = verts.Length - 1 ;
		float x = point.X ;
		float y = point.Y ;

		for( int i = 0; i < verts.Length; i++ )
		{
			Vector2 tpi = verts[ i ] ;
			Vector2 tpj = verts[ j ] ;

			if( tpi.Y < y && tpj.Y >= y || tpj.Y < y && tpi.Y >= y )
			{
				if( tpi.X + ( y - tpi.Y ) / ( tpj.Y - tpi.Y ) * ( tpj.X - tpi.X ) < x )
				{
					oddNodes = !oddNodes ;
				}
			}

			j = i ;
		}

		return oddNodes ;
	}


	public static Vector2 RotateAroundPoint( this Vector2 point, Vector2 pivot, float rotation )
	{
		Matrix m =
			Matrix.CreateTranslation( new Vector3( -pivot, 0.0f ) ) * // translate back to rotate about (0,0) 
				Matrix.CreateRotationZ( rotation ) * // rotate 
					Matrix.CreateTranslation( new Vector3( pivot, 0.0f ) ) ; // translate back to origin 

		return Vector2.Transform( point, m ) ;
	}

	public static Vector2[ ] RotateAroundPoint( this Vector2[ ] input, Vector2 pivot, float radians )
	{
		Matrix rotation = Matrix.CreateTranslation( new Vector3( -pivot, 0.0f ) ) *
			Matrix.CreateScale( 1, 1, 1 ) *
				Matrix.CreateRotationZ( radians ) *
					Matrix.CreateTranslation( new Vector3( pivot, 0.0f ) ) ;

		return input.Transform( rotation ) ;
	}

	public static Vector2[ ] RotateAroundPoint( this Rectangle input, Vector2 pivot, float radians )
	{
		Matrix rotation = Matrix.CreateTranslation( new Vector3( -pivot, 0.0f ) ) *
			Matrix.CreateScale( 1, 1, 1 ) *
				Matrix.CreateRotationZ( radians ) *
					Matrix.CreateTranslation( new Vector3( pivot, 0.0f ) ) ;

		return input.ToPolygon( ).Transform( rotation ) ;
	}

	public static Vector2[ ] RotateAroundCenter( this Rectangle input, float radians )
	{
		return input.RotateAroundPoint( input.Center.ToVector2( ), radians ) ;
	}

	public static Vector2[ ] Transform( this Vector2[ ] input, Matrix matrix )
	{
		var output = new Vector2[ input.Length ] ;

		Vector2.Transform( input, ref matrix, output ) ;

		return output ;
	}

	public static Rectangle BottomHalf( this Rectangle rectangle )
	{
		return new Rectangle( rectangle.X, rectangle.Y + ( rectangle.Height / 2 ), rectangle.Width, rectangle.Height / 2 ) ;
	}

	public static Rectangle TopHalf( this Rectangle rectangle )
	{
		return new Rectangle( rectangle.X, rectangle.Y, rectangle.Width, rectangle.Height / 2 ) ;
	}

	public static T SafeCast< T >( this object o ) where T : class
	{
		var t = o as T ;

		if( t == null )
		{
			throw new InvalidCastException( @"Cannot cast '{0}' to '{1}'.".FormatWith( o.GetType( ), typeof( T ) ) ) ;
		}

		return t ;
	}

	public static IEnumerable<T> ToEnumerable< T >( this T item ) where T : class
	{
		return new[ ]
			{
				item
			} ;
	}

	public static void Branch( this bool @bool, Action left, Action right )
	{
		if( @bool )
		{
			left( ) ;
		}
		else
		{
			right( ) ;
		}
	}

	public static void ForEach< T >( this IEnumerable<T> list, Action<T> action )
	{
		foreach( T item in list )
		{
			action( item ) ;
		}
	}

	public static Vector2 Size( this Rectangle rectangle )
	{
		return new Vector2( rectangle.Width, rectangle.Height ) ;
	}

	public static string FormatWith( this string format, params object[ ] args )
	{
		return FormatWith( format, CultureInfo.InvariantCulture, args ) ;
	}

	// ReSharper disable MemberCanBePrivate.Global
	public static string FormatWith( this string format, IFormatProvider provider, params object[ ] args )
		// ReSharper restore MemberCanBePrivate.Global
	{
		return string.Format( provider, format, args ) ;
	}

	public static T AsEnum< T >( this string value )
	{
		return (T) Enum.Parse( typeof( T ), value, false ) ;
	}

	public static int AsInteger( this string value )
	{
		return Convert.ToInt32( value, CultureInfo.InvariantCulture ) ;
	}

	public static float AsFloat( this string value )
	{
		return (float) Convert.ToDouble( value, CultureInfo.InvariantCulture ) ;
	}

	public static byte AsByte( this string value )
	{
		return Convert.ToByte( value, CultureInfo.InvariantCulture ) ;
	}

	public static bool AsBool( this string value )
	{
		return Convert.ToBoolean( value, CultureInfo.InvariantCulture ) ;
	}

	public static Vector2 AsVector2( this float value )
	{
		return new Vector2( value, value ) ;
	}

	public static Vector3 AsVector3( this Vector2 value )
	{
		return new Vector3( value, 0f ) ;
	}

	public static Vector2 AsVector2( this string value )
	{
		int indexOfComma = value.IndexOf( ',' ) ;

		string first = value.Substring( 0, indexOfComma ) ;
		string last = value.Substring( indexOfComma + 1 ) ;

		return new Vector2(
			(float) Convert.ToDouble( first, CultureInfo.InvariantCulture ),
			(float) Convert.ToDouble( last, CultureInfo.InvariantCulture ) ) ;
	}
}