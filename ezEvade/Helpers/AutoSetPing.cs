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
    class AutoSetPing
    {
        public static Obj_AI_Hero myHero { get { return ObjectManager.Player; } }

        private static float sumExtraDelayTime = 0;
        private static float avgExtraDelayTime = 0;
        private static float numExtraDelayTime = 0;

        private static float maxExtraDelayTime = 0;

        private static GameObjectIssueOrderEventArgs lastIssueOrderArgs;
        private static Vector2 lastMoveToServerPos;
        private static Vector2 lastPathEndPos;

        private static SpellbookCastSpellEventArgs lastSpellCastArgs;
        private static Vector2 lastSpellCastServerPos;
        private static Vector2 lastSpellCastEndPos;

        private static float testSkillshotDelayStart = 0;
        private static bool testSkillshotDelayOn = false;

        private static bool checkPing = true;

        private static List<float> pingList = new List<float>();

        public static Menu menu;

        public AutoSetPing(Menu mainMenu)
        {
            Obj_AI_Hero.OnNewPath += Hero_OnNewPath;
            Obj_AI_Hero.OnIssueOrder += Hero_OnIssueOrder;

            Spellbook.OnCastSpell += Game_OnCastSpell;
            MissileClient.OnCreate += Game_OnCreateObj;
            Obj_AI_Hero.OnProcessSpellCast += Game_ProcessSpell;

            //Game.OnUpdate += Game_OnUpdate;

            //Drawing.OnDraw += Game_OnDraw;

            Menu autoSetPingMenu = new Menu("AutoSetPing", "AutoSetPingMenu");
            autoSetPingMenu.AddItem(new MenuItem("AutoSetPingOn", "Auto Set Ping").SetValue<bool>(true));
            autoSetPingMenu.AddItem(new MenuItem("AutoSetPercentile", "Auto Set Percentile").SetValue(new Slider(75, 0, 100)));

            //autoSetPingMenu.AddItem(new MenuItem("TestSkillshotDelay", "TestSkillshotDelay").SetValue<bool>(false));

            mainMenu.AddSubMenu(autoSetPingMenu);

            menu = mainMenu;
        }

        private void Game_ProcessSpell(Obj_AI_Base sender, GameObjectProcessSpellCastEventArgs args)
        {
            if (!sender.IsMe)
            {
                return;
            }

            //lastSpellCastServerPos = myHero.Position.To2D();
        }

        private void Game_OnDraw(EventArgs args)
        {
            Render.Circle.DrawCircle(myHero.Position, 10, Color.Red, 5);
            Render.Circle.DrawCircle(myHero.ServerPosition, 10, Color.Red, 5);
        }

        private void Game_OnCastSpell(Spellbook sender, SpellbookCastSpellEventArgs args)
        {
            var hero = sender.Owner;

            checkPing = false;

            if (!hero.IsMe)
            {
                return;
            }

            lastSpellCastArgs = args;


            if (myHero.IsMoving && myHero.Path.Count() > 0)
            {
                lastSpellCastServerPos = EvadeUtils.GetGamePosition(myHero, Game.Ping);
                lastSpellCastEndPos = myHero.Path.Last().To2D();
                checkPing = true;

                Draw.RenderObjects.Add(new Draw.RenderCircle(lastSpellCastServerPos, 1000, Color.Green, 10));
            }

        }

        private void Game_OnCreateObj(GameObject sender, EventArgs args)
        {
            var missile = sender as MissileClient;
            if (missile != null && missile.SpellCaster.IsMe)
            {
                if (lastSpellCastArgs.Process == true
                    )
                {
                    //Draw.RenderObjects.Add(new Draw.RenderPosition(lastSpellCastServerPos, 1000, System.Drawing.Color.Red, 10));
                    Draw.RenderObjects.Add(new Draw.RenderCircle(missile.StartPosition.To2D(), 1000, System.Drawing.Color.Red, 10));

                    var distance = lastSpellCastServerPos.Distance(missile.StartPosition.To2D());
                    float moveTime = 1000 * distance / myHero.MoveSpeed;
                    Console.WriteLine("Extra Delay: " + moveTime);
                }
            }
        }

        private void Game_OnUpdate(EventArgs args)
        {
            if (ObjectCache.menuCache.cache["TestSkillshotDelay"].GetValue<bool>())
            {
                testSkillshotDelayStart = EvadeUtils.TickCount;
                testSkillshotDelayOn = true;
                ObjectCache.menuCache.cache["TestSkillshotDelay"].SetValue<bool>(false);
            }

            if (testSkillshotDelayOn && SpellDetector.spells.Count() > 0)
            {
                Console.WriteLine("Delay: " + (EvadeUtils.TickCount - testSkillshotDelayStart));
                testSkillshotDelayOn = false;
            }


        }

        private void Hero_OnIssueOrder(Obj_AI_Base hero, GameObjectIssueOrderEventArgs args)
        {
            checkPing = false;

            var distance = myHero.Position.To2D().Distance(myHero.ServerPosition.To2D());
            float moveTime = 1000 * distance / myHero.MoveSpeed;
            //Console.WriteLine("Extra Delay: " + moveTime);

            if (ObjectCache.menuCache.cache["AutoSetPingOn"].GetValue<bool>() == false)
            {
                return;
            }

            if (!hero.IsMe)
            {
                return;
            }

            lastIssueOrderArgs = args;

            if (args.Order == GameObjectOrder.MoveTo)
            {
                if (myHero.IsMoving && myHero.Path.Count() > 0)
                {
                    lastMoveToServerPos = myHero.ServerPosition.To2D();
                    lastPathEndPos = myHero.Path.Last().To2D();
                    checkPing = true;
                }
            }
        }

        private void Hero_OnNewPath(Obj_AI_Base hero, GameObjectNewPathEventArgs args)
        {
            if (ObjectCache.menuCache.cache["AutoSetPingOn"].GetValue<bool>() == false)
            {
                return;
            }

            if (!hero.IsMe)
            {
                return;
            }

            var path = args.Path;

            if (path.Length > 1 && !args.IsDash)
            {
                var movePos = path.Last().To2D();

                if (checkPing
                    && lastIssueOrderArgs.Process == true
                    && lastIssueOrderArgs.Order == GameObjectOrder.MoveTo
                    && lastIssueOrderArgs.TargetPosition.To2D().Distance(movePos) < 3
                    && myHero.Path.Count() == 1
                    && args.Path.Count() == 2
                    && myHero.IsMoving)
                {
                    //Draw.RenderObjects.Add(new Draw.RenderPosition(myHero.Path.Last().To2D(), 1000));

                    Draw.RenderObjects.Add(new Draw.RenderLine(args.Path.First().To2D(), args.Path.Last().To2D(), 1000));
                    Draw.RenderObjects.Add(new Draw.RenderLine(myHero.Position.To2D(), myHero.Path.Last().To2D(), 1000));

                    //Draw.RenderObjects.Add(new Draw.RenderCircle(lastMoveToServerPos, 1000, System.Drawing.Color.Red, 10));

                    var distanceTillEnd = myHero.Path.Last().To2D().Distance(myHero.Position.To2D());
                    float moveTimeTillEnd = 1000 * distanceTillEnd / myHero.MoveSpeed;

                    if (moveTimeTillEnd < 500)
                    {
                        return;
                    }

                    var dir1 = (myHero.Path.Last().To2D() - myHero.Position.To2D()).Normalized();
                    var ray1 = new Ray(myHero.Position.SetZ(0), new Vector3(dir1.X, dir1.Y, 0));

                    var dir2 = (args.Path.First().To2D() - args.Path.Last().To2D()).Normalized();
                    var pos2 = new Vector3(args.Path.First().X, args.Path.First().Y, 0);
                    var ray2 = new Ray(args.Path.First().SetZ(0), new Vector3(dir2.X, dir2.Y, 0));

                    Vector3 intersection3;
                    if (ray2.Intersects(ref ray1, out intersection3))
                    {
                        var intersection = intersection3.To2D();

                        var projection = intersection.ProjectOn(myHero.Path.Last().To2D(), myHero.Position.To2D());

                        if (projection.IsOnSegment && dir1.AngleBetween(dir2) > 20 && dir1.AngleBetween(dir2) < 160)
                        {
                            Draw.RenderObjects.Add(new Draw.RenderCircle(intersection, 1000, System.Drawing.Color.Red, 10));

                            var distance = //args.Path.First().To2D().Distance(intersection);
                                lastMoveToServerPos.Distance(intersection);
                            float moveTime = 1000 * distance / myHero.MoveSpeed;

                            //Console.WriteLine("waa: " + distance);

                            if (moveTime < 1000)
                            {
                                if (numExtraDelayTime > 0)
                                {
                                    sumExtraDelayTime += moveTime;
                                    avgExtraDelayTime = sumExtraDelayTime / numExtraDelayTime;

                                    pingList.Add(moveTime);
                                }
                                numExtraDelayTime += 1;

                                if (maxExtraDelayTime == 0)
                                {
                                    maxExtraDelayTime = ObjectCache.menuCache.cache["ExtraPingBuffer"].GetValue<Slider>().Value;
                                }

                                if (numExtraDelayTime % 100 == 0)
                                {
                                    pingList.Sort();

                                    var percentile = ObjectCache.menuCache.cache["AutoSetPercentile"].GetValue<Slider>().Value;
                                    int percentIndex = (int)Math.Floor(pingList.Count() * (percentile / 100f)) - 1;
                                    maxExtraDelayTime = Math.Max(pingList.ElementAt(percentIndex) - Game.Ping,0);
                                    ObjectCache.menuCache.cache["ExtraPingBuffer"].SetValue(new Slider((int)maxExtraDelayTime, 0, 200));

                                    pingList.Clear();

                                    Console.WriteLine("Max Extra Delay: " + maxExtraDelayTime);
                                }

                                Console.WriteLine("Extra Delay: " + Math.Max(moveTime - Game.Ping,0));
                            }
                        }
                    }
                }

                checkPing = false;
            }
        }
    }
}
