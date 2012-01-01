using System ;
using StructureMap ;

namespace Gleed2D.Core
{
	public class Command
	{
		public string Description
		{
			get;
			private set ;
		}

		readonly Level _levelBefore ;
		Level _levelAfter ;
		
		readonly IModel _model ;

		public Command(string description)
		{
			_model = ObjectFactory.GetInstance<IModel>( ) ;
		
			Description = description;

			_levelBefore = _model.Level.Clone( ) ;
		}

		public void Undo()
		{
			_model.Level = _levelBefore ;
		}

		public void Redo()
		{
			_model.Level = _levelAfter ;
		}

		public void Completed()
		{
			var clonedModel = _model.Level.Clone() ;

			_levelAfter=clonedModel;
		}
	}
}