using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Color = System.Drawing.Color;

using LeagueSharp;
using LeagueSharp.Common;
using SharpDX;

namespace ezEvade
{
    internal class SpellDrawer
    {
        public static Menu menu;

        private static Obj_AI_Hero myHero { get { return ObjectManager.Player; } }


        public SpellDrawer(Menu mainMenu)
        {
            Drawing.OnDraw += Drawing_OnDraw;

            menu = mainMenu;
            Game_OnGameLoad();
        }

        private void Game_OnGameLoad()
        {
            //Console.WriteLine("SpellDrawer loaded");

            Menu drawMenu = new Menu("Draw", "Draw");
            drawMenu.AddItem(new MenuItem("DrawSkillShots", "Draw SkillShots").SetValue(true));
            drawMenu.AddItem(new MenuItem("ShowStatus", "Show Evade Status").SetValue(true));
            drawMenu.AddItem(new MenuItem("DrawSpellPos", "Draw Spell Position").SetValue(false));
            drawMenu.AddItem(new MenuItem("DrawEvadePosition", "Draw Evade Position").SetValue(false));

            Menu dangerMenu = new Menu("DangerLevel Drawings", "DangerLevelDrawings");
            Menu lowDangerMenu = new Menu("Low", "LowDrawing");
            lowDangerMenu.AddItem(new MenuItem("LowWidth", "Line Width").SetValue(new Slider(3, 1, 15)));
            lowDangerMenu.AddItem(new MenuItem("LowColor", "Color").SetValue(new Circle(true, Color.FromArgb(60, 255, 255, 255))));

            Menu normalDangerMenu = new Menu("Normal", "NormalDrawing");
            normalDangerMenu.AddItem(new MenuItem("NormalWidth", "Line Width").SetValue(new Slider(3, 1, 15)));
            normalDangerMenu.AddItem(new MenuItem("NormalColor", "Color").SetValue(new Circle(true, Color.FromArgb(140, 255, 255, 255))));

            Menu highDangerMenu = new Menu("High", "HighDrawing");
            highDangerMenu.AddItem(new MenuItem("HighWidth", "Line Width").SetValue(new Slider(3, 1, 15)));
            highDangerMenu.AddItem(new MenuItem("HighColor", "Color").SetValue(new Circle(true, Color.FromArgb(255, 255, 255, 255))));

            Menu extremeDangerMenu = new Menu("Extreme", "ExtremeDrawing");
            extremeDangerMenu.AddItem(new MenuItem("ExtremeWidth", "Line Width").SetValue(new Slider(4, 1, 15)));
            extremeDangerMenu.AddItem(new MenuItem("ExtremeColor", "Color").SetValue(new Circle(true, Color.FromArgb(255, 255, 255, 255))));

            /*
            Menu undodgeableDangerMenu = new Menu("Undodgeable", "Undodgeable");
            undodgeableDangerMenu.AddItem(new MenuItem("Width", "Line Width").SetValue(new Slider(6, 1, 15)));
            undodgeableDangerMenu.AddItem(new MenuItem("Color", "Color").SetValue(new Circle(true, Color.FromArgb(255, 255, 0, 0))));*/

            dangerMenu.AddSubMenu(lowDangerMenu);
            dangerMenu.AddSubMenu(normalDangerMenu);
            dangerMenu.AddSubMenu(highDangerMenu);
            dangerMenu.AddSubMenu(extremeDangerMenu);

            drawMenu.AddSubMenu(dangerMenu);

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

        private void DrawLineTriangle(Vector2 start, Vector2 end, int radius, int width, Color color)
        {
            var dir = (end - start).Normalized();
            var pDir = dir.Perpendicular();

            var initStartPos = start + dir;
            var rightEndPos = end + pDir * radius;
            var leftEndPos = end - pDir * radius;

            var iStartPos = Drawing.WorldToScreen(new Vector3(initStartPos.X, initStartPos.Y, myHero.Position.Z));
            var rEndPos = Drawing.WorldToScreen(new Vector3(rightEndPos.X, rightEndPos.Y, myHero.Position.Z));
            var lEndPos = Drawing.WorldToScreen(new Vector3(leftEndPos.X, leftEndPos.Y, myHero.Position.Z));

            Drawing.DrawLine(iStartPos, rEndPos, width, color);
            Drawing.DrawLine(iStartPos, lEndPos, width, color);
            Drawing.DrawLine(rEndPos, lEndPos, width, color);
        }

        private void DrawEvadeStatus()
        {
            if (ObjectCache.menuCache.cache["ShowStatus"].GetValue<bool>())
            {
                var heroPos = Drawing.WorldToScreen(ObjectManager.Player.Position);
                var dimension = Drawing.GetTextExtent("Evade: ON");

                if (ObjectCache.menuCache.cache["DodgeSkillShots"].GetValue<KeyBind>().Active)
                {
                    if (Evade.isDodging)
                    {
                        Drawing.DrawText(heroPos.X - dimension.Width / 2, heroPos.Y, Color.Red, "Evade: ON");
                    }
                    else
                    {
                        if (ObjectCache.menuCache.cache["DodgeOnlyOnComboKeyEnabled"].GetValue<bool>() == true
                         && ObjectCache.menuCache.cache["DodgeComboKey"].GetValue<KeyBind>().Active == false)
                        {
                            Drawing.DrawText(heroPos.X - dimension.Width / 2, heroPos.Y, Color.Gray, "Evade: OFF");
                        }
                        else
                        {
                            if (ObjectCache.menuCache.cache["DontDodgeKeyEnabled"].GetValue<bool>() == true
                         && ObjectCache.menuCache.cache["DontDodgeKey"].GetValue<KeyBind>().Active == true)
                                Drawing.DrawText(heroPos.X - dimension.Width / 2, heroPos.Y, Color.Gray, "Evade: OFF");
                            else if (Evade.isDodgeDangerousEnabled())
                                Drawing.DrawText(heroPos.X - dimension.Width / 2, heroPos.Y, Color.Yellow, "Evade: ON");
                            else
                                Drawing.DrawText(heroPos.X - dimension.Width / 2, heroPos.Y, Color.Lime, "Evade: ON");
                        }
                    }
                }
                else
                {
                    if (ObjectCache.menuCache.cache["ActivateEvadeSpells"].GetValue<KeyBind>().Active)
                    {
                        if (ObjectCache.menuCache.cache["DodgeOnlyOnComboKeyEnabled"].GetValue<bool>() == true
                         && ObjectCache.menuCache.cache["DodgeComboKey"].GetValue<KeyBind>().Active == false)
                        {
                            Drawing.DrawText(heroPos.X - dimension.Width / 2, heroPos.Y, Color.Gray, "Evade: OFF");
                        }
                        else
                        {
                            if (Evade.isDodgeDangerousEnabled())
                                Drawing.DrawText(heroPos.X - dimension.Width / 2, heroPos.Y, Color.Yellow, "Evade: Spell");
                            else
                                Drawing.DrawText(heroPos.X - dimension.Width / 2, heroPos.Y, Color.DeepSkyBlue, "Evade: Spell");
                        }
                    }
                    else
                    {
                        Drawing.DrawText(heroPos.X - dimension.Width / 2, heroPos.Y, Color.Gray, "Evade: OFF");
                    }
                }



            }
        }

        private void Drawing_OnDraw(EventArgs args)
        {

            if (ObjectCache.menuCache.cache["DrawEvadePosition"].GetValue<bool>())
            {
                //Render.Circle.DrawCircle(myHero.Position.ExtendDir(dir, 500), 65, Color.Red, 10);

                /*foreach (var point in myHero.Path)
                {
                    Render.Circle.DrawCircle(point, 65, Color.Red, 10);
                }*/

                if (Evade.lastPosInfo != null)
                {
                    var pos = Evade.lastPosInfo.position; //Evade.lastEvadeCommand.targetPosition;
                    Render.Circle.DrawCircle(new Vector3(pos.X, pos.Y, myHero.Position.Z), 65, Color.Red, 10);
                }
            }

            DrawEvadeStatus();

            if (ObjectCache.menuCache.cache["DrawSkillShots"].GetValue<bool>() == false)
            {
                return;
            }

            foreach (KeyValuePair<int, Spell> entry in SpellDetector.drawSpells)
            {
                Spell spell = entry.Value;

                var dangerStr = spell.GetSpellDangerString();
                var spellDrawingConfig = ObjectCache.menuCache.cache[dangerStr + "Color"].GetValue<Circle>();
                var spellDrawingWidth = ObjectCache.menuCache.cache[dangerStr + "Width"].GetValue<Slider>().Value;
                var avoidRadius = ObjectCache.menuCache.cache["ExtraAvoidDistance"].GetValue<Slider>().Value;

                if (ObjectCache.menuCache.cache[spell.info.spellName + "DrawSpell"].GetValue<bool>()
                    && spellDrawingConfig.Active)
                {

                    bool canEvade = !(Evade.lastPosInfo != null && Evade.lastPosInfo.undodgeableSpells.Contains(spell.spellID)) || !Evade.devModeOn;

                    if (spell.spellType == SpellType.Line)
                    {
                        Vector2 spellPos = spell.currentSpellPosition;
                        Vector2 spellEndPos = spell.GetSpellEndPosition();

                        DrawLineRectangle(spellPos, spellEndPos, (int) spell.radius, 
                            spellDrawingWidth, !canEvade ? Color.Yellow : spellDrawingConfig.Color);

                        if (Evade.devModeOn)
                            DrawLineRectangle(spellPos, spellEndPos, (int) spell.radius + avoidRadius,
                                Math.Max(spellDrawingWidth - 1, 1), !canEvade ? Color.Yellow : spellDrawingConfig.Color);

                        if (ObjectCache.menuCache.cache["DrawSpellPos"].GetValue<bool>())// && spell.spellObject != null)
                        {
                            Render.Circle.DrawCircle(new Vector3(spellPos.X, spellPos.Y, spell.height), (int) spell.radius, !canEvade ? Color.Yellow : spellDrawingConfig.Color, spellDrawingWidth);
                        }

                    }
                    else if (spell.spellType == SpellType.Circular)
                    {

                        Render.Circle.DrawCircle(new Vector3(spell.endPos.X, spell.endPos.Y, spell.height), (int) spell.radius, !canEvade ? Color.Yellow : spellDrawingConfig.Color, spellDrawingWidth);

                        if (Evade.devModeOn)
                            Render.Circle.DrawCircle(new Vector3(spell.endPos.X, spell.endPos.Y, spell.height),
                                (int) spell.radius + avoidRadius, !canEvade ? Color.Yellow : spellDrawingConfig.Color,
                                Math.Max(spellDrawingWidth - 1, 1));

                        if (spell.info.spellName == "VeigarEventHorizon")
                        {
                            Render.Circle.DrawCircle(new Vector3(spell.endPos.X, spell.endPos.Y, spell.height), (int) spell.radius - 125, !canEvade ? Color.Yellow : spellDrawingConfig.Color, spellDrawingWidth);
                        }
                        else if (spell.info.spellName == "DariusCleave")
                        {
                            Render.Circle.DrawCircle(new Vector3(spell.endPos.X, spell.endPos.Y, spell.height), (int) spell.radius - 220, !canEvade ? Color.Yellow : spellDrawingConfig.Color, spellDrawingWidth);
                        }
                    }
                    else if (spell.spellType == SpellType.Arc)
                    {
                        /*var spellRange = spell.startPos.Distance(spell.endPos);
                        var midPoint = spell.startPos + spell.direction * (spellRange / 2);

                        Render.Circle.DrawCircle(new Vector3(midPoint.X, midPoint.Y, myHero.Position.Z), (int)spell.radius, spellDrawingConfig.Color, spellDrawingWidth);
                        
                        Drawing.DrawLine(Drawing.WorldToScreen(spell.startPos.To3D()),
                                         Drawing.WorldToScreen(spell.endPos.To3D()), 
                                         spellDrawingWidth, spellDrawingConfig.Color);*/
                    }
                    else if (spell.spellType == SpellType.Cone)
                    {
                        DrawLineTriangle(spell.startPos, spell.endPos, (int) spell.radius, spellDrawingWidth, spellDrawingConfig.Color);
                    }
                }
            }
        }
    }
}
