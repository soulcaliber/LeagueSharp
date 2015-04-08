using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using LeagueSharp;
using LeagueSharp.Common;
using SharpDX;

namespace ezEvade
{
    public class SpecialSpellEventArgs : EventArgs
    {
        public bool Process { get; set; }
    }

    class SpecialSpells
    {

        private static Obj_AI_Hero myHero { get { return ObjectManager.Player; } }

        public static Dictionary<string, bool> pDict = new Dictionary<string, bool>();

        public static Dictionary<int, GameObject> objTracker = new Dictionary<int, GameObject>();

        public static void LoadSpecialSpell(SpellData spellData)
        {
            if (spellData.isThreeWay && !pDict.ContainsKey("ProcessSpell_ProcessThreeWay"))
            {
                SpellDetector.OnProcessSpecialSpell += ProcessSpell_ThreeWay;
                pDict["ProcessSpell_ProcessThreeWay"] = true;
            }

            if (spellData.spellName == "ZiggsQ" && !pDict.ContainsKey("ProcessSpell_ProcessZiggsQ"))
            {
                SpellDetector.OnProcessSpecialSpell += ProcessSpell_ZiggsQ;
                pDict["ProcessSpell_ProcessZiggsQ"] = true;
            }

            if (spellData.spellName == "ZedShuriken" && !pDict.ContainsKey("ProcessSpell_ProcessZedShuriken"))
            {
                SpellDetector.OnProcessSpecialSpell += ProcessSpell_ZedShuriken;
                Obj_AI_Minion.OnCreate += OnCreateObj_ZedShuriken;
                Obj_AI_Minion.OnDelete += OnDeleteObj_ZedShuriken;
                pDict["ProcessSpell_ProcessZedShuriken"] = true;
            }
        }

        private static void OnCreateObj_ZedShuriken(GameObject obj, EventArgs args)
        {
            if (obj.Name == "Shadow" && obj.IsEnemy)
            {
                if (!objTracker.ContainsKey(obj.NetworkId))
                    objTracker.Add(obj.NetworkId, obj);
            }
        }

        private static void OnDeleteObj_ZedShuriken(GameObject obj, EventArgs args)
        {
            if (obj.Name == "Shadow" && obj.IsEnemy)
            {
                objTracker.Remove(obj.NetworkId);
            }
        }

        private static void ProcessSpell_ZedShuriken(Obj_AI_Base hero, GameObjectProcessSpellCastEventArgs args, SpellData spellData,
            SpecialSpellEventArgs specialSpellArgs)
        {
            if (spellData.spellName == "ZedShuriken")
            {
                foreach (KeyValuePair<int, GameObject> entry in objTracker)
                {
                    if (entry.Value.IsDead)
                    {
                        Utility.DelayAction.Add(1, () => objTracker.Remove(entry.Value.NetworkId));                        
                        continue;
                    }

                    if (entry.Value.Name == "Shadow")
                    {
                        Obj_AI_Minion obj = (Obj_AI_Minion)entry.Value;

                        Vector3 endPos2 = obj.Position.Extend(args.End, spellData.range);
                        SpellDetector.CreateSpellData(hero, obj.Position, endPos2, spellData, null, 0, false);
                    }
                }
            }
        }

        private static void ProcessSpell_ThreeWay(Obj_AI_Base hero, GameObjectProcessSpellCastEventArgs args, SpellData spellData,
            SpecialSpellEventArgs specialSpellArgs)
        {
            if (spellData.isThreeWay)
            {
                Vector3 endPos2 = MathUtils.RotateVector(args.Start.To2D(), args.End.To2D(), spellData.angle).To3D();
                SpellDetector.CreateSpellData(hero, args.Start, endPos2, spellData, null, 0, false);

                Vector3 endPos3 = MathUtils.RotateVector(args.Start.To2D(), args.End.To2D(), -spellData.angle).To3D();
                SpellDetector.CreateSpellData(hero, args.Start, endPos3, spellData, null, 0, false);
            }
        }

        private static void ProcessSpell_ZiggsQ(Obj_AI_Base hero, GameObjectProcessSpellCastEventArgs args, SpellData spellData,
            SpecialSpellEventArgs specialSpellArgs)
        {
            if (spellData.spellName == "ZiggsQ")
            {
                var startPos = hero.ServerPosition.To2D();
                var endPos = args.End.To2D();
                var dir = (endPos - startPos).Normalized();

                if (endPos.Distance(startPos) > 850)
                {
                    endPos = startPos + dir * 850;
                }

                SpellDetector.CreateSpellData(hero, args.Start, endPos.To3D(), spellData, null, 0, false);

                var endPos2 = endPos + dir * 0.4f * startPos.Distance(endPos);
                SpellDetector.CreateSpellData(hero, args.Start, endPos2.To3D(), spellData, null, 250, false);

                var endPos3 = endPos2 + dir * 0.6f * endPos.Distance(endPos2);
                SpellDetector.CreateSpellData(hero, args.Start, endPos3.To3D(), spellData, null, 800, true);

                specialSpellArgs.Process = false;
            }
        }

    }
}
