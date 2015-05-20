using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using System.Diagnostics;

using Color = System.Drawing.Color;

using LeagueSharp;
using LeagueSharp.Common;
using SharpDX;

namespace ezEvade
{
    class RenderPosition
    {
        public float renderEndTime = 0;
        public Vector2 renderPosition = new Vector2(0, 0);

        public RenderPosition(Vector2 renderPosition, float renderEndTime)
        {
            this.renderEndTime = renderEndTime;
            this.renderPosition = renderPosition;
        }
    }

    class EvadeTester
    {
        public static Menu menu;
        public static Menu testMenu;

        private static Obj_AI_Hero myHero { get { return ObjectManager.Player; } }

        private static Vector2 circleRenderPos;

        private static List<RenderPosition> renderPositions = new List<RenderPosition>();

        private static Vector2 startWalkPos;
        private static float startWalkTime = 0;

        private static Vector2 testCollisionPos;
        private static bool testingCollision = false;

        private static float lastStopingTime = 0;

        private static IOrderedEnumerable<EvadeHelper.PositionInfo> sortedBestPos;

        private static float lastGameTimerStart = 0;
        private static float lastTickCountTimerStart = 0;
        private static float lastWatchTimerStart = 0;

        private static float lastGameTimerTick = 0;
        private static float lastTickCountTimerTick = 0;
        private static float lastWatchTimerTick = 0;

        private static float getGameTimer { get { return Game.ClockTime * 1000; } }
        private static float getTickCountTimer { get { return Environment.TickCount & int.MaxValue; } }
        private static float getWatchTimer { get { return Evade.GetTickCount(); } }

        private static float lastTimerCheck = 0;
        private static bool lastRandomMoveCoeff = false;

        private static EvadeCommand lastTestMoveToCommand;

        private static float lastSpellCastTimeEx = 0;
        private static float lastSpellCastTime = 0;
        private static float lastHeroSpellCastTime = 0;

        public EvadeTester(Menu mainMenu)
        {
            lastGameTimerStart = getGameTimer;
            lastTickCountTimerStart = getTickCountTimer;
            lastWatchTimerStart = getWatchTimer;

            lastTimerCheck = getTickCountTimer;

            Drawing.OnDraw += Drawing_OnDraw;
            Obj_AI_Hero.OnIssueOrder += Game_OnIssueOrder;
            Game.OnUpdate += Game_OnGameUpdate;
            Game.OnInput += Game_OnGameInput;

            Obj_SpellMissile.OnCreate += SpellMissile_OnCreate;

            Obj_AI_Hero.OnProcessSpellCast += Game_ProcessSpell;
            Spellbook.OnCastSpell += Game_OnCastSpell;
            GameObject.OnFloatPropertyChange += GameObject_OnFloatPropertyChange;
            //GameObject.OnIntegerPropertyChange += GameObject_OnIntegerPropertyChange;
            //Game.OnGameNotifyEvent += Game_OnGameNotifyEvent;


            Obj_AI_Hero.OnNewPath += ObjAiHeroOnOnNewPath;

            SpellDetector.OnProcessDetectedSpells += SpellDetector_OnProcessDetectedSpells;

            menu = mainMenu;

            testMenu = new Menu("Test", "Test");
            testMenu.AddItem(new MenuItem("TestWall", "TestWall").SetValue(true));
            testMenu.AddItem(new MenuItem("TestPath", "TestPath").SetValue(true));
            testMenu.AddItem(new MenuItem("TestTracker", "TestTracker").SetValue(false));
            testMenu.AddItem(new MenuItem("TestHeroPos", "TestHeroPos").SetValue(true));
            testMenu.AddItem(new MenuItem("DrawHeroPos", "DrawHeroPos").SetValue(true));
            testMenu.AddItem(new MenuItem("TestSpellEndTime", "TestSpellEndTime").SetValue(true));
            testMenu.AddItem(new MenuItem("ShowBuffs", "ShowBuffs").SetValue(true));
            testMenu.AddItem(new MenuItem("ShowDashInfo", "ShowDashInfo").SetValue(true));
            testMenu.AddItem(new MenuItem("ShowProcessSpell", "ShowProcessSpell").SetValue(true));
            testMenu.AddItem(new MenuItem("ShowMissileInfo", "ShowMissileInfo").SetValue(true));
            testMenu.AddItem(new MenuItem("ShowWindupTime", "ShowWindupTime").SetValue(true));
            testMenu.AddItem(new MenuItem("TestMoveTo", "TestMoveTo").SetValue(new KeyBind('L', KeyBindType.Toggle, false)));
            menu.AddSubMenu(testMenu);

            Game_OnGameLoad();
        }

