using System;
using System.Linq;
using LeagueSharp;
using LeagueSharp.Common;
using SharpDX;
using Color = System.Drawing.Color;

namespace ezEvade
{
    internal class SpellDrawer
    {
        public static Menu Menu;

        public SpellDrawer(Menu mainMenu)
        {
            Drawing.OnDraw += Drawing_OnDraw;

            Menu = mainMenu;
            Game_OnGameLoad();
        }

        private static float GameTime
        {
            get { return Game.ClockTime*1000; }
        }

        private static Obj_AI_Hero MyHero
        {
            get { return ObjectManager.Player; }
        }

        private static void Game_OnGameLoad()
        {
            //Game.PrintChat("SpellDrawer loaded");

            var drawMenu = new Menu("Draw", "Draw");
            drawMenu.AddItem(new MenuItem("DrawSkillShots", "Draw SkillShots").SetValue(true));
            Menu.AddSubMenu(drawMenu);
        }

        private static void DrawLineRectangle(Vector2 start, Vector2 end, int radius, int width, Color color)
        {
            var dir = (end - start).Normalized();
            var pDir = dir.Perpendicular();

            var rightStartPos = start + pDir*radius;
            var leftStartPos = start - pDir*radius;
            var rightEndPos = end + pDir*radius;
            var leftEndPos = end - pDir*radius;

            var rStartPos = Drawing.WorldToScreen(new Vector3(rightStartPos.X, rightStartPos.Y, MyHero.Position.Z));
            var lStartPos = Drawing.WorldToScreen(new Vector3(leftStartPos.X, leftStartPos.Y, MyHero.Position.Z));
            var rEndPos = Drawing.WorldToScreen(new Vector3(rightEndPos.X, rightEndPos.Y, MyHero.Position.Z));
            var lEndPos = Drawing.WorldToScreen(new Vector3(leftEndPos.X, leftEndPos.Y, MyHero.Position.Z));

            Drawing.DrawLine(rStartPos, rEndPos, width, color);
            Drawing.DrawLine(lStartPos, lEndPos, width, color);
            Drawing.DrawLine(rStartPos, lStartPos, width, color);
            Drawing.DrawLine(lEndPos, rEndPos, width, color);
        }

        private static void Drawing_OnDraw(EventArgs args)
        {
            if (Menu.SubMenu("Draw").Item("DrawSkillShots").GetValue<bool>() == false)
            {
                return;
            }

            foreach (var spell in SpellDetector.DrawSpells.Select(entry => entry.Value))
            {
                switch (spell.Info.SpellType)
                {
                    case SpellType.Line:
                        var spellPos = SpellDetector.GetCurrentSpellPosition(spell);
                        DrawLineRectangle(spellPos, spell.EndPos, (int) spell.Info.Radius, 3, Color.White);
                        break;
                    case SpellType.Circular:
                        Render.Circle.DrawCircle(new Vector3(spell.EndPos.X, spell.EndPos.Y, MyHero.Position.Z),
                            spell.Info.Radius, Color.White, 3);
                        break;
                    case SpellType.Cone:

                        break;
                }
            }
        }
    }
}