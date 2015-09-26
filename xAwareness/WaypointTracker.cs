using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using LeagueSharp;
using LeagueSharp.Common;
using SharpDX;

namespace xAwareness
{
    static class WaypointTracker
    {
        public static Dictionary<int, List<Vector2>> paths = new Dictionary<int, List<Vector2>>();
        public static Dictionary<int, float> startTick = new Dictionary<int, float>();

        static WaypointTracker()
        {
            Obj_AI_Hero.OnNewPath += Hero_OnNewPath;

            Initialize();
            
        }

        private static void Initialize()
        {
            foreach (var hero in HeroManager.AllHeroes)
            {
                paths.Add(hero.NetworkId, new List<Vector2>());
                startTick.Add(hero.NetworkId, 0);
            }
        }

        private static void Hero_OnNewPath(Obj_AI_Base sender, GameObjectNewPathEventArgs args)
        {
            if (args.IsDash)
            {
                return;
            }

            var hero = sender as Obj_AI_Hero;
            if (hero != null && args.Path != null && args.Path.Count() > 0)
            {
                paths[hero.NetworkId] = args.Path.ToList().To2D();
                startTick[hero.NetworkId] = HelperUtils.TickCount;
            }
        }

        public static Vector2 GetHeroCurrentPosition(Obj_AI_Hero hero)
        {
            var path = paths[hero.NetworkId];
            var timePassed = (HelperUtils.TickCount - startTick[hero.NetworkId]) / 1000f;
            if (path.Count() > 0 && path.PathLength() >= hero.MoveSpeed * timePassed)
            {
                return CutPath(path, (int)(hero.MoveSpeed * timePassed)).FirstOrDefault();
            }

            return Vector2.Zero;
        }

        public static Vector2 GetLastPathPosition(Obj_AI_Hero hero)
        {
            var path = paths[hero.NetworkId];
            if (path.Count() > 0)
            {
                return path.Last();
            }

            return Vector2.Zero;
        }

        public static List<Vector2> CutPath(this List<Vector2> path, float distance)
        {
            var result = new List<Vector2>();
            var Distance = distance;
            if (distance < 0)
            {
                path[0] = path[0] + distance * (path[1] - path[0]).Normalized();
                return path;
            }

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
    }
}