        private void Game_OnGameLoad()
        {
            Console.WriteLine("EvadeTester loaded");
            menu.AddSubMenu(new Menu("Test", "Test"));

            Console.WriteLine("Ping:" + Game.Ping);

        }

        private void Game_OnGameInput(GameInputEventArgs args)
        {
            Console.WriteLine("" + args.Input);

        }

        private static void ObjAiHeroOnOnNewPath(Obj_AI_Base unit, GameObjectNewPathEventArgs args)
        {
            if (unit.IsMe)
            {
                Console.WriteLine("Dash windup: " + (Evade.GetTickCount() - EvadeSpell.lastSpellEvadeCommand.timestamp));
            }
        }

        private void Game_OnCastSpell(Spellbook spellbook, SpellbookCastSpellEventArgs args)
        {
            if (!spellbook.Owner.IsMe)
                return;

            lastSpellCastTimeEx = Evade.GetTickCount();
        }

        private void SpellDetector_OnProcessDetectedSpells()
        {
            //var pos1 = newSpell.startPos;//SpellDetector.GetCurrentSpellPosition(newSpell);
            //DelayAction.Add(250, () => CompareSpellLocation2(newSpell));

            sortedBestPos = EvadeHelper.GetBestPositionTest();
            circleRenderPos = Evade.lastPosInfo.position;

            lastSpellCastTime = Evade.GetTickCount();
        }

        private void SpellMissile_OnCreate(GameObject obj, EventArgs args)
        {
            if (obj.IsValid<MissileClient>())
            {
                MissileClient autoattack = (MissileClient)obj;

                /*if (!autoattack.SpellCaster.IsMinion)
                {
                    Console.WriteLine("Missile Name " + autoattack.SData.Name);
                    Console.WriteLine("Missile Speed " + autoattack.SData.MissileSpeed);
                    Console.WriteLine("LineWidth " + autoattack.SData.LineWidth);
                    Console.WriteLine("Range " + autoattack.SData.CastRange);
                    Console.WriteLine("Accel " + autoattack.SData.MissileAccel);
                }*/
            }

            if (!obj.IsValid<Obj_SpellMissile>())
                return;

            if (testMenu.Item("ShowMissileInfo").GetValue<bool>() == false)
            {
                return;
            }

            Obj_SpellMissile missile = (Obj_SpellMissile)obj;


            Console.WriteLine("CastTime: " + (Evade.GetTickCount() - lastHeroSpellCastTime));
            Console.WriteLine("Missile Name " + missile.SData.Name);
            Console.WriteLine("Missile Speed " + missile.SData.MissileSpeed);
            Console.WriteLine("LineWidth " + missile.SData.LineWidth);
            Console.WriteLine("Range " + missile.SData.CastRange);
            Console.WriteLine("Accel " + missile.SData.MissileAccel);
            /*Console.WriteLine("Offset: " + missile.SData.ParticleStartOffset);
            Console.WriteLine("Missile Speed " + missile.SData.MissileSpeed);
            Console.WriteLine("LineWidth " + missile.SData.LineWidth);
            circleRenderPos = missile.SData.ParticleStartOffset.To2D();*/

            renderPositions.Add(
                new RenderPosition(missile.StartPosition.To2D(), Evade.GetTickCount() + 500));
            renderPositions.Add(
                new RenderPosition(missile.EndPosition.To2D(), Evade.GetTickCount() + 500));

            SpellData spellData;

            if (missile.SpellCaster != null && missile.SpellCaster.Team != myHero.Team &&
                missile.SData.Name != null && SpellDetector.onMissileSpells.TryGetValue(missile.SData.Name, out spellData)
                && missile.StartPosition != null && missile.EndPosition != null)
            {

                if (missile.StartPosition.Distance(myHero.Position) < spellData.range + 1000)
                {
                    var hero = missile.SpellCaster;

                    if (hero.IsVisible)
                    {
                        foreach (KeyValuePair<int, Spell> entry in SpellDetector.spells)
                        {
                            Spell spell = entry.Value;

                            if (spell.info.missileName == missile.SData.Name
                                && spell.heroID == missile.SpellCaster.NetworkId)
                            {
                                if (spell.info.isThreeWay == false && spell.info.isSpecial == false)
                                {
                                    //spell.spellObject = obj;
                                    Console.WriteLine("Aquired: " + (Evade.GetTickCount() - spell.startTime));
                                }
                            }
                        }
                    }

                }
            }
        }

