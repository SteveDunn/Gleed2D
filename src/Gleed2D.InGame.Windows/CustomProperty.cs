using System ;

namespace Gleed2D.InGame
{
	public class CustomProperty
	{
		public string Name
		{
			get;
			set ;
		}

		public object Value
		{
			get;
			set ;
		}

		public Type Type
		{
			get;
			set ;
		}

		public string Description
		{
			get;
			set ;
		}


		public CustomProperty()
		{
		}

		public CustomProperty(string name, object value, Type type, string description)
		{
			Name = name;
			Value = value;
			Type = type;
			Description = description;
		}

		public CustomProperty Clone()
		{
			var result = new CustomProperty(Name, Value, Type, Description);
            
			return result;
		}
	}
}