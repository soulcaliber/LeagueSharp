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
        public bool noProcess { get; set; }
    }

    public class ObjectTrackerInfo
    {
        public GameObject obj;
        public Vector3 position;
        public string Name;
        public int OwnerNetworkID;
        public bool usePosition = false;

        public ObjectTrackerInfo(GameObject obj)
        {
            this.obj = obj;
            this.Name = obj.Name;
        }
    }

    class SpecialSpells
    {

        private static Obj_AI_Hero myHero { get { return ObjectManager.Player; } }

        public static Dictionary<string, bool> pDict = new Dictionary<string, bool>();

        public static Dictionary<int, ObjectTrackerInfo> objTracker = new Dictionary<int, ObjectTrackerInfo>();

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
                Obj_SpellMissile.OnCreate += SpellMissile_ZedShadowDash;
                Obj_AI_Minion.OnCreate += OnCreateObj_ZedShuriken;
                Obj_AI_Minion.OnDelete += OnDeleteObj_ZedShuriken;
                pDict["ProcessSpell_ProcessZedShuriken"] = true;
            }

            if (spellData.spellName == "LuluQ" && !pDict.ContainsKey("ProcessSpell_LuluQ"))
            {
                SpellDetector.OnProcessSpecialSpell += ProcessSpell_LuluQ;
                GetLuluPix();
                pDict["ProcessSpell_LuluQ"] = true;
            }

            if (spellData.spellName == "OrianaIzunaCommand" && !pDict.ContainsKey("ProcessSpell_OrianaIzunaCommand"))
            {
                foreach (var hero in HeroManager.Enemies)
                {
                    if (hero.ChampionName == "Orianna")
                    {
                        ObjectTrackerInfo info = new ObjectTrackerInfo(hero);
                        info.Name = "TheDoomBall";
                        info.OwnerNetworkID = hero.NetworkId;

                        objTracker.Add(hero.NetworkId, info);
                    }
                }


                Obj_AI_Minion.OnCreate += OnCreateObj_OrianaIzunaCommand;
                Obj_AI_Minion.OnDelete += OnDeleteObj_OrianaIzunaCommand;
                Obj_AI_Hero.OnProcessSpellCast += ProcessSpell_OrianaRedactCommand;
                SpellDetector.OnProcessSpecialSpell += ProcessSpell_OrianaIzunaCommand;

                pDict["ProcessSpell_OrianaIzunaCommand"] = true;
            }

            if (spellData.spellName == "AlZaharCalloftheVoid" && !pDict.ContainsKey("ProcessSpell_AlZaharCalloftheVoid"))
            {
                SpellDetector.OnProcessSpecialSpell += ProcessSpell_AlZaharCalloftheVoid;
                pDict["ProcessSpell_AlZaharCalloftheVoid"] = true;
            }

            if (spellData.spellName == "LucianQ" && !pDict.ContainsKey("ProcessSpell_LucianQ"))
            {
                SpellDetector.OnProcessSpecialSpell += ProcessSpell_LucianQ;
                pDict["ProcessSpell_LucianQ"] = true;
            }
        }

        private static void ProcessSpell_LucianQ(Obj_AI_Base hero, GameObjectProcessSpellCastEventArgs args, SpellData spellData,
            SpecialSpellEventArgs specialSpellArgs)
        {
            if (spellData.spellName == "LucianQ")
            {

                if (args.Target.IsValid<Obj_AI_Hero>())
                {
                    var target = args.Target as Obj_AI_Hero;

                    float spellDelay = ((float)(350 - Game.Ping))/1000;
                    var heroWalkDir = (target.ServerPosition - target.Position).Normalized();
                    var predictedHeroPos = target.Position + heroWalkDir * target.MoveSpeed * (spellDelay);


                    SpellDetector.CreateSpellData(hero, args.Start, predictedHeroPos, spellData, null, 0);

                    specialSpellArgs.noProcess = true;
                }                
            }
        }

        private static void ProcessSpell_AlZaharCalloftheVoid(Obj_AI_Base hero, GameObjectProcessSpellCastEventArgs args, SpellData spellData,
            SpecialSpellEventArgs specialSpellArgs)
        {
            if (spellData.spellName == "AlZaharCalloftheVoid")
            {
                var direction = (args.End.To2D() - args.Start.To2D()).Normalized();
                var pDirection = direction.Perpendicular();
                var targetPoint = args.End.To2D();

                var pos1 = targetPoint - pDirection * spellData.sideRadius;
                var pos2 = targetPoint + pDirection * spellData.sideRadius;

                SpellDetector.CreateSpellData(hero, pos1.To3D(), pos2.To3D(), spellData, null, 0, false);
                SpellDetector.CreateSpellData(hero, pos2.To3D(), pos1.To3D(), spellData, null, 0);

                specialSpellArgs.noProcess = true;
            }
        }

        private static void OnCreateObj_OrianaIzunaCommand(GameObject obj, EventArgs args)
        {
            if (obj.Name == "Orianna_Ball_Flash_Reverse.troy" && obj.IsEnemy)
            {
                foreach (KeyValuePair<int, ObjectTrackerInfo> entry in objTracker)
                {
                    var info = entry.Value;

                    if (entry.Value.Name == "TheDoomBall")
                    {
                        info.usePosition = false;

                        foreach (var hero in HeroManager.Enemies)
                        {
                            if (hero.ChampionName == "Orianna")
                            {
                                info.obj = hero;
                            }
                        }
                    }
                }
            }
        }

        private static void OnDeleteObj_OrianaIzunaCommand(GameObject obj, EventArgs args)
        {
            if (obj.Name == "oriana_ball_glow_red.troy" && obj.IsEnemy)
            {
                foreach (KeyValuePair<int, ObjectTrackerInfo> entry in objTracker)
                {
                    var info = entry.Value;

                    if (entry.Value.Name == "TheDoomBall")
                    {
                        info.usePosition = false;

                        foreach (var hero in HeroManager.Enemies)
                        {
                            if (hero.ChampionName == "Orianna")
                            {
                                info.obj = hero;
                            }
                        }
                    }
                }
            }
        }

        private static void ProcessSpell_OrianaRedactCommand(Obj_AI_Base hero, GameObjectProcessSpellCastEventArgs args)
        {
            if (!hero.IsValid<Obj_AI_Hero>())
                return;

            var champ = (Obj_AI_Hero)hero;

            if (champ.ChampionName == "Orianna" && champ.IsEnemy)
            {
                if (args.SData.Name == "OrianaRedactCommand")
                {
                    foreach (KeyValuePair<int, ObjectTrackerInfo> entry in objTracker)
                    {
                        var info = entry.Value;

                        if (entry.Value.Name == "TheDoomBall")
                        {
                            info.usePosition = false;
                            info.obj = args.Target;
                        }
                    }
                }
            }
        }

        private static void ProcessSpell_OrianaIzunaCommand(Obj_AI_Base hero, GameObjectProcessSpellCastEventArgs args, SpellData spellData,
            SpecialSpellEventArgs specialSpellArgs)
        {
            if (spellData.spellName == "OrianaIzunaCommand")
            {
                foreach (KeyValuePair<int, ObjectTrackerInfo> entry in objTracker)
                {
                    var info = entry.Value;

                    if (entry.Value.Name == "TheDoomBall")
                    {
                        if (info.usePosition)
                        {
                            SpellDetector.CreateSpellData(hero, info.position, args.End, spellData, null, 0);
                        }
                        else
                        {
                            if (info.obj == null)
                                return;

                            SpellDetector.CreateSpellData(hero, info.obj.Position, args.End, spellData, null, 0);
                        }

                        specialSpellArgs.noProcess = true;

                        info.position = args.End;
                        info.usePosition = true;
                    }
                }
            }

            if (spellData.spellName == "OrianaDetonateCommand" || spellData.spellName == "OrianaDissonanceCommand")
            {
                foreach (KeyValuePair<int, ObjectTrackerInfo> entry in objTracker)
                {
                    var info = entry.Value;

                    if (entry.Value.Name == "TheDoomBall")
                    {
                        if (info.usePosition)
                        {
                            Vector3 endPos2 = info.position;
                            SpellDetector.CreateSpellData(hero, endPos2, endPos2, spellData, null, 0);
                        }
                        else
                        {
                            if (info.obj == null)
                                return;

                            Vector3 endPos2 = info.obj.Position;
                            SpellDetector.CreateSpellData(hero, endPos2, endPos2, spellData, null, 0);
                        }

                        specialSpellArgs.noProcess = true;
                    }
                }
            }
        }

        private static void GetLuluPix()
        {
            bool gotObj = false;

            foreach (var obj in ObjectManager.Get<Obj_AI_Minion>())
            {
                if (obj.Name == "RobotBuddy" && obj.IsEnemy)
                {
                    gotObj = true;

                    if (!objTracker.ContainsKey(obj.NetworkId))
                        objTracker.Add(obj.NetworkId, new ObjectTrackerInfo(obj));
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
                foreach (KeyValuePair<int, ObjectTrackerInfo> entry in objTracker)
                {
                    var info = entry.Value;

                    if (entry.Value.Name == "RobotBuddy")
                    {
                        if (info.obj == null || info.obj.IsDead)
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

        private static void OnCreateObj_ZedShuriken(GameObject obj, EventArgs args)
        {
            if (obj.Name == "Shadow" && obj.IsEnemy)
            {
                if (!objTracker.ContainsKey(obj.NetworkId))
                {
                    objTracker.Add(obj.NetworkId, new ObjectTrackerInfo(obj));

                    foreach (KeyValuePair<int, ObjectTrackerInfo> entry in objTracker)
                    {
                        var info = entry.Value;

                        if (info.Name == "Shadow" && info.usePosition && info.position.Distance(obj.Position) < 5)
                        {
                            info.usePosition = false;
                            info.obj = obj;
                        }
                    }
                }
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
                foreach (KeyValuePair<int, ObjectTrackerInfo> entry in objTracker)
                {
                    var info = entry.Value;

                    if (info.obj.Name == "Shadow" || info.Name == "Shadow")
                    {
                        if (info.usePosition == false && (info.obj == null || info.obj.IsDead))
                        {
                            DelayAction.Add(1, () => objTracker.Remove(info.obj.NetworkId));
                            continue;
                        }
                        else
                        {
                            Vector3 endPos2;
                            if (info.usePosition == false)
                            {
                                endPos2 = info.obj.Position.Extend(args.End, spellData.range);
                                SpellDetector.CreateSpellData(hero, info.obj.Position, endPos2, spellData, null, 0, false);
                            }
                            else
                            {
                                endPos2 = info.position.Extend(args.End, spellData.range);
                                SpellDetector.CreateSpellData(hero, info.position, endPos2, spellData, null, 0, false);
                            }

                        }
                    }
                }
            }
        }

        private static void SpellMissile_ZedShadowDash(GameObject obj, EventArgs args)
        {
            if (!obj.IsValid<Obj_SpellMissile>())
                return;

            Obj_SpellMissile missile = (Obj_SpellMissile)obj;

            if (missile.SpellCaster.IsEnemy && missile.SData.Name == "ZedShadowDashMissile")
            {
                if (!objTracker.ContainsKey(obj.NetworkId))
                {
                    ObjectTrackerInfo info = new ObjectTrackerInfo(obj);
                    info.Name = "Shadow";
                    info.OwnerNetworkID = missile.SpellCaster.NetworkId;
                    info.usePosition = true;
                    info.position = missile.EndPosition;

                    objTracker.Add(obj.NetworkId, info);

                    DelayAction.Add(1000, () => objTracker.Remove(obj.NetworkId));
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
                SpellDetector.CreateSpellData(hero, args.Start, endPos3.To3D(), spellData, null, 800);

                specialSpellArgs.noProcess = true;
            }
        }

    }
}
