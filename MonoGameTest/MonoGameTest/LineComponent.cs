using System;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace MonoGameTest
{
	public class LineComponent : DrawableGameComponent 
	{
		private Texture2D _lineTexture2D;
		private Line _line;
		private SpriteBatch _spriteBatch;
		private Rectangle _boundingBox;

		public LineComponent(GameRoot game, Texture2D pixelTexture) : base(game)
		{
			_lineTexture2D = pixelTexture;
			_rnd = new Random(100);
			_line = Line.CreateLine(Vector2.Zero, new Vector2(100, 100));
			_boundingBox = Game.GraphicsDevice.Viewport.Bounds;

		}

		private int _alpha;
		private float _alphaF;
		private float _alphaSpeed = (float) 0.09;
		private readonly Random _rnd;
		public override void Update(GameTime gameTime)
		{
			_alphaF = _alphaF + _alphaSpeed;
			if (_alphaF > 1.0f)
			{
				_alphaF = 0f;
				var begin = new Vector2((float) (_boundingBox.Width *_rnd.NextDouble()), (float) (_boundingBox.Height *_rnd.NextDouble()));	
				var end = new Vector2((float) (_boundingBox.Width *_rnd.NextDouble()), (float) (_boundingBox.Height *_rnd.NextDouble()));	
				_line = Line.CreateLine(begin, end);
			}
			_alpha = (int)(255.0f*_alphaF);
			base.Update(gameTime);

		}

		public override void Initialize()
		{
			
			base.Initialize();
			_spriteBatch = new SpriteBatch(GraphicsDevice);
			
		}

		public override void Draw(GameTime gameTime)
		{
			_spriteBatch.Begin(SpriteSortMode.Immediate, BlendState.Additive, null,null,null);
			var color = new Color(255,255,255,_alpha);
			_line.Draw(gameTime, _spriteBatch, _lineTexture2D, color);
			_spriteBatch.End();
			base.Draw(gameTime);
		}
	}
}