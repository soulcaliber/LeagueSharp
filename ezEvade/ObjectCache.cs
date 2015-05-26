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
    public static class ObjectCache
    {
        public static Dictionary<int, Obj_AI_Turret> turrets = new Dictionary<int, Obj_AI_Turret>();

        private static Obj_AI_Hero myHero { get { return ObjectManager.Player; } }

        static ObjectCache()
        {
            InitializeCache();
        }

        private static void InitializeCache()
        {
            foreach (var obj in ObjectManager.Get<Obj_AI_Turret>())
            {
                if (!turrets.ContainsKey(obj.NetworkId))
                {
                    turrets.Add(obj.NetworkId, obj);
                }
            }
        }
    }
}
