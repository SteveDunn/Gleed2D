namespace Gleed2D.Core
{
	public class ContentRootChanged
	{
		public string OldContentRootFolder { get; private set; }
		public string NewContentRootFolder { get; private set; }

		public ContentRootChanged(string oldContentRootFolder, string contentRootFolder)
		{
			OldContentRootFolder = oldContentRootFolder;
			NewContentRootFolder = contentRootFolder;
		}
	}
}