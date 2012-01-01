using System ;

namespace Gleed2D.Core
{
	public class EntityCreation
	{
		readonly Action _whenEntityReadyToBeAdded ;
		public bool StartedCreating ;
		ItemEditor _currentEditor ;
		public EntityCreationProperties CreationProperties ;

		public EntityCreation( Action whenEntityReadyToBeAdded )
		{
			_whenEntityReadyToBeAdded = whenEntityReadyToBeAdded ;
		}

		public ItemEditor CurrentEditor
		{
			get
			{
				return _currentEditor ;
			}

			set
			{
				_currentEditor = value ;
				_currentEditor.PreviewEndedReadyForCreation += ( s, e ) => _whenEntityReadyToBeAdded( ) ;
			}
		}

		public ItemEditor EditorInstance
		{
			get ;
			set ;
		}

		public void ClearCurrentEditor( )
		{
			if( _currentEditor != null )
			{
				_currentEditor.PreviewEndedReadyForCreation -= ( s, e ) => _whenEntityReadyToBeAdded( ) ;
			}
			_currentEditor = null ;
		}
	}
}