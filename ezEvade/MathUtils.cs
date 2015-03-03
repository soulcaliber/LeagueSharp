using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using SharpDX;

namespace ezEvade
{
    class MathUtils
    {  
        
        public static bool CheckLineIntersection(Vector2 a, Vector2 b, Vector2 c, Vector2 d)
        {
            Tuple<float, float> ret = LineToLineIntersection(a.X, a.Y, b.X, b.Y, c.X, c.Y, d.X, d.Y);

            var t1 = ret.Item1;
            var t2 = ret.Item2;

            if(t1 >= 0 && t1 <= 1 && t2 >= 0 && t2 <= 1){
                return true;
            }else{
                return false;
            }            
        }

        public static Vector2 RotateVector(Vector2 start, Vector2 end, float angle)
        {
            angle = angle * ((float)(Math.PI / 180));
            Vector2 ret = end;
            ret.X = ((float)Math.Cos(angle) * (end.X - start.X) - 
                (float)Math.Sin(angle) * (end.Y - start.Y) + start.X);
            ret.Y = ((float)Math.Sin(angle) * (end.X - start.X) +
                (float)Math.Cos(angle) * (end.Y - start.Y) + start.Y);
            return ret;
        }

        public static Tuple<float, float> LineToLineIntersection(float x1, float y1, float x2, float y2, float x3, float y3, float x4, float y4)
        {
            var d = (y4 - y3) * (x2 - x1) - (x4 - x3) * (y2 - y1);

            if (d == 0)
            {
                return Tuple.Create(float.MaxValue, float.MaxValue); //lines are parallel or coincidental
            }
            else
            {
                return Tuple.Create(((x4 - x3) * (y1 - y3) - (y4 - y3) * (x1 - x3)) / d,
                    ((x2 - x1) * (y1 - y3) - (y2 - y1) * (x1 - x3)) / d);
            }
        }
        
        //https://code.google.com/p/xna-circle-collision-detection/downloads/detail?name=Circle%20Collision%20Example.zip&can=2&q=

        public static float GetCollisionTime(Vector2 Pa, Vector2 Pb, Vector2 Va, Vector2 Vb, float Ra, float Rb, out bool collision)
        {
            Vector2 Pab = Pa - Pb;
            Vector2 Vab = Va - Vb;
            float a = Vector2.Dot(Vab, Vab);
            float b = 2 * Vector2.Dot(Pab, Vab);
            float c = Vector2.Dot(Pab, Pab) - (Ra + Rb) * (Ra + Rb);

            float discriminant = b * b - 4 * a * c;

            float t;
            if (discriminant < 0)
            {
                t = -b / (2 * a);
                collision = false;
            }
            else
            {
                float t0 = (-b + (float)Math.Sqrt(discriminant)) / (2 * a);
                float t1 = (-b - (float)Math.Sqrt(discriminant)) / (2 * a);
                t = Math.Min(t0, t1);

                if (t < 0)
                    collision = false;
                else
                    collision = true;
            }

            if (t < 0)
                t = 0;

            return t;
        }
    }
}
