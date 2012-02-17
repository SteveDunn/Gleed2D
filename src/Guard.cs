using System ;
using System.Collections ;
using System.Diagnostics;
using System.Diagnostics.CodeAnalysis ;
using System.Linq ;
using JetBrains.Annotations ;

internal class CoverageExcludeAttribute : Attribute
{
}

/// <summary>
/// Guard class, used for guard clauses and argument validation.
/// <para>
/// Don't put this in a namespace or an assembly, simply add it your project as a linked file.  
/// This is to stop ambiguous instances where 2 or more assemblies contains this type.
/// </para>
/// </summary>
[CoverageExclude]
static class Guard
{
	[DebuggerHidden]
	[AssertionMethod]
	public static void ArgumentNotNull< T >(
		[InvokerParameterName] string argName,
		[AssertionCondition( AssertionConditionType.IS_NOT_NULL )] T argValue ) where T : class
	{
		if( argValue == null )
		{
			throw new ArgumentNullException( argName ) ;
		}
	}

	[DebuggerHidden]
	[AssertionMethod]
	public static void GenericArgumentNotNull<T>([InvokerParameterName] string argName, T arg)
		{
			Type type = typeof(T);

			if (!type.IsValueType || (type.IsGenericType && (type.GetGenericTypeDefinition() == typeof(Nullable<>))))
			{
				ArgumentNotNull(argName, (object) arg);
			}
		}

	[DebuggerHidden]
	[AssertionMethod]
	public static void ArgumentsNotNull(
		[InvokerParameterName] string argName,
		IEnumerable argValue )
	{
		ArgumentNotNull( argName, argValue ) ;

		if( argValue.Cast<object>( ).Any( obj => obj == null ) )
		{
			throw new ArgumentException( @"Argument cannot contain a null value.", argName ) ;
		}
	}

	[DebuggerHidden]
	[AssertionMethod]
	public static void StringArgumentNotEmpty(
		[InvokerParameterName] string argName,
		string argValue )
	{
		ArgumentNotNull( argName, argValue ) ;

		if( argValue.Length == 0 )
		{
			throw new ArgumentException( @"Argument cannot be zero length.", argName ) ;
		}
	}

	[DebuggerHidden]
	[AssertionMethod]
	public static void StringArgumentNotNullOrEmpty(
		[InvokerParameterName] string argName,
		string argValue )
	{
		ArgumentNotNull( argName, argValue ) ;
		StringArgumentNotEmpty( argName, argValue ) ;
	}

	[DebuggerHidden]
	[SuppressMessage(
		"Microsoft.Usage",
		"CA2208:InstantiateArgumentExceptionsCorrectly",
		Justification = @"Cannot call the overload that takes a parameter name as I'm not passed a parameter name." )]
	[AssertionMethod]
	public static void ArgumentNotNull< T >(
		[AssertionCondition( AssertionConditionType.IS_NOT_NULL )] T value ) where T : class
	{
		if( value == null )
		{
			throw new ArgumentNullException( ) ;
		}
	}
}