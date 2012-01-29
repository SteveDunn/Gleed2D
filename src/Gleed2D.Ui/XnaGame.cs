using System;
using System.Drawing ;
using System.Windows.Forms ;
using Gleed2D.Core ;
using StructureMap ;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using ButtonState = Microsoft.Xna.Framework.Input.ButtonState ;

namespace GLEED2D
{
	public class XnaGame : Game, IGame
	{
		readonly IntPtr _drawSurface;
		readonly IMainForm _mainForm ;
		readonly Form _winform;
		readonly GraphicsDeviceManager _graphics ;

		GamePadState _gamepadstate ;

		ICanvas _canvas ;

		public XnaGame( IMainForm mainForm )
		{
			_mainForm = mainForm ;
			
			_drawSurface = _mainForm.GetHandle( ) ;
			
			Logger.Instance.log("Game1 creation started.");

			_graphics = new GraphicsDeviceManager( this )
				{
					PreferredBackBufferWidth = 800,
					PreferredBackBufferHeight = 600
				} ;

			Content.RootDirectory = "Content";

			_graphics.PreparingDeviceSettings += graphics_PreparingDeviceSettings;
	
			_winform = (Form)Form.FromHandle(Window.Handle);
			
			_winform.VisibleChanged += game1VisibleChanged;
			_winform.Size = new Size(10, 10);
			
			Mouse.WindowHandle = _drawSurface;

			Size pictureBoxSize = _mainForm.CanvasSize ;

			ResizeBackBuffer( pictureBoxSize.Width, pictureBoxSize.Height ) ;

			_winform.Hide();
		}

		public new void Exit( )
		{
			HasExited=true;
			base.Exit(  );
		}

		public bool HasExited
		{
			get ;
			private set ;
		}

		public SpriteBatch SpriteBatch
		{
			get;
			private set ;
		}

		public event EventHandler<EventArgs> CanvasResized ;

		public GameTime GameTime
		{
			get;
			private set ;
		}

		ICanvas summonEditor( )
		{
			if( _canvas == null )
			{
				_canvas = ObjectFactory.GetInstance<ICanvas>( ) ;
			}

			return _canvas ;
		}

		protected override void Initialize()
		{
			base.Initialize();

			BasicEffect = new BasicEffect( GraphicsDevice );

			Matrix world = Matrix.Identity;
            Matrix projection = Matrix.CreateOrthographicOffCenter(0,
				GraphicsDevice.Viewport.Width, 
				GraphicsDevice.Viewport.Height, 
				0,
				0,
				1);

			BasicEffect.World = world ;
			BasicEffect.Projection = projection ;
		}

		public BasicEffect BasicEffect
		{
			get ;
			private set ;
		}

		void graphics_PreparingDeviceSettings(object sender, PreparingDeviceSettingsEventArgs e)
		{
			e.GraphicsDeviceInformation.PresentationParameters.DeviceWindowHandle = _drawSurface;
		}
		
		 void game1VisibleChanged(object sender, EventArgs e)
		{
			_winform.Hide();
			_winform.Size = new Size(10, 10);
			_winform.Visible = false;
		}


		/// <summary>
		/// LoadContent will be called once per game and is the place to load
		/// all of your content.
		/// </summary>
		protected override void LoadContent()
		{
			// Create a new SpriteBatch, which can be used to draw textures.
			SpriteBatch = new SpriteBatch(GraphicsDevice);
		}

		public void ResizeBackBuffer(int width, int height)
		{
			_graphics.PreferredBackBufferWidth = width;
			_graphics.PreferredBackBufferHeight = height;
			_graphics.ApplyChanges();

			if( CanvasResized != null )
			{
				CanvasResized( this, EventArgs.Empty ) ;
			}

			if (BasicEffect != null)
			{
				Matrix projection = Matrix.CreateOrthographicOffCenter(
					0,
					GraphicsDevice.Viewport.Width,
					GraphicsDevice.Viewport.Height,
					0,
					0,
					1 ) ;

				BasicEffect.Projection = projection ;
			}
		}

		/// <summary>
		/// UnloadContent will be called once per game and is the place to unload
		/// all content.
		/// </summary>
		protected override void UnloadContent()
		{

		}

		/// <summary>
		/// Allows the game to run logic such as updating the world,
		/// checking for collisions, gathering input, and playing audio.
		/// </summary>
		/// <param name="gameTime">Provides a snapshot of timing values.</param>
		protected override void Update(GameTime gameTime)
		{
			GameTime = gameTime;
			
			_gamepadstate = GamePad.GetState(PlayerIndex.One);
			
			if (_gamepadstate.IsConnected)
			{
				if (_gamepadstate.Buttons.Back == ButtonState.Pressed)
				{
					Exit();
				}
			}
			
			Keyboard.GetState();

			GameTime = gameTime;
			
			summonEditor( ).Update(gameTime);

			base.Update(gameTime);
		}

		/// <summary>
		/// This is called when the game should draw itself.
		/// </summary>
		/// <param name="gameTime">Provides a snapshot of timing values.</param>
		protected override void Draw(GameTime gameTime)
		{
			float fps = 1 / (float)gameTime.ElapsedGameTime.TotalSeconds;
			
			_mainForm.SetFpsDiagText( "FPS: " + fps.ToString( "#0.00" ) ) ;

			summonEditor( ).Draw( gameTime, GraphicsDevice, SpriteBatch);

			base.Draw(gameTime);
		}
	}
}
