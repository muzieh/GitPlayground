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
			_graphics.PreferredBackBufferHeight = 800;
			_graphics.PreferredBackBufferWidth = 800;
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
					_spriteBatch.Draw(_redTexture, new Vector2(_rnd.Next(100,200), _rnd.Next(100,200)), new Color(255,255,255,15));
			_spriteBatch.End();

			base.Draw(gameTime);
		}
	}

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

	public class Line
	{
		private Vector2 _begin;
		private Vector2 _end;

		public void Draw(GameTime gameTime, SpriteBatch spriteBatch, Texture2D texture, Color color )
		{
			var vector = _end - _begin;
			var angle = Math.Atan2(vector.Y, vector.X);
			spriteBatch.Draw(texture, _begin, null, color,(float)angle, Vector2.Zero, new Vector2(vector.Length(),1), SpriteEffects.None, 0 );
		}


		public static Line CreateLine(Vector2 begin, Vector2 end)
		{
			var line = new Line {_begin = begin, _end = end};
			return line;
		}
	}
}