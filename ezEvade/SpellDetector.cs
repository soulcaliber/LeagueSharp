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
    internal class SpellDetector
    {
        public delegate void OnCreateSpellHandler(Spell spell);
        public static event OnCreateSpellHandler OnCreateSpell;

        //public static event OnDeleteSpellHandler OnDeleteSpell;

        public static Dictionary<int, Spell> spells = new Dictionary<int, Spell>();
        public static Dictionary<int, Spell> drawSpells = new Dictionary<int, Spell>();

        public static Dictionary<string, SpellData> onProcessSpells = new Dictionary<string, SpellData>();
        public static Dictionary<string, SpellData> onMissileSpells = new Dictionary<string, SpellData>();

        public static Dictionary<string, SpellData> windupSpells = new Dictionary<string, SpellData>();

        private static int spellIDCount = 0;

        private static Obj_AI_Hero myHero { get { return ObjectManager.Player; } }
        private static float gameTime { get { return Game.ClockTime * 1000; } }

        public static Menu menu;
        public static Menu spellMenu;

        public SpellDetector(Menu mainMenu)
        {
            Obj_SpellMissile.OnCreate += SpellMissile_OnCreate;
            Obj_SpellMissile.OnDelete += SpellMissile_OnDelete;
            Obj_AI_Base.OnProcessSpellCast += Game_ProcessSpell;

            menu = mainMenu;
            Game_OnGameLoad();
        }

        private void Game_OnGameLoad()
        {
            //Game.PrintChat("SpellDetector loaded");
            spellMenu = new Menu("Spells", "Spells");
            menu.AddSubMenu(spellMenu);

            LoadSpellDictionary();
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
                                spell.spellObject = obj;
                                //Game.PrintChat("aquired: " + (obj.Position.To2D().Distance(spell.startPos)));
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
                    s => (EvadeHelper.GetSpellDangerLevel(s) < 2)))
            {
                DeleteSpell(spell.spellID);
            }
        }

        private void Game_ProcessSpell(Obj_AI_Base hero, GameObjectProcessSpellCastEventArgs args)
        {
            SpellData spellData;

            if (hero.Team != myHero.Team && onProcessSpells.TryGetValue(args.SData.Name, out spellData))
            {
                if (spellData.usePackets == false)
                {
                    CreateSpellData(hero, args.Start, args.End, spellData, null);
                }
            }
        }

        private void CreateSpellData(Obj_AI_Base hero, Vector3 startStartPos, Vector3 spellEndPos, SpellData spellData, GameObject obj)
        {
            if (startStartPos.Distance(myHero.Position) < spellData.range + 1000)
            {
                Vector2 startPosition = startStartPos.To2D();
                Vector2 endPosition = spellEndPos.To2D();
                Vector2 direction = (endPosition - startPosition).Normalized();
                float endTick = 0;

                if (spellData.rangeCap) //for diana q
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

                }

                Spell newSpell = new Spell();

                newSpell.startTime = gameTime;
                newSpell.endTime = gameTime + endTick;
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

        private int CreateSpell(Spell newSpell)
        {
            int spellID = spellIDCount++;
            newSpell.spellID = spellID;

            drawSpells.Add(spellID, newSpell);

            if (!(Evade.isDodgeDangerousEnabled() && EvadeHelper.GetSpellDangerLevel(newSpell) < 2)
                && Evade.menu.SubMenu("Spells").SubMenu(newSpell.info.charName + newSpell.info.spellName + "Settings").Item("DodgeSpell").GetValue<bool>())
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

        private void DeleteSpell(int spellID)
        {
            spells.Remove(spellID);
            drawSpells.Remove(spellID);

        }

        public static Vector2 GetCurrentSpellPosition(Spell spell)
        {
            Vector2 spellPos = spell.startPos;
            if (gameTime > spell.startTime + spell.info.spellDelay)
            {
                spellPos = spell.startPos + spell.direction * spell.info.projectileSpeed * (gameTime - spell.startTime - spell.info.spellDelay) / 1000;
            }

            if (spell.spellObject != null)
            {
                spellPos = spell.spellObject.Position.To2D();
            }
            return spellPos;
        }

        private void LoadSpellDictionary()
        {
            foreach (var hero in ObjectManager.Get<Obj_AI_Hero>())
            {
                if (hero.IsMe)
                {
                    foreach (var spell in SpellDatabase.Spells.Where(
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

                        if (spell.missileName == "")
                            spell.missileName = spell.spellName;

                        if (!onProcessSpells.ContainsKey(spell.spellName))
                        {
                            onProcessSpells.Add(spell.spellName, spell);
                            onMissileSpells.Add(spell.missileName, spell);

                            string menuName = spell.charName + " (" + spell.spellKey.ToString() + ") Settings";

                            var enableSpell = !spell.defaultOff;

                            Menu newSpellMenu = new Menu(menuName, spell.charName + spell.spellName + "Settings");
                            newSpellMenu.AddItem(new MenuItem("DodgeSpell", "Dodge Spell").SetValue(enableSpell));
                            newSpellMenu.AddItem(new MenuItem("DrawSpell", "Draw Spell").SetValue(enableSpell));
                            newSpellMenu.AddItem(new MenuItem("SpellRadius", "Spell Radius")
                                .SetValue(new Slider((int)spell.radius, (int)spell.radius - 100, (int)spell.radius + 100)));
                            newSpellMenu.AddItem(new MenuItem("DangerLevel", "Danger Level")
                                .SetValue(new StringList(new[] { "Low", "Normal", "High", "Extreme" }, spell.dangerlevel)));

                            spellMenu.AddSubMenu(newSpellMenu);
                        }
                    }

                }
            }

        }
    }
}
