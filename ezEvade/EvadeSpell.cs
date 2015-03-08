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
        public static EvadeCommand lastSpellEvadeCommand = new EvadeCommand { isProcessed = true, timestamp = Evade.GetTickCount() };

        public static Menu menu;
        public static Menu evadeSpellMenu;

        public EvadeSpell(Menu mainMenu)
        {
            menu = mainMenu;

            evadeSpellMenu = new Menu("Evade Spells", "EvadeSpells");
            menu.AddSubMenu(evadeSpellMenu);

            LoadEvadeSpellList();
        }

        public static void UseEvadeSpell()
        {
            if (!Evade.menu.SubMenu("Main").Item("UseEvadeSpells").GetValue<bool>())
            {
                return;
            }
            
            //int posDangerlevel = EvadeHelper.CheckPosDangerLevel(myHero.ServerPosition.To2D(), 0);

            if (Evade.GetTickCount() - lastSpellEvadeCommand.timestamp < 1000)
            {
                return;
            }

            foreach (KeyValuePair<int, Spell> entry in SpellDetector.spells)
            {
                Spell spell = entry.Value;

                if (!Evade.lastPosInfo.undodgeableSpells.Contains(spell.spellID))
                {
                    continue;
                }

                foreach (var evadeSpell in evadeSpells)
                {
                    if (Evade.menu.SubMenu("EvadeSpells").SubMenu(evadeSpell.charName + evadeSpell.name + "EvadeSpellSettings")
                .Item(evadeSpell.name + "UseEvadeSpell").GetValue<bool>() == false
                        || GetSpellDangerLevel(evadeSpell) > EvadeHelper.GetSpellDangerLevel(spell)
                        || !(myHero.Spellbook.CanUseSpell(evadeSpell.spellKey) == SpellState.Ready))
                    {
                        continue; //can't use spell right now
                    }

                    if (evadeSpell.evadeType == EvadeType.Blink)
                    {
                        var posInfo = EvadeHelper.GetBestPositionBlink();
                        if (posInfo != null)
                        {
                            EvadeCommand.CastSpell(evadeSpell, posInfo.position);
                        }
                    }
                    else if (evadeSpell.evadeType == EvadeType.Dash)
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
                        }
                    }
                    else if (evadeSpell.evadeType == EvadeType.SpellShield)
                    {
                        EvadeCommand.CastSpell(evadeSpell);
                    }

                    return;
                }
            }

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
