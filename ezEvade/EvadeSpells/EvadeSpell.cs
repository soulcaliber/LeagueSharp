using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using LeagueSharp;
using LeagueSharp.Common;
using SharpDX;

namespace ezEvade
{
    class EvadeSpell
    {
        private static Obj_AI_Hero myHero { get { return ObjectManager.Player; } }

        public static List<EvadeSpellData> evadeSpells = new List<EvadeSpellData>();
        public static List<EvadeSpellData> itemSpells = new List<EvadeSpellData>();
        public static EvadeCommand lastSpellEvadeCommand = new EvadeCommand { isProcessed = true, timestamp = Evade.TickCount };

        public static Menu menu;
        public static Menu evadeSpellMenu;

        public EvadeSpell(Menu mainMenu)
        {
            menu = mainMenu;

            //Game.OnUpdate += Game_OnGameUpdate;

            evadeSpellMenu = new Menu("Evade Spells", "EvadeSpells");
            menu.AddSubMenu(evadeSpellMenu);

            LoadEvadeSpellList();
            CheckForItems();
        }

        private void Game_OnGameUpdate(EventArgs args)
        {
            //CheckDashing();
        }

        public static void CheckDashing()
        {
            if (Evade.TickCount - lastSpellEvadeCommand.timestamp < 250 && myHero.IsDashing()
                && lastSpellEvadeCommand.evadeSpellData.evadeType == EvadeType.Dash)
            {
                var dashInfo = myHero.GetDashInfo();

                //Console.WriteLine("" + dashInfo.EndPos.Distance(lastSpellEvadeCommand.targetPosition));
                lastSpellEvadeCommand.targetPosition = dashInfo.EndPos;
            }
        }

        private static void CheckForItems()
        {
            foreach (var spell in itemSpells)
            {
                var hasItem = Items.HasItem((int)spell.itemID);

                if (hasItem && !evadeSpells.Exists(s => s.spellName == spell.spellName))
                {
                    evadeSpells.Add(spell);

                    string menuName = spell.name + " Settings";

                    Menu newSpellMenu = new Menu(menuName, spell.charName + spell.name + "EvadeSpellSettings");
                    newSpellMenu.AddItem(new MenuItem(spell.name + "UseEvadeSpell", "Use Spell").SetValue(true));
                    newSpellMenu.AddItem(new MenuItem(spell.name + "EvadeSpellDangerLevel", "Danger Level")
                        .SetValue(new StringList(new[] { "Low", "Normal", "High", "Extreme" }, spell.dangerlevel - 1)));

                    evadeSpellMenu.AddSubMenu(newSpellMenu);
                }
            }

            DelayAction.Add(5000, () => CheckForItems());
        }        

        public static bool PreferEvadeSpell()
        {
            if (!Situation.ShouldUseEvadeSpell())
                return false;

            var extraDelayBuffer = Evade.menu.Item("ExtraPingBuffer").GetValue<Slider>().Value;
            float fastEvadeTime = Evade.menu.Item("SpellActivationTime").GetValue<Slider>().Value + Game.Ping + extraDelayBuffer;
            
            foreach (KeyValuePair<int, Spell> entry in SpellDetector.spells)
            {
                Spell spell = entry.Value;

                if (!myHero.ServerPosition.To2D().InSkillShot(spell, myHero.BoundingRadius))
                    continue;
                
                float rEvadeTime, rSpellHitTime = 0;
                spell.CanHeroEvade(myHero, out rEvadeTime, out rSpellHitTime);

                float finalEvadeTime = (rSpellHitTime - rEvadeTime);              

                if (finalEvadeTime < fastEvadeTime)
                {
                    foreach (var evadeSpell in evadeSpells)
                    {
                        if (ActivateEvadeSpell(spell))
                        {
                            return true;
                        }
                    }
                }
            }

            return false;
        }

        public static void UseEvadeSpell()
        {
            if (!Situation.ShouldUseEvadeSpell())
            {
                return;
            }
            
            //int posDangerlevel = EvadeHelper.CheckPosDangerLevel(myHero.ServerPosition.To2D(), 0);

            if (Evade.TickCount - lastSpellEvadeCommand.timestamp < 1000)
            {
                return;
            }
            
            foreach (KeyValuePair<int, Spell> entry in SpellDetector.spells)
            {
                Spell spell = entry.Value;

                if (ShouldActivateEvadeSpell(spell))
                {
                    ActivateEvadeSpell(spell);
                }
            }

        }