        private void Game_ProcessSpell(Obj_AI_Base hero, GameObjectProcessSpellCastEventArgs args)
        {
            if (hero.IsMinion)
                return;

            if (testMenu.Item("ShowProcessSpell").GetValue<bool>())
            {
                Console.WriteLine(args.SData.Name + " CastTime: " + (hero.Spellbook.CastTime - Game.Time));
            }
            //circleRenderPos = args.SData.ParticleStartOffset.To2D();

            /*renderPositions.Add(
                new RenderPosition(args.Start.To2D(), Evade.GetTickCount() + 500));
            renderPositions.Add(
                new RenderPosition(args.End.To2D(), Evade.GetTickCount() + 500));*/

            /*float testTime;
            
            
            testTime = Evade.GetTickCount();
            for (int i = 0; i < 100000; i++)
            {
                var testVar = myHero.BoundingRadius;
            }
            Console.WriteLine("Test time1: " + (Evade.GetTickCount() - testTime));

            testTime = Evade.GetTickCount();
            var cacheVar = myHero.BoundingRadius;
            for (int i = 0; i < 100000; i++)
            {
                var testVar = cacheVar;
            }
            Console.WriteLine("Test time1: " + (Evade.GetTickCount() - testTime));*/

            lastHeroSpellCastTime = Evade.GetTickCount();

            if (hero.IsMe)
            {
                lastSpellCastTime = Evade.GetTickCount();
            }
        }

        private void CompareSpellLocation(Spell spell, Vector2 pos, float time)
        {
            var pos2 = spell.GetCurrentSpellPosition();
            if (spell.spellObject != null)
            {
                Console.WriteLine("Compare: " + (pos2.Distance(pos)) / (Evade.GetTickCount() - time));
            }

        }

        private void CompareSpellLocation2(Spell spell)
        {
            var pos1 = spell.GetCurrentSpellPosition();
            var timeNow = Evade.GetTickCount();

            if (spell.spellObject != null)
            {
                Console.WriteLine("start distance: " + (spell.startPos.Distance(pos1)));
            }

            DelayAction.Add(250, () => CompareSpellLocation(spell, pos1, timeNow));
        }

        private void Game_OnGameUpdate(EventArgs args)
        {
            if (startWalkTime > 0)
            {
                if (Evade.GetTickCount() - startWalkTime > 500 && myHero.IsMoving == false)
                {
                    //Console.WriteLine("walkspeed: " + startWalkPos.Distance(myHero.ServerPosition.To2D()) / (Evade.GetTickCount() - startWalkTime));
                    startWalkTime = 0;
                }
            }

            if (testMenu.Item("ShowWindupTime").GetValue<bool>())
            {
                if (myHero.IsMoving && lastStopingTime > 0)
                {
                    Console.WriteLine("WindupTime: " + (Evade.GetTickCount() - lastStopingTime));
                    lastStopingTime = 0;
                }
                else if (!myHero.IsMoving && lastStopingTime == 0)
                {
                    lastStopingTime = Evade.GetTickCount();
                }
            }

            if (testMenu.Item("ShowDashInfo").GetValue<bool>())
            {
                if (myHero.IsDashing())
                {
                    var dashInfo = myHero.GetDashInfo();
                    Console.WriteLine("Dash Speed: " + dashInfo.Speed + " Dash dist: " + dashInfo.EndPos.Distance(dashInfo.StartPos));
                }
            }

        }

        private void Game_OnGameNotifyEvent(GameNotifyEventArgs args)
        {
            //Console.WriteLine("" + args.EventId);
        }

        private void GameObject_OnFloatPropertyChange(GameObject obj, GameObjectFloatPropertyChangeEventArgs args)
        {
            //Console.WriteLine(obj.Name);

            /*foreach (var sth in ObjectManager.Get<Obj_AI_Base>())
            {
                Console.WriteLine(sth.Name);

            }*/

            if (obj.Name == "RobotBuddy")
            {
                renderPositions.Add(new RenderPosition(obj.Position.To2D(), Evade.GetTickCount() + 10));
            }


            if (args.Property == "mHP" && args.OldValue > args.NewValue)
            {
                Console.WriteLine("Damage taken time: " + (Evade.GetTickCount() - lastSpellCastTime));
            }

            if (!obj.IsMe)
            {
                return;
            }



            if (args.Property != "mExp" && args.Property != "mGold" && args.Property != "mGoldTotal")
            {
                //Console.WriteLine(args.Property + ": " + args.NewValue);
            }
        }

