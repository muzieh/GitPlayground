using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;

namespace MonoGameTest
{
	public class Game1 : Game
	{
		private Texture2D _redTexture;
		private Texture2D _blueTexture;

		private Texture2D _explosionTexture;
		private GraphicsDeviceManager _graphics;
		private SpriteBatch _spriteBatch;
		private SpriteFont _consolaFont;

		public Game1()
		{
			_graphics = new GraphicsDeviceManager(this);
			Content.RootDirectory = "Content";
			_graphics.PreferMultiSampling = true;
			_graphics.PreferredBackBufferHeight = 200;
			_graphics.PreferredBackBufferWidth = 200;
		}

		protected override void Initialize()
		{
			base.Initialize();
			Components.Add(new FrameCounter(this,_consolaFont, new Vector2(10,10)));

		}

		protected override void LoadContent()
		{
			_spriteBatch = new SpriteBatch(GraphicsDevice);
			_redTexture = Content.Load<Texture2D>("red");
			_blueTexture = Content.Load<Texture2D>("blue");
			_explosionTexture = Content.Load<Texture2D>("explosion");
			_consolaFont = Content.Load<SpriteFont>("ConsolaFont");
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
			GraphicsDevice.Clear(Color.CornflowerBlue);

			_spriteBatch.Begin();
			_spriteBatch.Draw(_redTexture, new Vector2(150, 150), Color.Red);
			_spriteBatch.End();

			base.Draw(gameTime);
		}
	}
}