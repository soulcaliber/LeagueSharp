using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Color = System.Drawing.Color;

using LeagueSharp;
using LeagueSharp.Common;
using SharpDX;

namespace ezEvade
{
    internal class SpellDrawer
    {
        public static Menu menu;
        private static float gameTime { get { return Game.ClockTime * 1000; } }
        private static Obj_AI_Hero myHero { get { return ObjectManager.Player; } }


        public SpellDrawer(Menu mainMenu)
        {
            Drawing.OnDraw += Drawing_OnDraw;

            menu = mainMenu;
            Game_OnGameLoad();
        }

        private void Game_OnGameLoad()
        {
            //Game.PrintChat("SpellDrawer loaded");

            Menu drawMenu = new Menu("Draw", "Draw");
            drawMenu.AddItem(new MenuItem("DrawSkillShots", "Draw SkillShots").SetValue(true));
            menu.AddSubMenu(drawMenu);           
        }

        private void DrawLineRectangle(Vector2 start, Vector2 end, int radius, int width, Color color)
        {
            var dir = (end - start).Normalized();
            var pDir = dir.Perpendicular();

            var rightStartPos = start + pDir * radius;
            var leftStartPos = start - pDir * radius;
            var rightEndPos = end + pDir * radius;
            var leftEndPos = end - pDir * radius;

            var rStartPos = Drawing.WorldToScreen(new Vector3(rightStartPos.X, rightStartPos.Y, myHero.Position.Z));
            var lStartPos = Drawing.WorldToScreen(new Vector3(leftStartPos.X, leftStartPos.Y, myHero.Position.Z));
            var rEndPos = Drawing.WorldToScreen(new Vector3(rightEndPos.X, rightEndPos.Y, myHero.Position.Z));
            var lEndPos = Drawing.WorldToScreen(new Vector3(leftEndPos.X, leftEndPos.Y, myHero.Position.Z));

            Drawing.DrawLine(rStartPos, rEndPos, width, color);
            Drawing.DrawLine(lStartPos, lEndPos, width, color);
            Drawing.DrawLine(rStartPos, lStartPos, width, color);
            Drawing.DrawLine(lEndPos, rEndPos, width, color);
        }

        private void Drawing_OnDraw(EventArgs args)
        {
            if (menu.SubMenu("Draw").Item("DrawSkillShots").GetValue<bool>() == false)
            {
                return;
            }     

            foreach (KeyValuePair<int, Spell> entry in SpellDetector.drawSpells)
            {
                Spell spell = entry.Value;

                if (spell.info.spellType == SpellType.Line)
                {
                    Vector2 spellPos = SpellDetector.GetCurrentSpellPosition(spell);
                    DrawLineRectangle(spellPos, spell.endPos, (int)spell.info.radius, 3, Color.White);
                }
                else if (spell.info.spellType == SpellType.Circular)
                {
                    Render.Circle.DrawCircle(new Vector3(spell.endPos.X, spell.endPos.Y, myHero.Position.Z), spell.info.radius, Color.White, 3);
                }
                else if (spell.info.spellType == SpellType.Cone)
                {

                }

            }
        }
    }
}
