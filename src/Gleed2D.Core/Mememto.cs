using System ;
using System.Collections.Generic ;
using JetBrains.Annotations ;

namespace Gleed2D.Core
{
	[PublicAPI]
	public class Mememto : IMemento
	{
		bool _commandInProgress ;

		readonly Stack<Command> _undoBuffer = new Stack<Command>( ) ;
		readonly Stack<Command> _redoBuffer = new Stack<Command>( ) ;

		public event EventHandler<CommandEndedArgs> OnCommandEnded ;
		public event EventHandler<CommandEndedArgs> OnCommandUndone ;
		public event EventHandler<CommandEndedArgs> OnCommandRedone ;

	    public void Record(string description, Action action)
	    {
	        BeginCommand(description);
	        action();
            EndCommand();
	    }

	    public void BeginCommand( string description )
		{
			if( _commandInProgress )
			{
				_undoBuffer.Pop( ) ;
			}

			_undoBuffer.Push( new Command( description ) ) ;
			_commandInProgress = true ;
		}

		public void EndCommand( )
		{
			if( !_commandInProgress )
			{
				return ;
			}

			_undoBuffer.Peek( ).Completed( ) ;
			
			_redoBuffer.Clear( ) ;

			if (OnCommandEnded != null)
			{
				OnCommandEnded(
					this,
					new CommandEndedArgs
						{
							Command = _undoBuffer.Peek( ),
							UndoCount = _undoBuffer.Count,
							RedoCount = _redoBuffer.Count
						} ) ;
			}

			_commandInProgress = false ;
		}

		public void AbortCommand( )
		{
			if( !_commandInProgress )
			{
				return ;
			}

			_undoBuffer.Pop( ) ;

			_commandInProgress = false ;
		}

		public void Clear( )
		{
			_undoBuffer.Clear( ) ;
			_redoBuffer.Clear( ) ;

			_commandInProgress = false ;
		}

		public void Undo( )
		{
			if( _commandInProgress )
			{
				_undoBuffer.Pop( ) ;
				_commandInProgress = false ;
			}

			if( _undoBuffer.Count == 0 )
			{
				return ;
			}

			_undoBuffer.Peek( ).Undo( ) ;
			_redoBuffer.Push( _undoBuffer.Pop( ) ) ;

			if (OnCommandUndone != null)
			{
				OnCommandUndone(
					this,
					new CommandEndedArgs
						{
							RedoCount = _redoBuffer.Count,
							UndoCount = _undoBuffer.Count
						} ) ;
			}
		}

		public void UndoMany( Command c )
		{
			while( _redoBuffer.Count == 0 || _redoBuffer.Peek( ) != c )
			{
				Undo( ) ;
			}
		}

		public void Redo( )
		{
			if( _commandInProgress )
			{
				_undoBuffer.Pop( ) ;
				_commandInProgress = false ;
			}

			if( _redoBuffer.Count == 0 )
			{
				return ;
			}

			_redoBuffer.Peek( ).Redo( ) ;
			_undoBuffer.Push( _redoBuffer.Pop( ) ) ;

			if( OnCommandRedone != null )
			{
				OnCommandRedone(
					this,
					new CommandEndedArgs
						{
							UndoCount = _undoBuffer.Count,
							RedoCount = _redoBuffer.Count
						} ) ;
			}
		}

		public void RedoMany( Command command )
		{
			while( _undoBuffer.Count == 0 || _undoBuffer.Peek( ) != command )
			{
				Redo( ) ;
			}
		}
		
	}
}