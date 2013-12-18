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
		private float _yRotation;
		private float _xRotation;

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
			
			var tp = new TorusGenerator();
			Vector3[] points = tp.Generate();
			var i = 0;
			var vertices = new VertexPositionColor[points.Length * 3];
			for (int ii = 0; ii < points.Length; ii++)
			{
				Vector3 point = points[ii];
				vertices[i++] = new VertexPositionColor(point, Color.Red);
				vertices[i++] = new VertexPositionColor(point + new Vector3(0.02f, 0, 0), Color.Green);
				vertices[i++] = new VertexPositionColor(point + new Vector3(0.02f,-0.02f,0), Color.Blue);

			}

			_vertexBuffer = new VertexBuffer(GraphicsDevice, typeof(VertexPositionColor), vertices.Length , BufferUsage.WriteOnly);
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

			_yRotation += 0.01f;
			_xRotation += 0.01f;

			base.Update(gameTime);
		}

		protected override void Draw(GameTime gameTime)
		{
			GraphicsDevice.Clear(Color.CornflowerBlue);
			var effect = new BasicEffect(GraphicsDevice);
			//effect.EnableDefaultLighting();
			effect.World = Matrix.CreateRotationX(_xRotation) * Matrix.CreateRotationY(_yRotation);
			effect.Projection = _camera.Projection;
			effect.View = _camera.View;
			//effect.DiffuseColor = new Vector3(0.2f, 0.5f, 1f);
			effect.VertexColorEnabled = true;

			GraphicsDevice.SetVertexBuffer(_vertexBuffer);
			
			foreach (var pass in effect.CurrentTechnique.Passes)
			{
				pass.Apply();
				GraphicsDevice.DrawPrimitives(PrimitiveType.TriangleList, 0, _vertexBuffer.VertexCount / 3);
			}

			base.Draw(gameTime);
		}

	}
}
