using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Input;

namespace MonoGameTest
{
	class InputComponent : GameComponent
	{
		public InputComponent(Game game) : base(game)
		{
		}

		public override void Update(GameTime gameTime)
		{
			var keyboardState = Keyboard.GetState();
 			if( keyboardState.IsKeyDown(Keys.Escape) )
				Game.Exit();

			base.Update(gameTime);
		}
	}
}
