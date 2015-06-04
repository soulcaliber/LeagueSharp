using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using LeagueSharp;
using LeagueSharp.Common;
using SharpDX;

namespace ezEvade
{
    internal class SpellDetector
    {
        //public delegate void OnCreateSpellHandler(Spell spell);
        //public static event OnCreateSpellHandler OnCreateSpell;

        public delegate void OnProcessDetectedSpellsHandler();
        public static event OnProcessDetectedSpellsHandler OnProcessDetectedSpells;

        public delegate void OnProcessSpecialSpellHandler(Obj_AI_Base hero, GameObjectProcessSpellCastEventArgs args,
            SpellData spellData, SpecialSpellEventArgs specialSpellArgs);
        public static event OnProcessSpecialSpellHandler OnProcessSpecialSpell;

        //public static event OnDeleteSpellHandler OnDeleteSpell;

        public static Dictionary<int, Spell> spells = new Dictionary<int, Spell>();
        public static Dictionary<int, Spell> drawSpells = new Dictionary<int, Spell>();
        public static Dictionary<int, Spell> detectedSpells = new Dictionary<int, Spell>();

        public static Dictionary<string, string> channeledSpells = new Dictionary<string, string>();

        public static Dictionary<string, SpellData> onProcessSpells = new Dictionary<string, SpellData>();
        public static Dictionary<string, SpellData> onMissileSpells = new Dictionary<string, SpellData>();

        public static Dictionary<string, SpellData> windupSpells = new Dictionary<string, SpellData>();

        private static int spellIDCount = 0;

        private static Obj_AI_Hero myHero { get { return ObjectManager.Player; } }

        public static float lastCheckTime = 0;
        public static float lastCheckSpellCollisionTime = 0;

        public static Menu menu;
        public static Menu spellMenu;

        public SpellDetector(Menu mainMenu)
        {
            Obj_SpellMissile.OnCreate += SpellMissile_OnCreate;
            Obj_SpellMissile.OnDelete += SpellMissile_OnDelete;
            Obj_AI_Hero.OnProcessSpellCast += Game_ProcessSpell;

            Game.OnUpdate += Game_OnGameUpdate; //in case ongameupdate crashes

            menu = mainMenu;

            //Console.WriteLine("SpellDetector loaded");
            spellMenu = new Menu("Spells", "Spells");
            menu.AddSubMenu(spellMenu);

            LoadSpellDictionary();
            InitChannelSpells();
        }

        private void SpellMissile_OnCreate(GameObject obj, EventArgs args)
        {
            if (!obj.IsValid<Obj_SpellMissile>())
                return;

            Obj_SpellMissile missile = (Obj_SpellMissile)obj;
            SpellData spellData;

            if (missile.SpellCaster != null && missile.SpellCaster.Team != myHero.Team &&
                missile.SData.Name != null && onMissileSpells.TryGetValue(missile.SData.Name, out spellData)
                && missile.StartPosition != null && missile.EndPosition != null)
            {

                if (missile.StartPosition.Distance(myHero.Position) < spellData.range + 1000)
                {
                    var hero = missile.SpellCaster;

                    if (hero.IsVisible)
                    {
                        if (spellData.usePackets)
                        {
                            CreateSpellData(hero, missile.StartPosition, missile.EndPosition, spellData, obj);
                            return;
                        }

                        foreach (KeyValuePair<int, Spell> entry in spells)
                        {
                            Spell spell = entry.Value;

                            if (spell.info.missileName == missile.SData.Name
                                && spell.heroID == missile.SpellCaster.NetworkId)
                            {
                                if (spell.info.isThreeWay == false && spell.info.isSpecial == false)
                                {
                                    spell.spellObject = obj;

                                    //var acquisitionTime = Evade.TickCount - spell.startTime;
                                    //Console.WriteLine("AcquiredTime: " + acquisitionTime);
                                }
                            }
                        }
                    }
                    else
                    {
                        if (Evade.menu.SubMenu("Main").Item("DodgeFOWSpells").GetValue<bool>())
                        {
                            CreateSpellData(hero, missile.StartPosition, missile.EndPosition, spellData, obj);
                        }
                    }
                }
            }
        }

