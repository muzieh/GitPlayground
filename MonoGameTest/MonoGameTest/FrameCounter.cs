using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace MonoGameTest
{
	class FrameCounter : DrawableGameComponent
	{

		private SpriteBatch _spriteBatch;
		private SpriteFont _font;
		private int _framePerSecond;
		private Vector2 _position;

		public FrameCounter(Game game, SpriteFont font, Vector2 vector2) : base(game)
		{
			_spriteBatch = new SpriteBatch(GraphicsDevice);
			_font = font;
			_position = vector2;
		}

		public override void Update(GameTime gameTime)
		{
			_framePerSecond = 1000/gameTime.ElapsedGameTime.Milliseconds;
			base.Update(gameTime);
		}

		public override void Draw(GameTime gameTime)
		{
			_spriteBatch.Begin();
			_spriteBatch.DrawString(_font, "fps: " + _framePerSecond, new Vector2(10,30),Color.White );
			_spriteBatch.End();
			base.Draw(gameTime);
		}
	}
}
