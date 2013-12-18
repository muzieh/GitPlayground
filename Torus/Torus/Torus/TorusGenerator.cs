using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;

namespace Torus
{
	public class TorusGenerator
	{
		public Vector3[] Generate()
		{
			double r0 = 2;
			double r1 = 0.4;
			double theta = 0;
			double phi = 0;
			double thetaDelta = 2.0 * MathHelper.Pi / 200.0;
			double phiDelta = 2.0 * MathHelper.Pi / 200.0;

			var list = new List<Vector3>();

			do
			{
				phi = 0.0;
				do
				{
					var point = GetPoint(r0, r1, theta, phi);
					list.Add(point);
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