        private void SpellMissile_OnDelete(GameObject obj, EventArgs args)
        {
            if (!obj.IsValid<Obj_SpellMissile>())
                return;

            Obj_SpellMissile missile = (Obj_SpellMissile)obj;
            //SpellData spellData;

            foreach (var spell in spells.Values.ToList().Where(
                    s => (s.spellObject != null && s.spellObject.NetworkId == obj.NetworkId))) //isAlive
            {
                DelayAction.Add(1, () => DeleteSpell(spell.spellID));
            }
        }

        public void RemoveNonDangerousSpells()
        {
            foreach (var spell in spells.Values.ToList().Where(
                    s => (s.GetSpellDangerLevel() < 3)))
            {
                DelayAction.Add(1, () => DeleteSpell(spell.spellID));
            }
        }

        private void Game_ProcessSpell(Obj_AI_Base hero, GameObjectProcessSpellCastEventArgs args)
        {
            try
            {
                if (hero.IsMe)
                {
                    string name;
                    if (channeledSpells.TryGetValue(args.SData.Name, out name))
                    {
                        Evade.isChanneling = true;
                        Evade.channelPosition = myHero.ServerPosition.To2D();
                    }

                    if (Evade.menu.Item("CalculateWindupDelay").GetValue<bool>())
                    {
                        var castTime = (hero.Spellbook.CastTime - Game.Time) * 1000;
                        if (castTime > 0 && !Orbwalking.IsAutoAttack(args.SData.Name))
                        {
                            var extraDelayBuffer = Evade.menu.Item("ExtraPingBuffer").GetValue<Slider>().Value;
                            Evade.lastWindupTime = Evade.TickCount + castTime - Game.Ping - extraDelayBuffer;
                        }
                    }

                }


                SpellData spellData;

                if (hero.Team != myHero.Team && onProcessSpells.TryGetValue(args.SData.Name, out spellData))
                {
                    if (spellData.usePackets == false)
                    {
                        var specialSpellArgs = new SpecialSpellEventArgs();
                        if (OnProcessSpecialSpell != null)
                        {
                            OnProcessSpecialSpell(hero, args, spellData, specialSpellArgs);
                        }

                        if (specialSpellArgs.noProcess == false && spellData.noProcess == false)
                        {
                            CreateSpellData(hero, args.Start, args.End, spellData, null);
                        }
                    }
                }
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
            }
        }

        public static void CreateSpellData(Obj_AI_Base hero, Vector3 startStartPos, Vector3 spellEndPos,
            SpellData spellData, GameObject obj = null, float extraEndTick = 0.0f, bool processSpell = true)
        {
            if (startStartPos.Distance(myHero.Position) < spellData.range + 1000)
            {
                Vector2 startPosition = startStartPos.To2D();
                Vector2 endPosition = spellEndPos.To2D();
                Vector2 direction = (endPosition - startPosition).Normalized();
                float endTick = 0;

                if (spellData.fixedRange) //for diana q
                {
                    if (endPosition.Distance(startPosition) > spellData.range)
                    {
                        //var heroCastPos = hero.ServerPosition.To2D();
                        //direction = (endPosition - heroCastPos).Normalized();
                        endPosition = startPosition + direction * spellData.range;
                    }
                }

                if (spellData.spellType == SpellType.Line)
                {
                    endTick = spellData.spellDelay + (spellData.range / spellData.projectileSpeed) * 1000;
                    endPosition = startPosition + direction * spellData.range;

                    if (obj != null)
                        endTick -= spellData.spellDelay;
                }
                else if (spellData.spellType == SpellType.Circular)
                {
                    endTick = spellData.spellDelay;

                    if (spellData.projectileSpeed == 0)
                    {
                        endPosition = hero.ServerPosition.To2D();
                    }
                    else if (spellData.projectileSpeed > 0)
                    {
                        endTick = endTick + 1000 * startPosition.Distance(endPosition) / spellData.projectileSpeed;
                    }
                }
                else if (spellData.spellType == SpellType.Cone)
                {
                    return;
                }

                endTick += extraEndTick;

                Spell newSpell = new Spell();

                newSpell.startTime = Evade.TickCount;
                newSpell.endTime = Evade.TickCount + endTick;
                newSpell.startPos = startPosition;
                newSpell.endPos = endPosition;
                newSpell.direction = direction;
                newSpell.heroID = hero.NetworkId;
                newSpell.info = spellData;

                if (obj != null)
                {
                    newSpell.spellObject = obj;
                    newSpell.projectileID = obj.NetworkId;
                }

                int spellID = CreateSpell(newSpell, processSpell);

                DelayAction.Add((int)endTick, () => DeleteSpell(spellID));
            }
        }

