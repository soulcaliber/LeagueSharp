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
        public delegate void OnCreateSpellHandler(Spell spell);
        public static event OnCreateSpellHandler OnCreateSpell;

        public delegate void OnProcessSpecialSpellHandler(Obj_AI_Base hero, GameObjectProcessSpellCastEventArgs args, SpellData spellData);
        public static event OnProcessSpecialSpellHandler OnProcessSpecialSpell;

        //public static event OnDeleteSpellHandler OnDeleteSpell;

        public static Dictionary<int, Spell> spells = new Dictionary<int, Spell>();
        public static Dictionary<int, Spell> drawSpells = new Dictionary<int, Spell>();

        public static Dictionary<string, string> channeledSpells = new Dictionary<string, string>();

        public static Dictionary<string, SpellData> onProcessSpells = new Dictionary<string, SpellData>();
        public static Dictionary<string, SpellData> onMissileSpells = new Dictionary<string, SpellData>();

        public static Dictionary<string, SpellData> windupSpells = new Dictionary<string, SpellData>();

        private static int spellIDCount = 0;

        private static Obj_AI_Hero myHero { get { return ObjectManager.Player; } }

        public static float lastCheckTime = 0;

        public static Menu menu;
        public static Menu spellMenu;

        public SpellDetector(Menu mainMenu)
        {
            Obj_SpellMissile.OnCreate += SpellMissile_OnCreate;
            Obj_SpellMissile.OnDelete += SpellMissile_OnDelete;
            Obj_AI_Hero.OnProcessSpellCast += Game_ProcessSpell;

            Drawing.OnDraw += Game_OnGameUpdate; //in case ongameupdate crashes

            menu = mainMenu;

            //Game.PrintChat("SpellDetector loaded");
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
                                    //Game.PrintChat("aquired: " + (obj.Position.To2D().Distance(spell.startPos)));
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
                DeleteSpell(spell.spellID);
            }
        }

        public void RemoveNonDangerousSpells()
        {
            foreach (var spell in spells.Values.ToList().Where(
                    s => (EvadeHelper.GetSpellDangerLevel(s) < 3)))
            {
                DeleteSpell(spell.spellID);
            }
        }

        private void Game_ProcessSpell(Obj_AI_Base hero, GameObjectProcessSpellCastEventArgs args)
        {
            if (hero.IsMe)
            {                
                string name;
                if (channeledSpells.TryGetValue(args.SData.Name, out name))
                {
                    Evade.isChanneling = true;
                    Evade.channelPosition = myHero.ServerPosition.To2D();
                }
            }
            
            SpellData spellData;

            if (hero.Team != myHero.Team && onProcessSpells.TryGetValue(args.SData.Name, out spellData))
            {
                if (spellData.usePackets == false)
                {
                    if (OnProcessSpecialSpell != null)
                    {
                        OnProcessSpecialSpell(hero, args, spellData);
                    }

                    if (spellData.noProcess == false)
                    {
                        CreateSpellData(hero, args.Start, args.End, spellData, null);
                    }
                }
            }
        }

        public static void CreateSpellData(Obj_AI_Base hero, Vector3 startStartPos, Vector3 spellEndPos,
            SpellData spellData, GameObject obj = null, float extraEndTick = 0.0f)
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

                newSpell.startTime = Evade.GetTickCount();
                newSpell.endTime = Evade.GetTickCount() + endTick;
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

                int spellID = CreateSpell(newSpell);
                Utility.DelayAction.Add((int)endTick, () => DeleteSpell(spellID));
            }
        }

        private void Game_OnGameUpdate(EventArgs args)
        {
            if (Evade.GetTickCount() - lastCheckTime > 100)
            {
                CheckCasterDead();
                CheckSpellEndTime();
                lastCheckTime = Evade.GetTickCount();
            }            
        }

        private void CheckSpellEndTime()
        {
            foreach (var spell in spells.Where(s => (s.Value.endTime < Evade.GetTickCount())))
            {
                DeleteSpell(spell.Key);
            }
        }

        private void CheckCasterDead()
        {
            foreach (var hero in ObjectManager.Get<Obj_AI_Hero>())
            {
                foreach (var spell in spells.Where(
                        s => (s.Value.heroID == hero.NetworkId && hero.IsDead)))
                {
                    DeleteSpell(spell.Key);
                }
            }
        }

        private static int CreateSpell(Spell newSpell)
        {
            int spellID = spellIDCount++;
            newSpell.spellID = spellID;

            drawSpells.Add(spellID, newSpell);

            if (!(Evade.isDodgeDangerousEnabled() && EvadeHelper.GetSpellDangerLevel(newSpell) < 3)
                && Evade.menu.SubMenu("Spells").SubMenu(newSpell.info.charName + newSpell.info.spellName + "Settings")
                .Item(newSpell.info.spellName + "DodgeSpell").GetValue<bool>())
            {
                if (newSpell.info.spellType == SpellType.Circular
                    && Evade.menu.SubMenu("Main").Item("DodgeCircularSpells").GetValue<bool>() == false)
                    return spellID;

                spells.Add(spellID, newSpell);
                if (OnCreateSpell != null)
                {
                    OnCreateSpell(newSpell);
                }
            }

            return spellID;
        }

        private static void DeleteSpell(int spellID)
        {
            spells.Remove(spellID);
            drawSpells.Remove(spellID);
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
            
        }

        public static Vector2 GetCurrentSpellPosition(Spell spell, bool allowNegative = false, float delay = 0)
        {
            Vector2 spellPos = spell.startPos;

            if (spell.info.spellType == SpellType.Line)
            {
                float spellTime = Evade.GetTickCount() - spell.startTime - spell.info.spellDelay;

                if (spellTime >= 0)
                {
                    spellPos = spell.startPos + spell.direction * spell.info.projectileSpeed * spellTime / 1000;
                }
                else if(allowNegative)
                {                    
                    spellPos = spell.startPos + spell.direction * spell.info.projectileSpeed * (spellTime / 1000);
                }
            }
            else if (spell.info.spellType == SpellType.Circular)
            {
                spellPos = spell.endPos;
            }

            if (spell.spellObject != null)
            {
                spellPos = spell.spellObject.Position.To2D();
            }

            if (delay > 0)
            {
                spellPos = spellPos + spell.direction * spell.info.projectileSpeed * (delay / 1000);
            }

            return spellPos;
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
                        s => (s.charName == hero.ChampionName)))
                    {
                        //Game.PrintChat(spell.spellName);                        

                        if (!(spell.spellType == SpellType.Circular || spell.spellType == SpellType.Line))
                            continue;

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
