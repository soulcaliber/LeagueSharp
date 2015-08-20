using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using LeagueSharp;
using LeagueSharp.Common;
using SharpDX;

namespace ezEvade.SpecialSpells
{
    class Lulu : ChampionPlugin
    {
        static Lulu()
        {

        }

        public void LoadSpecialSpell(SpellData spellData)
        {
            if (spellData.spellName == "LuluQ")
            {
                SpellDetector.OnProcessSpecialSpell += ProcessSpell_LuluQ;
                GetLuluPix();
            }
        }

        private static void GetLuluPix()
        {
            bool gotObj = false;

            foreach (var obj in ObjectManager.Get<Obj_AI_Minion>())
            {
                if (obj != null && obj.IsValid && obj.BaseSkinName == "lulufaerie" && obj.IsEnemy)
                {
                    gotObj = true;

                    if (!ObjectTracker.objTracker.ContainsKey(obj.NetworkId))
                    {
                        ObjectTracker.objTracker.Add(obj.NetworkId, new ObjectTrackerInfo(obj, "RobotBuddy"));
                    }
                }
            }

            if (gotObj == false)
            {
                DelayAction.Add(5000, () => GetLuluPix());
            }
        }

        private static void ProcessSpell_LuluQ(Obj_AI_Base hero, GameObjectProcessSpellCastEventArgs args, SpellData spellData,
            SpecialSpellEventArgs specialSpellArgs)
        {
            if (spellData.spellName == "LuluQ")
            {
                foreach (KeyValuePair<int, ObjectTrackerInfo> entry in ObjectTracker.objTracker)
                {
                    var info = entry.Value;

                    if (entry.Value.Name == "RobotBuddy")
                    {
                        if (info.obj == null || !info.obj.IsValid || info.obj.IsDead || info.obj.IsVisible)
                        {
                            continue;
                        }
                        else
                        {
                            Vector3 endPos2 = info.obj.Position.Extend(args.End, spellData.range);
                            SpellDetector.CreateSpellData(hero, info.obj.Position, endPos2, spellData, null, 0, false);
                        }
                    }
                }
            }
        }
    }
}