        private void Game_OnGameUpdate(EventArgs args)
        {
            if (Evade.TickCount - lastCheckSpellCollisionTime > 100)
            {
                CheckSpellCollision();
                lastCheckSpellCollisionTime = Evade.TickCount;
            }

            if (Evade.TickCount - lastCheckTime > 1)
            {
                //CheckCasterDead();                
                CheckSpellEndTime();
                AddDetectedSpells();
                lastCheckTime = Evade.TickCount;
            }
        }

        private void CheckSpellEndTime()
        {
            foreach (KeyValuePair<int, Spell> entry in detectedSpells)
            {
                Spell spell = entry.Value;

                foreach (var hero in HeroManager.Enemies)
                {
                    if (hero.IsDead && spell.heroID == hero.NetworkId)
                    {
                        if (spell.spellObject == null)
                            DelayAction.Add(1, () => DeleteSpell(entry.Key));
                    }
                }

                if (spell.endTime < Evade.TickCount || CanHeroWalkIntoSpell(spell) == false)
                    DelayAction.Add(1, () => DeleteSpell(entry.Key));
            }
        }

        private static void CheckSpellCollision()
        {
            if (Evade.menu.Item("CheckSpellCollision").GetValue<bool>() == false)
            {
                return;
            }

            foreach (KeyValuePair<int, Spell> entry in detectedSpells)
            {
                Spell spell = entry.Value;

                var collisionObject = spell.CheckSpellCollision();

                if (collisionObject != null)
                {
                    spell.predictedEndPos = spell.GetSpellProjection(collisionObject.ServerPosition.To2D());

                    if (spell.GetCurrentSpellPosition().Distance(collisionObject.ServerPosition)
                        < collisionObject.BoundingRadius + spell.GetSpellRadius())
                    {
                        DelayAction.Add(1, () => DeleteSpell(entry.Key));
                    }
                }
            }
        }

        public static bool CanHeroWalkIntoSpell(Spell spell)
        {
            if (Evade.menu.Item("AdvancedSpellDetection").GetValue<bool>())
            {
                Vector2 heroPos = myHero.Position.To2D();
                var extraDist = myHero.Distance(myHero.ServerPosition.To2D());

                if (spell.info.spellType == SpellType.Line)
                {
                    var walkRadius = myHero.MoveSpeed * (spell.endTime - Evade.TickCount) / 1000 + myHero.BoundingRadius + spell.info.radius + extraDist + 10;
                    var spellPos = spell.GetCurrentSpellPosition();
                    var spellEndPos = spell.GetSpellEndPosition();

                    var projection = heroPos.ProjectOn(spellPos, spellEndPos);

                    return projection.SegmentPoint.Distance(heroPos) <= walkRadius;
                }
                else if (spell.info.spellType == SpellType.Circular)
                {
                    var walkRadius = myHero.MoveSpeed * (spell.endTime - Evade.TickCount) / 1000 + myHero.BoundingRadius + spell.info.radius + extraDist + 10;

                    if (heroPos.Distance(spell.endPos) < walkRadius)
                    {
                        return true;
                    }

                }

                return false;
            }
            else
            {
                return true;
            }
        }

