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

                //Obj_AI_Minion.OnCreate += (obj, args) => OnCreateObj_jayceshockblast(obj, args, hero, spellData);
                //Obj_AI_Hero.OnProcessSpellCast += OnProcessSpell_jayceshockblast;
                //SpellDetector.OnProcessSpecialSpell += ProcessSpell_jayceshockblast;
            }*/
        }

        private static void OnCreateObj_jayceshockblast(GameObject obj, EventArgs args, Obj_AI_Hero hero, SpellData spellData)
        {
            if (obj.Type == GameObjectType.obj_GeneralParticleEmitter
                && obj.Name.Contains("Jayce")
                && obj.Name.Contains("accel_gate_end")
                && obj.Name.Contains("RED"))
            {
                foreach (var tracker in ObjectTracker.objTracker)
                {
                    var gateObj = tracker.Value;

                    if (gateObj.Name == "AccelGate")
                    {
                        DelayAction.Add(0, () => ObjectTracker.objTracker.Remove(tracker.Key));
                    }
                }
            }

            if (obj.Type == GameObjectType.obj_GeneralParticleEmitter
                && obj.Name.Contains("Jayce")
                && obj.Name.Contains("accel_gate_start")
                && obj.Name.Contains("RED"))
            {
                //var particle = obj as Obj_GeneralParticleEmitter;                

                /*var dir = obj.Orientation.To2D();
                var pos1 = obj.Position.To2D() - dir * 470;
                var pos2 = obj.Position.To2D() + dir * 470;

                //Draw.RenderObjects.Add(new Draw.RenderLine(pos1, pos2, 3500));
                Draw.RenderObjects.Add(new Draw.RenderCircle(pos1, 3500));*/

                var gateTracker = new ObjectTrackerInfo(obj, "AccelGate");
                //gateTracker.direction = dir.To3D();

                ObjectTracker.objTracker.Add(obj.NetworkId, gateTracker);

                foreach (var entry in SpellDetector.spells) //check currently moving skillshot
                {
                    var spell = entry.Value;

                    if (spell.info.spellName == "JayceShockBlast")
                    {
                        var tHero = spell.heroID;

                        Vector2 int1, int2;
                        var intersection =
                            MathUtils.FindLineCircleIntersections(obj.Position.To2D(), 470, spell.startPos, spell.endPos,
                            out int1, out int2);
                        var projection = obj.Position.To2D().ProjectOn(spell.startPos, spell.endPos);

                        //var intersection = spell.startPos.Intersection(spell.endPos, pos1, pos2);
                        //var projection = intersection.Point.ProjectOn(spell.startPos, spell.endPos);

                        //if (intersection.Intersects && projection.IsOnSegment)
                        if (intersection > 0 && projection.IsOnSegment)
                        {
                            SpellDetector.CreateSpellData(hero, projection.SegmentPoint.To3D(), spell.endPos.To3D(), 
                                spellData, spell.spellObject);
                            
                            DelayAction.Add(1, () => SpellDetector.DeleteSpell(entry.Key));
                            break;
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

        private static void ProcessSpell_jayceshockblast(Obj_AI_Base hero, GameObjectProcessSpellCastEventArgs args, SpellData spellDataOld, SpecialSpellEventArgs specialSpellArgs)
        {
            if (spellDataOld.spellName == "jayceshockblast")
            {
                SpellData spellData;

                if (SpellDetector.onProcessSpells.TryGetValue("JayceShockBlastWall", out spellData))
                {
                    foreach (var tracker in ObjectTracker.objTracker)
                    {
                        var gateObj = tracker.Value;

                        if (gateObj.Name == "AccelGate")
                        {
                            if (gateObj.obj == null)
                            {
                                DelayAction.Add(0, () => ObjectTracker.objTracker.Remove(tracker.Key));
                            }
                            else
                            {
                                var startPos = args.Start.To2D();
                                var endPos = args.End.To2D();
                                var dir = (endPos - startPos).Normalized();
                                endPos = startPos + dir * spellDataOld.range;

                                var obj = gateObj.obj;

                                Vector2 int1, int2;
                                var intersection =
                                    MathUtils.FindLineCircleIntersections(obj.Position.To2D(), 470, startPos, endPos,
                                    out int1, out int2);
                                var projection = obj.Position.To2D().ProjectOn(startPos, endPos);

                                if (intersection > 0 && projection.IsOnSegment)
                                {
                                    SpellDetector.CreateSpellData(hero, startPos.To3D(), endPos.To3D(), spellData);

                                    specialSpellArgs.noProcess = true;
                                }
                            }
                        }
                    }
                }
            }
        }
    }
}
