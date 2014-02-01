using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Audio;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.GamerServices;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework.Media;

namespace RenderTargetTest
{
	public class Game1 : Microsoft.Xna.Framework.Game
	{
		GraphicsDeviceManager _graphics;
		SpriteBatch _spriteBatch;
		SpriteBatch _spriteBatchRT;
		Texture2D _square;
		RenderTarget2D _renderTarget;

		public Game1()
		{
			_graphics = new GraphicsDeviceManager(this);
			Content.RootDirectory = "Content";

		}

		protected override void Initialize()
		{
			base.Initialize();
		}

		protected override void LoadContent()
		{
			_spriteBatch = new SpriteBatch(GraphicsDevice);
			_spriteBatchRT = new SpriteBatch(GraphicsDevice);
			_square = Content.Load<Texture2D>("square");
			_renderTarget = new RenderTarget2D(_graphics.GraphicsDevice, 
				_square.Width,
				_square.Height,
				false , 
				_graphics.GraphicsDevice.PresentationParameters.BackBufferFormat, 
				_graphics.GraphicsDevice.PresentationParameters.DepthStencilFormat, 1, RenderTargetUsage.DiscardContents);
		}

		protected override void UnloadContent()
		{
		}

		protected override void Update(GameTime gameTime)
		{
			if( Keyboard.GetState().IsKeyDown(Keys.Escape))
				this.Exit();

			base.Update(gameTime);
		}

		protected override void Draw(GameTime gameTime)
		{
			
			GraphicsDevice.SetRenderTarget(_renderTarget);
			GraphicsDevice.Clear(Color.Black);
			_spriteBatch.Begin(SpriteSortMode.FrontToBack, BlendState.AlphaBlend,null , DepthStencilState.Default,null);
			_spriteBatch.Draw(_square, Vector2.Zero, Color.Red);
			_spriteBatch.End();

			GraphicsDevice.SetRenderTarget(null);

			GraphicsDevice.Clear(Color.Black);
			_spriteBatch.Begin(SpriteSortMode.Texture, BlendState.AlphaBlend, SamplerState.LinearWrap, DepthStencilState.Default, RasterizerState.CullNone);
			_spriteBatch.Draw(_square, Vector2.Zero, Color.White);
			_spriteBatch.Draw((Texture2D) _renderTarget, new Vector2(200,200), Color.White);
			_spriteBatch.End();
			base.Draw(gameTime);
		}
	}
}
