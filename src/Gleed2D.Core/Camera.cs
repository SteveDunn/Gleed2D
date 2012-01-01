using Microsoft.Xna.Framework ;

namespace Gleed2D.Core
{
	public class Camera
	{
		Vector2 _position;
		float _rotation;
		float _scale;
		Vector2 _viewport;                //width and height of the viewport

		public Camera(float width, float height)
		{
			_position = Vector2.Zero;
			_rotation = 0;
			_scale = 1.0f;
			_viewport = new Vector2(width, height);
			updatematrix();
		}

		public Vector2 Position
		{
			get
			{
				return _position ;
			}
			set
			{
				_position = value ;
				updatematrix( ) ;
			}
		}

		public float Rotation
		{
			get
			{
				return _rotation;
			}
			set
			{
				_rotation = value;
				updatematrix();
			}
		}

		public float Scale
		{
			get
			{
				return _scale;
			}
			set
			{
				_scale = value;
				updatematrix();
			}
		}

		public Matrix Matrix
		{
			get;
			private set ;
		}

		void updatematrix()
		{
			Matrix = Matrix.CreateTranslation(-_position.X, -_position.Y, 0.0f) *
					 Matrix.CreateRotationZ(_rotation) *
					 Matrix.CreateScale(_scale) *
					 Matrix.CreateTranslation(_viewport.X / 2, _viewport.Y / 2, 0.0f);
		}

		public void UpdateViewport(float width, float height)
		{
			_viewport.X = width;
			_viewport.Y = height;
			updatematrix();
		}
	}
}
