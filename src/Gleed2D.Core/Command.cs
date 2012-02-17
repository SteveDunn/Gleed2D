using System ;
using System.Xml.Linq;
using StructureMap ;

namespace Gleed2D.Core
{
	public class Command
	{
		readonly XElement _levelBefore ;
		readonly IModel _model ;

		XElement _levelAfter ;

		public string Description
		{
			get;
			private set ;
		}

		public Command(string description)
		{
			_model = ObjectFactory.GetInstance<IModel>();

			Description = description;

			_levelBefore = _model.Level.ToXml();
		}

		public void Undo()
		{
			_model.DeserialiseLevel(_levelBefore ) ;
		}

		public void Redo()
		{
			_model.DeserialiseLevel(_levelAfter);
		}

		public void Completed()
		{
		    _levelAfter=_model.Level.ToXml();
		}
	}
}