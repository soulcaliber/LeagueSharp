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
    class Jayce : ChampionPlugin
    {
        static Jayce()
        {

        }

        public void LoadSpecialSpell(SpellData spellData)
        {
            /*if (spellData.spellName == "JayceShockBlastWall")
            {
                Obj_AI_Hero hero = HeroManager.Enemies.FirstOrDefault(h => h.ChampionName == "Jayce");
                if (hero == null)
                {
                    return;
                }

                Obj_AI_Minion.OnCreate += (obj, args) => OnCreateObj_jayceshockblast(obj, args, hero, spellData);
                //Obj_AI_Hero.OnProcessSpellCast += OnProcessSpell_jayceshockblast;
                //SpellDetector.OnProcessSpecialSpell += ProcessSpell_jayceshockblast;
            }*/
        }

        private static void OnCreateObj_jayceshockblast(GameObject obj, EventArgs args, Obj_AI_Hero hero, SpellData spellData)
        {
            return;

            if (obj.IsEnemy && obj.Type == GameObjectType.obj_GeneralParticleEmitter
                && obj.Name.Contains("Jayce") && obj.Name.Contains("accel_gate_start"))
            {
                var dir = ObjectTracker.GetLastHiuOrientation();
                var pos1 = obj.Position.To2D() - dir * 470;
                var pos2 = obj.Position.To2D() + dir * 470;

                var gateTracker = new ObjectTrackerInfo(obj, "AccelGate");
                gateTracker.direction = dir.To3D();

                ObjectTracker.objTracker.Add(obj.NetworkId, gateTracker);

                foreach (var entry in SpellDetector.spells)
                {
                    var spell = entry.Value;

                    if (spell.info.spellName == "jayceshockblast")
                    {
                        var tHero = spell.heroID;

                        var intersection = spell.startPos.Intersection(spell.endPos, pos1, pos2);
                        var projection = intersection.Point.ProjectOn(spell.startPos, spell.endPos);

                        if (intersection.Intersects && projection.IsOnSegment)
                        {
                            SpellDetector.CreateSpellData(hero, intersection.Point.To3D(), spell.endPos.To3D(), spellData, spell.spellObject);

                            DelayAction.Add(1, () => SpellDetector.DeleteSpell(entry.Key));
                        }
                    }
                }

                //SpellDetector.CreateSpellData(hero, pos1.To3D(), pos2.To3D(), spellData, null, 0);                               
            }
        }

        private static void OnProcessSpell_jayceshockblast(Obj_AI_Base sender, GameObjectProcessSpellCastEventArgs args)
        {
            if (sender.IsEnemy && args.SData.Name == "jayceaccelerationgate")
            {
                //ObjectTracker.objTracker.Add(obj.NetworkId, new ObjectTrackerInfo(obj, "RobotBuddy"));

                //AddObjectTracker.objTrackerPosition
            }
        }

        private static void ProcessSpell_jayceshockblast(Obj_AI_Base hero, GameObjectProcessSpellCastEventArgs args, SpellData spellData, SpecialSpellEventArgs specialSpellArgs)
        {
            if (spellData.spellName == "jayceshockblast")
            {
            }
        }
    }
}
