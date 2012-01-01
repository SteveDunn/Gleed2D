namespace Gleed2D.Core
{
	public class PropertyCustomisation
	{
		public PropertyCustomisation( )
		{
			IsBrowsable = true ;
			Category = @"General" ;
		}

		public string Description
		{
			get;
			set ;
		}

		public string Category
		{
			get;
			set ;
		}

		public string DisplayName
		{
			get;
			set ;
		}

		public bool IsReadOnly
		{
			get ;
			set ;
		}

		public bool IsBrowsable
		{
			get ;
			set ;
		}

		public PropertyCustomisation SetCategory( string category )
		{
			Category = category ;
			return this ;
		}

		public PropertyCustomisation SetDisplayName( string displayName )
		{
			DisplayName = displayName ;
			return this ;
		}

		public PropertyCustomisation SetDescription( string description )
		{
			Description = description ;
			return this ;
		}

		public PropertyCustomisation Hide( )
		{
			IsBrowsable = false ;
			return this ;
		}

		public void MakeReadOnly( )
		{
			IsReadOnly = true ;
		}
	}
}