using System ;
using Microsoft.Xna.Framework ;

namespace Gleed2D.Core
{
	public class RendererParams
	{
		public RendererParams( Camera camera, 
			UserActionInEditor userActionInEditor, 
			Rectangle selectionRectangle, 
			EntityCreation entityCreation, 
			SnapPoint snapPoint )
		{
			Camera = camera ;
			UserActionInEditor = userActionInEditor ;
			SelectionRectangle = selectionRectangle ;
			EntityCreation = entityCreation ;
			SnapPoint = snapPoint ;
			ItemsToRender=ItemsToRender.Everything;
		}

		public ItemsToRender ItemsToRender
		{
			get;
			set ;
		}

		public Camera Camera
		{
			get ;
			private set ;
		}

		public UserActionInEditor UserActionInEditor
		{
			get ;
			private set ;
		}

		public Rectangle SelectionRectangle
		{
			get ;
			private set ;
		}

		public EntityCreation EntityCreation
		{
			get ;
			private set ;
		}

		public SnapPoint SnapPoint
		{
			get ;
			private set ;
		}
	}
}