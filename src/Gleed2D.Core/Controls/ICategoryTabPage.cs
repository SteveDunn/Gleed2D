namespace Gleed2D.Core.Controls
{
	public interface ICategoryTabPage
	{
		string CategoryName
		{
			get;
			set ;
		}

		void AddPlugin( IEditorPlugin editorPlugin ) ;
	}
}