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

        public ObjectTrackerInfo(GameObject obj, string name)
        {
            this.obj = obj;
            this.Name = name;
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

            /*if (spellData.spellName == "LuxMaliceCannon" && !pDict.ContainsKey("ProcessSpell_LuxMaliceCannon"))
            {
                var hero = HeroManager.Enemies.FirstOrDefault(h => h.ChampionName == "Lux");
                if (hero != null)
                {
                    GameObject.OnCreate += (obj, args) => OnCreateObj_LuxMaliceCannon(obj, args, hero, spellData);
                }

                pDict["ProcessSpell_LuxMaliceCannon"] = true;
            }*/

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

            if (spellData.spellName == "SionE" && !pDict.ContainsKey("ProcessSpell_SionE"))
            {
                SpellDetector.OnProcessSpecialSpell += ProcessSpell_SionE;
                pDict["ProcessSpell_SionE"] = true;
            }

            if (spellData.spellName == "EkkoR" && !pDict.ContainsKey("ProcessSpell_EkkoR"))
            {
                SpellDetector.OnProcessSpecialSpell += ProcessSpell_EkkoR;
                pDict["ProcessSpell_EkkoR"] = true;
            }
        }

        private static void ProcessSpell_EkkoR(Obj_AI_Base hero, GameObjectProcessSpellCastEventArgs args, SpellData spellData,
            SpecialSpellEventArgs specialSpellArgs)
        {
            if (spellData.spellName == "EkkoR")
            {
                foreach (var obj in ObjectManager.Get<Obj_AI_Minion>())
                {
                    if (obj != null && obj.IsValid && obj.Name == "Ekko" && obj.IsEnemy)
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
            if (hero != null && !hero.IsVisible
                && obj.Name == "hiu" && obj.IsEnemy)
            {
                objTracker.Add(obj.NetworkId, new ObjectTrackerInfo(obj));
                DelayAction.Add(250, () => objTracker.Remove(obj.NetworkId));

                var objList = objTracker.Values.Where(o => o.Name == "hiu");
                if (objList.Count() > 4)
                {
                    var pos1 = objList.First().obj.Position;
                    var pos2 = objList.Last().obj.Position;
                    SpellDetector.CreateSpellData(hero, pos1, pos2, spellData, null, 0);

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

                    float spellDelay = ((float)(350 - Game.Ping)) / 1000;
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

                    if (!objTracker.ContainsKey(obj.NetworkId)){         
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
                        if (info.obj == null || info.obj.IsValid || info.obj.IsDead || info.obj.IsVisible)
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
                        if (info.usePosition == false && (info.obj == null || info.obj.IsValid || info.obj.IsDead))
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
