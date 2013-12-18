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

namespace WindowsGame3
{
	public class Game1 : Microsoft.Xna.Framework.Game
	{
		GraphicsDeviceManager _graphics;
		SpriteBatch _spriteBatch;
		private Texture2D _sprite;
		private Effect _effect;
		private Texture2D _point;

		public Game1()
		{
			_graphics = new GraphicsDeviceManager(this);
			Content.RootDirectory = "Content";
			_graphics.PreferredBackBufferHeight = 235;
			_graphics.PreferredBackBufferWidth = 214;
		}

		protected override void Initialize()
		{
			// TODO: Add your initialization logic here

			base.Initialize();
		}

		private float PercentageR = 0;
		private float PercentageG = 0;
		private float PercentageB = 0;
		private float StepR = 0.01f;
		private float StepG = 0.33f;
		private float StepB = 0.05f;
		private Vector2 _lightPosition;
		private float _intensity;
		private Vector2 _pointPosition;
		private float _factor;

		protected override void LoadContent()
		{
			// Create a new SpriteBatch, which can be used to draw textures.
			_spriteBatch = new SpriteBatch(GraphicsDevice);
			_effect = Content.Load<Effect>("Effect2");
			_sprite = Content.Load<Texture2D>("images");
			_lightPosition = new Vector2(0.5f, 0.5f);
			_intensity = 0.94f;
			_point = Content.Load<Texture2D>("point");
			_factor = 2.1f;

		}

		protected override void UnloadContent()
		{
		}

		protected override void Update(GameTime gameTime)
		{
			if (Keyboard.GetState().IsKeyDown(Keys.Escape))
				Exit();
			PercentageR += StepR;
			if (Math.Abs(PercentageR) >= 1)
				StepR *= -1;
			PercentageG += StepG;
			if (Math.Abs(PercentageG) >= 1)
				StepG *= -1;
			PercentageB += StepB;
			if (Math.Abs(PercentageB) >= 1)
				StepB *= -1;

			var rect = GraphicsDevice.Viewport;
			_factor = 2.0f;
			var mouse = Mouse.GetState();
			_pointPosition = new Vector2(mouse.X, mouse.Y);
			_lightPosition = new Vector2((float)mouse.X / rect.Width, (float)mouse.Y / rect.Height);


			base.Update(gameTime);
		}

		protected override void Draw(GameTime gameTime)
		{
			_graphics.GraphicsDevice.Clear(Color.Red);

			//effect = null;
			//_effect.Parameters["PercentageR"].SetValue(PercentageR);
			//_effect.Parameters["PercentageB"].SetValue(PercentageG);
			//_effect.Parameters["PercentageG"].SetValue(PercentageB);
			_effect.Parameters["LightPosition"].SetValue(_lightPosition);
			_effect.Parameters["Intensity"].SetValue(_intensity);
			_effect.Parameters["Factor"].SetValue(_factor);
			_spriteBatch.Begin(SpriteSortMode.FrontToBack, BlendState.AlphaBlend, SamplerState.LinearClamp, DepthStencilState.Default, RasterizerState.CullNone,_effect);
			//_spriteBatch.Begin(SpriteSortMode.FrontToBack, BlendState.AlphaBlend, SamplerState.LinearClamp, DepthStencilState.Default, RasterizerState.CullNone);
				
			_spriteBatch.Draw(_sprite,Vector2.Zero,Color.White);
			_spriteBatch.End();
			_spriteBatch.Begin();
			_spriteBatch.Draw(_point,_pointPosition,null, Color.White,0,new Vector2(2,2),1,SpriteEffects.None, 0 );
			_spriteBatch.End();
			base.Draw(gameTime);
		}
	}
}
