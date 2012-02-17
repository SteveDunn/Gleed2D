using System ;

namespace Gleed2D.Core
{
	public interface IMemento
	{
	    void Record(string description, Action action);
		void BeginCommand( string text ) ;
		void EndCommand( ) ;
		void Undo( ) ;
		void Redo( ) ;
		void UndoMany( Command command ) ;
		void RedoMany( Command command ) ;
		
		void AbortCommand( ) ;
		void Clear( ) ;
		event EventHandler<CommandEndedArgs> OnCommandEnded ;
		event EventHandler<CommandEndedArgs> OnCommandUndone ;
		event EventHandler<CommandEndedArgs> OnCommandRedone ;
	}
}