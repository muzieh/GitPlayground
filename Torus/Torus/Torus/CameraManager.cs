using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace Torus
{
	public class CameraManager
	{
		public Matrix View { get; set; }
		public Matrix Projection {get;set;}
		private Vector3 _cameraPosition;
		private Vector3 _cameraTarget;
		private Vector3 _cameraUpVector;
		public GraphicsDevice _graphicsDevice;

		public CameraManager(GraphicsDevice graphicsDevice)
		{
			_graphicsDevice = graphicsDevice;
			_cameraPosition = new Vector3(0, 0, 3f);
			_cameraTarget = new Vector3(0, 0, 0);
			_cameraUpVector = Vector3.UnitY;

			View = Matrix.CreateLookAt(_cameraPosition, _cameraTarget, _cameraUpVector);
			float aspectRatio = (float)graphicsDevice.Viewport.Width / (float)graphicsDevice.Viewport.Height;
			Projection = Matrix.CreatePerspectiveFieldOfView(MathHelper.PiOver4, aspectRatio, 0.5f, 1000);
		}

		public Vector3 Position 
		{
			set
			{
				_cameraPosition = value;
				View = Matrix.CreateLookAt(_cameraPosition, _cameraTarget, _cameraUpVector); 
			}

			get
			{
				return _cameraPosition;
			}
		}
		
	}
}
