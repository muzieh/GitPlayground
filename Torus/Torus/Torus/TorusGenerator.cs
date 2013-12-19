using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace Torus
{
	public class TorusGenerator
	{

        public VertexPositionNormalTexture[] Generate(int thetaParts, int phiParts)
		{
			double r0 = 2;
			double r1 = 1;
			double theta = 0;
			double phi = 0;
            double thetaDelta = 2.0 * MathHelper.Pi / thetaParts;
            double phiDelta = 2.0 * MathHelper.Pi / phiParts;

			var list = new List<VertexPositionNormalTexture>();

			do
			{
					
					var rotMatrix = Matrix.CreateRotationZ((float)theta);
					var center = Vector3.Transform(new Vector3((float)r0, 0, 0), rotMatrix);
				phi = 0.0;
				do
				{
					var point = GetPoint(r0, r1, theta, phi);
				    var normal = Vector3.Normalize(point-center);
					list.Add(new VertexPositionNormalTexture(point, normal, Vector2.Zero));
					phi += phiDelta;
				} while (phi < MathHelper.Pi * 2.0);
				theta += thetaDelta;
			    
			} while (theta < MathHelper.Pi * 2.0);

			return list.ToArray();
		}

		public Vector3 GetPoint(double r0, double r1, double theta, double phi)
		{
			double x = Math.Cos(theta) * ( r0 + r1 * Math.Cos(phi) );
			double y = Math.Sin(theta) * ( r0 + r1 * Math.Cos(phi) );
			double z = r1 * Math.Sin(phi);
			return new Vector3((float)x, (float)y, (float)z);
		}
	}
}
