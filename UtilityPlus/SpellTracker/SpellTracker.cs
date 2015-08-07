using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Color = System.Drawing.Color;

using LeagueSharp;
using LeagueSharp.Common;
using SharpDX;

namespace UtilityPlus
{
    class SpellTracker
    {
        private static Menu menu;
        public static SpellSlot[] summonerSpellSlots = { SpellSlot.Summoner1, SpellSlot.Summoner2 };
        public static SpellSlot[] spellSlots = { SpellSlot.Q, SpellSlot.W, SpellSlot.E, SpellSlot.R };

        public static Color backgroundColor = Color.FromArgb(255, 15, 130, 0);

        public static float allyOffsetY = 14;
        public static float enemyOffsetY = 16;

        public SpellTracker(Menu mainMenu)
        {
            menu = mainMenu;

            Menu spellTrackerMenu = new Menu("Spell Tracker", "SpellTracker");

            spellTrackerMenu.AddItem(new MenuItem("TrackEnemyCooldown", "Track Enemies").SetValue(true));
            spellTrackerMenu.AddItem(new MenuItem("TrackAllyCooldown", "Track Allies").SetValue(true));

            menu.AddSubMenu(spellTrackerMenu);

            //Obj_AI_Hero.OnProcessSpellCast += Game_OnProcessSpell;

            SummonerData.GetSummonerColor("test");

            Drawing.OnDraw += Drawing_OnDraw;
        }

        private void Game_OnProcessSpell(Obj_AI_Base sender, GameObjectProcessSpellCastEventArgs args)
        {

        }

        private void Drawing_OnDraw(EventArgs args)
        {
            //var testTime = HelperUtils.TickCount;

            //for (int j = 0; j < 10;j++ )

            foreach (var hero in HeroManager.AllHeroes)
            {
                if (hero.IsMe || !hero.IsHPBarRendered
                    || (hero.IsAlly && !menu.Item("TrackAllyCooldown").GetValue<bool>())
                    || (hero.IsEnemy && !menu.Item("TrackEnemyCooldown").GetValue<bool>())
                    )
                {
                    continue;
                }

                var startX = hero.HPBarPosition.X - 13;
                var startY = hero.HPBarPosition.Y +
                    (hero.IsAlly ? allyOffsetY : enemyOffsetY);

                foreach (var slot in summonerSpellSlots)
                {
                    var spell = hero.Spellbook.GetSpell(slot);
                    var time = spell.CooldownExpires - Game.Time;

                    var percent = (time > 0 && Math.Abs(spell.Cooldown) > float.Epsilon)
                            ? 1f - (time / spell.Cooldown)
                            : 1f;

                    var spellState = hero.Spellbook.CanUseSpell(slot);

                    if (percent != 1f)
                    {
                        Drawing.DrawLine(new Vector2(startX, startY), new Vector2(startX + 20, startY), 12, Color.Black);
                        Drawing.DrawLine(new Vector2(startX + 1, startY + 1), new Vector2(startX + 19, startY + 1), 10, Color.Gray);
                    }
                    else
                    {
                        Drawing.DrawLine(new Vector2(startX, startY), new Vector2(startX + 20, startY), 12, Color.Green);
                    }


                    if (spellState != SpellState.NotLearned)
                    {
                        var color = Color.Orange;

                        if (slot == SpellSlot.Summoner1)
                        {
                            color = SummonerData.heroS1Color[hero.NetworkId];
                        }
                        else
                        {
                            color = SummonerData.heroS2Color[hero.NetworkId];
                        }

                        Drawing.DrawLine(new Vector2(startX + 1, startY + 1),
                                        new Vector2(startX + 1 + 18 * percent, startY + 1),
                                        10, color);
                    }

                    startY += 12;
                }

                startX += 23;
                startY -= 6;

                Drawing.DrawLine(new Vector2(startX - 2, startY - 1), new Vector2(startX + 105, startY - 1), 8, Color.Black);

                foreach (var slot in spellSlots)
                {
                    var spell = hero.Spellbook.GetSpell(slot);
                    var time = spell.CooldownExpires - Game.Time;

                    var percent = (time > 0 && Math.Abs(spell.Cooldown) > float.Epsilon)
                            ? 1f - (time / spell.Cooldown)
                            : 1f;

                    var spellState = hero.Spellbook.CanUseSpell(slot);

                    if (percent != 1f)
                    {
                        Drawing.DrawLine(new Vector2(startX, startY), new Vector2(startX + 20, startY), 5, Color.Gray);
                    }

                    if (spellState != SpellState.NotLearned)
                    {
                        var color = Color.Yellow;

                        if (percent == 1f)
                        {
                            color = Color.Green;
                        }

                        /*if (spellState == SpellState.NoMana)
                        {
                            color = Color.Blue;
                        }

                        if (spellState == SpellState.Disabled)
                        {
                            color = Color.Black;
                        }*/

                        if (time > 0 && time < 2)
                        {
                            color = Color.Orange;
                        }

                        Drawing.DrawLine(new Vector2(startX, startY), new Vector2(startX + 20 * percent, startY), 5, color);
                    }

                    startX = startX + 27;
                }
            }

            //Console.WriteLine("Time spent: " + (HelperUtils.TickCount - testTime));
        }
    }
}
