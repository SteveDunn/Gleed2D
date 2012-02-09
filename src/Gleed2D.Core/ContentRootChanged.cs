namespace Gleed2D.Core
{
	public class ContentRootChanged
	{
		public string ContentRootFolder { get; private set; }

		public ContentRootChanged(string contentRootFolder)
		{
			ContentRootFolder = contentRootFolder;
		}
	}
}