        private void GameObject_OnIntegerPropertyChange(GameObject obj, GameObjectIntegerPropertyChangeEventArgs args)
        {
            if (!obj.IsMe)
            {
                if (args.Property != "mExp" && args.Property != "mGold" && args.Property != "mGoldTotal")
                {
                    Console.WriteLine("Int" + args.Property + ": " + args.NewValue);
                }

            }
        }

        private void Game_OnIssueOrder(Obj_AI_Base hero, GameObjectIssueOrderEventArgs args)
        {
            if (!hero.IsMe)
                return;

            if (args.Order == GameObjectOrder.HoldPosition)
            {
                var path = myHero.Path;
                var heroPoint = myHero.ServerPosition.To2D();


                if (path.Length > 0)
                {
                    var movePos = path[path.Length - 1].To2D();
                    var walkDir = (movePos - heroPoint).Normalized();

                    //circleRenderPos = EvadeHelper.GetRealHeroPos();
                    //heroPoint;// +walkDir * myHero.MoveSpeed * (((float)Game.Ping) / 1000);
                }
            }


            /*
            if (args.Order == GameObjectOrder.MoveTo)
            {         
                if (testingCollision)
                {
                    if (args.TargetPosition.To2D().Distance(testCollisionPos) < 3)
                    {
                        //var path = myHero.GetPath();
                        //circleRenderPos

                        args.Process = false;
                    }
                }
            }*/

            if (args.Order == GameObjectOrder.MoveTo)
            {

                Vector2 heroPos = myHero.ServerPosition.To2D();
                Vector2 pos = args.TargetPosition.To2D();
                float speed = myHero.MoveSpeed;

                startWalkPos = heroPos;
                startWalkTime = Evade.GetTickCount();

                foreach (KeyValuePair<int, Spell> entry in SpellDetector.spells)
                {
                    Spell spell = entry.Value;
                    var spellPos = spell.GetCurrentSpellPosition();
                    var walkDir = (pos - heroPos).Normalized();


                    float spellTime = (Evade.GetTickCount() - spell.startTime) - spell.info.spellDelay;
                    spellPos = spell.startPos + spell.direction * spell.info.projectileSpeed * (spellTime / 1000);
                    //Console.WriteLine("aaaa" + spellTime);


                    bool isCollision = false;
                    float movingCollisionTime = MathUtils.GetCollisionTime(heroPos, spellPos, walkDir * (speed - 25), spell.direction * (spell.info.projectileSpeed - 200), myHero.BoundingRadius, spell.GetSpellRadius(), out isCollision);
                    if (isCollision)
                    {
                        //Console.WriteLine("aaaa" + spellPos.Distance(spell.endPos) / spell.info.projectileSpeed);
                        if (true)//spellPos.Distance(spell.endPos) / spell.info.projectileSpeed > movingCollisionTime)
                        {
                            Console.WriteLine("movingCollisionTime: " + movingCollisionTime);
                            //circleRenderPos = heroPos + walkDir * speed * movingCollisionTime;
                        }

                    }
                }
            }
        }

        private void GetPath(Vector2 movePos)
        {

        }

        private void PrintTimers()
        {
            Drawing.DrawText(10, 10, Color.White, "Timer1 Freq: " + (getGameTimer - lastGameTimerTick));
            Drawing.DrawText(10, 30, Color.White, "Timer2 Freq: " + (getTickCountTimer - lastTickCountTimerTick));
            Drawing.DrawText(10, 50, Color.White, "Timer3 Freq: " + (getWatchTimer - lastWatchTimerTick));//(getWatchTimer - lastWatchTimerTick));

            if (getTickCountTimer - lastTimerCheck > 1000)
            {
                Console.WriteLine("" + ((getGameTimer - lastGameTimerStart) - (getTickCountTimer - lastTickCountTimerStart)));
                lastTimerCheck = getTickCountTimer;
            }


            Drawing.DrawText(10, 70, Color.White, "Timer1 Freq: " + (getGameTimer - lastGameTimerStart));
            Drawing.DrawText(10, 90, Color.White, "Timer2 Freq: " + (getTickCountTimer - lastTickCountTimerStart));
            Drawing.DrawText(10, 110, Color.White, "Timer3 Freq: " + (getWatchTimer - lastWatchTimerStart));

            /*Drawing.DrawText(10, 70, Color.White, "Timer1 Freq: " + (getGameTimer));
            Drawing.DrawText(10, 90, Color.White, "Timer2 Freq: " + (getTickCountTimer));
            Drawing.DrawText(10, 100, Color.White, "Timer3 Freq: " + (getWatchTimer));*/



            lastGameTimerTick = getGameTimer;
            lastTickCountTimerTick = getTickCountTimer;
            lastWatchTimerTick = getWatchTimer;
        }

