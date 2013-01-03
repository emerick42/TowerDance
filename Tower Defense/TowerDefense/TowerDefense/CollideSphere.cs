using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;

namespace TowerDance
{
    public class CollideSphere
    {
        public float X;
        public float Y;
        public float range;

        public CollideSphere()
        {}

        public CollideSphere(float newX, float newY, float newRange)
        {
            X = newX;
            Y = newY;
            range = newRange;
        }

        public bool Intersect(CollideSphere sphere)
        {
            Point d;
            d.X = Math.Abs((int)(X - sphere.X));
            d.Y = Math.Abs((int)(Y - sphere.Y));
            int dist2 = (d.X * d.X) + (d.Y * d.Y);

            float radiusSum = range;

            if (dist2 <= radiusSum * radiusSum)
                return true;
            else
                return false;
        }
    }
}
