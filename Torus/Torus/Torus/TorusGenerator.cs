using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;

namespace Torus
{
	public class TorusGenerator
	{
		public Vector3 GetPoint(double r0, double r1, double theta, double phi)
		{
			double x = Math.Cos(theta) * ( r0 + r1 * Math.Cos(phi) );
			double y = Math.Sin(theta) * ( r0 + r1 * Math.Cos(phi) );
			double z = r1 * Math.Sin(phi);
			return new Vector3((float)x, (float)y, (float)z);
		}
	}
}
