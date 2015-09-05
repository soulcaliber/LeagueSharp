using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using LeagueSharp;
using LeagueSharp.Common;
using SharpDX;

namespace ezEvade
{
    public static class EvadeUtils
    {
        public static Random random = new Random(DateTime.Now.Millisecond);
        private static DateTime assemblyLoadTime = DateTime.Now;

        static EvadeUtils()
        {

        }

        public static float TickCount
        {
            get
            {
                return (int)DateTime.Now.Subtract(assemblyLoadTime).TotalMilliseconds;
            }
        }

        public static List<Vector2> PathToVector2(this Vector3[] path)
        {
            return path.Select(p => p.To2D()).ToList();
        }

        public static List<Vector2> PathToVector2(this List<Vector3> path)
        {
            return path.Select(p => p.To2D()).ToList();
        }

        public static Vector2 GetGamePosition(Obj_AI_Hero hero, float delay = 0)
        {
            if (hero != null)
            {
                if (hero.IsMoving)
                {
                    var path = new List<Vector2>();
                    path.Add(hero.ServerPosition.To2D());
                    path.AddRange(hero.Path.Select(point => point.To2D()));

                    var finalPath = EvadeUtils.CutPath(path, hero, delay);

                    return finalPath.Last();
                }

                return hero.ServerPosition.To2D();
            }

            return Vector2.Zero;
        }

        public static List<Vector2> CutPath(this List<Vector2> path, Obj_AI_Base unit, float delay, float speed = 0)
        {
            if (speed == 0)
            {
                speed = unit.MoveSpeed;
            }

            var dist = speed * delay / 1000;
            return CutPath(path, dist);
        }

        public static List<Vector2> CutPathPrev(this List<Vector2> path, Obj_AI_Base unit, float delay)
        {
            var dist = unit.MoveSpeed * delay / 1000;
            return CutPathPrev(path, dist);
        }

        public static List<Vector2> CutPath(this List<Vector2> path, float distance)
        {
            var result = new List<Vector2>();
            var Distance = distance;

            if (path.Count > 0)
            {
                result.Add(path.First());
            }
            
            for (var i = 0; i < path.Count - 1; i++)
            {
                var dist = path[i].Distance(path[i + 1]);
                if (dist > Distance)
                {
                    result.Add(path[i] + Distance * (path[i + 1] - path[i]).Normalized());
                    break;
                }
                else
                {
                    result.Add(path[i + 1]);
                }
                Distance -= dist;
            }
            return result.Count > 0 ? result : new List<Vector2> { path.Last() };
        }

        public static List<Vector2> CutPathPrev(this List<Vector2> path, float distance)
        {
            var result = new List<Vector2>();
            var Distance = distance;

            for (var i = 0; i < path.Count - 1; i++)
            {
                var dist = path[i].Distance(path[i + 1]);
                if (dist > Distance)
                {
                    result.Add(path[i] + Distance * (path[i + 1] - path[i]).Normalized());

                    for (var j = i + 1; j < path.Count; j++)
                    {
                        result.Add(path[j]);
                    }

                    break;
                }
                Distance -= dist;
            }
            return result.Count > 0 ? result : new List<Vector2> { path.Last() };
        }

        public static Vector2 ExtendDir(this Vector2 pos, Vector2 dir, float distance)
        {
            return pos + dir * distance;
        }

        public static Vector3 ExtendDir(this Vector3 pos, Vector3 dir, float distance)
        {
            return pos + dir * distance;
        }
    }
}