        public static bool ActivateEvadeSpell(Spell spell)
        {
            var sortedEvadeSpells = evadeSpells.OrderBy(s => s.dangerlevel);

            foreach (var evadeSpell in sortedEvadeSpells)
            {
                if (Evade.menu.SubMenu("EvadeSpells").SubMenu(evadeSpell.charName + evadeSpell.name + "EvadeSpellSettings")
                        .Item(evadeSpell.name + "UseEvadeSpell").GetValue<bool>() == false
                    || GetSpellDangerLevel(evadeSpell) > spell.GetSpellDangerLevel()
                    || (evadeSpell.isItem == false && !(myHero.Spellbook.CanUseSpell(evadeSpell.spellKey) == SpellState.Ready))
                    || (evadeSpell.isItem == true && !(Items.CanUseItem((int)evadeSpell.itemID)))
                    || (evadeSpell.checkSpellName == true && myHero.Spellbook.GetSpell(evadeSpell.spellKey).Name != evadeSpell.spellName))
                {
                    continue; //can't use spell right now               
                }

                if (evadeSpell.isSpecial == true)
                {
                    if (evadeSpell.useSpellFunc != null)
                    {
                        if (evadeSpell.useSpellFunc(evadeSpell))
                        {
                            return true;
                        }
                    }

                    continue;
                }
                else if (evadeSpell.evadeType == EvadeType.Blink)
                {
                    if (evadeSpell.castType == CastType.Position)
                    {
                        var posInfo = EvadeHelper.GetBestPositionBlink();
                        if (posInfo != null)
                        {
                            EvadeCommand.CastSpell(evadeSpell, posInfo.position);
                            //DelayAction.Add(50, () => myHero.IssueOrder(GameObjectOrder.MoveTo, posInfo.position.To3D()));
                            return true;   
                        }
                    }
                    else if (evadeSpell.castType == CastType.Target)
                    {
                        var posInfo = EvadeHelper.GetBestPositionTargetedDash(evadeSpell);
                        if (posInfo != null && posInfo.target != null)
                        {
                            EvadeCommand.CastSpell(evadeSpell, posInfo.target);
                            //DelayAction.Add(50, () => myHero.IssueOrder(GameObjectOrder.MoveTo, posInfo.position.To3D()));
                            return true;
                        }
                    }
                }
                else if (evadeSpell.evadeType == EvadeType.Dash)
                {
                    if (evadeSpell.castType == CastType.Position)
                    {
                        var posInfo = EvadeHelper.GetBestPositionDash(evadeSpell);
                        if (posInfo != null)
                        {
                            if (evadeSpell.isReversed)
                            {
                                var dir = (posInfo.position - myHero.ServerPosition.To2D()).Normalized();
                                var range = myHero.ServerPosition.To2D().Distance(posInfo.position);
                                var pos = myHero.ServerPosition.To2D() - dir * range;

                                posInfo.position = pos;
                            }

                            EvadeCommand.CastSpell(evadeSpell, posInfo.position);
                            //DelayAction.Add(50, () => myHero.IssueOrder(GameObjectOrder.MoveTo, posInfo.position.To3D()));
                            return true;
                        }
                    }
                    else if (evadeSpell.castType == CastType.Target)
                    {
                        var posInfo = EvadeHelper.GetBestPositionTargetedDash(evadeSpell);
                        if (posInfo != null && posInfo.target != null)
                        {
                            EvadeCommand.CastSpell(evadeSpell, posInfo.target);
                            //DelayAction.Add(50, () => myHero.IssueOrder(GameObjectOrder.MoveTo, posInfo.position.To3D()));
                            return true;
                        }
                    }
                }
                else if (evadeSpell.evadeType == EvadeType.WindWall)
                {
                    if (spell.hasProjectile())
                    {
                        var dir = (spell.startPos - myHero.ServerPosition.To2D()).Normalized();
                        var pos = myHero.ServerPosition.To2D() + dir * 100;

                        EvadeCommand.CastSpell(evadeSpell, pos);
                    }
                }
                else if (evadeSpell.evadeType == EvadeType.SpellShield)
                {
                    if (evadeSpell.isItem)
                    {
                        Items.UseItem((int)evadeSpell.itemID);
                        return true;
                    }
                    else
                    {
                        EvadeCommand.CastSpell(evadeSpell);
                        return true;
                    }
                }               
            }

            return false;
        }

