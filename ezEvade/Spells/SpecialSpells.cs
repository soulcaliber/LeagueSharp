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
        public Vector3 direction;
        public string Name;
        public int OwnerNetworkID;
        public bool usePosition = false;
        public float timestamp = 0;

        public ObjectTrackerInfo(GameObject obj)
        {
            this.obj = obj;
            this.Name = obj.Name;
            this.timestamp = Game.Time;
        }

        public ObjectTrackerInfo(GameObject obj, string name)
        {
            this.obj = obj;
            this.Name = name;
            this.timestamp = Game.Time;
        }

        public ObjectTrackerInfo(string name, Vector3 position)
        {
            this.Name = name;
            this.usePosition = true;
            this.position = position;
        }
    }

    class SpecialSpells
    {
        private static Obj_AI_Hero myHero { get { return ObjectManager.Player; } }

        public static Dictionary<string, bool> pDict = new Dictionary<string, bool>();

        public static Dictionary<int, ObjectTrackerInfo> objTracker = new Dictionary<int, ObjectTrackerInfo>();
        public static int objTrackerID = 0;

        public static void LoadSpecialSpell(SpellData spellData)
        {
            Obj_AI_Minion.OnCreate += HiuCreate_ObjectTracker;
            //Obj_AI_Minion.OnCreate += HiuDelete_ObjectTracker;

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
                MissileClient.OnCreate += SpellMissile_ZedShadowDash;
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
                Obj_AI_Hero hero = HeroManager.Enemies.FirstOrDefault(h => h.ChampionName == "Orianna");
                if (hero == null)
                {
                    return;
                }

                ObjectTrackerInfo info = new ObjectTrackerInfo(hero);
                info.Name = "TheDoomBall";
                info.OwnerNetworkID = hero.NetworkId;

                objTracker.Add(hero.NetworkId, info);

                Obj_AI_Minion.OnCreate += (obj, args) => OnCreateObj_OrianaIzunaCommand(obj, args, hero);
                Obj_AI_Minion.OnDelete += (obj, args) => OnDeleteObj_OrianaIzunaCommand(obj, args, hero);
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

            if (spellData.spellName == "LuxMaliceCannon" && !pDict.ContainsKey("ProcessSpell_LuxMaliceCannon"))
            {
                var hero = HeroManager.Enemies.FirstOrDefault(h => h.ChampionName == "Lux");
                if (hero != null)
                {
                    GameObject.OnCreate += (obj, args) => OnCreateObj_LuxMaliceCannon(obj, args, hero, spellData);
                }

                pDict["ProcessSpell_LuxMaliceCannon"] = true;
            }

            /*if (spellData.spellName == "JinxWMissile" && !pDict.ContainsKey("ProcessSpell_JinxWMissile"))
            {
                var hero = HeroManager.Enemies.FirstOrDefault(h => h.ChampionName == "Jinx");
                if (hero != null)
                {
                    GameObject.OnCreate += (obj, args) => OnCreateObj_JinxWMissile(obj, args, hero, spellData);
                }

                pDict["ProcessSpell_JinxWMissile"] = true;
            }*/

            if (spellData.spellName == "Volley" && !pDict.ContainsKey("ProcessSpell_AsheVolley"))
            {
                SpellDetector.OnProcessSpecialSpell += ProcessSpell_AsheVolley;
                pDict["ProcessSpell_AsheVolley"] = true;
            }

            if (spellData.spellName == "FizzPiercingStrike" && !pDict.ContainsKey("ProcessSpell_FizzPiercingStrike"))
            {
                SpellDetector.OnProcessSpecialSpell += ProcessSpell_FizzPiercingStrike;
                pDict["ProcessSpell_FizzPiercingStrike"] = true;
            }

            if (spellData.spellName == "FizzMarinerDoom" && !pDict.ContainsKey("ProcessSpell_FizzMarinerDoom"))
            {
                Obj_AI_Minion.OnCreate += (obj, args) => OnCreateObj_FizzMarinerDoom(obj, args, spellData);
                Obj_AI_Minion.OnDelete += (obj, args) => OnDeleteObj_FizzMarinerDoom(obj, args, spellData);

                pDict["ProcessSpell_FizzMarinerDoom"] = true;
            }

            /*if (spellData.spellName == "SionE" && !pDict.ContainsKey("ProcessSpell_SionE"))
            {
                SpellDetector.OnProcessSpecialSpell += ProcessSpell_SionE;
                pDict["ProcessSpell_SionE"] = true;
            }*/

            if (spellData.spellName == "EkkoR" && !pDict.ContainsKey("ProcessSpell_EkkoR"))
            {
                SpellDetector.OnProcessSpecialSpell += ProcessSpell_EkkoR;
                pDict["ProcessSpell_EkkoR"] = true;
            }

            if (spellData.spellName == "YasuoQW" && !pDict.ContainsKey("ProcessSpell_YasuoQW"))
            {
                Obj_AI_Hero hero = HeroManager.Enemies.FirstOrDefault(h => h.ChampionName == "Yasuo");
                if (hero == null)
                {
                    return;
                }

                Obj_AI_Hero.OnProcessSpellCast += (sender, args) => ProcessSpell_YasuoQW(sender, args, spellData);

                pDict["ProcessSpell_YasuoQW"] = true;
            }

            /*if (spellData.spellName == "JayceShockBlastWall" && !pDict.ContainsKey("ProcessSpell_JayceShockBlastWall"))
            {
                Obj_AI_Hero hero = HeroManager.Enemies.FirstOrDefault(h => h.ChampionName == "Jayce");
                if (hero == null)
                {
                    return;
                }

                Obj_AI_Minion.OnCreate += (obj, args) => OnCreateObj_jayceshockblast(obj, args, hero, spellData);
                //Obj_AI_Hero.OnProcessSpellCast += OnProcessSpell_jayceshockblast;
                //SpellDetector.OnProcessSpecialSpell += ProcessSpell_jayceshockblast;
                pDict["ProcessSpell_JayceShockBlastWall"] = true;
            }*/

            if (spellData.spellName == "ViktorDeathRay3" && !pDict.ContainsKey("ProcessSpell_ViktorDeathRay3"))
            {
                Obj_AI_Minion.OnCreate += OnCreateObj_ViktorDeathRay3;

                pDict["ProcessSpell_ViktorDeathRay3"] = true;
            }

            if (spellData.spellName == "xeratharcanopulse2" && !pDict.ContainsKey("ProcessSpell_XerathArcanopulse2"))
            {
                SpellDetector.OnProcessSpecialSpell += ProcessSpell_XerathArcanopulse2;
                pDict["ProcessSpell_XerathArcanopulse2"] = true;
            }

            if (spellData.spellName == "JarvanIVDragonStrike" && !pDict.ContainsKey("ProcessSpell_JarvanIVDragonStrike"))
            {
                Obj_AI_Hero hero = HeroManager.Enemies.FirstOrDefault(h => h.ChampionName == "JarvanIV");
                if (hero == null)
                {
                    return;
                }

                Obj_AI_Hero.OnProcessSpellCast += ProcessSpell_JarvanIVDemacianStandard;

                SpellDetector.OnProcessSpecialSpell += ProcessSpell_JarvanIVDragonStrike;
                Obj_AI_Minion.OnCreate += OnCreateObj_JarvanIVDragonStrike;
                Obj_AI_Minion.OnDelete += OnDeleteObj_JarvanIVDragonStriken;
                pDict["ProcessSpell_JarvanIVDragonStrike"] = true;
            }
        }

        private static void AddObjTrackerPosition(string name, Vector3 position, float timeExpires)
        {
            objTracker.Add(objTrackerID, new ObjectTrackerInfo(name, position));

            int trackerID = objTrackerID; //store the id for deletion
            DelayAction.Add((int)timeExpires, () => objTracker.Remove(objTrackerID));

            objTrackerID += 1;
        }

        private static void ProcessSpell_JarvanIVDemacianStandard(Obj_AI_Base hero, GameObjectProcessSpellCastEventArgs args)
        {
            if (hero.IsEnemy && args.SData.Name == "JarvanIVDemacianStandard")
            {
                AddObjTrackerPosition("Beacon", args.End, 1000);
            }
        }

        private static void OnDeleteObj_JarvanIVDragonStriken(GameObject obj, EventArgs args)
        {
            if (obj.Name == "Beacon")
            {
                objTracker.Remove(obj.NetworkId);
            }
        }

        private static void OnCreateObj_JarvanIVDragonStrike(GameObject obj, EventArgs args)
        {
            if (obj.Name == "Beacon")
            {
                objTracker.Add(obj.NetworkId, new ObjectTrackerInfo(obj));
            }
        }

        private static void ProcessSpell_JarvanIVDragonStrike(Obj_AI_Base hero, GameObjectProcessSpellCastEventArgs args, SpellData spellData, SpecialSpellEventArgs specialSpellArgs)
        {
            if (args.SData.Name == "JarvanIVDragonStrike")
            {
                if (SpellDetector.onProcessSpells.TryGetValue("JarvanIVDragonStrike2", out spellData))
                {
                    foreach (KeyValuePair<int, ObjectTrackerInfo> entry in objTracker)
                    {
                        var info = entry.Value;

                        if (info.Name == "Beacon" || info.obj.Name == "Beacon")
                        {
                            if (info.usePosition == false && (info.obj == null || !info.obj.IsValid || info.obj.IsDead))
                            {
                                DelayAction.Add(1, () => objTracker.Remove(info.obj.NetworkId));
                                continue;
                            }

                            var objPosition = info.usePosition ? info.position.To2D() : info.obj.Position.To2D();

                            if (args.End.To2D().Distance(objPosition) < 300)
                            {
                                var dir = (objPosition - args.Start.To2D()).Normalized();
                                var endPosition = objPosition + dir * 110;

                                SpellDetector.CreateSpellData(hero, args.Start, endPosition.To3D(), spellData);
                                specialSpellArgs.noProcess = true;
                                return;
                            }
                        }
                    }


                }
            }
        }

        private static void OnDeleteObj_FizzMarinerDoom(GameObject obj, EventArgs args, SpellData spellData)
        {
            //need to track where bait is attached to

            if (!obj.IsValid<MissileClient>())
                return;

            MissileClient missile = (MissileClient)obj;

            if (missile.SpellCaster != null && missile.SpellCaster.Team != myHero.Team &&
                missile.SData.Name == "FizzMarinerDoomMissile")
            {
                SpellDetector.CreateSpellData(missile.SpellCaster, missile.StartPosition, missile.EndPosition,
                spellData, null, 1000, true, SpellType.Circular, false, 350);
            }
        }

        private static void OnCreateObj_FizzMarinerDoom(GameObject obj, EventArgs args, SpellData spellData)
        {
            if (!obj.IsValid<MissileClient>())
                return;

            MissileClient missile = (MissileClient)obj;

            if (missile.SpellCaster != null && missile.SpellCaster.Team != myHero.Team &&
                missile.SData.Name == "FizzMarinerDoomMissile")
            {
                SpellDetector.CreateSpellData(missile.SpellCaster, missile.StartPosition, missile.EndPosition,
                spellData, null, 500, true, SpellType.Circular, false, spellData.secondaryRadius);

                /*foreach (KeyValuePair<int, Spell> entry in SpellDetector.spells)
                {
                    var spell = entry.Value;

                    if (spell.info.spellName == "FizzMarinerDoom" &&
                        spell.spellObject != null && spell.spellObject.NetworkId == missile.NetworkId)
                    {
                        if (spell.spellType == SpellType.Circular)
                        {                            
                            spell.spellObject = null;
                        }
                    }
                }*/
            }
        }

        private static void ProcessSpell_XerathArcanopulse2(Obj_AI_Base hero, GameObjectProcessSpellCastEventArgs args, SpellData spellData, SpecialSpellEventArgs specialSpellArgs)
        {
            if (args.SData.Name == "XerathArcanopulseChargeUp")// || spellData.spellName == "xeratharcanopulse2")
            {
                var castTime = -1 * (hero.Spellbook.CastTime - Game.Time) * 1000;

                if (castTime > 0)
                {
                    var dir = (args.End.To2D() - args.Start.To2D()).Normalized();
                    var endPos = args.Start.To2D() + dir * Math.Min(spellData.range, 750 + castTime / 2);
                    SpellDetector.CreateSpellData(hero, args.Start, endPos.To3D(), spellData);
                }

                specialSpellArgs.noProcess = true;
            }
        }

        private static void OnCreateObj_ViktorDeathRay3(GameObject obj, EventArgs args)
        {
            if (!obj.IsValid<MissileClient>())
                return;

            MissileClient missile = (MissileClient)obj;

            SpellData spellData;

            if (missile.SpellCaster != null && missile.SpellCaster.Team != myHero.Team &&
                missile.SData.Name != null && missile.SData.Name == "viktoreaugmissile"
                && SpellDetector.onMissileSpells.TryGetValue("ViktorDeathRay3", out spellData)
                && missile.StartPosition != null && missile.EndPosition != null)
            {
                var missileDist = missile.EndPosition.To2D().Distance(missile.StartPosition.To2D());
                var delay = missileDist / 1.5f + 600;

                spellData.spellDelay = delay;

                SpellDetector.CreateSpellData(missile.SpellCaster, missile.StartPosition, missile.EndPosition, spellData);
            }
        }

        private static void HiuCreate_ObjectTracker(GameObject obj, EventArgs args)
        {
            if (obj.IsEnemy && obj.Type == GameObjectType.obj_AI_Minion
                && !objTracker.ContainsKey(obj.NetworkId))
            {
                var minion = obj as Obj_AI_Minion;

                if (minion.CharData.BaseSkinName.Contains("testcube"))
                {
                    objTracker.Add(obj.NetworkId, new ObjectTrackerInfo(obj, "hiu"));
                    DelayAction.Add(250, () => objTracker.Remove(obj.NetworkId));
                }
            }
        }

        private static void HiuDelete_ObjectTracker(GameObject obj, EventArgs args)
        {
            if (objTracker.ContainsKey(obj.NetworkId))
            {
                objTracker.Remove(obj.NetworkId);
            }
        }

        private static Vector2 GetLastHiuOrientation()
        {
            var objList = objTracker.Values.Where(o => o.Name == "hiu");
            var sortedObjList = objList.OrderByDescending(o => o.timestamp);

            if (sortedObjList.Count() >= 2)
            {
                var pos1 = sortedObjList.First().obj.Position;
                var pos2 = sortedObjList.ElementAt(1).obj.Position;

                return (pos2.To2D() - pos1.To2D()).Normalized();
            }

            return Vector2.Zero;
        }

        private static void OnCreateObj_jayceshockblast(GameObject obj, EventArgs args, Obj_AI_Hero hero, SpellData spellData)
        {
            return;
            if (obj.IsEnemy && obj.Type == GameObjectType.obj_GeneralParticleEmitter
                && obj.Name.Contains("Jayce") && obj.Name.Contains("accel_gate_start"))
            {
                var dir = GetLastHiuOrientation();
                var pos1 = obj.Position.To2D() - dir * 470;
                var pos2 = obj.Position.To2D() + dir * 470;

                var gateTracker = new ObjectTrackerInfo(obj, "AccelGate");
                gateTracker.direction = dir.To3D();

                objTracker.Add(obj.NetworkId, gateTracker);

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
                //objTracker.Add(obj.NetworkId, new ObjectTrackerInfo(obj, "RobotBuddy"));

                //AddObjTrackerPosition
            }
        }

        private static void ProcessSpell_jayceshockblast(Obj_AI_Base hero, GameObjectProcessSpellCastEventArgs args, SpellData spellData, SpecialSpellEventArgs specialSpellArgs)
        {
            if (spellData.spellName == "jayceshockblast")
            {
            }
        }

        private static void ProcessSpell_YasuoQW(Obj_AI_Base hero, GameObjectProcessSpellCastEventArgs args, SpellData spellData)
        {
            if (hero.IsEnemy && args.SData.Name == "yasuoq")
            {
                var castTime = (hero.Spellbook.CastTime - Game.Time) * 1000;

                if (castTime > 0)
                {
                    spellData.spellDelay = castTime;
                }
            }
        }

        private static void ProcessSpell_EkkoR(Obj_AI_Base hero, GameObjectProcessSpellCastEventArgs args, SpellData spellData,
            SpecialSpellEventArgs specialSpellArgs)
        {
            if (spellData.spellName == "EkkoR")
            {
                foreach (var obj in ObjectManager.Get<Obj_AI_Minion>())
                {
                    if (obj != null && obj.IsValid && !obj.IsDead && obj.Name == "Ekko" && obj.IsEnemy)
                    {
                        var blinkPos = obj.ServerPosition.To2D();

                        SpellDetector.CreateSpellData(hero, args.Start, blinkPos.To3D(), spellData);
                    }
                }

                specialSpellArgs.noProcess = true;
            }
        }

        private static void ProcessSpell_SionE(Obj_AI_Base hero, GameObjectProcessSpellCastEventArgs args, SpellData spellData,
            SpecialSpellEventArgs specialSpellArgs)
        {
            if (spellData.spellName == "SionE")
            {
                var objList = new List<Obj_AI_Minion>();
                foreach (var obj in ObjectManager.Get<Obj_AI_Minion>())
                {
                    if (obj != null && obj.IsValid && !obj.IsDead && obj.IsAlly)
                    {
                        objList.Add(obj);
                    }
                }

                objList.OrderBy(o => o.Distance(hero.ServerPosition));

                var spellStart = args.Start.To2D();
                var dir = (args.End.To2D() - spellStart).Normalized();
                var spellEnd = spellStart + dir * spellData.range;

                foreach (var obj in objList)
                {
                    var objProjection = obj.ServerPosition.To2D().ProjectOn(spellStart, spellEnd);

                    if (objProjection.IsOnSegment && objProjection.SegmentPoint.Distance(obj.ServerPosition.To2D()) < obj.BoundingRadius + spellData.radius)
                    {
                        //sth happens
                    }
                }


                //specialSpellArgs.noProcess = true;
            }
        }

        private static void ProcessSpell_FizzPiercingStrike(Obj_AI_Base hero, GameObjectProcessSpellCastEventArgs args, SpellData spellData,
            SpecialSpellEventArgs specialSpellArgs)
        {
            if (spellData.spellName == "FizzPiercingStrike")
            {
                if (args.Target != null && args.Target.IsMe)
                {
                    SpellDetector.CreateSpellData(hero, args.Start, args.End, spellData, null, 0);
                }

                specialSpellArgs.noProcess = true;
            }
        }

        private static void ProcessSpell_AsheVolley(Obj_AI_Base hero, GameObjectProcessSpellCastEventArgs args, SpellData spellData,
            SpecialSpellEventArgs specialSpellArgs)
        {
            if (spellData.spellName == "Volley")
            {
                for (int i = -4; i < 5; i++)
                {
                    Vector3 endPos2 = MathUtils.RotateVector(args.Start.To2D(), args.End.To2D(), i * spellData.angle).To3D();
                    if (i != 0)
                    {
                        SpellDetector.CreateSpellData(hero, args.Start, endPos2, spellData, null, 0, false);
                    }
                }
            }
        }

        private static void OnCreateObj_JinxWMissile(GameObject obj, EventArgs args, Obj_AI_Hero hero, SpellData spellData)
        {
            if (hero != null && !hero.IsVisible
                && obj.IsEnemy && obj.Name.Contains("Jinx") && obj.Name.Contains("W_Cas"))
            {
                var pos1 = hero.Position;
                var dir = (obj.Position - myHero.Position).Normalized();
                var pos2 = pos1 + dir * 500;

                SpellDetector.CreateSpellData(hero, pos1, pos2, spellData, null, 0);
            }
        }

        private static void OnCreateObj_LuxMaliceCannon(GameObject obj, EventArgs args, Obj_AI_Hero hero, SpellData spellData)
        {
            if (obj.IsEnemy && !hero.IsVisible &&
                obj.Name.Contains("Lux") && obj.Name.Contains("R_mis_beam_middle"))
            {
                var objList = objTracker.Values.Where(o => o.Name == "hiu");
                if (objList.Count() > 3)
                {
                    var dir = GetLastHiuOrientation();
                    var pos1 = obj.Position.To2D() - dir * 1750;
                    var pos2 = obj.Position.To2D() + dir * 1750;

                    SpellDetector.CreateSpellData(hero, pos1.To3D(), pos2.To3D(), spellData, null, 0);

                    foreach (ObjectTrackerInfo gameObj in objList)
                    {
                        DelayAction.Add(1, () => objTracker.Remove(gameObj.obj.NetworkId));
                    }
                }
            }
        }

        private static void ProcessSpell_LucianQ(Obj_AI_Base hero, GameObjectProcessSpellCastEventArgs args, SpellData spellData,
            SpecialSpellEventArgs specialSpellArgs)
        {
            if (spellData.spellName == "LucianQ")
            {

                if (args.Target.IsValid<Obj_AI_Base>())
                {
                    var target = args.Target as Obj_AI_Base;

                    float spellDelay = ((float)(350 - ObjectCache.gamePing)) / 1000;
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

        private static void OnCreateObj_OrianaIzunaCommand(GameObject obj, EventArgs args, Obj_AI_Hero hero)
        {
            if (obj.Name == "Orianna_Ball_Flash_Reverse.troy" && obj.IsEnemy)
            {
                foreach (KeyValuePair<int, ObjectTrackerInfo> entry in objTracker)
                {
                    var info = entry.Value;

                    if (entry.Value.Name == "TheDoomBall")
                    {
                        info.usePosition = false;
                        info.obj = hero;
                    }
                }
            }
        }

        private static void OnDeleteObj_OrianaIzunaCommand(GameObject obj, EventArgs args, Obj_AI_Hero hero)
        {
            if (obj.Name == "oriana_ball_glow_red.troy" && obj.IsEnemy)
            {
                foreach (KeyValuePair<int, ObjectTrackerInfo> entry in objTracker)
                {
                    var info = entry.Value;

                    if (entry.Value.Name == "TheDoomBall")
                    {
                        info.usePosition = false;
                        info.obj = hero;
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

                        info.position = args.End;
                        info.usePosition = true;
                    }
                }

                specialSpellArgs.noProcess = true;
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
                    }
                }

                specialSpellArgs.noProcess = true;
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

                    if (!objTracker.ContainsKey(obj.NetworkId))
                    {
                        objTracker.Add(obj.NetworkId, new ObjectTrackerInfo(obj, "RobotBuddy"));
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
                foreach (KeyValuePair<int, ObjectTrackerInfo> entry in objTracker)
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
            if (obj != null && obj.IsValid && obj.Name == "Shadow" && obj.IsEnemy)
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
                        if (info.usePosition == false && (info.obj == null || !info.obj.IsValid || info.obj.IsDead))
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
            if (!obj.IsValid<MissileClient>())
                return;

            MissileClient missile = (MissileClient)obj;

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
