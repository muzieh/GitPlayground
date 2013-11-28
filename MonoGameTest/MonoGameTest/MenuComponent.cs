using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace MonoGameTest
{
	class MenuComponent : DrawableGameComponent
	{
		private SpriteFont _menuFont;
		private SpriteBatch _batch;

		public MenuComponent(Game game, SpriteFont menuFont) : base(game)
		{
			_menuFont = menuFont;
			_batch = new SpriteBatch(this.GraphicsDevice);
		}

		public override void Update(GameTime gameTime)
		{
			base.Update(gameTime);
		}

		public override void Draw(GameTime gameTime)
		{
			_batch.Begin();
			_batch.DrawString();
			_batch.End();

			base.Draw(gameTime);
		}
	}
}