        private static bool ShouldActivateEvadeSpell(Spell spell)
        {
            if (Evade.lastPosInfo == null)
                return false;

            if (Evade.menu.SubMenu("Main").Item("DodgeSkillShots").GetValue<KeyBind>().Active)
            {
                if (Evade.lastPosInfo.undodgeableSpells.Contains(spell.spellID)
                && myHero.ServerPosition.To2D().InSkillShot(spell, myHero.BoundingRadius))
                {
                    return true;
                }
            }
            else
            {
                if (myHero.ServerPosition.To2D().InSkillShot(spell, myHero.BoundingRadius))
                {
                    return true;
                }
            }
            

            /*float activationTime = Evade.menu.SubMenu("MiscSettings").SubMenu("EvadeSpellMisc").Item("EvadeSpellActivationTime")
                .GetValue<Slider>().Value + Game.Ping;

            if (spell.spellHitTime != float.MinValue && activationTime > spell.spellHitTime - spell.evadeTime)
            {
                return true;
            }*/

            return false;
        }

        public static int GetSpellDangerLevel(EvadeSpellData spell)
        {
            var dangerStr = Evade.menu.SubMenu("EvadeSpells").SubMenu(spell.charName + spell.name + "EvadeSpellSettings")
                .Item(spell.name + "EvadeSpellDangerLevel").GetValue<StringList>().SelectedValue;

            var dangerlevel = 1;

            switch (dangerStr)
            {
                case "Low":
                    dangerlevel = 1;
                    break;
                case "High":
                    dangerlevel = 3;
                    break;
                case "Extreme":
                    dangerlevel = 4;
                    break;
                default:
                    dangerlevel = 2;
                    break;
            }

            return dangerlevel;
        }

        private SpellSlot GetSummonerSlot(string spellName)
        {
            if (myHero.Spellbook.GetSpell(SpellSlot.Summoner1).SData.Name == spellName)
            {
                return SpellSlot.Summoner1;
            }
            else if (myHero.Spellbook.GetSpell(SpellSlot.Summoner2).SData.Name == spellName)
            {
                return SpellSlot.Summoner2;
            }

            return SpellSlot.Unknown;
        }

        private void LoadEvadeSpellList()
        {

            foreach (var spell in EvadeSpellDatabase.Spells.Where(
                s => (s.charName == myHero.ChampionName || s.charName == "AllChampions")))
            {

                if (spell.isSummonerSpell)
                {
                    SpellSlot spellKey = GetSummonerSlot(spell.spellName);
                    if (spellKey == SpellSlot.Unknown)
                    {
                        continue;
                    }
                    else
                    {
                        spell.spellKey = spellKey;
                    }
                }

                if (spell.isItem)
                {
                    itemSpells.Add(spell);
                    continue;
                }

                if (spell.isSpecial)
                {
                    SpecialEvadeSpell.LoadSpecialSpell(spell);
                }

                evadeSpells.Add(spell);

                string menuName = spell.name + " (" + spell.spellKey.ToString() + ") Settings";

                Menu newSpellMenu = new Menu(menuName, spell.charName + spell.name + "EvadeSpellSettings");
                newSpellMenu.AddItem(new MenuItem(spell.name + "UseEvadeSpell", "Use Spell").SetValue(true));
                newSpellMenu.AddItem(new MenuItem(spell.name + "EvadeSpellDangerLevel", "Danger Level")
                    .SetValue(new StringList(new[] { "Low", "Normal", "High", "Extreme" }, spell.dangerlevel - 1)));

                evadeSpellMenu.AddSubMenu(newSpellMenu);
            }

            evadeSpells.Sort((a, b) => a.dangerlevel.CompareTo(b.dangerlevel));
        }
    }
}
