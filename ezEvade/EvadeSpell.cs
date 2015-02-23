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
    class EvadeSpell
    {
        private static Obj_AI_Hero myHero { get { return ObjectManager.Player; } }
        private static float gameTime { get { return Game.ClockTime * 1000; } }

        public static List<EvadeSpellData> evadeSpells = new List<EvadeSpellData>();
        public static EvadeCommand lastSpellEvadeCommand = new EvadeCommand { isProcessed = true, timestamp = gameTime };

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
            int posDangerlevel = EvadeHelper.CheckPosDangerLevel(myHero.ServerPosition.To2D(), 0);

            if (posDangerlevel > 0 && Evade.lastPosInfo.dodgeableSpells.Count == 0 && gameTime - lastSpellEvadeCommand.timestamp > 1000)
            {
                foreach (var spell in evadeSpells)
                {
                    if (GetSpellDangerLevel(spell) > posDangerlevel || !(myHero.Spellbook.CanUseSpell(spell.spellKey) == SpellState.Ready))
                    {
                        continue; //can't use spell right now
                    }

                    if (spell.evadeType == EvadeType.Blink)
                    {
                        var posInfo = EvadeHelper.GetBestPositionBlink();
                        if (posInfo != null)
                        {
                            EvadeCommand.CastSpell(spell, posInfo.position);                            
                        }
                    }
                    else if (spell.evadeType == EvadeType.Dash)
                    {
                        var posInfo = EvadeHelper.GetBestPositionDash(spell);
                        if (posInfo != null)
                        {
                            EvadeCommand.CastSpell(spell, posInfo.position);
                        }
                    }
                    else if (spell.evadeType == EvadeType.SpellShield)
                    {

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
                    .SetValue(new StringList(new[] { "Low", "Normal", "High", "Extreme" }, spell.dangerlevel-1)));

                evadeSpellMenu.AddSubMenu(newSpellMenu);            
            }

            evadeSpells.Sort((a, b) => a.dangerlevel.CompareTo(b.dangerlevel));
        }
    }
}