        private static void AddDetectedSpells()
        {
            bool spellAdded = false;

            foreach (KeyValuePair<int, Spell> entry in detectedSpells)
            {
                Spell spell = entry.Value;

                float evadeTime, spellHitTime;
                spell.CanHeroEvade(myHero, out evadeTime, out spellHitTime);

                spell.spellHitTime = spellHitTime;
                spell.evadeTime = evadeTime;

                var extraDelay = Game.Ping + Evade.menu.Item("ExtraPingBuffer").GetValue<Slider>().Value;;

                if (spell.spellHitTime - spell.evadeTime - extraDelay < 1500 && CanHeroWalkIntoSpell(spell))
                {
                    Spell newSpell = spell;
                    int spellID = spell.spellID;

                    if (!drawSpells.ContainsKey(spell.spellID))
                    {
                        drawSpells.Add(spellID, newSpell);
                    }

                    //var spellFlyTime = Evade.GetTickCount - spell.startTime;
                    if (spellHitTime < Evade.menu.Item("ReactionTime").GetValue<Slider>().Value)
                    {
                        continue;
                    }

                    var dodgeInterval = menu.Item("DodgeInterval").GetValue<Slider>().Value;
                    if (Evade.lastPosInfo != null && dodgeInterval > 0)
                    {
                        var timeElapsed = Evade.TickCount - Evade.lastPosInfo.timestamp;

                        if (dodgeInterval > timeElapsed)
                        {
                            //var delay = dodgeInterval - timeElapsed;
                            //DelayAction.Add((int)delay, () => SpellDetector_OnProcessDetectedSpells());
                            continue;
                        }
                    }

                    if (!spells.ContainsKey(spell.spellID))
                    {
                        if (!(Evade.isDodgeDangerousEnabled() && newSpell.GetSpellDangerLevel() < 3)
                            && Evade.menu.SubMenu("Spells").SubMenu(newSpell.info.charName + newSpell.info.spellName + "Settings")
                            .Item(newSpell.info.spellName + "DodgeSpell").GetValue<bool>())
                        {
                            if (newSpell.info.spellType == SpellType.Circular
                                && Evade.menu.SubMenu("Main").Item("DodgeCircularSpells").GetValue<bool>() == false)
                            {
                                //return spellID;
                                continue;
                            }

                            spells.Add(spellID, newSpell);

                            spellAdded = true;
                        }
                    }

                    if (Evade.menu.Item("CheckSpellCollision").GetValue<bool>()
                        && spell.predictedEndPos != Vector2.Zero)
                    {
                        spellAdded = false;
                    }
                }
            }

            if (spellAdded && OnProcessDetectedSpells != null)
            {
                OnProcessDetectedSpells();
            }
        }

        private static int CreateSpell(Spell newSpell, bool processSpell = true)
        {
            int spellID = spellIDCount++;
            newSpell.spellID = spellID;

            detectedSpells.Add(spellID, newSpell);

            if (processSpell)
            {
                CheckSpellCollision();
                AddDetectedSpells();
            }

            return spellID;
        }

        private static void DeleteSpell(int spellID)
        {
            spells.Remove(spellID);
            drawSpells.Remove(spellID);
            detectedSpells.Remove(spellID);
        }

        public static int GetCurrentSpellID()
        {
            return spellIDCount;
        }

        public static List<int> GetSpellList()
        {
            List<int> spellList = new List<int>();

            foreach (KeyValuePair<int, Spell> entry in SpellDetector.spells)
            {
                Spell spell = entry.Value;
                spellList.Add(spell.spellID);
            }

            return spellList;
        }

        public static int GetHighestDetectedSpellID()
        {
            int highest = 0;

            foreach (var spell in SpellDetector.spells)
            {
                highest = Math.Max(highest, spell.Key);
            }

            return highest;
        }

        public static float GetLowestEvadeTime(out Spell lowestSpell)
        {
            float lowest = float.MaxValue;
            lowestSpell = null;

            foreach (KeyValuePair<int, Spell> entry in SpellDetector.spells)
            {
                Spell spell = entry.Value;

                if (spell.spellHitTime != float.MinValue)
                {
                    //Console.WriteLine("spellhittime: " + spell.spellHitTime);
                    lowest = Math.Min(lowest, (spell.spellHitTime - spell.evadeTime));
                    lowestSpell = spell;
                }
            }

            return lowest;
        }

        public static Spell GetMostDangerousSpell(bool hasProjectile = false)
        {
            int maxDanger = 0;
            Spell maxDangerSpell = null;

            foreach (Spell spell in SpellDetector.spells.Values)
            {
                if (!hasProjectile || (spell.info.projectileSpeed > 0 && spell.info.projectileSpeed != float.MaxValue))
                {
                    var dangerlevel = spell.GetSpellDangerLevel();

                    if (dangerlevel > maxDanger)
                    {
                        maxDanger = dangerlevel;
                        maxDangerSpell = spell;
                    }
                }
            }

            return maxDangerSpell;
        }

