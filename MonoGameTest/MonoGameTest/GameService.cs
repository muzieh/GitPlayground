using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;

namespace MonoGameTest
{
	public class GameService
	{
		private GameRoot _game;
		public GameService(GameRoot gameRoot)
		{
			_game = gameRoot;
		}
	}
}
