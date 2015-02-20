using System;
using SharpDX;

namespace ezEvade
{
    internal class MathUtils
    {
        public static bool CheckLineIntersection(Vector2 a, Vector2 b, Vector2 c, Vector2 d)
        {
            var ret = LineToLineIntersection(a.X, a.Y, b.X, b.Y, c.X, c.Y, d.X, d.Y);

            var t1 = ret.Item1;
            var t2 = ret.Item2;

            return t1 >= 0 && t1 <= 1 && t2 >= 0 && t2 <= 1;
        }

        public static Tuple<float, float> LineToLineIntersection(float x1, float y1, float x2, float y2, float x3,
            float y3, float x4, float y4)
        {
            var d = (y4 - y3)*(x2 - x1) - (x4 - x3)*(y2 - y1);

            if (d == 0)
            {
                return Tuple.Create(float.MaxValue, float.MaxValue); // Lines are parallel or coincidental
            }

            return Tuple.Create(((x4 - x3)*(y1 - y3) - (y4 - y3)*(x1 - x3))/d,
                ((x2 - x1)*(y1 - y3) - (y2 - y1)*(x1 - x3))/d);
        }

        // https://code.google.com/p/xna-circle-collision-detection/downloads/detail?name=Circle%20Collision%20Example.zip&can=2&q=

        public static float GetCollisionTime(Vector2 pa, Vector2 pb, Vector2 va, Vector2 vb, float ra, float rb,
            out bool collision)
        {
            var pab = pa - pb;
            var vab = va - vb;
            var a = Vector2.Dot(vab, vab);
            var b = 2*Vector2.Dot(pab, vab);
            var c = Vector2.Dot(pab, pab) - (ra + rb)*(ra + rb);

            var discriminant = b*b - 4*a*c;

            float t;
            if (discriminant < 0)
            {
                t = -b/(2*a);
                collision = false;
            }
            else
            {
                var t0 = (-b + (float) Math.Sqrt(discriminant))/(2*a);
                var t1 = (-b - (float) Math.Sqrt(discriminant))/(2*a);
                t = Math.Min(t0, t1);

                collision = !(t < 0);
            }

            if (t < 0)
            {
                t = 0;
            }

            return t;
        }
    }
}