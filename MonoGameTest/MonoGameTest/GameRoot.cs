using System;
using System.Runtime.InteropServices;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;

namespace MonoGameTest
{
	public class GameRoot : Game
	{
		private Texture2D _redTexture;
		private Texture2D _blueTexture;
		private Texture2D _pixel;

		private Texture2D _explosionTexture;
		private GraphicsDeviceManager _graphics;
		private SpriteBatch _spriteBatch;
		private SpriteFont _consoleFont;
		private readonly Random _rnd;

		public GameRoot()
		{
			_graphics = new GraphicsDeviceManager(this);
			Content.RootDirectory = "Content";
			_graphics.PreferMultiSampling = true;
			_graphics.PreferredBackBufferHeight = 200;
			_graphics.PreferredBackBufferWidth = 200;
			this.IsFixedTimeStep = false;
			_rnd = new Random(100);

		}

		protected override void Initialize()
		{
			base.Initialize();
			this.Services.AddService(typeof(GameService), new GameService(this));
			this.Components.Add(new InputComponent(this));
			var lineComponent = new LineComponent(this, _pixel);
			lineComponent.Initialize();
			this.Components.Add(lineComponent);
			this.Components.Add(new FrameRateComponent(this, new Vector2(5,5), _consoleFont));
			

		}

		protected override void LoadContent()
		{
			_spriteBatch = new SpriteBatch(GraphicsDevice);
			_redTexture = Content.Load<Texture2D>("red");
			_blueTexture = Content.Load<Texture2D>("blue");
			_explosionTexture = Content.Load<Texture2D>("explosion");
			_consoleFont = Content.Load<SpriteFont>("consoleFont");
			_pixel = Content.Load<Texture2D>("pixel");
		}

		protected override void UnloadContent()
		{
			;
		}

		protected override void Update(GameTime gameTime)
		{
			if (GamePad.GetState(PlayerIndex.One).Buttons.Back == ButtonState.Pressed)
				Exit();

			base.Update(gameTime);
		}

		protected override void Draw(GameTime gameTime)
		{
			GraphicsDevice.Clear(Color.Black);

			_spriteBatch.Begin(SpriteSortMode.Immediate, BlendState.Additive, null,null,null);
			for(int i=0; i<300; i++)
					_spriteBatch.Draw(_redTexture, new Vector2(_rnd.Next(30,100), _rnd.Next(30,100)), new Color(255,255,255,15));
			_spriteBatch.End();

			base.Draw(gameTime);
		}
	}
}