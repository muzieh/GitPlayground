using System;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace MonoGameTest
{
	public class Line
	{
		private Vector2[] _parts;

		public void Draw(GameTime gameTime, SpriteBatch spriteBatch, Texture2D texture, Color color )
		{
			var length = _parts.Length;
			var i = 0;
			do
			{
				var begin = _parts[i];
				var end = _parts[i + 1];
				var vector = end - begin;
				var angle = Math.Atan2(vector.Y, vector.X);
				spriteBatch.Draw(texture, begin, null, color,(float)angle, Vector2.Zero, new Vector2(vector.Length(),2), SpriteEffects.None, 0 );
				i++;
			} while (i < length-1);
		}


		public static Line CreateLine(Vector2 begin, Vector2 end)
		{
			
			var line = new Line {_parts = new Vector2[4]};
			line._parts[0] = new Vector2(0,0);
			line._parts[1] = new Vector2(10,10);
			line._parts[2] = new Vector2(19,10);
			line._parts[3] = new Vector2(20,20);
			return line;
		}
	}
}