        private static void RenderTestCircles()
        {
            foreach (RenderPosition rendPos in renderPositions)
            {
                if (rendPos.renderEndTime - Evade.GetTickCount() > 0)
                {
                    Render.Circle.DrawCircle(rendPos.renderPosition.To3D(), 50, Color.White, 3);
                }
                else
                {
                    DelayAction.Add(1, () => renderPositions.Remove(rendPos));
                }
            }
        }

        private void TestUnderTurret()
        {
            if (Game.CursorPos.To2D().IsUnderTurret())
            {
                Render.Circle.DrawCircle(Game.CursorPos, 50, Color.Red, 3);
            }
            else
            {
                Render.Circle.DrawCircle(Game.CursorPos, 50, Color.White, 3);
            }
        }

        private void Drawing_OnDraw(EventArgs args)
        {
            //PrintTimers();

            //EvadeHelper.CheckMovePath(Game.CursorPos.To2D());            

            //TestUnderTurret();

            foreach (KeyValuePair<int, Spell> entry in SpellDetector.drawSpells)
            {
                Spell spell = entry.Value;

                if (spell.info.spellType == SpellType.Line)
                {
                    Vector2 spellPos = spell.GetCurrentSpellPosition();

                    Render.Circle.DrawCircle(new Vector3(spellPos.X, spellPos.Y, myHero.Position.Z), spell.info.radius, Color.White, 3);

                    /*spellPos = spellPos + spell.direction * spell.info.projectileSpeed * (60 / 1000); //move the spellPos by 50 miliseconds forwards
                    spellPos = spellPos + spell.direction * 200; //move the spellPos by 50 units forwards        

                    Render.Circle.DrawCircle(new Vector3(spellPos.X, spellPos.Y, myHero.Position.Z), spell.info.radius, Color.White, 3);*/
                }
            }

            RenderTestCircles();

            if (testMenu.Item("TestHeroPos").GetValue<bool>())
            {
                var path = myHero.Path;
                if (path.Length > 0)
                {
                    var heroPos2 = EvadeHelper.GetRealHeroPos(Game.Ping + 50);// path[path.Length - 1].To2D();
                    var heroPos1 = myHero.ServerPosition.To2D();

                    Render.Circle.DrawCircle(new Vector3(heroPos2.X, heroPos2.Y, myHero.ServerPosition.Z), myHero.BoundingRadius, Color.Red, 3);
                    Render.Circle.DrawCircle(new Vector3(myHero.ServerPosition.X, myHero.ServerPosition.Y, myHero.ServerPosition.Z), myHero.BoundingRadius, Color.White, 3);

                    var heroPos = Drawing.WorldToScreen(ObjectManager.Player.Position);
                    var dimension = Drawing.GetTextExtent("Evade: ON");
                    Drawing.DrawText(heroPos.X - dimension.Width / 2, heroPos.Y, Color.Red, "" + (int)(heroPos2.Distance(heroPos1)));

                    Render.Circle.DrawCircle(new Vector3(circleRenderPos.X, circleRenderPos.Y, myHero.ServerPosition.Z), 10, Color.Red, 3);
                }
            }

            if (testMenu.Item("DrawHeroPos").GetValue<bool>())
            {
                Render.Circle.DrawCircle(new Vector3(myHero.ServerPosition.X, myHero.ServerPosition.Y, myHero.ServerPosition.Z), myHero.BoundingRadius, Color.White, 3);
            }

            if (testMenu.Item("TestMoveTo").GetValue<KeyBind>().Active)
            {
                var keyBind = testMenu.Item("TestMoveTo").GetValue<KeyBind>();
                testMenu.Item("TestMoveTo").SetValue(new KeyBind(keyBind.Key, KeyBindType.Toggle, false));

                myHero.IssueOrder(GameObjectOrder.MoveTo, Game.CursorPos);

                var dir = (Game.CursorPos - myHero.Position).Normalized();
                var pos2 = myHero.Position - dir * Game.CursorPos.Distance(myHero.Position);

                DelayAction.Add(1, () => myHero.IssueOrder(GameObjectOrder.MoveTo, pos2));
            }

            if (testMenu.Item("TestPath").GetValue<bool>())
            {
                var tPath = myHero.GetPath(Game.CursorPos);
                Vector2 lastPoint = Vector2.Zero;

                foreach (Vector3 point in tPath)
                {
                    var point2D = point.To2D();
                    //Render.Circle.DrawCircle(new Vector3(point.X, point.Y, point.Z), myHero.BoundingRadius, Color.Violet, 3);

                    lastPoint = point2D;
                }
            }

            if (testMenu.Item("TestPath").GetValue<bool>())
            {
                var tPath = myHero.GetPath(Game.CursorPos);
                Vector2 lastPoint = Vector2.Zero;

                foreach (Vector3 point in tPath)
                {
                    var point2D = point.To2D();
                    //Render.Circle.DrawCircle(new Vector3(point.X, point.Y, point.Z), myHero.BoundingRadius, Color.Violet, 3);

                    lastPoint = point2D;
                }

                foreach (KeyValuePair<int, Spell> entry in SpellDetector.spells)
                {
                    Spell spell = entry.Value;

                    Vector2 to = Game.CursorPos.To2D();
                    var dir = (to - myHero.Position.To2D()).Normalized();
                    Vector2 cPos1, cPos2;

                    var cpa = MathUtilsCPA.CPAPointsEx(myHero.Position.To2D(), dir * myHero.MoveSpeed, spell.endPos, spell.direction * spell.info.projectileSpeed, to, spell.endPos);
                    var cpaTime = MathUtilsCPA.CPATime(myHero.Position.To2D(), dir * myHero.MoveSpeed, spell.endPos, spell.direction * spell.info.projectileSpeed);

                    //Console.WriteLine("" + cpaTime);
                    //Render.Circle.DrawCircle(cPos1.To3D(), myHero.BoundingRadius, Color.Red, 3);

                    if (cpa < myHero.BoundingRadius + spell.GetSpellRadius())
                    {

                    }
                }
            }

            if (testMenu.Item("ShowBuffs").GetValue<bool>())
            {
                var target = myHero;

                foreach (var hero in HeroManager.Enemies)
                {
                    target = hero;
                }

                var buffs = target.Buffs;

                //Console.WriteLine(myHero.ChampionName);

                //if(myHero.IsDead)
                //    Console.WriteLine("dead");

                if (!target.IsTargetable)
                    Console.WriteLine("invul" + Evade.GetTickCount());

                int height = 20;

                foreach (var buff in buffs)
                {
                    if (buff.IsValidBuff())
                    {
                        Drawing.DrawText(10, height, Color.White, buff.Name);
                        height += 20;

                        Console.WriteLine(buff.Name);
                    }
                }
            }

            if (testMenu.Item("TestTracker").GetValue<bool>())
            {
                foreach (KeyValuePair<int, ObjectTrackerInfo> entry in SpecialSpells.objTracker)
                {
                    var info = entry.Value;

                    Vector3 endPos2;
                    if (info.usePosition == false)
                        endPos2 = info.obj.Position;
                    else
                        endPos2 = info.position;

                    Render.Circle.DrawCircle(new Vector3(endPos2.X, endPos2.Y, myHero.Position.Z), 50, Color.Green, 3);
                }
            }

            if (testMenu.Item("TestWall").GetValue<bool>())
            {
                foreach (var posInfo in sortedBestPos)
                {
                    var posOnScreen = Drawing.WorldToScreen(posInfo.position.To3D());
                    //Drawing.DrawText(posOnScreen.X, posOnScreen.Y, Color.Aqua, "" + (int)posInfo.closestDistance);

                    /*
                    if (!posInfo.rejectPosition)
                    {
                        Drawing.DrawText(posOnScreen.X, posOnScreen.Y, Color.Aqua, "" + (int)posInfo.closestDistance);
                    }*/

                    Drawing.DrawText(posOnScreen.X, posOnScreen.Y, Color.Aqua, "" + (int)posInfo.closestDistance);

                    /*if (posInfo.posDangerCount <= 0)
                    {
                        var pos = posInfo.position;
                        Render.Circle.DrawCircle(new Vector3(pos.X, pos.Y, myHero.Position.Z), (float)25, Color.White, 3);
                    }*/

                }
            }

        }
    }
}