        public static void InitChannelSpells()
        {

            channeledSpells["Drain"] = "FiddleSticks";
            channeledSpells["Crowstorm"] = "FiddleSticks";
            channeledSpells["KatarinaR"] = "Katarina";
            channeledSpells["AbsoluteZero"] = "Nunu";
            channeledSpells["GalioIdolOfDurand"] = "Galio";
            channeledSpells["MissFortuneBulletTime"] = "MissFortune";
            channeledSpells["Meditate"] = "MasterYi";
            channeledSpells["NetherGrasp"] = "Malzahar";
            channeledSpells["ReapTheWhirlwind"] = "Janna";
            channeledSpells["KarthusFallenOne"] = "Karthus";
            channeledSpells["KarthusFallenOne2"] = "Karthus";
            channeledSpells["VelkozR"] = "Velkoz";
            channeledSpells["XerathLocusOfPower2"] = "Xerath";
            channeledSpells["ZacE"] = "Zac";
            channeledSpells["Pantheon_Heartseeker"] = "Pantheon";

            channeledSpells["OdinRecall"] = "AllChampions";
            channeledSpells["Recall"] = "AllChampions";

        }

        private void LoadSpellDictionary()
        {
            foreach (var hero in ObjectManager.Get<Obj_AI_Hero>())
            {
                if (hero.IsMe)
                {
                    foreach (var spell in SpellWindupDatabase.Spells.Where(
                        s => (s.charName == hero.ChampionName)))
                    {
                        if (!windupSpells.ContainsKey(spell.spellName))
                        {
                            windupSpells.Add(spell.spellName, spell);
                        }
                    }
                }

                if (hero.Team != myHero.Team)
                {
                    foreach (var spell in SpellDatabase.Spells.Where(
                        s => (s.charName == hero.ChampionName) || (s.charName == "AllChampions")))
                    {
                        //Console.WriteLine(spell.spellName); 

                        if (!(spell.spellType == SpellType.Circular || spell.spellType == SpellType.Line))
                            continue;

                        if (spell.charName == "AllChampions")
                        {
                            SpellSlot slot = hero.GetSpellSlot(spell.spellName);
                            if (slot == SpellSlot.Unknown)
                            {
                                continue;
                            }
                        }

                        if (!onProcessSpells.ContainsKey(spell.spellName))
                        {
                            if (spell.missileName == "")
                                spell.missileName = spell.spellName;

                            onProcessSpells.Add(spell.spellName, spell);
                            onMissileSpells.Add(spell.missileName, spell);

                            if (spell.extraSpellNames != null)
                            {
                                foreach (string spellName in spell.extraSpellNames)
                                {
                                    onProcessSpells.Add(spellName, spell);
                                }
                            }

                            if (spell.extraMissileNames != null)
                            {
                                foreach (string spellName in spell.extraMissileNames)
                                {
                                    onMissileSpells.Add(spellName, spell);
                                }
                            }

                            SpecialSpells.LoadSpecialSpell(spell);

                            string menuName = spell.charName + " (" + spell.spellKey.ToString() + ") Settings";

                            var enableSpell = !spell.defaultOff;

                            Menu newSpellMenu = new Menu(menuName, spell.charName + spell.spellName + "Settings");
                            newSpellMenu.AddItem(new MenuItem(spell.spellName + "DodgeSpell", "Dodge Spell").SetValue(enableSpell));
                            newSpellMenu.AddItem(new MenuItem(spell.spellName + "DrawSpell", "Draw Spell").SetValue(enableSpell));
                            newSpellMenu.AddItem(new MenuItem(spell.spellName + "SpellRadius", "Spell Radius")
                                .SetValue(new Slider((int)spell.radius, (int)spell.radius - 100, (int)spell.radius + 100)));
                            newSpellMenu.AddItem(new MenuItem(spell.spellName + "DangerLevel", "Danger Level")
                                .SetValue(new StringList(new[] { "Low", "Normal", "High", "Extreme" }, spell.dangerlevel - 1)));

                            spellMenu.AddSubMenu(newSpellMenu);
                        }
                    }

                }
            }

        }
    }
}
