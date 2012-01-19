using System ;
using System.Collections.Generic ;
using System.Drawing ;
using JetBrains.Annotations ;
using Microsoft.Xna.Framework ;
using Microsoft.Xna.Framework.Graphics ;
using Rectangle = Microsoft.Xna.Framework.Rectangle ;

namespace Gleed2D.Core
{
	public interface IEditor
	{
		Camera Camera
		{
			get ;
		}

		[CanBeNull]
		ItemEditor ItemUnderMouse
		{
			get ;
		}

		Vector2 GrabPoint
		{
			get ;
		}

		UserActionInEditor CurrentUserAction
		{
			get;
		}

		List<Vector2> PositionsBeforeUserInteraction
		{
			get ;
		}

		List<float> RotationsBeforeUserInteraction
		{
			get ;
		}

		Rectangle SelectionRectangle
		{
			get ;
			set ;
		}

		List<Vector2> ScalesBeforeUserInteraction
		{
			get ;
		}

		void Update( GameTime gameTime ) ;

		void Draw( GameTime gameTime, GraphicsDevice graphicsDevice, SpriteBatch spriteBatch ) ;
		void SetMousePosition( int x, int y ) ;
		void StartCreatingEntityAfterNextClick( EntityCreationProperties creationProperties ) ;
		EntityCreation StartCreatingEntityNow(EntityCreationProperties properties);
		void TrySetCameraZoom( float zoom ) ;
		void SetViewportSize( Size size ) ;
		void CreateSelectionRectangle( ) ;
		void StartMovingCamera( ) ;
		void StartRotatingItems( ) ;
		void StartScalingSelectedItems( ) ;
		void StartCopyingSelectedItems( ) ;
		void StartMovingSelectedItems( ) ;
		Vector2 SnapToGrid( Vector2 input ) ;
		void SetModeToIdle( ) ;
		void SetModeTo( UserActionInEditor userAction ) ;
		void AddNewItemAtMouse( ItemEditor newEditor ) ;
		void StopCreatingEntity();
		void CancelCreatingEntity();
	}
}