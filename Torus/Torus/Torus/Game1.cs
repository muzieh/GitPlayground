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
	    private BasicEffect _effect;
	    private Texture2D _texture2;

	    public Game1()
		{
			graphics = new GraphicsDeviceManager(this);
			Content.RootDirectory = "Content";
	        graphics.PreferMultiSampling = true;
		}

		protected override void Initialize()
		{
			base.Initialize();
			_camera = new CameraManager(GraphicsDevice);
			_effect = new BasicEffect(GraphicsDevice);

        }

		protected override void LoadContent()
		{
			spriteBatch = new SpriteBatch(GraphicsDevice);
		    _texture2 = Content.Load<Texture2D>("texture");
			var tp = new TorusGenerator();
		    int thetaParts = 40;
		    int phiParts = 30;
            VertexPositionNormalTexture[] points = tp.Generate(thetaParts, phiParts);
			var i = 0;
			var vertices = new List<VertexPositionNormalTexture>();
			for (int ii = 0; ii < points.Length; ii++)
			{
				var point = points[ii];

                vertices.Add(MapTexture(points[ii % points.Length], new Vector2(0,1)));
                vertices.Add(MapTexture(points[(ii + 1) % points.Length], new Vector2(0,0)));
                vertices.Add(MapTexture(points[(ii + phiParts) % points.Length], new Vector2(1,1)));

                vertices.Add(MapTexture(points[(ii + 1) % points.Length], new Vector2(0,0)));
                vertices.Add(MapTexture(points[(ii + 1 + phiParts) % points.Length], new Vector2(1,0)));
                vertices.Add(MapTexture(points[(ii + phiParts) % points.Length], new Vector2(1,1)));

			}

            _vertexBuffer = new VertexBuffer(GraphicsDevice, typeof(VertexPositionNormalTexture), vertices.Count, BufferUsage.None);
            _vertexBuffer.SetData<VertexPositionNormalTexture>(vertices.ToArray());
			GraphicsDevice.SetVertexBuffer(_vertexBuffer);
		}

	    private VertexPositionNormalTexture MapTexture(VertexPositionNormalTexture p0, Vector2 p1)
	    {
			return new VertexPositionNormalTexture(p0.Position, p0.Normal, p1);
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
			GraphicsDevice.Clear(Color.Black);
			_effect.EnableDefaultLighting();
		    _effect.World = Matrix.CreateRotationZ(_xRotation)*Matrix.CreateRotationX(_xRotation);// * Matrix.CreateRotationY(_yRotation);
			_effect.Projection = _camera.Projection;
			_effect.View = _camera.View;
			//_effect.DiffuseColor = new Vector3(0.2f, 0.5f, 1f);
			//_effect.VertexColorEnabled = true;
		    _effect.PreferPerPixelLighting = true;
		    _effect.TextureEnabled = true;
		    _effect.Texture = _texture2;
			
			foreach (var pass in _effect.CurrentTechnique.Passes)
			{
				pass.Apply();
			    GraphicsDevice.DrawPrimitives(PrimitiveType.TriangleList, 0, _vertexBuffer.VertexCount / 3);
			}

			base.Draw(gameTime);
		}

	}
}
