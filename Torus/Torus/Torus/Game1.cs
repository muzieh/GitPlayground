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

namespace Torus
{
	public class Game1 : Microsoft.Xna.Framework.Game
	{
		GraphicsDeviceManager graphics;
		SpriteBatch spriteBatch;
		CameraManager _camera;
		public VertexBuffer _vertexBuffer;

		public Game1()
		{
			graphics = new GraphicsDeviceManager(this);
			Content.RootDirectory = "Content";
		}

		protected override void Initialize()
		{
			base.Initialize();
			_camera = new CameraManager(GraphicsDevice);
		}

		protected override void LoadContent()
		{
			spriteBatch = new SpriteBatch(GraphicsDevice);
			
			var vertices = new VertexPositionColor[3];
			var i = 0;
			vertices[i++] = new VertexPositionColor(new Vector3(0,1,0), Color.Red);
			vertices[i++] = new VertexPositionColor(new Vector3(1f,-1f,0), Color.Green);
			vertices[i++] = new VertexPositionColor(new Vector3(-1f,-1f,0), Color.Blue);

			_vertexBuffer = new VertexBuffer(GraphicsDevice, typeof(VertexPositionColor), 3, BufferUsage.WriteOnly);
			_vertexBuffer.SetData<VertexPositionColor>(vertices);
		}

		protected override void UnloadContent()
		{
		}

		protected override void Update(GameTime gameTime)
		{
			if (GamePad.GetState(PlayerIndex.One).Buttons.Back == ButtonState.Pressed)
				this.Exit();
			if (Keyboard.GetState().IsKeyDown(Keys.Escape))
				this.Exit();
		
			base.Update(gameTime);
		}

		protected override void Draw(GameTime gameTime)
		{
			GraphicsDevice.Clear(Color.CornflowerBlue);
			var effect = new BasicEffect(GraphicsDevice);
			//effect.EnableDefaultLighting();
			effect.World = Matrix.Identity;
			effect.Projection = _camera.Projection;
			effect.View = _camera.View;
			//effect.DiffuseColor = new Vector3(0.2f, 0.5f, 1f);
			effect.VertexColorEnabled = true;

			GraphicsDevice.SetVertexBuffer(_vertexBuffer);
			
			foreach (var pass in effect.CurrentTechnique.Passes)
			{
				pass.Apply();
				GraphicsDevice.DrawPrimitives(PrimitiveType.TriangleList, 0, 1);
			}

			base.Draw(gameTime);
		}

	}
}
