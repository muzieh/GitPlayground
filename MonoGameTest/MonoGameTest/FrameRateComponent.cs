using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace MonoGameTest
{
	public class FrameRateComponent : DrawableGameComponent
	{
		private readonly SpriteFont _spriteFont ;
		private readonly Vector2 _position;
		private const int BufferSize = 10;
		private int _bufferPosition = 0;
		private readonly int[] _buffer;
		private int _frameRate;
		private readonly SpriteBatch _spriteBatch;

		public FrameRateComponent(Game game, Vector2 position, SpriteFont spriteFont) : base(game)
		{
			_spriteFont = spriteFont;
			_position = position;
			_spriteBatch = new SpriteBatch(GraphicsDevice);
			_buffer = new int[BufferSize];
		}

		public override void Update(GameTime gameTime)
		{
			if (gameTime.ElapsedGameTime.TotalMilliseconds > 0)
			{
				_buffer[_bufferPosition] = (int)gameTime.ElapsedGameTime.TotalMilliseconds;
				_bufferPosition = (_bufferPosition + 1)%BufferSize;
				int sum = 0;
				for (int i = 0; i < BufferSize; i++)
				{
					sum += _buffer[i];
				}
				_frameRate = (1000 * BufferSize) / sum;
			}
			
			base.Update(gameTime);
		}

		public override void Draw(GameTime gameTime)
		{
			_spriteBatch.Begin();
			_spriteBatch.DrawString(_spriteFont, "fps:" + _frameRate, _position, Color.White);
			_spriteBatch.End();
			base.Draw(gameTime);
		}
	}
}